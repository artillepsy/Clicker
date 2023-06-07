using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;

namespace Business.Components
{
    /// Component which represents upgrade button
    public struct UpgradeButton
    {
        /// Unique number for easy search in arrays
        public int index;
        /// Parent entity
        public EcsEntity businessEntity;
        
        /// Displays upgrade cost
        public TextMeshProUGUI costLabel;
        /// Displays earn multiplier in percents
        public TextMeshProUGUI earnMultiplierLabel;
        
        /// After clicking sends purchase request
        public Button button;
        
        public int earnMultiplier;
        public int cost;
    }
}