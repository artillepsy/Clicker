using Leopotam.Ecs;
using TMPro;
using UnityEngine.UI;

namespace Business.Components
{
    public struct Upgrade
    {
        public EcsEntity businessEntityRef;
        public TextMeshProUGUI costLabel;
        public TextMeshProUGUI earnMultiplierLabel;
        public Button upgradeButton;
        public int cost;
        public int earnMultiplier;
    }
}