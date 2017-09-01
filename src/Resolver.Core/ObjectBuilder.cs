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
        private static Dictionary<TypeMap, Type> mapper = new Dictionary<TypeMap, Type>();

        public static T Resolve<T>(string mapName = "")
        {
            var map = new TypeMapper(typeof(T), mapName);
            if (mapper.ContainsKey(map))
            {
                return (T)Activate(mapper[map]);
            }
            return default(T);
        }
        private static object Activate(Type type)
        {
            return FastActivator.CreateInstance(type);
        }
        internal static void Register<Tp, Tc>(string name)
        {
            var map = new TypeMap(typeof(Tp), name);
            mapper[map] = typeof(Tc);
        }
    }
    public class Register
    {
        protected Type RgisteredType;
        protected RegisterExpression<T> For<T>()
        {
            return new RegisterExpression<T>();
        }
        public class RegisterExpression<Tp>
        {
            public void Use<Tc>(string name = "") where Tc : Tp
            {
                ObjectBuilder.Register<Tp, Tc>(name);
            }
        }
    }
    
    internal class TypeMap
    {
        private string _name;
        private string _mapName;
        private Type _type;
        public TypeMap(Type type, string map = "")
        {
            _type = type;
            _name = _type.FullName;
            _mapName = map;
        }
        public override bool Equals(object obj)
        {
            return obj != null && obj is TypeMap && GetHashCode() == obj.GetHashCode();
        }
        public override int GetHashCode()
        {
            return _name.GetHashCode() ^ _mapName.GetHashCode();
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
            }

        }
    }
}
