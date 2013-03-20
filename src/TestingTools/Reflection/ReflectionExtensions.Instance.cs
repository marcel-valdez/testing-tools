namespace TestingTools.Reflection
{
    using System;
    using Fasterflect;
    using Helpers;

    public static partial class ReflectionExtensions
    {
        /// <summary>
        /// Gets the instance method.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult> 
            GetMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult, T>(
                this T instance, 
                string methodName,
                params Type[] genericTypeArgs)
        {
            Type[] parameterTypes = new Type[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5), typeof(TParam6) };
            Flags flags = Flags.InstanceAnyVisibility;
            return GetMethod<TResult>(instance, typeof(T), methodName, parameterTypes, genericTypeArgs, flags)
                    .Wrap<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>();
        }

        /// <summary>
        /// Gets the instance method.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult> GetMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TResult, T>(this T instance, string methodName, params Type[] genericTypeArgs)
        {
            Type[] parameterTypes = new Type[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5) };
            Flags flags = Flags.InstanceAnyVisibility;
            return GetMethod<TResult>(instance, typeof(T), methodName, parameterTypes, genericTypeArgs, flags)
                    .Wrap<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>();
        }

        /// <summary>
        /// Gets the instance method.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TParam3, TParam4, TResult> GetMethod<TParam1, TParam2, TParam3, TParam4, TResult, T>(this T instance, string methodName, params Type[] genericTypeArgs)
        {
            Type[] parameterTypes = new Type[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4) };
            Flags flags = Flags.InstanceAnyVisibility;
            return GetMethod<TResult>(instance, typeof(T), methodName, parameterTypes, genericTypeArgs, flags)
                    .Wrap<TParam1, TParam2, TParam3, TParam4, TResult>();
        }

        /// <summary>
        /// Gets the instance method.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TParam3, TResult> GetMethod<TParam1, TParam2, TParam3, TResult, T>(this T instance, string methodName, params Type[] genericTypeArgs)
        {
            Type[] parameterTypes = new Type[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) };
            Flags flags = Flags.InstanceAnyVisibility;
            return GetMethod<TResult>(instance, typeof(T), methodName, parameterTypes, genericTypeArgs, flags)
                    .Wrap<TParam1, TParam2, TParam3, TResult>();
        }

        /// <summary>
        /// Gets the instance method.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TResult> GetMethod<TParam1, TParam2, TResult, T>(this T instance, string methodName, params Type[] genericTypeArgs)
        {
            Type[] parameterTypes = new Type[] { typeof(TParam1), typeof(TParam2) };
            Flags flags = Flags.InstanceAnyVisibility;
            return GetMethod<TResult>(instance, typeof(T), methodName, parameterTypes, genericTypeArgs, flags)
                    .Wrap<TParam1, TParam2, TResult>();
        }

        /// <summary>
        /// Gets the instance method.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam, TResult> GetMethod<TParam, TResult, T>(this T instance, string methodName, params Type[] genericTypeArgs)
        {
            Type[] parameterTypes = new Type[] { typeof(TParam) };
            Flags flags = Flags.InstanceAnyVisibility;
            return GetMethod<TResult>(instance, typeof(T), methodName, parameterTypes, genericTypeArgs, flags)
                    .Wrap<TParam, TResult>();
        }

        /// <summary>
        /// Gets the method.
        /// </summary>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance">The instance.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The parameter types.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<object[], TResult> GetMethod<TResult, T>(this T instance, string methodName, Type[] parameterTypes, params Type[] genericTypeArgs)
        {
            Flags flags = Flags.InstanceAnyVisibility;
            return GetMethod<TResult>(instance, typeof(T), methodName, parameterTypes, genericTypeArgs, flags);
        }
    }
}
