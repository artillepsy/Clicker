using Business.Components;
using Business.Flags;
using Business.Reactive;
using Leopotam.Ecs;
using Utils;

namespace Business.Systems
{
    /// Applies upgrade after it's bying and reduces player's money on balance
    public class UpgradePurchaseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UpgradeButton, PurchaseUpgradeRequest> _upgradesfilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;
        
        public void Run()
        {
            ref var balanceEntity = ref _balanceFilter.GetEntity(0);
            ref var balance = ref balanceEntity.Get<Balance.Components.Balance>();
            
            foreach (var i in _upgradesfilter)
            {
                PurchaseUpgrade(i);
                ReduceMoney(ref balance, i);
                UpdateEarn(i);
            }

            balanceEntity.Get<MoneyChangedEvent>();
        }

        private void PurchaseUpgrade(int i)
        {
            ref var upgradeEntity = ref _upgradesfilter.GetEntity(i);
            ref var upgrade = ref _upgradesfilter.Get1(i);
            
            upgradeEntity.Del<PurchaseUpgradeRequest>();
            upgradeEntity.Get<PurchasedMarker>();
            
            upgrade.costLabel.text = Literals.GetPurchasedLabel();
            upgrade.button.onClick.RemoveAllListeners();
            upgrade.button.interactable = false;
        }
        
        private void ReduceMoney(ref Balance.Components.Balance balance, int i)
        {
            ref var upgrade = ref _upgradesfilter.Get1(i);
            balance.moneyCount -= upgrade.cost;
            balance.label.text = Literals.GetBalanceLabel(balance.moneyCount);
        }

        private void UpdateEarn(int i)
        {
            ref var entity = ref _upgradesfilter.Get1(i).businessEntity;
            Utils.Calculation.UpdateEarn(ref entity, ref entity.Get<Earn>(), ref entity.Get<Level>());
        }
    }
}