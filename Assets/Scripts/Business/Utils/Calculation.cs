using Business.Components;
using Business.Flags;
using Leopotam.Ecs;
using Utils;

namespace Business.Utils
{
    /// Contains helper methods with specific formules
    public static class Calculation
    {
        /// Applies level cost and visuals for levelUp Button
        public static void UpdateLevelCost(ref LevelUpButton levelUpButton, int level)
        {
             levelUpButton.cost = levelUpButton.startCost * (ulong)(level + 1);
             levelUpButton.levelCostLabel.text = Literals.GetCostLabel(levelUpButton.cost);
        }

        /// Updates earn info for business depending on level
        public static void UpdateEarn(ref EcsEntity entity, ref Earn earn, ref Level level)
        {
            earn.earn = CalculateEarn(level.level, earn.startEarn, ref entity.Get<UpgradesContainer>());
            earn.earnLabel.text = Literals.GetPriceLabel(earn.earn);
        }

        /// Calculates current earn info depending on all purchased upgrades
        private static ulong CalculateEarn(int level, ulong startEarn, ref UpgradesContainer upgradesContainer)
        {
            var totalMult = 1f;
            
            foreach (var entity in upgradesContainer.upgradeEntities)
            {
                if (!entity.Has<PurchasedMarker>()) continue;
                totalMult += entity.Get<UpgradeButton>().earnMultiplier / 100f;
            }
            return (ulong)((ulong)level * startEarn * totalMult);
        }
    }
}