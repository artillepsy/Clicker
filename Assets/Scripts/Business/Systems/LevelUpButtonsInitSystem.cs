using Business.Components;
using Business.Flags;
using Business.Reactive;
using Core.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class LevelUpButtonsInitSystem : IEcsInitSystem
    {
        private readonly EcsFilter<LevelUp, UpgradeContainer, BusinessLevel> _businessesFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;
        
        public void Init()
        {
            foreach (var i in _businessesFilter)
            {
                ref var levelUp = ref _businessesFilter.Get1(i);
                ref var upgradeContainer = ref _businessesFilter.Get2(i);
                ref var businessLevel = ref _businessesFilter.Get3(i);

                foreach (var entity in upgradeContainer.upgradeEntities)
                {
                    var upgrade = entity.Get<Upgrade>();
                    Utils.UpdateUpgradeButtonInteractable(ref upgrade, ref  businessLevel);
                }
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
            _businessesFilter.GetEntity(i).Get<LevelUpRequest>();
            Debug.Log($"level up: {i}");
        }
    }
}