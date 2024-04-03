using System;
using DataModels;
using Managers;
using UnityEngine;
using Utilities;

namespace UI.Screen
{
    public class UIViewModel : MonoSingleton<UIViewModel>
    {
        public event Action<MatchScoreContext> OnScoreChanged;
        public event Action<GameClockContext> OnClockUpdated;
        
        private MatchScoreContext _lastKnownScore;
        private const float UpdateInterval = 0.1f;
        private float _lastUpdateTime = 0f;


        void OnEnable()
        {
            MatchStateManager.Instance.OnFrameAdvanced += OnGameAdvanced;
        }

        void OnDisable()
        {
            MatchStateManager.Instance.OnFrameAdvanced -= OnGameAdvanced;
        }

        private void OnGameAdvanced(FrameData frameData)
        {
            if (frameData == null)
            {
                Debug.LogError("Invalid frame data received.");
                return;
            }

            // Clock update
            if (Time.time - _lastUpdateTime >= UpdateInterval)
            {
                OnClockUpdated?.Invoke(TimeHelper.ClockContextFromTimeStamp(frameData.TimestampUtc));
                _lastUpdateTime = Time.time;
            }

            // Score change detection
            if (_lastKnownScore == null || HasScoreChanged(frameData.MatchScoreContext))
            {
                OnScoreChanged?.Invoke(frameData.MatchScoreContext);
                _lastKnownScore = frameData.MatchScoreContext;
            }
        }

        private bool HasScoreChanged(MatchScoreContext newScoreContext)
        {
            return _lastKnownScore.HomeScore != newScoreContext.HomeScore || _lastKnownScore.AwayScore != newScoreContext.AwayScore;
        }
    }
}