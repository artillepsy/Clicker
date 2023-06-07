using System.Linq;
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
            var data = SaveLoadUtils.Load<GameStateSaveData>(out var success, Literals.SaveFileName);

            if (!success)
            {
                InitSaveData(ref data);
            }
            var saveEntity = _world.NewEntity();
            saveEntity.Get<GameStateSaveData>();
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

                businessData.level = businessConfig.startLevel;
                businessData.earnFillAmount = 0f;
                businessData.upgradesPurchases = Enumerable.Repeat(false, upgradesLength).ToArray();
            }
        }
    }
}