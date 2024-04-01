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

        private float _timeSinceLastFrameChange = 0f;
        private const float TimePerFrame = 0.03f;
        private bool _isPlaying = true;

        void Update()
        {
            if (!_isPlaying || MatchDataManager.Instance.allFrameData.Count == 0) return;

            _timeSinceLastFrameChange += Time.deltaTime;
            if (Math.Abs(_timeSinceLastFrameChange) >= TimePerFrame)
            {
                _timeSinceLastFrameChange = 0f;
                AdvanceFrame();
            }
        }

        private void AdvanceFrame()
        {
            var frameCount = MatchDataManager.Instance.allFrameData.Count;

            _currentFrameIndex = (_currentFrameIndex + 1) % frameCount;
            CurrentFrame = MatchDataManager.Instance.allFrameData[_currentFrameIndex];
            
            OnFrameDataChanged?.Invoke(CurrentFrame);
        }

        public void UpdateGameState(FrameData frameData)
        {
            CurrentFrame = frameData;
            OnFrameDataChanged?.Invoke(frameData);
        }
        
        public void TogglePlayback(bool play)
        {
            _isPlaying = play;
        }
    }
}