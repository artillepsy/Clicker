﻿namespace Utils
{
    /// Container for constants and methods which work with strings 
    public static class Literals
    {
        public const string SaveFileName = "Saves";
        
        public static string GetCostLabel(int value) => $"Cost: {value}$";
        public static string GetPriceLabel(int value) => $"{value}$";
        public static string GetPurchasedLabel() => "Purchased";
        public static string GetBalanceLabel(int value) => $"Balance: {value}$";
        public static string GetEarnMultiplierLabel(int value) => $"Earn: {value}%";
        public static string GetSpeedLabel(int value) => $"X{value}";
    }
}