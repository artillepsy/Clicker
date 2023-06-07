using System;

namespace Saves.Components
{
    [Serializable]
    public struct GameStateSaveData
    {
        public int moneyCount;
        public BusinessSaveData[] businesses;
    }
}