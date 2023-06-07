using Business.Components;
using Business.Flags;
using Business.Reactive;
using Leopotam.Ecs;

namespace Business.Systems
{
    /// Changes buttons interactable status after changing balance.
    /// If player hasn't enough money for buy level or upgrade the button will be non-clickable
    public class UpdateButtonsSystem : IEcsRunSystem, IEcsInitSystem
    {
        private readonly EcsFilter<Balance.Components.Balance> _initBalanceFilter = null;
        private readonly EcsFilter<Balance.Components.Balance, MoneyChangedEvent> _balanceFilter = null;
        
        private readonly EcsFilter<LevelUpButton> _levelUpButtonsFilter = null;
        private readonly EcsFilter<UpgradeButton>.Exclude<PurchasedMarker> _upgradeButtonsFilter = null;

        /// Sets interactable status before game prosess 
        public void Init()
        {
            var balance = _initBalanceFilter.Get1(0);
            
            foreach (var i in _upgradeButtonsFilter)
            {
                ref var upgradeButton = ref _upgradeButtonsFilter.Get1(i);
                upgradeButton.button.interactable = 
                    balance.moneyCount >= upgradeButton.cost && upgradeButton.businessEntity.Get<Level>().level > 0;
            }
        }

        /// Sets interactable status after every change on player's balance
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
                    balance.moneyCount >= upgradeButton.cost && upgradeButton.businessEntity.Get<Level>().level > 0;
            }
        }
    }
}