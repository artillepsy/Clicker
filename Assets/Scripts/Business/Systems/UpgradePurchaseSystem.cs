using Business.Components;
using Business.Flags;
using Business.Reactive;
using Core.Constants;
using Core.Utils;
using Leopotam.Ecs;

namespace Business.Systems
{
    public class UpgradePurchaseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Upgrade, PurchaseUpgradeRequest> _filter = null;
        
        public void Run()
        {
            foreach (var i in _filter)
            {
                PurchaseUpgrade(i);
                UpdateEarn(i);
                
            }
        }

        private void PurchaseUpgrade(int i)
        {
            ref var upgradeEntity = ref _filter.GetEntity(i);
            ref var upgrade = ref _filter.Get1(i);
            
            upgradeEntity.Del<PurchaseUpgradeRequest>();
            upgradeEntity.Get<PurchasedMarker>();
            
            upgrade.costLabel.text = Literals.GetPurchasedLabel();
            upgrade.upgradeButton.onClick.RemoveAllListeners();
            upgrade.upgradeButton.interactable = false;
        }

        private void UpdateEarn(int i)
        {
            ref var entity = ref _filter.Get1(i).businessEntity;
            Utils.UpdateEarn(ref entity, ref entity.Get<Earn>(), ref entity.Get<BusinessLevel>());
        }
    }
}