using TMPro;

namespace Balance.Components
{
    /// Contains info about player's money
    public struct Balance
    {
        /// Displays current money count
        public TextMeshProUGUI label;
        public ulong moneyCount;
    }
}