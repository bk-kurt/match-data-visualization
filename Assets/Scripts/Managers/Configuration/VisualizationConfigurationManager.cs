using Scriptables.Configuration;
using Utilities;

namespace Managers.Configuration
{
    // this is tier level config management compared to ConfigurationManager.cs,
    public class VisualizationConfigurationManager : MonoSingleton<VisualizationConfigurationManager>
    {
        private VisualizationAssetsConfigSo _visualizationAssetsConfigSo;

        protected void OnEnable()
        {
            ConfigurationManager.Instance.OnConfigurationChanged += SetGameAssetConfiguration;
        }

        private void SetGameAssetConfiguration(VisualizationAssetsConfigSo config)
        {
            _visualizationAssetsConfigSo = config;
            NotifyConfigChange(_visualizationAssetsConfigSo);
            // notify others that need to refresh by the new config or config swap
        }

        private void NotifyConfigChange(VisualizationAssetsConfigSo config)
        {
            // to be implemented in demand
        }

        private void OnDisable()
        {
            ConfigurationManager.Instance.OnConfigurationChanged -= SetGameAssetConfiguration;
        }
    }
}