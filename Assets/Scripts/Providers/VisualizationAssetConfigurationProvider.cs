using System;
using DefaultNamespace;
using Managers;
using UnityEngine;

namespace Providers
{
    public class VisualizationAssetConfigurationProvider : MonoBehaviour, IVisualizationAssetConfiguration
    {
        [SerializeField] private VisualizationAssetsConfigSo visualizationAssetsConfigSo;

        private void Awake()
        {
            // sample coded
            // ConfigurationManager.Instance.SetConfiguration(visualizationAssetsConfigSo);
        }

        public VisualizationAssetsConfigSo GetGameAssetsConfig()
        {
            return visualizationAssetsConfigSo;
        }
    }
}