using DataModels.Contexts;
using UI.ViewModel;
using UI.Views.Screen;
using UnityEngine;

namespace UI
{
    public class UIViews : MonoBehaviour
    {
        public Scoreboard scoreboard;
        public GameClock gameClock;

        private void OnEnable()
        {
            var uiViewModel = UIViewModel.Instance;

            uiViewModel.OnScoreChanged += UpdateScoreBoard;
            uiViewModel.OnClockUpdated += UpdateClock;
        }

        private void OnDisable()
        {
            var uiViewModel = UIViewModel.Instance;
            
            if (uiViewModel != null)
            {
                uiViewModel.OnScoreChanged -= UpdateScoreBoard;
                uiViewModel.OnClockUpdated -= UpdateClock;
            }
        }

        private void UpdateScoreBoard(MatchScoreContext scoreContext)
        {
            if (scoreboard) // null check will no longer be needed with dep.initialization system
            {
                scoreboard.UpdateUI(scoreContext);
            }
        }

        private void UpdateClock(GameClockContext clockContext)
        {
            if (gameClock)
            {
                gameClock.UpdateUI(clockContext);
            }
        }
    }
}