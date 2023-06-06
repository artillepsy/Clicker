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
        public Image progressBarImage;

        public UpgradeDisplay firstUpgradeDisplay;
        public UpgradeDisplay secondUpgradeDisplay;
    }
}