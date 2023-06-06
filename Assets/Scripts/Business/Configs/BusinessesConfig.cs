using System.Collections.Generic;
using Business.SceneData;
using UnityEngine;

namespace Business.Configs
{
    [CreateAssetMenu]
    public class BusinessesConfig : ScriptableObject
    {
        public BusinessDisplay businessDisplayPrefab;
        public List<BusinessConfig> businesses;
    }
}