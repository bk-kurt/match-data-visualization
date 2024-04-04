using DataModels;
using GamePlay.Environment;
using Unity.VisualScripting;
using UnityEngine;
using VisualizationAssetsConfigProviderSo = Scriptables.Configuration.VisualizationAssetsConfigProviderSo;

namespace GamePlay.Factory
{
    public static class BallFactory
    {
        private static IVisualizationAssetConfigProvider _visualizationAssetConfig;

        public static void Initialize(IVisualizationAssetConfigProvider config)
        {
            _visualizationAssetConfig = config;
        }

        public static Ball Create(BallData ballData)
        {
            if (ballData == null)
            {
                Debug.LogError("BallFactory: Ball data is null.");
                return null;
            }

            VisualizationAssetsConfigProviderSo assetsConfigProviderSo =
                _visualizationAssetConfig as VisualizationAssetsConfigProviderSo;

            if (assetsConfigProviderSo == null || assetsConfigProviderSo.GetConfiguredBallConfig() == null)
            {
                Debug.LogError("BallFactory: Ball configuration or prefab is null.");
                return null;
            }

            Vector3 ballPosition = ballData.TargetPosition;
            var parentTransform = EnvironmentSetUp.Instance.transform;
            Ball instantiatedBallGo =
                Object.Instantiate(assetsConfigProviderSo.GetConfiguredBallConfig().ballPrefab, ballPosition,
                    Quaternion.identity, parentTransform);

            Ball ballComponent = instantiatedBallGo.GetComponent<Ball>();
            if (ballComponent == null)
            {
                ballComponent = instantiatedBallGo.AddComponent<Ball>();
            }

            ballComponent.Initialize(ballData);
            return ballComponent;
        }
    }
}