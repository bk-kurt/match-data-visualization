using System.Collections;
using Providers;
using UnityEngine;

namespace Managers
{
    public class MatchInitializer : MonoBehaviour
    {
        [SerializeField] private VisualizationAssetConfigurationProvider VisualizationAssetConfigurationProvider;
        private MatchStateManager _matchStateManager;
        private MatchDataManager _matchDataManager;
        private MatchVisualizationManager _matchVisualizationManager;

        void Awake()
        {
            _matchDataManager = MatchDataManager.Instance;
            _matchStateManager = MatchStateManager.Instance;
            _matchVisualizationManager = MatchVisualizationManager.Instance;
            
            //fetches the current configuration and applies it to the MatchVisualizationManager.
            //This is a good approach, as it decouples the MatchVisualizationManager from directly
            //depending on the VisualizationAssetConfigurationProvider.
            ConfigurationManager.Instance.SetConfiguration(VisualizationAssetConfigurationProvider.GetGameAssetsConfig());
            StartConfig();
        }

        IEnumerator Start()
        {
            yield return LoadGameDataAsync("Assets/Data/Applicant-test-1.JSON");

            if (_matchDataManager.IsDataLoaded && _matchDataManager.AllFrameData.Count > 0)
            {
                var initialFrameData = _matchDataManager.AllFrameData[0];
                _matchStateManager.InitializeMatchState(initialFrameData);

                _matchStateManager.TogglePlayback(true);
            }
            else
            {
                Debug.LogError("Failed to load frame data or data is empty.");
            }
        }
        
        private void StartConfig()
        {
            var config = ConfigurationManager.Instance.GetCurrentConfiguration();
            if (config != null)
            {
                _matchVisualizationManager.SetGameAssetConfiguration(config);
            }
            else
            {
                Debug.LogError("ConfigurationManager does not have a current configuration.");
            }
        }

        private IEnumerator LoadGameDataAsync(string path)
        {
            var loadTask = _matchDataManager.LoadJsonDataAsync(path);
            while (!loadTask.IsCompleted)
            {
                yield return null;
            }

            if (loadTask.Exception != null)
            {
                Debug.LogError($"Error loading data: {loadTask.Exception}");
            }
        }
    }
}