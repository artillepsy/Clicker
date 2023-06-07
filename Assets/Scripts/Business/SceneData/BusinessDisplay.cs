using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Business.SceneData
{
    /// displays certain business
    public class BusinessDisplay : MonoBehaviour
    {
        public TextMeshProUGUI nameLabel;
        public TextMeshProUGUI businessIndexLabel;
        /// Displays business number
        public TextMeshProUGUI levelCounterLabel;
        /// Displays how much this business currently earns
        public TextMeshProUGUI earnCounterLabel;
        /// Displays current cost of next levelUp
        public TextMeshProUGUI levelUpCostLabel;
        
        /// Displays visual time which updates every frame
        [Space]
        public Image progressBarImage;
        public Button levelUpButton;

        /// Transform to parent all upgrades relative to this businesses
        [Space]
        public RectTransform upgradesParent;
    }
}