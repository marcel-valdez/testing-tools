namespace TestingTools.Reflection
{
    using System;
    using System.Diagnostics.Contracts;
    using System.Linq;
    using System.Reflection;
    using Fasterflect;
    using Helpers;

    public static partial class ReflectionExtensions
    {
        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="calledOn">The called on.</param>
        /// <param name="classType">Type of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <param name="flags">The flags.</param>
        /// <returns></returns>
        public static Func<object[], TResult> GetMethod<TResult>(object calledOn, Type classType, string methodName, Type[] parameterTypes, Type[] genericTypeArgs, Flags flags)
        {
            bool shouldBeGenericDefinition = genericTypeArgs != null && genericTypeArgs.Length > 0;
            MethodInfo methodInfo;

            methodInfo = classType.GetMethods(flags)
                        .Where(mInfo => mInfo.Name == methodName &&
                            mInfo.IsGenericMethodDefinition == shouldBeGenericDefinition &&
                            mInfo.GetParameters().Length == parameterTypes.Length && (parameterTypes.Length == 0 ||
                            mInfo.Parameters().ToList()
                                 .TrueForAll(methodParameterInfo =>
                                 {
                                     return parameterTypes.Any(pType =>
                                     {
                                         return methodParameterInfo.ParameterType.IsGenericType ?
                                             pType.IsSubclassOrImplementsBaseGeneric(methodParameterInfo.ParameterType) :
                                             methodParameterInfo.ParameterType.IsAssignableFrom(pType);
                                     });
                                 })))
                        .Single();


            if (methodInfo.IsGenericMethodDefinition)
            {
                methodInfo = methodInfo.MakeGenericMethod(genericTypeArgs);
            }

            MethodInvoker invoker = methodInfo.DelegateForCallMethod();

            Func<object[], TResult> method = WrapMethodInvoker<object[], TResult>(calledOn, invoker);

            return method;
        }

        /// <summary>
        /// Determines whether [is subclass or implements base generic] [the specified sub generic].
        /// </summary>
        /// <param name="subGeneric">The generic type.</param>
        /// <param name="baseGeneric">The base generic.</param>
        /// <returns>
        ///   <c>true</c> if [is subclass of raw generic] [the specified sub generic]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSubclassOrImplementsBaseGeneric(this Type subGeneric, Type baseGeneric)
        {
            Contract.Requires(subGeneric != null, "subGeneric is null.");
            Contract.Requires(baseGeneric != null, "baseGeneric is null.");
            Contract.Requires(baseGeneric.IsGenericType);

            if (subGeneric.GetInterfaces()
                .Select(iface => iface.GUID)
                .Contains(baseGeneric.GUID))
            {
                return true;
            }

            while (subGeneric != typeof(object))
            {
                Type currentType = subGeneric.IsGenericType ? subGeneric.GetGenericTypeDefinition() : subGeneric;
                if (currentType.IsGenericType && baseGeneric.GetGenericTypeDefinition() == currentType.GetGenericTypeDefinition())
                {
                    return true;
                }

                subGeneric = subGeneric.BaseType;
            }

            return false;
        }

        /// <summary>
        /// Wraps the method invoker.
        /// </summary>
        /// <typeparam name="TParam">The type of the param.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="calledOn">The called on.</param>
        /// <param name="invoker">The invoker.</param>
        /// <returns></returns>
        public static Func<TParam, T> WrapMethodInvoker<TParam, T>(object calledOn, MethodInvoker invoker)
        {
            return (args) =>
            {
                return invoker(calledOn, args).Cast<T>();
            };
        }
    }
}
