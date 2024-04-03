using System.Collections;
using Managers.Configuration;
using Managers.Data;
using Managers.State;
using Providers;
using UnityEngine;

namespace Managers
{
    // in a defined larger project that has CI/CD and QA concerns, I would follow ServiceLocator pattern or use Zenject,
    // and would populate scene following that and configs. but for simple project like this, not preferring that for now.
    public class MatchInitializer : MonoBehaviour
    {
        [SerializeField] private VisualizationAssetConfigurationProvider visualizationAssetConfigurationProvider;
        private MatchStateManager _matchStateManager;
        private MatchDataLoader _matchDataLoader;
        private MatchVisualizationManager _matchVisualizationManager;

        /// <summary>
        /// I put in use OnEnable instead of awake, although singletons are lazy loaded,
        /// just make sure for scaled and heavy scenarios scenarios.
        /// </summary>
        private void OnEnable()
        {
            _matchDataLoader = MatchDataLoader.Instance;
            _matchStateManager = MatchStateManager.Instance;
            _matchVisualizationManager = MatchVisualizationManager.Instance;


            // fetches the current configuration and applies it to the MatchVisualizationManager.
            //This approach saves the MatchVisualizationManager from directly depending on the
            // VisualizationAssetConfigurationProvider.
            ConfigurationManager.Instance.SetConfiguration(
                visualizationAssetConfigurationProvider.GetGameAssetsConfig());
            ApplyConfiguration();
        }

        private void ApplyConfiguration()
        {
            var config = ConfigurationManager.Instance.GetCurrentConfiguration();
            if (!config)
            {
                Debug.LogError("ConfigurationManager does not have a current configuration.");
                // we can create a fallback mechanism to request a reconfiguration here

                return;
            }
            _matchVisualizationManager.SetGameAssetConfiguration(config);
        }

        private void Start()
        {
            if (!_matchDataLoader.HasFrameData())
            {
                Debug.LogWarning("Failed to load prepared frame data or data is empty, preparing again...");
                StartCoroutine(StartLoadingData());
                return;
            }

            InitializeMatch();
        }

        IEnumerator StartLoadingData()
        {
            yield return _matchDataLoader.LoadJsonDataAsync("Assets/Data/Applicant-test-1.JSON");

            if (!_matchDataLoader.HasInitializableData())
            {
                Debug.LogError("Failed to load frame data or data is empty.");
                yield break;
            }

            InitializeMatch();
        }

        private void InitializeMatch()
        {
            var initialFrameData = _matchDataLoader.GetFrameDataAtIndex(0);
            _matchStateManager.InitializeMatchState(initialFrameData);
        }
    }
}