using DataModels;
using GamePlay.Controllers;
using UnityEngine;

namespace DefaultNamespace
{
    public class Ball : BaseInterpolatedObject
    {
        [SerializeField] private BallController Controller;
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
            Controller.ApplyTopLevelChanges(interpolatedStateData);
        }

        public void RefreshConfig(VisualizationAssetsConfigSo visualizationAssetsConfigSo)
        {
            //  this might involve updating the ball's visual effects, textures,
            // or even physics properties to reflect the new configuration.
            throw new System.NotImplementedException();
        }
    }
}