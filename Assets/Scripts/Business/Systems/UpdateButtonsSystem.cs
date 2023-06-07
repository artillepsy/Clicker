using Business.Components;
using Business.Flags;
using Business.Reactive;
using Leopotam.Ecs;

namespace Business.Systems
{
    public class UpdateButtonsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<Balance.Components.Balance> _initBalanceFilter = null;
        private readonly EcsFilter<Balance.Components.Balance, MoneyChangedEvent> _balanceFilter = null;
        private readonly EcsFilter<LevelUpButton> _levelUpButtonsFilter = null;
        private readonly EcsFilter<UpgradeButton>.Exclude<PurchasedMarker> _upgradeButtonsFilter = null;

        public void Init()
        {
            var balance = _initBalanceFilter.Get1(0);
            
            foreach (var i in _upgradeButtonsFilter)
            {
                ref var upgradeButton = ref _upgradeButtonsFilter.Get1(i);
                upgradeButton.button.interactable = 
                    balance.moneyCount >= upgradeButton.cost && upgradeButton.businessEntity.Get<BusinessLevel>().level > 0;
            }
        }

        public void Run()
        {
            if (_balanceFilter.IsEmpty()) return;

            var balance = _balanceFilter.Get1(0);

            foreach (var i in _levelUpButtonsFilter)
            {
                ref var levelUpButton = ref _levelUpButtonsFilter.Get1(i);
                levelUpButton.button.interactable = balance.moneyCount >= levelUpButton.cost;
            }
            
            foreach (var i in _upgradeButtonsFilter)
            {
                ref var upgradeButton = ref _upgradeButtonsFilter.Get1(i);
                upgradeButton.button.interactable = 
                    balance.moneyCount >= upgradeButton.cost && upgradeButton.businessEntity.Get<BusinessLevel>().level > 0;
            }
        }
    }
}