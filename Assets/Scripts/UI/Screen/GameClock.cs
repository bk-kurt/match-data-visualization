using DataModels;
using UnityEngine.UI;

namespace UI.Screen
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