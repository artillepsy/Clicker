using Business.Flags;
using Leopotam.Ecs;

namespace Business.Systems
{
    public class UpdateBalanceSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Balance.Components.Balance, EarnedMoneyEvent> _balanceFilter = null;
        
        public void Run()
        {
            foreach (var i in _balanceFilter)
            {
                ref var balance = ref _balanceFilter.Get1(i);
                ref var earnedEvent = ref _balanceFilter.Get2(i);
               
                balance.moneyCount += earnedEvent.moneyToAdd;
                balance.label.text = $"Balance: {balance.moneyCount}$";
                
                _balanceFilter.GetEntity(i).Del<EarnedMoneyEvent>();
            }
        }
    }
}