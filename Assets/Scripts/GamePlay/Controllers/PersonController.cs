using System;
using DataModels;
using DefaultNamespace;
using UnityEngine;

namespace GamePlay.Controllers
{
    public class PersonController : MonoBehaviour, ITopLevelController
    {
        [SerializeField] private Animator animator;
        private static readonly int SpeedHash = Animator.StringToHash("Speed");
        private static readonly int IsRunningHash = Animator.StringToHash("isRunning");

        [SerializeField] private float runningSpeedThreshold = 3f;
        private bool _lastIsRunningState;
        private float _lastSpeed = -1;

        public void ApplyTopLevelChanges(IInterpolatedStateData interpolatedStateData)
        {
            PersonData personData = (PersonData)interpolatedStateData;
            AdjustAnimationBasedOnSpeed(personData.Speed);
        }


        private void AdjustAnimationBasedOnSpeed(float newSpeed)
        {
            if (!animator)
            {
                Debug.LogWarning($"Animator component not found on {gameObject.name}, unable to adjust animation.");
                return;
            }

            bool isRunning = newSpeed >= runningSpeedThreshold;
            if (isRunning != _lastIsRunningState)
            {
                animator.SetBool(IsRunningHash, isRunning);
                _lastIsRunningState = isRunning;
            }

            // Calculate adjusted speed only if necessary to minimize processing.
            if (Math.Abs(newSpeed - _lastSpeed) > 0.05f)
            {
                float adjustedSpeed = isRunning ? newSpeed * 0.1f : newSpeed * 0.4f;
                animator.SetFloat(SpeedHash, adjustedSpeed);
                _lastSpeed = newSpeed;
            }
        }
    }
}