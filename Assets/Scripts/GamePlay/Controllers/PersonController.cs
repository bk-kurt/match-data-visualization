using System;
using DataModels;
using DefaultNamespace;
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

            // avoid recalculating if unnecessary
            if (Math.Abs(animator.GetFloat(TopLevelPersonAnimaVariables.SpeedHash) - newSpeed) >
                topLevelPersonAnimaVariables.speedCalculationTolerance)
            {
                animator.SetFloat(TopLevelPersonAnimaVariables.SpeedHash, CalculateAdjustedSpeed(newSpeed, isRunning));
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
        public float runningSpeedMultiplier = 0.4f;

        public static readonly int SpeedHash = Animator.StringToHash("Speed");
        public static readonly int IsRunningHash = Animator.StringToHash("isRunning");
    }
}