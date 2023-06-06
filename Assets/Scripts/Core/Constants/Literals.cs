namespace Core.Constants
{
    public static class Literals
    {
        public static string GetCostLabel(int value) => $"Cost: {value}$";
        public static string GetEarnLabel(int value) => $"Earn: {value}$";
        public static string GetBalanceLabel(int value) => $"Balance: {value}$";
        public static string GetEarnMultiplierLabel(int value) => $"Earn: {value}%";
    }
}