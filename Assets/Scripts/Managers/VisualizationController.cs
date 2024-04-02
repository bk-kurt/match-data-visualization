using System.Collections.Generic;
using UnityEngine;

namespace Managers
{
    public class VisualizationController : MonoBehaviour
    {
        public MatchStateManager matchStateManager;
        private MatchVisualizationManager _matchVisualizationManager;
        private static FrameDataStorage FrameDataStorage => MatchDataManager.Instance.frameDataStorage;

        private void Awake()
        {
            _matchVisualizationManager = MatchVisualizationManager.Instance;
        }

        private void OnEnable()
        {
            FrameDataStorage.OnFrameDataUpdated += HandleNewFrameDataLoaded;
            matchStateManager.OnFrameAdvanced += HandleFrameAdvanced;
        }

        private void HandleNewFrameDataLoaded(List<FrameData> newFrameData)
        {
            _matchVisualizationManager.InitializeWithNewFrameData(newFrameData);
        }

        private void HandleFrameAdvanced(FrameData currentFrame)
        {
            _matchVisualizationManager.UpdateVisualStateFromFrameData(currentFrame);
        }

        private void OnDisable()
        {
            FrameDataStorage.OnFrameDataUpdated -= HandleNewFrameDataLoaded;
            matchStateManager.OnFrameAdvanced -= HandleFrameAdvanced;
        }
    }
}