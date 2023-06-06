using Balance.Configs;
using Balance.SceneData;
using Balance.Systems;
using Business.Configs;
using Business.Reactive;
using Business.SceneData;
using Business.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Management
{
    public class EcsStartup : MonoBehaviour
    {
        public float timeScale = 3f;
        
        public BalanceCanvas balanceCanvas;
        public BalanceConfig balanceConfig;
        
        public BusinessCanvas businessCanvas;
        public BusinessesConfig businessesConfig;
        
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
            Time.timeScale = timeScale;
            
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            AddSystems();
            AddOneFrames();
            InitializeSystems();
        }

        private void AddSystems()
        {
            _systems
                .Add(new BalanceInitSystem())
                .Add(new BusinessInitSystem())
                .Add(new LevelUpButtonsInitSystem())
                .Add(new UpgradeButtonsInitSystem())
                .Add(new BusinessEarnSystem())
                .Add(new UpdateLevelSystem())
                .Add(new UpdateBalanceSystem())
                .Add(new UpgradePurchaseSystem())
                ;
        }

        private void InitializeSystems()
        {
            _systems
                .Inject(balanceCanvas)
                .Inject(balanceConfig)
                .Inject(businessCanvas)
                .Inject(businessesConfig)
                ;
            
            _systems.Init();
        }

        private void AddOneFrames()
        {
            _systems
                .OneFrame<EarnedMoneyEvent>()
                .OneFrame<LevelUpRequest>()
                .OneFrame<PurchaseUpgradeRequest>()
                ;
        }

        private void Update()
        {
            _systems.Run();
        }

        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;
            _world?.Destroy();
            _world = null;
        }
    }
}