using Business.Components;
using Business.Flags;
using Constants;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Utils
{
    public static class CalculationUtils
    {
        public static void UpdateLevelCost(ref LevelUpButton levelUpButton, int level)
        {
             levelUpButton.cost = levelUpButton.startCost * (level + 1);
             levelUpButton.levelCostLabel.text = Literals.GetCostLabel(levelUpButton.cost);
        }

        public static void UpdateEarn(ref EcsEntity entity, ref Earn earn, ref BusinessLevel businessLevel)
        {
            earn.earn = CalculateEarn(businessLevel.level, earn.startEarn, ref entity.Get<UpgradesContainer>());
            earn.earnLabel.text = Literals.GetPriceLabel(earn.earn);
        }

        private static int CalculateEarn(int level, int startEarn, ref UpgradesContainer upgradesContainer)
        {
            var totalMult = 1f;
            
            foreach (var entity in upgradesContainer.upgradeEntities)
            {
                if (!entity.Has<PurchasedMarker>()) continue;
                totalMult += entity.Get<UpgradeButton>().earnMultiplier / 100f;
            }
            return Mathf.RoundToInt(level * startEarn * totalMult);
        }
    }
}