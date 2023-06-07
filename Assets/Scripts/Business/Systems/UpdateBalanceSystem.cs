using Business.Flags;
using Business.Reactive;
using Constants;
using Leopotam.Ecs;

namespace Business.Systems
{
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