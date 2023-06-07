using Balance.SceneData;
using Leopotam.Ecs;
using Saves.Components;
using Utils;

namespace Balance.Systems
{
    /// Creates balance entity and connect it with scene. Also sets loaded money count from file 
    public class InitBalanceSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BalanceCanvas _balanceCanvas = null;
        private readonly EcsFilter<GameStateSaveData> _saveDataFilter = null;
        
        public void Init()
        {
            var balanceEntity = _world.NewEntity();
            var gameStateData = _saveDataFilter.Get1(0);
            ref var balance = ref balanceEntity.Get<Components.Balance>();

            balance.label = _balanceCanvas.label;
            balance.moneyCount = gameStateData.moneyCount;
            balance.label.text = Literals.GetBalanceLabel(balance.moneyCount);
        }
    }
}