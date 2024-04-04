using System;
using System.Collections.Generic;
using GamePlay.Factory;
using Interfaces.Configuration;
using Managers.Configuration;
using Managers.Object;
using Scriptables.Configuration;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class MatchVisualizationManager : MonoSingleton<MatchVisualizationManager>
    {
        private void OnEnable()
        {
            ConfigurationManager.Instance.OnAssetVisualizationConfigChanged += ApplyVisualizationAssetsConfigChanges;
            ConfigurationManager.Instance.OnDataPathConfigChanged += ApplyDataPathConfigChanges;
        }

        public void InitializeFactories()
        {
            var assetConfig = ConfigurationManager.Instance.GetVisualAssetsConfiguration();
            BallFactory.Initialize(assetConfig);
            PersonFactory.Initialize(assetConfig);
            ApplyCommonConfigChanges();
        }

        
        /// <summary>
        ///  this handles updates of data packs. apart from steaming single frame data
        /// </summary>
        /// <param name="newFrameData"></param>
        public void InitializeWithNewFrameData(List<FrameData> newFrameData)
        {
            PersonManager.Instance.ClearAllPersons();
            BallManager.Instance.ClearBall();

            // for now reset to starting point for initialization
            if (newFrameData.Count > 0)
            {
                UpdateVisualStateFromFrameData(newFrameData[0]);
            }
        }


        private void ApplyVisualizationAssetsConfigChanges(IVisualizationAssetConfigProvider configProvider)
        {
            if (configProvider == null)
            {
                Debug.LogError("Visualization assets config provider is null.");
                return;
            }

            try
            {
                ApplyCommonConfigChanges(); // Consolidated method call
                UpdateVisualStateFromConfigData(); // Single, necessary call
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to apply visualization assets config changes: {e.Message}");
            }
        }

        private void ApplyCommonConfigChanges()
        {
            VisualElementFactory visualElementFactory = new VisualElementFactory(ConfigurationManager.Instance.GetVisualAssetsConfiguration());
            PersonManager.Instance.SetVisualElementFactory(visualElementFactory);
            BallManager.Instance.SetVisualElementFactory(visualElementFactory);
        }

        private void ApplyDataPathConfigChanges(IDataPathConfigProvider configProvider)
        {
            if (configProvider == null)
            {
                Debug.LogError("Data path config provider is null.");
                return;
            }

            try
            {
                ConfigurationManager.Instance.SetDataPathConfiguration(configProvider);
                UpdateVisualStateFromConfigData();
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to apply data path config changes: {e.Message}");
            }
        }


        private void UpdateVisualStateFromConfigData()
        {
            // todo
        }


        public void UpdateVisualStateFromFrameData(FrameData frameData)
        {
            if (!IsFrameDataRequestingUpdate(frameData))
            {
                return;
            }

            PersonManager.Instance.UpdatePersonsState(frameData.Persons);
            BallManager.Instance.UpdateBallState(frameData.Ball);
        }

        private bool IsFrameDataRequestingUpdate(FrameData frameData)
        {
            // since we call the updating via advancing frame data, we may also want additional checks.
            // Implement comparison logic to check if the new frame data is different
            // comparing timestamps, positions, or any relevant data
            return true;
        }

        private void OnDisable()
        {
            ConfigurationManager.Instance.OnAssetVisualizationConfigChanged -= ApplyVisualizationAssetsConfigChanges;
            ConfigurationManager.Instance.OnDataPathConfigChanged -= ApplyDataPathConfigChanges;
        }
    }
}