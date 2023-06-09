﻿using Business.Components;
using Business.Flags;
using Leopotam.Ecs;
using Saves.Components;
using Saves.SceneData;
using Saves.Utils;
using Utils;

namespace Saves.Systems
{
    /// Saves game state if app is in unfocus
    public class SaveSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly ApplicationFocusCatcher _focusCatcher = null;
        private readonly EcsFilter<GameStateSaveData> _saveDataFilter = null;
        private readonly EcsFilter<TimeScale.Components.TimeScale> _timeScaleFilter = null;
        private readonly EcsFilter<Balance.Components.Balance> _balanceFilter = null;
        private readonly EcsFilter<Level, EarnTimer, UpgradesContainer, Index> _businessesfilter = null;
        
        /// Subscribes on unfocus event at the start
        public void Init()
        {
            _focusCatcher.OnApplicationUnfocused += OnUnfocused;
        }

        /// Unsubscribes from unfocus event at the destroy
        public void Destroy()
        {
            _focusCatcher.OnApplicationUnfocused -= OnUnfocused;
        }
        
        /// Gets all data from needed components and saves it to file
        private void OnUnfocused()
        {
            ref var data = ref _saveDataFilter.Get1(0);
            var timeScale = _timeScaleFilter.Get1(0);
            var balance = _balanceFilter.Get1(0);

            data.moneyCount = balance.moneyCount;
            data.timeScale = timeScale.value;
            
            foreach (var i in _businessesfilter)
            {
                var businessLevel = _businessesfilter.Get1(i);
                var earnTimer = _businessesfilter.Get2(i);
                var upgradesContainer = _businessesfilter.Get3(i);
                var businessIndex = _businessesfilter.Get4(i);

                ref var business = ref data.businesses.Find(b => businessIndex.index == b.index);
                business.level = businessLevel.level;
                business.earnCurrentTime = earnTimer.currentTime;

                var upgradesLength = business.upgrades.Length;

                for (int j = 0; j < upgradesLength; j++)
                {
                    var upgradeComponent = upgradesContainer.upgradeEntities[j].Get<UpgradeButton>();
                    ref var upgradeSaveData = ref business.upgrades.Find(u => u.index == upgradeComponent.index);
                    upgradeSaveData.purchased = upgradesContainer.upgradeEntities[j].Has<PurchasedMarker>();
                }
            }
            SaveLoadUtils.Save(data, Literals.SaveFileName);
        }
    }
}