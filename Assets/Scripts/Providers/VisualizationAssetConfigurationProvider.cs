using Scriptables.Configuration;
using UnityEngine;

namespace Providers
{
    public class VisualizationAssetConfigurationProvider : MonoBehaviour, IVisualizationAssetConfiguration
    {
        [SerializeField] private VisualizationAssetsConfigSo visualizationAssetsConfigSo;

        public VisualizationAssetsConfigSo GetGameAssetsConfig()
        {
            return visualizationAssetsConfigSo;
        }
    }
}