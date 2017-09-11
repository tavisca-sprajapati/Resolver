using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = false)]
    public class DependencyAttribute : Attribute
    {
        private string _name;
        public DependencyAttribute(string name)
        {
            _name = name;
        }
        public string Name => _name;
    }

    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class DefaultCtorAttribute : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Constructor, AllowMultiple = false, Inherited = false)]
    public class CtorSelectorAttribute : Attribute
    {
        private string _name;
        public CtorSelectorAttribute(string name)
        {
            _name = name;
        }
        public string Name => _name;
    }
}
