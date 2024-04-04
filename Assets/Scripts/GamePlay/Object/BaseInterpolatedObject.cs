using DataModels;
using Providers;
using Scriptables.Settings;
using UnityEngine;

//qqq5 I noticed a frame-independent interpolation facility is needed in a frame-manipulated environment. 
// With the help of this interpolation we can even interpolate while jumping between distinct timestamps (frame indexes)!!
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

        if (!InterpolationSettings.IsInterpolationEnabled) return;

        AdjustInterpolationSpeed();
        AdjustRotationSpeed();

        InterpolatePosition();
        InterpolateRotation();
    }

    private void InterpolatePosition()
    {
        _positionSmoothTime = InterpolationSettings.CommonPositionInterpolationSpeed;
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _positionVelocity,
            _positionSmoothTime);
    }

    private void InterpolateRotation()
    {
        _rotationSmoothTime = InterpolationSettings.CommonRotationInterpolationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Slerp(transform.rotation, _targetRotation, _rotationSmoothTime);
    }

    private void AdjustInterpolationSpeed()
    {
        float distance = Vector3.Distance(transform.position, _targetPosition);
        _positionSmoothTime = Mathf.Clamp(distance / 10f, 0.1f, 0.5f);
    }

    private void AdjustRotationSpeed()
    {
        float angle = Quaternion.Angle(transform.rotation, _targetRotation);
        _rotationSmoothTime = Mathf.Clamp(angle / 90f, 0.1f, 0.5f);
    }

    public virtual void UpdateState(IInterpolatedStateData interpolatedStateData)
    {
        _targetPosition = interpolatedStateData.TargetPosition;
        _targetRotation = interpolatedStateData.TargetRotation;
    }
}