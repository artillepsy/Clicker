using Business.Components;
using Business.Flags;
using Business.Reactive;
using Leopotam.Ecs;
using Utils;

namespace Business.Systems
{
    /// Increments level and reduces money on player's balance after recieving levelUpRequest.
    public class UpdateLevelSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Level, LevelUpButton, Earn, LevelUpRequest> _businessesFilter = null;
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

                if (!entity.Has<PurchasedMarker>())
                {
                    entity.Get<PurchasedMarker>();
                }
                ReduceMoney(ref balance, ref levelUp);
                IncrementLevel(ref businessLevel);
                Utils.Calculation.UpdateEarn(ref entity, ref earn, ref businessLevel);
                Utils.Calculation.UpdateLevelCost(ref levelUp, businessLevel.level);
                
                _balanceFilter.GetEntity(0).Get<MoneyChangedEvent>();
            }
        }

        private void IncrementLevel(ref Level level)
        { 
            level.level++;
            level.label.text = level.level.ToString();
        }

        private void ReduceMoney(ref Balance.Components.Balance balance, ref LevelUpButton levelUpButton)
        {
            balance.moneyCount -= levelUpButton.cost;
            balance.label.text = Literals.GetBalanceLabel(balance.moneyCount);
        }
    }
}