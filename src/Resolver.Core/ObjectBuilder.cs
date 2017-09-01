using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    public class ObjectBuilder
    {
        private static Dictionary<TypeMap, ConcreteTypeMap> mapper = new Dictionary<TypeMap, ConcreteTypeMap>();

        public static T Resolve<T>()
        {
            var map = new TypeMap(typeof(T));
            if (mapper.ContainsKey(map))
            {
                return (T)Activate(mapper[map].Type);
            }
            return default(T);
        }
        private static object Activate(Type type)
        {
            return FastActivator.CreateInstance(type);
        }
        internal static void Register<Tp, Tc>(string name)
        {
            var map = new TypeMap(typeof(Tp));
            var concreteMap = new ConcreteTypeMap(typeof(Tc), name);
            mapper[map] = concreteMap;
        }
        internal static bool Contains(TypeMap map)
        {
            return mapper.ContainsKey(map);
        }
        internal static ConcreteTypeMap GetValue(TypeMap map)
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
    public class RegisterExpression<Tp>
    {
        public void Use<Tc>(string name = "") where Tc : Tp
        {
            ObjectBuilder.Register<Tp, Tc>(name);
        }
    }
    internal class TypeMap
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
    internal class ConcreteTypeMap
    {
        private string _mapName;
        private Type _type;
        internal ConcreteTypeMap(Type type, string mapName)
        {
            _type = type;
            _mapName = mapName;
        }
        public string MapName
        {
            get
            {
                return _mapName;
            }
        }
        public Type Type
        {
            get
            {
                return _type;
            }
        }
    }
    internal class Resolver
    {
        public static object GetConstructor(Type type)
        {
            ConstructorInfo[] info = type.GetConstructors();

            if (info != null && info.Length > 0)
            {
                var parameters = info[0].GetParameters();
                if (parameters != null && parameters.Length > 0)
                {
                    foreach (var parameter in parameters)
                    {
                        Type t = parameter.ParameterType;
                        var key = new TypeMap(t);
                        if (ObjectBuilder.Contains(key))
                        {
                            var value = ObjectBuilder.GetValue(key);

                        }
                    }
                }
                else
                {
                }
            }

        }
        private 
        private object[] InitializeParameters(ParameterInfo[] parameters)
        {

        }
    }
}
