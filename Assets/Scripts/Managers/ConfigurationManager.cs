using UnityEngine;
using Utilities;

public class ConfigurationManager : MonoSingleton<ConfigurationManager>
{
    public delegate void ConfigurationChangedDelegate(VisualizationAssetsConfigSo newConfig);
    public event ConfigurationChangedDelegate OnConfigurationChanged;

    private VisualizationAssetsConfigSo _currentConfig;

    
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