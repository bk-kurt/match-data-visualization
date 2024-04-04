using System;
using Managers.Data;
using UnityEngine;
using Utilities;

namespace Managers.State
{
    public class MatchStateManager : MonoSingleton<MatchStateManager>
    {
        //qqq11 since our visualization is basically relying on frame data,
        // the architecture should be reactive and powered by frame based signals and their advancement.
        public event Action<FrameData> OnFrameAdvanced;
        public bool IsPlaying { get; private set; }
        private FrameData CurrentFrame { get; set; }
        private int _currentFrameIndex;

        [Range(-3, 3)] public float playbackSpeed = 1f;
        private float _timeSinceLastFrameChange = 0f;
        private const float TimePerFrame = 0.03f;

        public void InitializeMatchState(FrameData frameData)
        {
            CurrentFrame = frameData;
            OnFrameAdvanced?.Invoke(frameData);
            TogglePlayback(true);
        }


        //qqq12 Instead of starting a coroutine, I take the advantage of update cycle for creating
        // more dynamic, responsive and consistent application takes the adv of good set of conditions.
        private void Update()
        {
            if (!IsPlaying || !MatchDataLoader.Instance.HasFrameData()) return;

            _timeSinceLastFrameChange += Time.deltaTime * playbackSpeed;
            if (Math.Abs(_timeSinceLastFrameChange) >= TimePerFrame)
            {
                _timeSinceLastFrameChange = 0f;
                AdvanceFrame();
            }
        }

        private void AdvanceFrame()
        {
            var frameCount = MatchDataLoader.Instance.GetFrameCount();
            if (playbackSpeed > 0)
            {
                _currentFrameIndex = (_currentFrameIndex + 1) % frameCount;
            }
            else if (playbackSpeed < 0)
            {
                _currentFrameIndex = (_currentFrameIndex - 1 + frameCount) % frameCount;
            }

            CurrentFrame = MatchDataLoader.Instance.GetFrameDataAtIndex(_currentFrameIndex);
            OnFrameAdvanced?.Invoke(CurrentFrame);
        }

        public void TogglePlayback(bool play)
        {
            IsPlaying = play;
        }

        public void SetCurrentFrameIndex(int index)
        {
            if (index >= 0 && index < MatchDataLoader.Instance.GetFrameCount())
            {
                _currentFrameIndex = index;
                InitializeMatchState(MatchDataLoader.Instance.GetFrameDataAtIndex(_currentFrameIndex));
            }
        }

        public int GetCurrentFrameIndex()
        {
            return _currentFrameIndex;
        }

        private void OnDisable()
        {
            TogglePlayback(false);
        }
    }
}