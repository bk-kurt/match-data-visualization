using System;
using DataModels;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class PersonController : MonoBehaviour, ITopLevelController
    {
        [SerializeField] private Animator animator;
        [SerializeField] private TopLevelPersonAnimaVariables topLevelPersonAnimaVariables;

        public void ApplyTopLevelChanges(IInterpolatedStateData interpolatedStateData)
        {
            if (interpolatedStateData is PersonData personData)
            {
                AdjustAnimationBasedOnSpeed(personData.Speed);
                // AdjustRotationBasedOnSpeed(personData.Speed); deprecation
            }
        }

        private void AdjustAnimationBasedOnSpeed(float newSpeed)
        {
            if (animator == null)
            {
                Debug.LogWarning($"Animator component not found on {gameObject.name}, unable to adjust animation.");
                return;
            }

            bool isRunning = newSpeed >= topLevelPersonAnimaVariables.runningSpeedThreshold;
            animator.SetBool(TopLevelPersonAnimaVariables.IsRunningHash, isRunning);

            // avoid unnecessary recalculations
            if (Math.Abs(animator.GetFloat(TopLevelPersonAnimaVariables.SpeedHash) - newSpeed) >
                topLevelPersonAnimaVariables.speedCalculationTolerance)
            {
                animator.SetFloat(TopLevelPersonAnimaVariables.SpeedHash, CalculateAdjustedSpeed(newSpeed, isRunning));
            }
        }
        /// <summary>
        /// // since the movement orientation data is not correct, this can function as PlaceHolder rotation
        /// </summary>
        /// <param name="newSpeed"></param>
        private void AdjustRotationBasedOnSpeed(float newSpeed)
        {
            if (newSpeed > 0)
            {
                // Calculate a forward direction based on the character's current rotation and speed
                Vector3 forwardDirection = transform.forward + transform.right * (newSpeed * 1);
                Vector3 targetPoint = transform.position + forwardDirection * 5;

                // Create a target rotation based on the new target point
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);

                // Smoothly rotate towards the target rotation
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 1);
            }
        }



        private float CalculateAdjustedSpeed(float speed, bool isRunning)
        {
            return isRunning
                ? speed * topLevelPersonAnimaVariables.runningSpeedMultiplier
                : speed * topLevelPersonAnimaVariables.walkingSpeedMultiplier;
        }
    }

    [Serializable]
    public class TopLevelPersonAnimaVariables
    {
        public float runningSpeedThreshold = 2f;
        public float speedCalculationTolerance = 0.1f;
        public float walkingSpeedMultiplier = 0.5f;
        public float runningSpeedMultiplier = 0.25f;

        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        public static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    }
}