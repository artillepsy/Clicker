﻿using TMPro;

namespace Business.Components
{
    /// Component responsible for earn money 
    public struct Earn
    {
        /// label which displays current earn value
        public TextMeshProUGUI earnLabel;
        /// Current earn amount
        public int earn;
        /// Earn amount from config. Doesn't change
        public int startEarn;
    }
}