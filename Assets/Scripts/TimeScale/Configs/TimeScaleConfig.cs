using UnityEngine;

namespace TimeScale.Configs
{
    /// Config which contains start time scale
    [CreateAssetMenu]
    public class TimeScaleConfig : ScriptableObject
    {
        public int timeScale = 1;
    }
}