using Business.Components;
using Business.Flags;
using Business.Reactive;
using Constants;
using Leopotam.Ecs;

namespace Business.Systems
{
    public class UpdateLevelSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BusinessLevel, LevelUpButton, Earn, UpgradeContainer, LevelUpRequest> _businessesFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;

        public void Run()
        {
            foreach (var i in _businessesFilter)
            {
                ref var businessLevel = ref _businessesFilter.Get1(i);
                ref var levelUp = ref _businessesFilter.Get2(i);
                ref var earn = ref _businessesFilter.Get3(i);
                ref var upgradeContainer = ref _businessesFilter.Get4(i);
                ref var entity = ref _businessesFilter.GetEntity(i);
                ref var balance = ref _balanceFilter.Get1(0);

                if (businessLevel.level == 0)
                {
                    entity.Get<PurchasedMarker>();
                }

                ReduceMoney(ref balance, ref levelUp);
                IncrementLevel(ref businessLevel);
                Utils.CalculationUtils.UpdateEarn(ref entity, ref earn, ref businessLevel);
                Utils.CalculationUtils.UpdateLevelCost(ref levelUp, businessLevel.level);
            }
        }

        private void IncrementLevel(ref BusinessLevel businessLevel)
        { 
            businessLevel.level++;
            businessLevel.label.text = businessLevel.level.ToString();
        }

        private void ReduceMoney(ref Balance.Components.Balance balance, ref LevelUpButton levelUpButton)
        {
            balance.moneyCount -= levelUpButton.cost;
            balance.label.text = Literals.GetBalanceLabel(balance.moneyCount);
        }
    }
}