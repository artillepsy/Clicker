using TMPro;
using UnityEngine.UI;

namespace Business.Components
{
    public struct LevelUpButton
    {
        public Button button;
        public TextMeshProUGUI levelCostLabel;
        public int cost;
        public int startCost;
    }
}