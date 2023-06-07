using TMPro;
using UnityEngine.UI;

namespace TimeScale.Components
{
    /// Component that responsible for game time scale  
    public struct TimeScale
    {
        /// Displays current time speed
        public TextMeshProUGUI speedLabel;
        /// Reduces time speed on click
        public Button reduceButon;
        /// Increments time speed on click
        public Button increaseButon;
        /// current speed value
        public int value;
    }
}