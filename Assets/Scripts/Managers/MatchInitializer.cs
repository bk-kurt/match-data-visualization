using System.Collections;
using Managers.Configuration;
using Managers.Data;
using Managers.State;
using Providers;
using UnityEngine;

namespace Managers
{
    // in more defined product, I would create/use a custom dependency wrapper or use Zenject,
    // followed by scene set up config. but for a flexible project building, leaving that for this iteration.
    public class MatchInitializer : MonoBehaviour
    {
        [SerializeField] private VisualizationAssetConfigurationProvider visualizationAssetConfigurationProvider;
        private MatchStateManager _matchStateManager;
        private MatchDataManager _matchDataManager;
        private MatchVisualizationManager _matchVisualizationManager;

        void Awake()
        {
            _matchDataManager = MatchDataManager.Instance;
            _matchStateManager = MatchStateManager.Instance;
            _matchVisualizationManager = MatchVisualizationManager.Instance;

            // fetches the current configuration and applies it to the MatchVisualizationManager.
            //This approach saves the MatchVisualizationManager from directly depending on the
            // VisualizationAssetConfigurationProvider.
            ConfigurationManager.Instance.SetConfiguration(
                visualizationAssetConfigurationProvider.GetGameAssetsConfig());
            StartConfig();
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
        
        private void Start()
        {
            if (_matchDataManager.HasFrameData())
            {
                InitializeMatch();
            }
            else
            {
                Debug.LogWarning("Failed to load prepared frame data or data is empty, preparing again...");
                StartCoroutine(StartLoadingData());
            }
        }

        IEnumerator StartLoadingData()
        {
            yield return LoadGameDataAsync("Assets/Data/Applicant-test-1.JSON");

            if (_matchDataManager.IsDataLoaded && _matchDataManager.GetFrameCount() > 0)
            {
                InitializeMatch();
            }
            else
            {
                Debug.LogError("Failed to load frame data or data is empty.");
            }
        }

        private void InitializeMatch()
        {
            var initialFrameData = _matchDataManager.GetFrameDataAtIndex(0);
            _matchStateManager.InitializeMatchState(initialFrameData);
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