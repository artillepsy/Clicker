using Balance.Configs;
using Balance.Data;
using Leopotam.Ecs;

namespace Balance.Systems
{
    public class BalanceInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BalanceCanvas _balanceCanvas = null;
        private readonly BalanceConfig _balanceConfig = null;
        
        public void Init()
        {
            var balanceEntity = _world.NewEntity();
            ref var balance = ref balanceEntity.Get<Components.Balance>();

            balance.label = _balanceCanvas.label;
            balance.prefix = _balanceCanvas.prefix;
            balance.postfix = _balanceCanvas.postfix;
            balance.moneyCount = _balanceConfig.startMoneyCount;

            balance.label.text = $"{balance.prefix}{balance.moneyCount}{balance.postfix}";
        }
    }
}