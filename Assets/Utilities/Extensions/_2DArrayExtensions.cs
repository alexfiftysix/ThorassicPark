using System.Collections.Generic;
using System.Linq;
using UnityEditor;

namespace Utilities.Extensions
{
    public static class _2DArrayExtensions
    {
        public static T[] Flatten<T>(this T[,] array)
        {
            return array.Cast<T>().ToArray();
        }
    }
}