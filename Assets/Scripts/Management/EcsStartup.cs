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
using UnityEngine;

namespace Management
{
    public class EcsStartup : MonoBehaviour
    {
        public float timeScale = 3f;

        public ApplicationFocusCatcher focusCatcher;        
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
                .Add(new InitSaveDataSystem())
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

        private void InitializeSystems()
        {
            _systems
                .Inject(balanceCanvas)
                .Inject(balanceConfig)
                .Inject(businessCanvas)
                .Inject(businessesConfig)
                .Inject(focusCatcher)
                ;
            
            _systems.Init();
        }

        private void AddOneFrames()
        {
            _systems
                .OneFrame<EarnedMoneyEvent>()
                .OneFrame<LevelUpRequest>()
                .OneFrame<PurchaseUpgradeRequest>()
                .OneFrame<MoneyChangedEvent>()
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