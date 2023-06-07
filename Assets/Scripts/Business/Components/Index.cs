using TMPro;

namespace Business.Components
{
    /// Component which gives entity an index 
    public struct Index
    {
        /// Displays current index info
        public TextMeshProUGUI label;
        /// Unique number for entity to easy search it in arrays 
        public int index;
    }
}