using DataModels.Contexts;
using UnityEngine.UI;

namespace UI.Views.Screen
{
    public class GameClock : UIView
    {
        public Text clockText;

        public void UpdateUI(GameClockContext context)
        {
            clockText.text = $"Min: {context.Minute} " + $" Sec: {context.Second}" + $" Per: {context.Period}";
        }
    }
}