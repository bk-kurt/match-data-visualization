using System.Collections;
using UnityEngine;

namespace Managers
{
    public class MatchInitializer : MonoBehaviour
    {
        private FrameDataManager _matchDataManager;

        void Awake()
        {
            _matchDataManager = FrameDataManager.Instance;
        }

        IEnumerator Start()
        {
            yield return LoadGameDataAsync("Assets/Data/Applicant-test.JSON");
        }
        
        private IEnumerator LoadGameDataAsync(string path)
        {
            var loadTask = _matchDataManager.LoadJsonDataAsync(path);
            while (!loadTask.IsCompleted)
            {
                Debug.Log("completed");
                yield return null;
            }
            if (loadTask.Exception != null)
            {
                Debug.LogError($"Error loading data: {loadTask.Exception}");
            }
        }
    }
}