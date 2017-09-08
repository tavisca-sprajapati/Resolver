using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
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
            if (args != null && args.Length > 0)
            {
                var ctor = args[0];
                var parameters = ctor.GetParameters();
                if (parameters != null && parameters.Length > 0)
                    _ctorParameters = parameters.Select(parameter => parameter.ParameterType).ToArray();
            }
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
}
