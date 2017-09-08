using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    public sealed class ObjectBuilder
    {
        public static T Resolve<T>(string mapName = "")
        {
            Type type = typeof(T);
            var map = new TypeMap(type);

            if (MapProvider.Map.ContainsKey(map))
            {
                ConcreteTypeMap resolvedType = Helper.GetResolvedType(MapProvider.Map[map], mapName);
                object[] parameters = Helper.GetParameters(resolvedType);
                return (T)ObjectActivator.CreateInstance(resolvedType.Type, parameters);
            }
            return default(T);
        }
        
        internal static void Register<Tp, Tc>(string name)
        {
            var map = new TypeMap(typeof(Tp));
            var concreteMap = new ConcreteTypeMap(typeof(Tc), name);
            if (MapProvider.Map.ContainsKey(map))
            {
                MapProvider.Map[map].Add(concreteMap);
            }
            else
            {
                MapProvider.Map[map] = new List<ConcreteTypeMap> { concreteMap };
            }
        }
    }
    
}
