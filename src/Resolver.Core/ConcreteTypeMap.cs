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
        private string _ctorIdentifier;
        private Parameter[] _ctorParameters;
        internal ConcreteTypeMap(Type type, string mapName, string ctorIdentifier)
        {
            _type = type;
            _mapName = mapName;
            _ctorIdentifier = ctorIdentifier;
            _ctorParameters = GetParams(type, _ctorIdentifier);
        }
        internal Parameter[] Parameters => _ctorParameters;
        private Parameter[] GetParams(Type type, string ctorIdentifier)
        {
            Parameter[] prameters = new Parameter[0];
            var args = type.GetConstructors();
            if (args != null && args.Length > 0)
            {
                var ctor = Helper.SelectCtor(args, ctorIdentifier);
                var parameters = ctor.GetParameters();
                if (parameters != null && parameters.Length > 0)
                {
                    prameters = parameters.Select(parameter => new Parameter { Type = parameter.ParameterType, Name = Helper.GetAttributedMapName(parameter) }).ToArray();
                }       
            }
            return prameters;
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
