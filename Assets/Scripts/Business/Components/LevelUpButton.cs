using TMPro;
using UnityEngine.UI;

namespace Business.Components
{
    /// Component which represents level up button
    public struct LevelUpButton
    {
        /// After clicking increments level
        public Button button;
        /// Displays current levelUp cost
        public TextMeshProUGUI levelCostLabel;
        /// Current cost value
        public ulong cost;
        /// Start cost value. Sets from config
        public ulong startCost;
    }
}