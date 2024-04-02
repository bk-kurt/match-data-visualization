using System;
using UnityEngine;
using Utilities;

namespace Managers
{
    public class MatchStateManager : MonoSingleton<MatchStateManager>
    {
        public event Action<FrameData> OnFrameDataChanged;

        private FrameData CurrentFrame { get; set; }
        private int _currentFrameIndex;

        [Range(-3, 3)] public float playbackSpeed = 1f;
        private float _timeSinceLastFrameChange = 0f;
        private const float TimePerFrame = 0.03f;
        private bool _isPlaying = true;

        public void InitializeMatchState(FrameData frameData)
        {
            CurrentFrame = frameData;
            OnFrameDataChanged?.Invoke(frameData);
        }
        void Update()
        {
            if (!_isPlaying || MatchDataManager.Instance.AllFrameData.Count == 0) return;

            _timeSinceLastFrameChange += Time.deltaTime* playbackSpeed;
            if (Math.Abs(_timeSinceLastFrameChange) >= TimePerFrame)
            {
                _timeSinceLastFrameChange = 0f;
                AdvanceFrame();
            }
        }

        private void AdvanceFrame()
        {
            var frameCount = MatchDataManager.Instance.AllFrameData.Count;
            if (playbackSpeed > 0)
            {
                _currentFrameIndex = (_currentFrameIndex + 1) % frameCount;
            }
            else if (playbackSpeed < 0)
            {
                _currentFrameIndex = (_currentFrameIndex - 1 + frameCount) % frameCount;
            }
            CurrentFrame = MatchDataManager.Instance.AllFrameData[_currentFrameIndex];
            OnFrameDataChanged?.Invoke(CurrentFrame);
        }
        
        public void TogglePlayback(bool play)
        {
            _isPlaying = play;
        }

        public void SetCurrentFrameIndex(int index)
        {
            if (index >= 0 && index < MatchDataManager.Instance.GetFrameCount())
            {
                _currentFrameIndex = index;
                InitializeMatchState(MatchDataManager.Instance.GetFrameDataAtIndex(_currentFrameIndex));
            }
        }
        public int GetCurrentFrameIndex()
        {
            return _currentFrameIndex;
        }
    }
}