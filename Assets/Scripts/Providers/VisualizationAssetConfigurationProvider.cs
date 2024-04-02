using System;
using DefaultNamespace;
using Managers;
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