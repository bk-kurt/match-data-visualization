using DataModels;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ball : BaseInterpolatedObject
    {
        public void Initialize(BallData ballData)
        {
            if (ballData == null )
            {
                Debug.LogError("BallData is null");
                return;
            }
            UpdateState(ballData);
        }


        public override void UpdateState(IInterpolatedStateData interpolatedStateData)
        {
            var ballData = interpolatedStateData as BallData;
            base.UpdateState(ballData);
        }
    }
}