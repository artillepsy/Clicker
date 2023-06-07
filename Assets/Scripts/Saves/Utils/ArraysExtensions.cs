using System;

namespace Saves.Utils
{
    public static class ArraysExtensions
    {
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