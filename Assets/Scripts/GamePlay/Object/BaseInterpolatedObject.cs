using DataModels;
using Providers;
using Scriptables.Settings;
using UnityEngine;


public abstract class BaseInterpolatedObject : MonoBehaviour
{
    private static VisualizationSettingsSo InterpolationSettings => VisualizationSettingsProvider.CurrentSettings;


    private Vector3 _targetPosition;
    private Quaternion _targetRotation = Quaternion.identity;
    private Vector3 _positionVelocity = Vector3.zero;
    private float _positionSmoothTime;
    private float _rotationSmoothTime;

    protected virtual void Update()
    {
        if (InterpolationSettings == null)
        {
            Debug.Log("interpolation settings is null");
            return;
        }

        if (!InterpolationSettings.isInterpolationEnabled) return;

        AdjustInterpolationSpeed();
        AdjustRotationSpeed();

        InterpolatePosition();
        InterpolateRotation();
    }

    private void InterpolatePosition()
    {
        _positionSmoothTime = InterpolationSettings.commonPositionInterpolationSpeed;
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _positionVelocity,
            _positionSmoothTime);
    }

    private void InterpolateRotation()
    {
        _rotationSmoothTime = InterpolationSettings.commonRotationInterpolationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSmoothTime);
    }

    private void AdjustInterpolationSpeed()
    {
        float distance = Vector3.Distance(transform.position, _targetPosition);
        // Adjust _positionSmoothTime based on distance
        _positionSmoothTime = Mathf.Clamp(distance / 10f, 0.1f, 0.5f); // Example values, adjust as needed
    }

    private void AdjustRotationSpeed()
    {
        float angle = Quaternion.Angle(transform.rotation, _targetRotation);
        // Adjust _rotationSmoothTime based on the angular difference
        _rotationSmoothTime = Mathf.Clamp(angle / 90f, 0.1f, 0.5f); // Example values, adjust as needed
    }

    public virtual void UpdateState(IInterpolatedStateData interpolatedStateData)
    {
        _targetPosition = interpolatedStateData.TargetPosition;
        _targetRotation = interpolatedStateData.TargetRotation;
        // Ensure this method does not conflict with smooth interpolation logic
    }
}