using Balance.Configs;
using Business.Configs;
using Constants;
using Leopotam.Ecs;
using Saves.Components;
using Saves.Utils;

namespace Saves.Systems
{
    public class InitSaveDataSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world = null;
        private readonly BusinessesConfig _businessesConfig = null;
        private readonly BalanceConfig _balanceConfig = null;

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

        private void InitSaveData(ref GameStateSaveData data)
        {
            var businessCount = _businessesConfig.businesses.Length;
            
            data.businesses = new BusinessSaveData[businessCount];
            data.moneyCount = _balanceConfig.startMoneyCount;
            
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