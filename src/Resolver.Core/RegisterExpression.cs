using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    public sealed class RegisterExpression<Tp>
    {
        private string _ctorIdentifier;
        private string _mapName;
        private Type _concreteType;
        private Type _contractType;
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="Tc">Concrete type against the contract</typeparam>
        /// <param name="name">Optional: Dependency resolver identifier</param>
        /// <param name="ctorIdentifier">Optional: Ctor selector if multiple implemented, if not specified 0th will be resolved</param>
        public RegisterExpression<Tp> Use<Tc>(string mapName = "") where Tc : Tp
        {
            _mapName = mapName;
            _concreteType = typeof(Tc);
            _contractType = typeof(Tp);
            return this;
            
        }
        public RegisterExpression<Tp> WithCtor(string ctorIdentifier)
        {
            _ctorIdentifier = ctorIdentifier;
            return this;
        }
        public void Register()
        {
            Register(_concreteType, _mapName, _ctorIdentifier, _contractType);
        }
        private static void Register(Type concreteType, string mapName, string ctorIdentifier, Type contractType)
        {
            var map = new TypeMap(contractType);
            var concreteMap = new ConcreteTypeMap(concreteType, mapName, ctorIdentifier);
            if (MapProvider.Map.ContainsKey(map))
            {
                MapProvider.Map[map].Add(concreteMap);
            }
            else
            {
                MapProvider.Map[map] = new List<ConcreteTypeMap> { concreteMap };
            }
        }
    }
}
