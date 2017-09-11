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
        public static T Resolve<T>(string mapName)
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
        public static T Resolve<T>()
        {
            return Resolve<T>("");
        }
    }
    
}
