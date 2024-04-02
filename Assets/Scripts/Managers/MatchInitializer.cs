using System.Collections;
using UnityEngine;

namespace Managers
{
    public class MatchInitializer : MonoBehaviour
    {
        private MatchStateManager _matchStateManager;
        private MatchDataManager _matchDataManager;

        void Awake()
        {
            _matchDataManager = MatchDataManager.Instance;
            _matchStateManager = MatchStateManager.Instance;
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