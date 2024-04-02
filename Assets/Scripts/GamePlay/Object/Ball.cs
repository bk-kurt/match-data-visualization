using DataModels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ball : MonoBehaviour
    {
        public void Initialize(BallData ballData)
        {
            //todo
            UpdateState(ballData);
        }

        public void UpdateState(BallData ballData)
        {
            transform.position = ballData.targetPosition;
            transform.rotation = ballData.TargetRotation;
        }
    }
}