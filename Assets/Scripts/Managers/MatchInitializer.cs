using System.Collections;
using Managers.Data;
using Managers.State;
using UnityEngine;

namespace Managers
{
    //qqq13 in a defined larger project that has CI/CD and QA concerns, I would follow ServiceLocator pattern or use Zenject,
    // and would populate scene following that and configs. but for simple project like this, not preferring that for now.
    public class MatchInitializer : MonoBehaviour
    {
        private MatchStateManager _matchStateManager;
        private MatchDataLoader _matchDataLoader;

        /// <summary>
        ///qqq14 I put in use OnEnable instead of awake, although singletons are lazy loaded,
        /// just make sure for scaled and heavy scenarios scenarios.
        /// </summary>
        private void OnEnable()
        {
            _matchDataLoader = MatchDataLoader.Instance;
            _matchStateManager = MatchStateManager.Instance;
            
            MatchVisualizationManager.Instance.InitializeFactories();
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
            yield return _matchDataLoader.LoadJsonDataAsync();

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