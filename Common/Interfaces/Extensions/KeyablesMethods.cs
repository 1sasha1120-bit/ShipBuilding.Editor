using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Extensions
{
    public static class KeyablesMethods
    {
        public static string ToKeyList(this IEnumerable<IKeyable> objects, string prompt)
        {
            return $"{prompt}:\n\t{string.Join("\n\t", objects.Select(e => e.Key))}";
        }
        public static string ToKeyLine(this IEnumerable<IKeyable> objects, string prompt)
        {
            return $"{prompt}:\t{string.Join(", ", objects.Select(e => e.Key))}";
        }

    }
}
