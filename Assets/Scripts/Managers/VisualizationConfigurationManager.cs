using Scriptables.Configuration;
using Utilities;

namespace Managers
{
    // this is tier level config management compared to ConfigurationManager.cs,
    // atm has no usage but very applicable in firther
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
            // notify other managers or elements that need to refresh based on the new configuration
            NotifyConfigChange(_visualizationAssetsConfigSo);
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