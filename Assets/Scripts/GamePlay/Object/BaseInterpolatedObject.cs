using DataModels;
using Settings;
using UnityEngine;

namespace DefaultNamespace
{
    public abstract class BaseInterpolatedObject : MonoBehaviour
    {
        protected Vector3 TargetPosition;
        private readonly Quaternion _targetRotation = Quaternion.identity;

        protected virtual void Update()
        {
            float positionSmoothing = VisualizationSettings.Instance.commonInterpolationSpeed*Time.deltaTime;
            float rotationSmoothing = VisualizationSettings.Instance.commonInterpolationSpeed*Time.deltaTime;

            // InterpolatePosition(positionSmoothing);
            // InterpolateRotation(rotationSmoothing);
        }

        private void InterpolatePosition(float smoothing)
        {
            transform.position = Vector3.Lerp(transform.position, TargetPosition, smoothing);
        }

        private void InterpolateRotation(float smoothing)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, smoothing);
        }

        public virtual void UpdateState(IInterpolatedStateData interpolatedStateData)
        {
            transform.position = interpolatedStateData.TargetPosition;
            transform.rotation = interpolatedStateData.TargetRotation;
        }
    }
}