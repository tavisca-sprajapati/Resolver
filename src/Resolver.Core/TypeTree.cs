using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    internal class TypeTree
    {
        private readonly List<TypeTree> _children = new List<TypeTree>();

        internal object Instance;
        internal Type type;
        public TypeTree(Type type)
        {
            this.type = type;
        }
        public void Initialize()
        {
            Type[] childTypes = GetCtorParams(type);
            if (childTypes != null && childTypes.Length > 0)
            {
                AddChildren(childTypes);
                foreach (var child in Children)
                    child.Initialize();
            }
        }
        
        private Type[] GetCtorParams(Type type)
        {
            throw new NotImplementedException();
        }

        public TypeTree CreateInstance()
        {
            if (_children != null && _children.Count > 0)
            {
                int count = _children.Count;
                object[] parameters = new object[count];
                for (int index = 0; index < count; index++)
                {
                    var typeTree = this[index].CreateInstance();
                    parameters[index] = typeTree.Instance;
                }
                Instance = ObjectActivator.CreateInstance(type, parameters);
            }
            Instance = ObjectActivator.CreateInstance(type, new object[0]);
            return this;
        }

        public TypeTree this[int i]
        {
            get
            {
                return _children[i];
            }
        }
        public List<TypeTree> Children => _children;
        public TypeTree Parent
        {
            get; private set;
        }

        public Type Value => type;

        public TypeTree AddChild(Type value)
        {
            var node = new TypeTree(value) { Parent = this };
            _children.Add(node);
            return node;
        }

        public TypeTree[] AddChildren(params Type[] values)
        {
            return values.Select(AddChild).ToArray();
        }
    }
}
