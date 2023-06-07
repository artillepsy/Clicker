using Balance.Configs;
using Balance.SceneData;
using Balance.Systems;
using Business.Configs;
using Business.Reactive;
using Business.SceneData;
using Business.Systems;
using Leopotam.Ecs;
using Saves.SceneData;
using Saves.Systems;
using TimeScale.Configs;
using TimeScale.SceneData;
using TimeScale.Systems;
using UnityEngine;

namespace Management
{
    /// Handles all systems and external references
    public class EcsStartup : MonoBehaviour
    {
        public ApplicationFocusCatcher focusCatcher;    
        [Space]
        public BalanceCanvas balanceCanvas;
        public BalanceConfig balanceConfig;
        [Space]
        public BusinessCanvas businessCanvas;
        public BusinessesConfig businessesConfig;
        [Space]
        public TimeScaleCanvas timeScaleCanvas;
        public TimeScaleConfig timeScaleConfig;

        private EcsWorld _world;
        private EcsSystems _systems;

        /// Initializes world and entities
        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            AddSystems();
            AddOneFrames();
            InitializeSystems();
        }

        /// Initializes systems with order
        private void AddSystems()
        {
            _systems
                .Add(new InitSaveDataLoadSystem())
                .Add(new InitTimeScaleSystem())
                .Add(new InitBalanceSystem())
                .Add(new InitBusinessSystem())
                .Add(new InitLevelUpButtonsSystem())
                .Add(new InitUpgradeButtonsSystem())
                .Add(new BusinessEarnSystem())
                .Add(new UpdateLevelSystem())
                .Add(new UpdateBalanceSystem())
                .Add(new UpgradePurchaseSystem())
                .Add(new UpdateButtonsSystem())
                .Add(new SaveSystem())
                ;
        }

        /// Injects variables to the systems
        private void InitializeSystems()
        {
            _systems
                .Inject(balanceCanvas)
                .Inject(balanceConfig)
                .Inject(businessCanvas)
                .Inject(businessesConfig)
                .Inject(timeScaleConfig)
                .Inject(timeScaleCanvas)
                .Inject(focusCatcher)
                ;
            
            _systems.Init();
        }

        /// Initializes oneFrame events and requests
        private void AddOneFrames()
        {
            _systems
                .OneFrame<EarnedMoneyEvent>()
                .OneFrame<LevelUpRequest>()
                .OneFrame<PurchaseUpgradeRequest>()
                .OneFrame<MoneyChangedEvent>()
                ;
        }

        /// Updates each system
        private void Update()
        {
            _systems.Run();
        }

        /// Clears systems and world
        private void OnDestroy()
        {
            _systems?.Destroy();
            _systems = null;
            _world?.Destroy();
            _world = null;
        }
    }
}