using System;
using UnityEngine;

namespace Saves.SceneData
{
    /// Checks when application is unfocused.
    /// It helps checking close and terminate processes
    public class ApplicationFocusCatcher : MonoBehaviour
    {
        public event Action OnApplicationUnfocused;
        
        /// Fires event when application is unfocused
        private void OnApplicationFocus(bool hasFocus)
        {
            if (hasFocus) return;
            OnApplicationUnfocused?.Invoke();
        }
    }
}