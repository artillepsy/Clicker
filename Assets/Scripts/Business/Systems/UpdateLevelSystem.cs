using Business.Components;
using Business.Flags;
using Core;
using Core.Constants;
using Core.Utils;
using Leopotam.Ecs;

namespace Business.Systems
{
    public class UpdateLevelSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BusinessLevel, LevelUp, Earn, LevelUpEvent> _businessesFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;

        public void Run()
        {
            foreach (var i in _businessesFilter)
            {
                ref var businessLevel = ref _businessesFilter.Get1(i);
                ref var levelUp = ref _businessesFilter.Get2(i);
                ref var earn = ref _businessesFilter.Get3(i);
                ref var entity = ref _businessesFilter.GetEntity(i);
                ref var balance = ref _balanceFilter.Get1(0);

                if (businessLevel.level == 0)
                {
                    entity.Get<PurchasedMarker>();
                }

                IncrementLevel(ref businessLevel, ref levelUp);
                UpdateEarn(ref entity, ref earn, ref businessLevel);
                ReduceMoney(ref balance, ref levelUp);
            }
        }

        private static void UpdateEarn(ref EcsEntity entity, ref Earn earn, ref BusinessLevel businessLevel)
        {
            ref var upgrade1 = ref entity.Get<UpgradeContainer>().upgrade1.Get<Upgrade>();
            ref var upgrade2 = ref entity.Get<UpgradeContainer>().upgrade2.Get<Upgrade>();

            var mult1Percent = upgrade1.purchased ? upgrade1.earnMultiplier : 0;
            var mult2Percent = upgrade2.purchased ? upgrade2.earnMultiplier : 0;
            earn.earn = Utils.GetEarn(businessLevel.level, earn.startEarn, mult1Percent, mult2Percent);
        }

        private void IncrementLevel(ref BusinessLevel businessLevel, ref LevelUp levelUp)
        { 
            businessLevel.level++;
            businessLevel.label.text = businessLevel.level.ToString();
            levelUp.cost = Utils.GetLevelCost(businessLevel.level, levelUp.cost);
            
        }

        private static void ReduceMoney(ref Balance.Components.Balance balance, ref LevelUp levelUp)
        {
            balance.moneyCount -= levelUp.cost;
            balance.label.text = Literals.GetBalanceLabel(balance.moneyCount);
        }
    }
}