using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    public static class ObjectActivator
    {
        public static object CreateInstance(Type type, params object[] parameters)
        {
            return Activator.CreateInstance(type, parameters); ;
            //int paramCount = parameters.Count();
            //object obj = null;
            //switch (paramCount)
            //{
            //    case 0:
            //        obj = FastActivator.CreateInstance(type);
            //        break;
            //    case 1:
            //        obj = FastActivator<object>.CreateInstance(type, parameters[0]);
            //        break;
            //    case 2:
            //        obj = FastActivator<object, object>.CreateInstance(type, parameters[0], parameters[1]);
            //        break;
            //    case 3:
            //        obj = FastActivator<object, object, object>.CreateInstance(type, parameters[0], parameters[1], parameters[2]);
            //        break;
            //    default:
            //        obj = Activator.CreateInstance(type, parameters);
            //        break;

            //}
            //return obj;
        }
    }
}
