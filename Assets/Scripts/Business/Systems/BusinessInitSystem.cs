using Leopotam.Ecs;
using UnityEngine;

namespace Business.Systems
{
    public class BusinessInitSystem : IEcsInitSystem
    {
        private readonly BusinessConfig _businessConfig = null;
        
        public void Init()
        {
                        
        }

        private void SpawnBusinessInstance(Business business)
        {
            var instance = Object.Instantiate(_businessConfig.businessDisplayPrefab);
            
        }
    }
}