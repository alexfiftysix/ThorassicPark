using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace Utilities.Extensions
{
    public static class IEnumerableExtensions
    {
        private static Random _random = new Random();

        public static T RandomChoice<T>(this IEnumerable<T> collection)
        {
            var array = collection.ToArray();
            if (array.Length == 0)
            {
                return default;
            }

            var index = _random.Next(1, array.Length) - 1;
            return array[index];
        }
    }
}
