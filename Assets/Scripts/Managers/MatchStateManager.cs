using System;
using Utilities;

namespace Managers
{
    public class MatchStateManager: MonoSingleton<MatchStateManager>
    {
        public event Action<FrameData> OnFrameDataChanged;
        public void TogglePlayback(bool b)
        {
            throw new System.NotImplementedException();
        }

        public void UpdateGameState(FrameData initialFrameData)
        {
            throw new System.NotImplementedException();
        }
    }
}