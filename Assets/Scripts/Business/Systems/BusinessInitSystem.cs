using Business.Components;
using Business.Configs;
using Business.Flags;
using Business.SceneData;
using Core.Constants;
using Core.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class BusinessInitSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessesConfig _businessesConfig = null;
        private readonly BusinessCanvas _businessCanvas = null;
        
        public void Init()
        {
            for (int i = 0; i < _businessesConfig.businesses.Count; i++)
            {
                SpawnBusinessInstance(_businessesConfig.businesses[i], i+1, _businessesConfig.businesses.Count);
            }
        }

        private void SpawnBusinessInstance(BusinessConfig businessConfig, int businessCurrentIndex, int businessAmount)
        {
            var display = Object.Instantiate(_businessesConfig.businessDisplayPrefab, _businessCanvas.businessParent);
            var entity = _world.NewEntity();
            ref var upgradeContainer = ref entity.Get<UpgradeContainer>();

            upgradeContainer.upgradeEntity1 = InitializeUpgrade(ref entity, display.upgrade1Display, businessConfig.upgrade1Config);
            upgradeContainer.upgradeEntity2 = InitializeUpgrade(ref entity, display.upgrade2Display, businessConfig.upgrade2Config);
            
            Utils.UpdateUpgradeButtonInteractable(
                ref upgradeContainer.upgradeEntity1.Get<Upgrade>(), ref entity.Get<BusinessLevel>());
            
            Utils.UpdateUpgradeButtonInteractable(
                ref upgradeContainer.upgradeEntity2.Get<Upgrade>(), ref entity.Get<BusinessLevel>());
            
            InitializeBusinessDisplay(entity, display, businessConfig, businessCurrentIndex, businessAmount);
        }

        private void InitializeBusinessDisplay(EcsEntity entity, BusinessDisplay display, BusinessConfig businessConfig, int currentIndex, int amount)
        {
            ref var earnProgressBar = ref entity.Get<EarnProgressBar>();
            ref var businessLevel = ref entity.Get<BusinessLevel>();
            ref var businessIndex = ref entity.Get<BusinessIndex>();
            ref var earnTimer = ref entity.Get<EarnTimer>();
            ref var earn = ref entity.Get<Earn>();
            ref var levelUp = ref entity.Get<LevelUp>();
            
            businessLevel.label = display.levelCounterLabel;
            businessLevel.level = businessConfig.startLevel;
            businessLevel.label.text = businessLevel.level.ToString();

            businessIndex.label = display.businessIndexLabel;
            businessIndex.label.text = $"{currentIndex}/{amount}";

            display.nameLabel.text = businessConfig.name;

            earnTimer.currentTime = 0f;
            earnTimer.earnTime = businessConfig.earnTime;

            earn.earnLabel = display.earnCounterLabel;
            earn.startEarn = businessConfig.startEarnCount;
            Utils.UpdateEarn(ref entity, ref earn, ref businessLevel);

            levelUp.levelCostLabel = display.levelUpCostLabel;
            levelUp.levelUpButton = display.levelUpButton;
            levelUp.startCost = businessConfig.levelUpCost;
            Utils.UpdateLevelCost(ref levelUp, businessLevel.level);

            earnProgressBar.fillImage = display.progressBarImage;
            earnProgressBar.fillImage.fillAmount = earnTimer.currentTime / earnTimer.earnTime;


            if (businessConfig.startLevel > 0)
            {
                entity.Get<PurchasedMarker>();
            }
        }

        private EcsEntity InitializeUpgrade(ref EcsEntity entity, UpgradeDisplay display, UpgradeConfig upgradeConfig)
        {
            var upgradeEntity = _world.NewEntity();
            ref var upgrade = ref upgradeEntity.Get<Upgrade>();

            display.nameLabel.text = upgradeConfig.name;
            upgrade.businessEntity = entity;
            
            upgrade.earnMultiplierLabel = display.earnLabel;
            upgrade.upgradeButton = display.upgradeButton;
            upgrade.costLabel = display.costLabel;
            
            upgrade.cost = upgradeConfig.buyCost;
            upgrade.costLabel.text = Literals.GetCostLabel(upgrade.cost);

            upgrade.earnMultiplier = upgradeConfig.earnMultiplier;
            upgrade.earnMultiplierLabel.text = Literals.GetEarnMultiplierLabel(upgrade.earnMultiplier);

            return upgradeEntity;
        }
    }
}