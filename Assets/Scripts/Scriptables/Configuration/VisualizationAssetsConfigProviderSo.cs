using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace Scriptables.Configuration
{
    [CreateAssetMenu(fileName = "GameAssets", menuName = "Scriptable/Configuration/VisualizationAssets", order = 1)]
    public class VisualizationAssetsConfigProviderSo : ScriptableObject, IVisualizationAssetConfigProvider
    {
        public List<PersonConfigSo> personConfigs;
        public BallConfigSo defaultBallConfig;

        public PersonConfigSo GetConfiguredPersonByTeamSide(int teamSide)
        {
            foreach (var config in personConfigs)
            {
                if (config.teamSide == teamSide)
                {
                    return config;
                }
            }

            Debug.LogError($"No PersonConfiguration found for team side: {teamSide}");
            return null;
        }

        public BallConfigSo GetConfiguredBallConfig()
        {
            if (!defaultBallConfig)
            {
                Debug.LogError($"No PersonConfiguration found for ball");
                return null;
            }

            return defaultBallConfig;
        }

        public IVisualizationAssetConfigProvider SetGameAssetsConfig(IVisualizationAssetConfigProvider configProvider)
        {
            throw new System.NotImplementedException();
        }

        IVisualizationAssetConfigProvider IVisualizationAssetConfigProvider.GetGameAssetsConfig()
        {
            return GetGameAssetsConfig();
        }

        private VisualizationAssetsConfigProviderSo GetGameAssetsConfig()
        {
            return this;
        }
    }
}