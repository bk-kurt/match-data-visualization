using UnityEngine.UI;

namespace UI.Views.World
{
    public class PersonUI : UIView
    {
        public Text displayName;
        public Image highlightImage;

        public void Initialize(string personName)
        {
            displayName.text = personName;
            highlightImage.gameObject.SetActive(false);
        }

        public void UpdateView(bool hasBall)
        {
            highlightImage.gameObject.SetActive(hasBall);
        }
    }
}