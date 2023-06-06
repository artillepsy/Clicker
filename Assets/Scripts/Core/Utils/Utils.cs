using Business.Components;
using Business.Flags;
using Core.Constants;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Utils
{
    public static class Utils
    {
        public static void UpdateUpgradeButtonInteractable(ref Upgrade upgrade, ref BusinessLevel businessLevel)
        {
            upgrade.upgradeButton.interactable = businessLevel.level > 0;
        }
        
        public static void UpdateLevelCost(ref LevelUp levelUp, int level)
        {
             levelUp.cost = levelUp.startCost * (level + 1);
             levelUp.levelCostLabel.text = Literals.GetCostLabel(levelUp.cost);
        }

        public static void UpdateEarn(ref EcsEntity entity, ref Earn earn, ref BusinessLevel businessLevel)
        {
            ref var upgradeEntity1 = ref entity.Get<UpgradeContainer>().upgradeEntity1;
            ref var upgradeEntity2 = ref entity.Get<UpgradeContainer>().upgradeEntity2;
            
            ref var upgrade1 = ref entity.Get<UpgradeContainer>().upgradeEntity2.Get<Upgrade>();
            ref var upgrade2 = ref entity.Get<UpgradeContainer>().upgradeEntity2.Get<Upgrade>();
            
            var mult1Percent = upgradeEntity1.Has<PurchasedMarker>() ? upgrade1.earnMultiplier : 0;
            var mult2Percent = upgradeEntity2.Has<PurchasedMarker>() ?  upgrade2.earnMultiplier : 0;
            
            earn.earn = CalculateEarn(businessLevel.level, earn.startEarn, mult1Percent, mult2Percent);
            earn.earnLabel.text = Literals.GetPriceLabel(earn.earn);
        }

        private static int CalculateEarn(int level, int startEarn, int mult1Percent = 0, int mult2Percent = 0)
        {
            return Mathf.RoundToInt(level * startEarn * (1f + mult1Percent / 100f + mult2Percent / 100f));
        }
    }
}