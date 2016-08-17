using System.Collections.Generic;

namespace JSpank.Test.Helpers.Data
{
    public class Db
    {
        public static IEnumerable<int> Ints(int total = 10)
        {
            for (int i = 0; i < 10; i++)
                yield return i;
        }
    }
}
