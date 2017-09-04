using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    internal sealed class MapProvider
    {
        private static Dictionary<ConcreteTypeMap, Type[]> _concreteMapper;
        internal static Dictionary<ConcreteTypeMap, Type[]> ConcreteMapper => _concreteMapper;
    }
    public sealed class ObjectBuilder
    {
        
        private static Dictionary<TypeMap, List<ConcreteTypeMap>> mapper = new Dictionary<TypeMap, List<ConcreteTypeMap>>();

        public static T Resolve<T>(string mapName = "")
        {
            var map = new TypeMap(typeof(T));
            if (mapper.ContainsKey(map))
            {
                ConcreteTypeMap resolvedType = Helper.GetResolvedType(mapper[map], mapName);
                object[] parameters = Helper.GetParameters(resolvedType);
                return (T)Activate(resolvedType.Type, parameters);
            }
            return default(T);
        }

        private static object Activate(Type type, object[] parameters)
        {
            return Activator.CreateInstance(type, parameters);
        }
        internal static void Register<Tp, Tc>(string name)
        {
            var map = new TypeMap(typeof(Tp));
            var concreteMap = new ConcreteTypeMap(typeof(Tc), name);
            MapProvider.ConcreteMapper[concreteMap] = concreteMap.Parameters;
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
    public class Register
    {
        protected Type RgisteredType;
        protected RegisterExpression<T> For<T>()
        {
            return new RegisterExpression<T>();
        }
    }
    public sealed class RegisterExpression<Tp>
    {
        public void Use<Tc>(string name = "") where Tc : Tp
        {
            ObjectBuilder.Register<Tp, Tc>(name);
        }
    }
    internal sealed class TypeMap
    {
        private string _name;
        private Type _type;
        public TypeMap(Type type)
        {
            _type = type;
            _name = _type.FullName;
        }
        public override bool Equals(object obj)
        {
            return obj != null && obj is TypeMap && GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return _name.GetHashCode();
        }
    }
    internal sealed class ConcreteTypeMap
    {
        private string _mapName;
        private Type _type;
        private Type[] _ctorParameters;
        internal ConcreteTypeMap(Type type, string mapName)
        {
            _type = type;
            _mapName = mapName;
            GetParams(type);
        }
        internal Type[] Parameters => _ctorParameters;
        private void GetParams(Type type)
        {
            var args = type.GetConstructors();
            var ctor = args[0];
            var parameters = ctor.GetParameters();
            _ctorParameters = parameters.Select(parameter => parameter.ParameterType).ToArray();
        }
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
                return false;

            return this.GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return _type.FullName.GetHashCode();
        }
        public string MapName => _mapName;
        public Type Type => _type;
    }
    internal sealed class Helper
    {
        internal static ConcreteTypeMap GetResolvedType(List<ConcreteTypeMap> types, string mapName)
        {
            if (string.IsNullOrEmpty(mapName))
                return types[0];
            return types.FirstOrDefault(t => string.Equals(t.MapName, mapName));
        }
        internal static object[] GetParameters(ConcreteTypeMap typeMap)
        {
            if (typeMap.Parameters == null || typeMap.Parameters.Length == 0)
                return new object[0];

            object[] parameters = new object[typeMap.Parameters.Length];

            for (int index = 0; index > parameters.Length; index++)
            {
                parameters[index] = InitializeParameter(typeMap.Parameters[index]);
            }
            return parameters;
        }
        private static object InitializeParameter(Type type)
        {
            var searchMap = new ConcreteTypeMap(type, string.Empty);
            if (MapProvider.ConcreteMapper.ContainsKey(searchMap))
            {
                var map = MapProvider.ConcreteMapper[searchMap];
                if (map != null)
                {
                    if (map.Length == 0)
                    {
                        return FastActivator.CreateInstance(type);
                    }
                    var args = new object[map.Length];
                    for (int index = 0; index > map.Length; index++)
                    {
                        args[index] = InitializeParameter(map[index]);
                    }
                    return Activator.CreateInstance(type, args);
                }
            }
            return null;
        }
    }
}
