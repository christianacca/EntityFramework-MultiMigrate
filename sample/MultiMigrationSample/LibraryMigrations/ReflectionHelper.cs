using System;
using System.Linq;
using System.Reflection;

namespace CcAcca.LibraryMigrations
{
    internal static class ReflectionHelper
    {
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="obj"/> does not implement method or does not expect the <paramref name="args"/> supplied</exception>
        public static object InvokeMethod(this object obj, string methodName, params object[] args)
        {
            Type targetType = obj.GetType();
            Type[] paremeterTypes;
            if (args != null && args.Length != 0)
            {
                if (args.Contains(null))
                    throw new ArgumentException("Cannot determine type of argument; args must not contain null", "args");
                paremeterTypes = args.Select(arg => arg.GetType()).ToArray();
            }
            else
            {
                paremeterTypes = new Type[0];
            }

            MethodInfo method = targetType.GetMethod(methodName, BindingFlags.Instance |
                                                                 BindingFlags.NonPublic | BindingFlags.Public, null, paremeterTypes, null);
            if (method == null)
            {
                string errorMsg =
                    string.Format(
                        "Type '{0}' does not implement method '{1}' or there is a mismatch between arguments supplied and the parameters the method expects",
                        targetType.Name,
                        methodName);
                throw new InvalidOperationException(errorMsg);
            }
            return method.Invoke(obj, args);
        }
    }
}