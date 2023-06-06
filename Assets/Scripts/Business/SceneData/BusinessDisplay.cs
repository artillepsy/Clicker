using Microsoft.Unity.VisualStudio.Editor;
using TMPro;
using UnityEngine;

namespace Business.SceneData
{
    public class BusinessDisplay : MonoBehaviour
    {
        public TextMeshProUGUI nameLabel;
        public TextMeshProUGUI levelCounterLabel;
        public TextMeshProUGUI earnLabel;
        public TextMeshProUGUI levelUpCostLabel;
        public Image progressBarImage;

        public UpgradeDisplay firstUpgrade;
        public UpgradeDisplay secondUpgrade;
    }
}