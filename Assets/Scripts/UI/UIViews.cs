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

        public void UpdateScoreBoard(MatchScoreContext scoreContext)
        {
            scoreboard.UpdateUI(scoreContext);
        }

        public void UpdateClock(GameClockContext clockContext)
        {
            gameClock.UpdateUI(clockContext);
        }
    }
}