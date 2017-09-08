using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    public sealed class RegisterExpression<Tp>
    {
        public void Use<Tc>(string name = "") where Tc : Tp
        {
            ObjectBuilder.Register<Tp, Tc>(name);
        }
    }
}
