using Business.Reactive;
using Leopotam.Ecs;
using Utils;

namespace Business.Systems
{
    /// Changes money count after each earn tick
    public class UpdateBalanceSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Balance.Components.Balance, EarnedMoneyEvent> _balanceFilter = null;

        public void Run()
        {
            ref var balance = ref _balanceFilter.Get1(0);
            ref var earnedEvent = ref _balanceFilter.Get2(0);

            balance.moneyCount += earnedEvent.moneyToAdd;
            balance.label.text = Literals.GetBalanceLabel(balance.moneyCount);
        }
    }
}