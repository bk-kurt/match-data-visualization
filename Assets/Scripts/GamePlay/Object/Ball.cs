using DataModels;
using GamePlay.Controllers;
using Scriptables.Configuration;
using UnityEngine;


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

        public void RefreshConfig(VisualizationAssetsConfigProviderSo visualizationAssetsConfigProviderSo)
        {
            //qqq4  this might involve updating the ball's visual effects, textures,
            // or even physics properties to reflect the new configuration.
            throw new System.NotImplementedException();
        }
    }
