using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        
        internal static ConstructorInfo SelectCtor(ConstructorInfo[] ctors, string ctorIdentifier)
        {
            if (ctors.Length == 1)
                return ctors[0];

            var defaultCtor = ctors[0];
            foreach (var ctor in ctors)
            {
                if (string.IsNullOrWhiteSpace(ctorIdentifier) == false)
                {
                    CtorSelectorAttribute selectorAttributes = ctor.GetCustomAttribute(typeof(CtorSelectorAttribute), false) as CtorSelectorAttribute;
                    if (selectorAttributes != null && string.Equals(ctorIdentifier, selectorAttributes.Name))
                        return ctor;
                }

                Attribute defaultAttribute = ctor.GetCustomAttribute(typeof(DefaultCtorAttribute), false);
                if (defaultAttribute != null)
                    defaultCtor = ctor;
            }
            return defaultCtor;
        }
        internal static string GetAttributedMapName(ParameterInfo parameterInfo)
        {
            string mapName = "";
            var attribute = parameterInfo.GetCustomAttribute(typeof(DependencyAttribute), false) as DependencyAttribute;
            if (attribute != null)
            {
                mapName = attribute.Name;
            }
            return mapName;
        }
    }
}
