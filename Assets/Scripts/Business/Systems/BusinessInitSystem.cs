using Business.Components;
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
            
            InitializeBusinessDisplay(entity, display, businessConfig, businessCurrentIndex, businessAmount);
            upgradeContainer.upgrade1 = InitializeBusinessUpgrade(display.firstUpgradeDisplay, businessConfig.firstUpgradeConfig);
            upgradeContainer.upgrade2 = InitializeBusinessUpgrade(display.secondUpgradeDisplay, businessConfig.secondUpgradeConfig);
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
            earn.earnLabel.text = $"{earn.earn.ToString()}$";

            levelUp.levelCostLabel = display.levelUpCostLabel;
            levelUp.startCost = businessConfig.levelUpCost;
            levelUp.levelCostLabel.text = $"{levelUp.cost.ToString()}$";
            levelUp.levelUpButton = display.levelUpButton;
            
            earnProgressBar.fillImage = display.progressBarImage;
            earnProgressBar.fillImage.fillAmount = earnTimer.currentTime / earnTimer.earnTime;

            earn.earn = Utils.GetEarn(businessLevel.level, earn.startEarn, x, yy);
            levelUp.cost = Utils.GetLevelCost(businessLevel.level, levelUp.startCost);
            
            if (businessConfig.startLevel > 0)
            {
                entity.Get<PurchasedMarker>();
            }
        }

        private EcsEntity InitializeBusinessUpgrade(UpgradeDisplay display, UpgradeConfig upgradeConfig)
        {
            var upgradeEntity = _world.NewEntity();
            ref var upgrade = ref upgradeEntity.Get<Upgrade>();

            display.nameLabel.text = upgradeConfig.name;
            
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