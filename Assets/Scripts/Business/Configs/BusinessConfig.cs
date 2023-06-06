using System.Collections.Generic;
using Business.SceneData;
using UnityEngine;

namespace Business
{
    [CreateAssetMenu]
    public class BusinessConfig : ScriptableObject
    {
        public BusinessDisplay businessDisplayPrefab;
        public List<Business> businesses;
    }
}