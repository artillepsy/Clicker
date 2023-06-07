using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Business.SceneData
{
    public class BusinessDisplay : MonoBehaviour
    {
        public TextMeshProUGUI nameLabel;
        public TextMeshProUGUI businessIndexLabel;
        public TextMeshProUGUI levelCounterLabel;
        public TextMeshProUGUI earnCounterLabel;
        public TextMeshProUGUI levelUpCostLabel;
        [Space]
        public Image progressBarImage;
        public Button levelUpButton;
        [Space]
        public RectTransform upgradesParent;
    }
}