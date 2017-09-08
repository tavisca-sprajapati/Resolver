using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    internal sealed class MapProvider
    {
        internal static Dictionary<TypeMap, List<ConcreteTypeMap>> Map { get; } = new Dictionary<TypeMap, List<ConcreteTypeMap>>();
    }
}
