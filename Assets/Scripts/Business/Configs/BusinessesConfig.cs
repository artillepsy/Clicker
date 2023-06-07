using Business.SceneData;
using UnityEngine;

namespace Business.Configs
{
    [CreateAssetMenu]
    public class BusinessesConfig : ScriptableObject
    {
        public BusinessDisplay businessDisplayPrefab;
        public UpgradeDisplay upgradeDisplayPrefab;
        [Space]
        public BusinessConfig[] businesses;
    }
}