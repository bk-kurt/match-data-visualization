using Scriptables.Configuration;
using UnityEngine;
using Utilities;

namespace Managers.Configuration
{
    public class ConfigurationManager : MonoSingleton<ConfigurationManager>
    {
        public delegate void ConfigurationChangedDelegate(VisualizationAssetsConfigSo newConfig);

        public event ConfigurationChangedDelegate OnConfigurationChanged;

        private VisualizationAssetsConfigSo _currentConfig;

        // an option I decided to not use is=
        // providing a dynamic Configuration via interfaces, but on very frequent config updates it can cause a big workload
        // futher cases. I can convert all configured entities to an observer,for more flexibility
        // can be commanded by upper level configuration management system controlled by the user
        public void SetConfiguration(VisualizationAssetsConfigSo newConfig)
        {
            if (newConfig == null)
            {
                Debug.LogError("Attempted to set a null configuration.");
                return;
            }

            if (_currentConfig != newConfig)
            {
                _currentConfig = newConfig;
                OnConfigurationChanged?.Invoke(newConfig);
            }
        }


        public VisualizationAssetsConfigSo GetCurrentConfiguration() => _currentConfig;
    }
}