using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    public class Register
    {
        protected Type RgisteredType;
        protected RegisterExpression<T> For<T>()
        {
            return new RegisterExpression<T>();
        }
    }
}
