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

        public static IEnumerable<T> Shuffled<T>(this IEnumerable<T> source)
        {
            var list = new List<T>();
            foreach (var item in source)
            {
                var i = _random.Next(list.Count + 1);
                if (i == list.Count)
                {
                    list.Add(item);
                }
                else
                {
                    var temp = list[i];
                    list[i] = item;
                    list.Add(temp);
                }
            }
            return list;
        }
    }
}
