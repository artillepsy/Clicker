using System;
using UnityEngine;

namespace Saves.SceneData
{
    public class ApplicationFocusCatcher : MonoBehaviour
    {
        public event Action OnApplicationUnfocused;
        
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;
            OnApplicationUnfocused?.Invoke();
        }
    }
}