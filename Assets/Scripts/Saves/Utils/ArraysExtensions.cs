using System;

namespace Saves.Utils
{
    /// Helper methods for arrays
    public static class ArraysExtensions
    {
        /// Finds array element and returns it's reference
        public static ref T Find<T>(this T[] source, Func<T, bool> predicate)
        {
            for (var i = 0; i < source.Length; i++)
            {
                if (!predicate(source[i])) continue;
                return ref source[i];
            }
            throw new Exception("here is no match for any array element");
        }
    }
}