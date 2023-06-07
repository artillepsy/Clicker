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
            earn.earn = CalculateEarn(businessLevel.level, earn.startEarn, ref entity.Get<UpgradeContainer>());
            earn.earnLabel.text = Literals.GetPriceLabel(earn.earn);
        }

        private static int CalculateEarn(int level, int startEarn, ref UpgradeContainer upgradeContainer)
        {
            var totalMult = 1f;
            
            foreach (var entity in upgradeContainer.upgradeEntities)
            {
                Debug.Log($"entity: {entity}");
                if (!entity.Has<PurchasedMarker>()) continue;
                totalMult += entity.Get<Upgrade>().earnMultiplier / 100f;
            }
            return Mathf.RoundToInt(level * startEarn * totalMult);
        }
    }
}