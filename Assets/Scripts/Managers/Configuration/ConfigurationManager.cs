using System;
using Interfaces.Configuration;
using Scriptables.Configuration;
using UnityEngine;
using Utilities;

namespace Managers.Configuration
{
    public class ConfigurationManager : MonoSingleton<ConfigurationManager>
    {
        [SerializeField] private DataPathConfigSo defaultDataPathConfigSo;
        [SerializeField] private VisualizationAssetsConfigProviderSo defaultVisualizationAssetsConfigSo;

        public event Action<IVisualizationAssetConfigProvider> OnAssetVisualizationConfigChanged;
        public event Action<IDataPathConfigProvider> OnDataPathConfigChanged;

        private IVisualizationAssetConfigProvider _currentVisualizationAssetsConfigProvider;
        private IDataPathConfigProvider _currentDataPathConfig;

        private void Awake()
        {
            SetVisualAssetsConfiguration(defaultVisualizationAssetsConfigSo);
            SetDataPathConfiguration(defaultDataPathConfigSo);
        }

        
        /// <summary>
        ///qqq7 we can even set configurations with remote config freely..
        /// </summary>
        /// <param name="newConfigProvider"></param>
        public void SetVisualAssetsConfiguration(IVisualizationAssetConfigProvider newConfigProvider)
        {
            if (newConfigProvider == null)
            {
                Debug.LogError("Attempted to set a null Visual Assets configuration.");
                return;
            }

            if (_currentVisualizationAssetsConfigProvider != newConfigProvider)
            {
                _currentVisualizationAssetsConfigProvider = newConfigProvider;
                OnAssetVisualizationConfigChanged?.Invoke(_currentVisualizationAssetsConfigProvider);
            }
        }

        public IVisualizationAssetConfigProvider GetVisualAssetsConfiguration()
        {
            return _currentVisualizationAssetsConfigProvider ?? defaultVisualizationAssetsConfigSo;
        }

        public void SetDataPathConfiguration(IDataPathConfigProvider dataPathConfig)
        {
            if (dataPathConfig == null)
            {
                Debug.LogError("Attempted to set a null Data Path configuration.");
                return;
            }

            _currentDataPathConfig = dataPathConfig;
            OnDataPathConfigChanged?.Invoke(_currentDataPathConfig);
        }

        public string GetJsonDataPathConfig()
        {
            return _currentDataPathConfig?.GetJsonDataPath() ?? defaultDataPathConfigSo.GetJsonDataPath();
        }
    }
}
