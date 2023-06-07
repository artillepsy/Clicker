using Balance.SceneData;
using Leopotam.Ecs;
using Saves.Components;

namespace Balance.Systems
{
    public class InitBalanceSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BalanceCanvas _balanceCanvas = null;
        private readonly EcsFilter<GameStateSaveData> _saveDataFilter = null;
        
        public void Init()
        {
            var balanceEntity = _world.NewEntity();
            ref var balance = ref balanceEntity.Get<Components.Balance>();
            var data = _saveDataFilter.Get1(0);
            
            balance.label = _balanceCanvas.label;
            balance.moneyCount = data.moneyCount;

            balance.label.text = $"Balance: {balance.moneyCount}$";
        }
    }
}