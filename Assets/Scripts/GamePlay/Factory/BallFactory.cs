using DataModels;
using GamePlay.Environment;
using UnityEngine;
using Scriptables.Configuration;
using Unity.VisualScripting;

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

            var ballConfig = assetsConfigProviderSo.GetConfiguredBallConfig();
            if (ballConfig.ballPrefab == null)
            {
                Debug.LogError("BallFactory: Ball configuration or prefab is null.");
                return null;
            }

            return InstantiateAndInitializeBall(ballData, ballConfig.ballPrefab);
        }

        private static Ball InstantiateAndInitializeBall(BallData ballData, Object ballPrefab)
        {
            var parentTransform = EnvironmentSetUp.Instance.transform; // Ensure this does not return null.
            var instantiatedBallGo =
                Object.Instantiate(ballPrefab, ballData.TargetPosition, Quaternion.identity, parentTransform);

            var ballComponent = instantiatedBallGo.GetComponent<Ball>() ?? instantiatedBallGo.AddComponent<Ball>();
            ballComponent.Initialize(ballData);

            return ballComponent;
        }
    }
}