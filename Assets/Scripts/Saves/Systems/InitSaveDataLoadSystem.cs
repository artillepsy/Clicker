using Balance.Configs;
using Business.Configs;
using Leopotam.Ecs;
using Saves.Components;
using Saves.Utils;
using TimeScale.Configs;
using Utils;

namespace Saves.Systems
{
    /// Loads gameState data at start of the game
    public class InitSaveDataLoadSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessesConfig _businessesConfig = null;
        private readonly BalanceConfig _balanceConfig = null;
        private readonly TimeScaleConfig _timeScaleConfig = null;

        /// Creates new Save Data entity and gets info from file
        public void Init()
        {
            var saveEntity = _world.NewEntity();
            ref var data = ref saveEntity.Get<GameStateSaveData>();
            data = SaveLoadUtils.Load<GameStateSaveData>(out var success, Literals.SaveFileName);

            if (!success)
            {
                InitSaveData(ref data);
            }
        }

        /// Fills data fields by values from config if it's empty
        private void InitSaveData(ref GameStateSaveData data)
        {
            var businessCount = _businessesConfig.businesses.Length;
            
            data.businesses = new BusinessSaveData[businessCount];
            data.moneyCount = _balanceConfig.startMoneyCount;
            data.timeScale = _timeScaleConfig.timeScale;
            
            for (int i = 0; i < businessCount; i++)
            {
                ref var businessData = ref data.businesses[i];
                var businessConfig = _businessesConfig.businesses[i];
                var upgradesLength = businessConfig.upgradeConfigs.Length;

                businessData.index = i;
                businessData.level = businessConfig.startLevel;
                businessData.earnCurrentTime = 0f;
                businessData.upgrades = new UpgradeSaveData[upgradesLength];

                for (int j = 0; j < upgradesLength; j++)
                {
                    businessData.upgrades[j].index = j;
                    businessData.upgrades[j].purchased = false;
                }
            }
        }
    }
}