using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;

namespace Business.Components
{
    public struct UpgradeButton
    {
        public int index;
        public EcsEntity businessEntity;
        
        public TextMeshProUGUI costLabel;
        public TextMeshProUGUI earnMultiplierLabel;
        
        public Button button;
        
        public int earnMultiplier;
        public int cost;
    }
}