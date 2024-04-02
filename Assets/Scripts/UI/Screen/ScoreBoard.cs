using DataModels;
using UnityEngine.UI;

namespace UI.Screen
{
    public class Scoreboard : UIView
    {
        public Text scoreText;

        public void UpdateUI(MatchScoreContext context)
        {
            scoreText.text = $"Home: {context.HomeScore} - Away: {context.AwayScore}";
        }
    }
}