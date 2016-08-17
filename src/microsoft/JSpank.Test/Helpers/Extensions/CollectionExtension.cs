using System;
using System.Collections.Generic;
using System.Linq;

namespace JSpank.Test.Helpers.Extensions
{
    public static class CollectionExtension
    {
        public static IEnumerable<T> GetToRandon<T>(this IEnumerable<T> collection, int take = int.MaxValue)
        {
            return collection.OrderBy(a => Guid.NewGuid()).Take(take);
        }

        public static T GetToRandonOne<T>(this IEnumerable<T> collection)
        {
            return collection.GetToRandon<T>().First();
        }
    }
}
