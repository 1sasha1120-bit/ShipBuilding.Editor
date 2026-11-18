using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces.Extensions
{
    public static class IdentifiablesMethods
    {

        public static int NextId(this IEnumerable<IIdentifiable> objects)
        {
            return objects.Any() ? objects.Select(e => e.Id).Max() + 1 : 1;
        }
    }
}
