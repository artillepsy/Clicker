using Business.Components;
using Business.Flags;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class AssignListenerToLevelUpButtons : IEcsInitSystem
    {
        private readonly EcsFilter<LevelUp> _businessesFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;
        
        public void Init()
        {
            foreach (var i in _businessesFilter)
            {
                ref var levelUp = ref _businessesFilter.Get1(i);
                levelUp.levelUpButton.onClick.AddListener(delegate { OnClickLevelUp(i); });
            }
        }

        private void OnClickLevelUp(int i)
        {
            ref var balance = ref _balanceFilter.Get1(0);
            ref var levelUp = ref _businessesFilter.Get1(i);

            if (levelUp.cost > balance.moneyCount)
            {
                return;
            }
            _businessesFilter.GetEntity(i).Get<LevelUpEvent>();
            Debug.Log($"level up: {i}");
        }
    }
}