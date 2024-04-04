using System;
using DataModels.Contexts;
using Managers.State;
using UnityEngine;
using Utilities;

namespace UI.ViewModel
{
    public class UIViewModel : MonoSingleton<UIViewModel>
    {
        public event Action<MatchScoreContext> OnScoreChanged;
        public event Action<GameClockContext> OnClockUpdated;

        private MatchScoreContext _lastKnownScore;
        private const float UpdateInterval = 0.1f;
        private float _lastUpdateTime;


        private void OnEnable()
        {
            MatchStateManager.Instance.OnFrameAdvanced += HandleFrameAdvanced;
        }

        /// <summary>
        ///qqq27 selectively notifies, because constant notification by frame unit is not efficient
        /// not the state of art application of selective/dynamic ui notification, due to conditional checks disadvantage.
        /// I may review design.
        /// </summary>
        /// <param name="frameData"></param>
        private void HandleFrameAdvanced(FrameData frameData)
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
            return _lastKnownScore.HomeScore != newScoreContext.HomeScore ||
                   _lastKnownScore.AwayScore != newScoreContext.AwayScore;
        }


        private void OnDisable()
        {
            UnsubscribeFromEvents();
        }

        private void UnsubscribeFromEvents()
        {
            if (MatchStateManager.Instance)
            {
                MatchStateManager.Instance.OnFrameAdvanced -= HandleFrameAdvanced;
            }
        }
    }
}