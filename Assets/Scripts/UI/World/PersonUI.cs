using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace
{
    public class PersonUI : MonoBehaviour
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