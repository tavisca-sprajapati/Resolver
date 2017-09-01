using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Resolver.Core
{
    public static class FastActivator
    {
        // THIS VERSION NOT THREAD SAFE YET
        static Dictionary<Type, Func<object>> constructorCache = new Dictionary<Type, Func<object>>();

        private const string DynamicMethodPrefix = "DM$_FastActivator_";

        public static object CreateInstance(Type objType)
        {
            return GetConstructor(objType)();
        }

        private static Func<object> GetConstructor(Type objType)
        {
            Func<object> constructor;
            if (!constructorCache.TryGetValue(objType, out constructor))
            {
                constructor = (Func<object>)FastActivator.BuildConstructorDelegate(objType, typeof(Func<object>), new Type[] { });
                constructorCache.Add(objType, constructor);
            }
            return constructor;
        }

        public static object BuildConstructorDelegate(Type objType, Type delegateType, Type[] argTypes)
        {
            var dynMethod = new DynamicMethod(DynamicMethodPrefix + objType.Name + "$" + argTypes.Length.ToString(), objType, argTypes, objType);
            ILGenerator ilGen = dynMethod.GetILGenerator();
            for (int argIdx = 0; argIdx < argTypes.Length; argIdx++)
            {
                ilGen.Emit(OpCodes.Ldarg, argIdx);
            }
            ilGen.Emit(OpCodes.Newobj, objType.GetConstructor(argTypes));
            ilGen.Emit(OpCodes.Ret);
            return dynMethod.CreateDelegate(delegateType);
        }
    }
}
