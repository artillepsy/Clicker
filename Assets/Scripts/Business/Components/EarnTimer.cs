namespace Business.Components
{
    /// Component which contains info about earn stage
    public struct EarnTimer
    {
        /// Time in seconds which need to update money info. Sets from config
        public float earnTime;
        /// Current earn stage in seconds
        public float currentTime;
    }
}