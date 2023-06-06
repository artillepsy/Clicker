using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;

namespace Business.Components
{
    public struct Upgrade
    {
        public EcsEntity businessEntity;
        
        public TextMeshProUGUI costLabel;
        public TextMeshProUGUI earnMultiplierLabel;
        
        public Button upgradeButton;
        
        public int earnMultiplier;
        public int cost;
    }
}