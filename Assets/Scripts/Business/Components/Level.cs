using TMPro;

namespace Business.Components
{
    /// Component which responsible for level
    public struct Level
    {
        /// current business level. Increments after clicking level up button
        public int level;
        /// Displays current level
        public TextMeshProUGUI label;
    }
}