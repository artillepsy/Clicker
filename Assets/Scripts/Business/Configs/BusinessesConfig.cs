using System.Collections.Generic;
using Business.SceneData;
using UnityEngine;

namespace Business
{
    [CreateAssetMenu]
    public class BusinessesConfig : ScriptableObject
    {
        public BusinessDisplay businessDisplayPrefab;
        public List<BusinessConfig> businesses;
    }
}