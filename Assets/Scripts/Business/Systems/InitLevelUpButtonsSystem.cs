using Business.Components;
using Business.Reactive;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class InitLevelUpButtonsSystem : IEcsInitSystem
    {
        private readonly EcsFilter<LevelUpButton, UpgradesContainer, BusinessLevel> _businessesFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;
        
        public void Init()
        {
            foreach (var i in _businessesFilter)
            {
                ref var levelUp = ref _businessesFilter.Get1(i);
                levelUp.button.onClick.AddListener(delegate { OnClickLevelUp(i); });
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
            _businessesFilter.GetEntity(i).Get<LevelUpRequest>();
            Debug.Log($"level up: {i}");
        }
    }
}