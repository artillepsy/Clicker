using Business.SceneData;
using UnityEngine;

namespace Business.Configs
{
    /// Container for all businesses and their upgrades. Also contains prefabs to spawn
    [CreateAssetMenu]
    public class BusinessesConfig : ScriptableObject
    {
        public BusinessDisplay businessDisplayPrefab;
        public UpgradeDisplay upgradeDisplayPrefab;
        [Space]
        public BusinessConfig[] businesses;
    }
}