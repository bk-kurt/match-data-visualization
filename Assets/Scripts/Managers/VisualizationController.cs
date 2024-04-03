using System.Collections.Generic;
using Managers.Data;
using Managers.State;
using Scriptables.Data;
using UnityEngine;

namespace Managers
{
    // this component acts like a branch maker, to distrubute actions that are relied on frame unit or whole frame data.
    public class VisualizationController : MonoBehaviour
    {
        public MatchStateManager matchStateManager;
        private MatchVisualizationManager _matchVisualizationManager;
        private static FrameDataStorage FrameDataStorage => MatchDataLoader.Instance.frameDataStorage;

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