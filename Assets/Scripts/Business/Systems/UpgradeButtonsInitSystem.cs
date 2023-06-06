using Business.Components;
using Business.Reactive;
using Core.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class UpgradeButtonsInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<Upgrade> _upgradesFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;
        
        public void Init()
        {
            foreach (var i in _upgradesFilter)
            {
                ref var upgrade = ref _upgradesFilter.Get1(i);
                ref var businessLevel = ref upgrade.businessEntity.Get<BusinessLevel>();
                
                Utils.UpdateUpgradeButtonInteractable(ref upgrade, ref  businessLevel);
                upgrade.upgradeButton.onClick.AddListener(delegate { OnClickLevelUp(i); });
            }
        }

        private void OnClickLevelUp(int i)
        {
            ref var balance = ref _balanceFilter.Get1(0);
            ref var upgrade = ref _upgradesFilter.Get1(i);
            ref var upgradeEntity = ref _upgradesFilter.GetEntity(i);

            if (upgrade.cost > balance.moneyCount)
            {
                return;
            }
            upgradeEntity.Get<PurchaseUpgradeRequest>();
            Debug.Log($"Purchase upgrade: {i}");
        }
    }
}