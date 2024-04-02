using UnityEngine;
using Utilities;

namespace Managers
{
    public class VisualizationConfigurationManager : MonoSingleton<VisualizationConfigurationManager>
    {
        private VisualizationAssetsConfigSo _visualizationAssetsConfigSo;

        protected void OnEnable()
        {
            ConfigurationManager.Instance.OnConfigurationChanged += SetGameAssetConfiguration;
        }

        public void SetGameAssetConfiguration(VisualizationAssetsConfigSo config)
        {
            _visualizationAssetsConfigSo = config;
            // Notify other managers or elements that need to refresh based on the new configuration
            NotifyConfigChange(_visualizationAssetsConfigSo);
        }

        private void NotifyConfigChange(VisualizationAssetsConfigSo config)
        {
            // Implement notification logic here.
            // This might involve calling a method on other managers or broadcasting an event.
        }

        protected void OnDisable()
        {
            ConfigurationManager.Instance.OnConfigurationChanged -= SetGameAssetConfiguration;
        }
    }
}