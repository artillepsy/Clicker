﻿using Business.Components;
using Business.Configs;
using Business.Flags;
using Business.SceneData;
using Leopotam.Ecs;
using Saves.Components;
using UnityEngine;
using Utils;

namespace Business.Systems
{
    /// Creates business and upgrades entities and gives values to them. Uses saved info for initializing
    public class InitBusinessSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessesConfig _businessesConfig = null;
        private readonly BusinessCanvas _businessCanvas = null;
        private readonly EcsFilter<GameStateSaveData> _saveDataFilter;
        
        public void Init()
        {
            var businessCount = _businessesConfig.businesses.Length;
            var gameStateData = _saveDataFilter.Get1(1);
            
            for (int i = 0; i < businessCount; i++)
            {
                SpawnBusinessInstance(_businessesConfig.businesses[i], gameStateData, i, businessCount);
            }
        }
        
        /// Instatiates business instance and it's children on scene
        private void SpawnBusinessInstance(BusinessConfig businessConfig, GameStateSaveData data,
            int i, int businessCount)
        {
            var display = Object.Instantiate(_businessesConfig.businessDisplayPrefab, _businessCanvas.businessParent);
            var entity = _world.NewEntity();
            
            SpawnUpgrades(ref entity, _businessesConfig.businesses[i], data.businesses[i].upgrades, display);
            InitializeBusinessEntity(ref entity, display, data.businesses[i], businessConfig, i, businessCount);
        }

        /// Initializes business components by values from config and save
        private void InitializeBusinessEntity(ref EcsEntity entity, BusinessDisplay display, BusinessSaveData data, 
            BusinessConfig businessConfig, int currentIndex, int amount)
        {
            ref var earnProgressBar = ref entity.Get<EarnProgressBar>();
            ref var businessLevel = ref entity.Get<Level>();
            ref var businessIndex = ref entity.Get<Index>();
            ref var earnTimer = ref entity.Get<EarnTimer>();
            ref var earn = ref entity.Get<Earn>();
            ref var levelUp = ref entity.Get<LevelUpButton>();

            businessIndex.index = currentIndex;
            businessIndex.label = display.businessIndexLabel;
            businessIndex.label.text = $"{currentIndex + 1}/{amount}";
            
            businessLevel.label = display.levelCounterLabel;
            businessLevel.level = data.level;
            businessLevel.label.text = businessLevel.level.ToString();
           
            display.nameLabel.text = businessConfig.name;

            earnTimer.currentTime = data.earnCurrentTime;
            earnTimer.earnTime = businessConfig.earnTime;

            earn.earnLabel = display.earnCounterLabel;
            earn.startEarn = businessConfig.startEarnCount;
            Utils.Calculation.UpdateEarn(ref entity, ref earn, ref businessLevel);

            levelUp.levelCostLabel = display.levelUpCostLabel;
            levelUp.button = display.levelUpButton;
            levelUp.startCost = businessConfig.levelUpCost;
            Utils.Calculation.UpdateLevelCost(ref levelUp, businessLevel.level);

            earnProgressBar.fillImage = display.progressBarImage;
            earnProgressBar.fillImage.fillAmount = earnTimer.currentTime / earnTimer.earnTime;
            
            if (data.level > 0)
            {
                entity.Get<PurchasedMarker>();
            }
        }
        
        /// Initializes upgrade components by values from config and save
        private EcsEntity InitializeUpgradeEntity(ref EcsEntity entity, UpgradeDisplay display, UpgradeSaveData data, 
            UpgradeConfig upgradeConfig, int index)
        {
            var upgradeEntity = _world.NewEntity();
            ref var upgrade = ref upgradeEntity.Get<UpgradeButton>();
           
            display.nameLabel.text = upgradeConfig.name;
            
            upgrade.index = index;
            upgrade.businessEntity = entity;
            
            upgrade.earnMultiplierLabel = display.earnLabel;
            upgrade.button = display.upgradeButton;
            upgrade.costLabel = display.costLabel;
            
            upgrade.cost = upgradeConfig.buyCost;

            upgrade.earnMultiplier = upgradeConfig.earnMultiplier;
            upgrade.earnMultiplierLabel.text = Literals.GetEarnMultiplierLabel(upgrade.earnMultiplier);
            
            if (data.purchased)
            {
                upgradeEntity.Get<PurchasedMarker>();
                upgrade.button.interactable = false;
                upgrade.costLabel.text = Literals.GetPurchasedLabel();
            }
            else
            {
                upgrade.costLabel.text = Literals.GetCostLabel(upgrade.cost);
            }
            return upgradeEntity;
        }
        
        /// Instantiates upgrades related to current entity
        private void SpawnUpgrades(ref EcsEntity entity, BusinessConfig businessConfig, UpgradeSaveData[] upgradesData,
            BusinessDisplay display)
        {
            var upgradesCount = businessConfig.upgradeConfigs.Length;
            ref var upgradeContainer = ref entity.Get<UpgradesContainer>();
            upgradeContainer.upgradeEntities = new EcsEntity[upgradesCount];
            
            foreach (Transform child in display.upgradesParent)
            {
                Object.Destroy(child.gameObject);
            }
            for (int i = 0; i < upgradesCount; i++)
            {
                var upgradeConfig = businessConfig.upgradeConfigs[i];
                var upgradeDisplay = Object.Instantiate(_businessesConfig.upgradeDisplayPrefab, display.upgradesParent);

                upgradeContainer.upgradeEntities[i]
                    = InitializeUpgradeEntity(ref entity, upgradeDisplay, upgradesData[i], upgradeConfig, i);
            }
        }
    }
}