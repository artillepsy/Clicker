using Business.Components;
using Business.Reactive;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class InitUpgradeButtonsSystem : IEcsInitSystem
    {
        private readonly EcsFilter<UpgradeButton> _upgradesFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;
        
        public void Init()
        {
            foreach (var i in _upgradesFilter)
            {
                ref var upgrade = ref _upgradesFilter.Get1(i);
                upgrade.button.onClick.AddListener(delegate { OnClickPurchaseUpgrade(i); });
            }
        }

        private void OnClickPurchaseUpgrade(int i)
        {
            ref var balance = ref _balanceFilter.Get1(0);
            ref var upgrade = ref _upgradesFilter.Get1(i);
            ref var upgradeEntity = ref _upgradesFilter.GetEntity(i);

            if (upgrade.cost > balance.moneyCount)
            {
                return;
            }
            upgradeEntity.Get<PurchaseUpgradeRequest>();
        }
    }
}