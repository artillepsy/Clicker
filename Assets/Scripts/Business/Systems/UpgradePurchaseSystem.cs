using Business.Components;
using Business.Flags;
using Business.Reactive;
using Constants;
using Leopotam.Ecs;

namespace Business.Systems
{
    public class UpgradePurchaseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UpgradeButton, PurchaseUpgradeRequest> _filter = null;
        
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
            upgrade.button.onClick.RemoveAllListeners();
            upgrade.button.interactable = false;
        }

        private void UpdateEarn(int i)
        {
            ref var entity = ref _filter.Get1(i).businessEntity;
            Utils.CalculationUtils.UpdateEarn(ref entity, ref entity.Get<Earn>(), ref entity.Get<BusinessLevel>());
        }
    }
}