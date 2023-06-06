using Balance.Configs;
using Balance.Data;
using Balance.Systems;
using Business;
using Business.SceneData;
using Business.Systems;
using Leopotam.Ecs;
using UnityEngine;

namespace Core
{
    public class EcsStartup : MonoBehaviour
    {
        public BalanceCanvas balanceCanvas;
        public BalanceConfig balanceConfig;
        
        public BusinessCanvas businessCanvas;
        public BusinessesConfig businessesConfig;
        
        private EcsWorld _world;
        private EcsSystems _systems;

        private void Start()
        {
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
                .Add(new BusinessEarnSystem())
                .Add(new UpdateBalanceSystem())
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