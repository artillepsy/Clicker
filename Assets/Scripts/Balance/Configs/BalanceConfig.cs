using UnityEngine;

namespace Balance.Configs
{
    /// Config for manual start value set
    [CreateAssetMenu]
    public class BalanceConfig : ScriptableObject
    {
        /// This value sets to player's balance only at start of the game 
        public int startMoneyCount = 0;
    }
}
