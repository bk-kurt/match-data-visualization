using System.Collections.Generic;
using GamePlay.Factory;
using Managers.Configuration;
using Managers.Object;
using Scriptables.Configuration;
using Utilities;

namespace Managers
{
    public class MatchVisualizationManager : MonoSingleton<MatchVisualizationManager>
    {
        private void OnEnable()
        {
            ConfigurationManager.Instance.OnConfigurationChanged += SetGameAssetConfiguration;
        }

        public void SetGameAssetConfiguration(VisualizationAssetsConfigSo config)
        {
            VisualElementFactory visualElementFactory = new VisualElementFactory(config);
            PersonManager.Instance.SetVisualElementFactory(visualElementFactory);
            BallManager.Instance.SetVisualElementFactory(visualElementFactory);

            RefreshVisualStateFromConfigData();
        }

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

        public void UpdateVisualStateFromFrameData(FrameData frameData)
        {
            PersonManager.Instance.UpdatePersonsState(frameData.Persons);
            BallManager.Instance.UpdateBallState(frameData.Ball);
        }

        private void RefreshVisualStateFromConfigData()
        {
            // Optional: Implement logic to refresh visual elements based on new config.
        }

        private void OnDisable()
        {
            ConfigurationManager.Instance.OnConfigurationChanged -= SetGameAssetConfiguration;
        }
    }
}