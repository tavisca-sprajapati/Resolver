using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
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
}
