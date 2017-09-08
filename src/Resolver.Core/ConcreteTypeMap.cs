using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    internal sealed class ConcreteTypeMap
    {
        private string _mapName;
        private Type _type;
        private Parameter[] _ctorParameters;
        internal ConcreteTypeMap(Type type, string mapName)
        {
            _type = type;
            _mapName = mapName;
            GetParams(type);
        }
        internal Parameter[] Parameters => _ctorParameters;
        private void GetParams(Type type)
        {
            var args = type.GetConstructors();
            if (args != null && args.Length > 0)
            {
                var ctor = args[0];
                var parameters = ctor.GetParameters();
                if (parameters != null && parameters.Length > 0)
                {
                    _ctorParameters = parameters.Select(parameter => new Parameter { Type = parameter.ParameterType, Name = GetAttributedMapName(parameter) }).ToArray();
                }
                    
            }
        }

        private static string GetAttributedMapName(ParameterInfo parameterInfo)
        {
            string mapName = "";
            object[] attributes = parameterInfo.GetCustomAttributes(typeof(DependencyAttribute), true);
            if (attributes.Length > 0)
            {
                mapName = ((DependencyAttribute)attributes[0]).Name;
            }
            return mapName;
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
    internal class Parameter
    {
        public Type Type;
        public string Name;
    }
}
