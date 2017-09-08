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
        
        private static Dictionary<TypeMap, List<ConcreteTypeMap>> mapper { get; } = new Dictionary<TypeMap, List<ConcreteTypeMap>>();

        public static T Resolve<T>(string mapName = "")
        {
            Type type = typeof(T);
            var map = new TypeMap(type);

            if (mapper.ContainsKey(map))
            {
                ConcreteTypeMap resolvedType = Helper.GetResolvedType(mapper[map], mapName);
                object[] parameters = Helper.GetParameters(resolvedType);
                return (T)ObjectActivator.CreateInstance(resolvedType.Type, parameters);
            }
            return default(T);
        }
        
        internal static void Register<Tp, Tc>(string name)
        {
            var map = new TypeMap(typeof(Tp));
            var concreteMap = new ConcreteTypeMap(typeof(Tc), name);
            if (mapper.ContainsKey(map))
            {
                mapper[map].Add(concreteMap);
            }
            mapper[map] = new List<ConcreteTypeMap> { concreteMap };
        }
        internal static bool Contains(TypeMap map)
        {
            return mapper.ContainsKey(map);
        }
        internal static List<ConcreteTypeMap> GetValue(TypeMap map)
        {
            return mapper[map];
        }
    }
    
}
