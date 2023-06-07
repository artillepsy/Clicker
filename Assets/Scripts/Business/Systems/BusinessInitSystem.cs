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
            var businessCount = _businessesConfig.businesses.Length;
            
            for (int i = 0; i < businessCount; i++)
            {
                SpawnBusinessInstance(_businessesConfig.businesses[i], i+1, businessCount);
            }
        }

        private void SpawnBusinessInstance(BusinessConfig businessConfig, int currentBusinessIndex, int businessCount)
        {
            var display = Object.Instantiate(_businessesConfig.businessDisplayPrefab, _businessCanvas.businessParent);
            var entity = _world.NewEntity();
            
            SpawnUpgrades(ref entity, businessConfig, display);
            InitializeBusinessDisplay(ref entity, display, businessConfig, currentBusinessIndex, businessCount);
        }

        private void SpawnUpgrades(ref EcsEntity entity, BusinessConfig businessConfig,
            BusinessDisplay display)
        {
            var upgradesCount = businessConfig.upgradeConfigs.Length;
            ref var upgradeContainer = ref entity.Get<UpgradeContainer>();
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
                    = InitializeUpgrade(ref entity, upgradeDisplay, upgradeConfig);

                Utils.UpdateUpgradeButtonInteractable(
                    ref upgradeContainer.upgradeEntities[i].Get<Upgrade>(), ref entity.Get<BusinessLevel>());
            }
        }

        private void InitializeBusinessDisplay(ref EcsEntity entity, BusinessDisplay display, 
            BusinessConfig businessConfig, int currentIndex, int amount)
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