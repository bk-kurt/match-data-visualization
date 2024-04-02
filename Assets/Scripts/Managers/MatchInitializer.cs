using System.Collections;
using Providers;
using UnityEngine;

namespace Managers
{
    public class MatchInitializer : MonoBehaviour
    {
        [SerializeField] private VisualizationAssetConfigurationProvider configProvider;
        
        private MatchStateManager _matchStateManager;
        private MatchDataManager _matchDataManager;
        private MatchVisualizationManager _matchVisualizationManager;

        void Awake()
        {
            _matchDataManager = MatchDataManager.Instance;
            _matchStateManager = MatchStateManager.Instance;
            _matchVisualizationManager = MatchVisualizationManager.Instance;
        }

        IEnumerator Start()
        {
            ConfigureVisualizationManager();
            
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

        private void ConfigureVisualizationManager()
        {
            if (configProvider != null)
            {
                _matchVisualizationManager.SetGameAssetConfiguration(configProvider);
            }
            else
            {
                Debug.LogError("GameAssetConfigurationProvider not found.");
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