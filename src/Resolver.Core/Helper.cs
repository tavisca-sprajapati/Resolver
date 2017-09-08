using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    internal sealed class Helper
    {
        internal static ConcreteTypeMap GetResolvedType(List<ConcreteTypeMap> types, string mapName)
        {
            if (string.IsNullOrEmpty(mapName))
            {
                return types[0];
            }
            return types.FirstOrDefault(t => string.Equals(t.MapName, mapName));
        }
        internal static object[] GetParameters(ConcreteTypeMap typeMap)
        {
            if (typeMap.Parameters == null || typeMap.Parameters.Length == 0)
                return new object[0];

            object[] parameters = new object[typeMap.Parameters.Length];

            for (int index = 0; index < parameters.Length; index++)
            {
                parameters[index] = InitializeParameter(typeMap.Parameters[index]);
            }
            return parameters;
        }
        private static object InitializeParameter(Parameter parameter)
        {
            var map = new TypeMap(parameter.Type);

            if (MapProvider.Map.ContainsKey(map))
            {
                ConcreteTypeMap resolvedType = GetResolvedType(MapProvider.Map[map], parameter.Name);
                   if (resolvedType != null)
                    {
                    if (resolvedType.Parameters == null || resolvedType.Parameters.Length == 0)
                    {
                        return ObjectActivator.CreateInstance(resolvedType.Type, new object[0]);
                    }
                    var args = new object[resolvedType.Parameters.Length];
                    for (int index = 0; index < resolvedType.Parameters.Length; index++)
                    {
                        args[index] = InitializeParameter(resolvedType.Parameters[index]);
                    }
                    return ObjectActivator.CreateInstance(resolvedType.Type, args);
                }
            }
            return null;
        }

       
    }
}
