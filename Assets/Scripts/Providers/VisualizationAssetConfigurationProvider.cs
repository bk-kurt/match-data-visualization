using Scriptables.Configuration;
using UnityEngine;


namespace Providers
{
    /// <summary>
    /// this enables dynamic switching and injection of visualization settings with no hardcoding references or values.
    /// will be useful in scenarios where configurations are sourced from different providers or need to be swapped at runtime.
    /// </summary>
    public class VisualizationAssetConfigurationProvider : MonoBehaviour, IVisualizationAssetConfiguration
    {
        [SerializeField] private VisualizationAssetsConfigSo visualizationAssetsConfigSo;

        public VisualizationAssetsConfigSo GetGameAssetsConfig()
        {
            return visualizationAssetsConfigSo;
        }
    }
}