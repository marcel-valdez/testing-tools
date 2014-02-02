namespace NVerify.Reflection
{
    using System;
    using Fasterflect;
    using Helpers;

    public static partial class ReflectionExtensions
    {
        /// <summary>
        /// Gets the static method.
        /// </summary>
        /// <typeparam name="TParam1">The type of the param1.</typeparam>
        /// <typeparam name="TParam2">The type of the param2.</typeparam>
        /// <typeparam name="TParam3">The type of the param3.</typeparam>
        /// <typeparam name="TParam4">The type of the param4.</typeparam>
        /// <typeparam name="TParam5">The type of the param5.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="classType">Type of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult> 
            GetStaticMethod<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(
                this Type classType, 
                string methodName, 
                params Type[] genericTypeArgs)
        {
            return GetStaticMethod<TResult>(classType, methodName, new Type[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4), typeof(TParam5) }, genericTypeArgs)
                   .Wrap<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>();
        }

        /// <summary>
        /// Gets the static method.
        /// </summary>
        /// <typeparam name="TParam1">The type of the param1.</typeparam>
        /// <typeparam name="TParam2">The type of the param2.</typeparam>
        /// <typeparam name="TParam3">The type of the param3.</typeparam>
        /// <typeparam name="TParam4">The type of the param4.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="classType">Type of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TParam3, TParam4, TResult> GetStaticMethod<TParam1, TParam2, TParam3, TParam4, TResult>(this Type classType, string methodName, params Type[] genericTypeArgs)
        {
            return GetStaticMethod<TResult>(classType, methodName, new Type[] { typeof(TParam1), typeof(TParam2), typeof(TParam3), typeof(TParam4) }, genericTypeArgs)
                   .Wrap<TParam1, TParam2, TParam3, TParam4, TResult>();
        }

        /// <summary>
        /// Gets the static method.
        /// </summary>
        /// <typeparam name="TParam1">The type of the param1.</typeparam>
        /// <typeparam name="TParam2">The type of the param2.</typeparam>
        /// <typeparam name="TParam3">The type of the param3.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="classType">Type of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TParam3, TResult> GetStaticMethod<TParam1, TParam2, TParam3, TResult>(this Type classType, string methodName, params Type[] genericTypeArgs)
        {
            return GetStaticMethod<TResult>(classType, methodName, new Type[] { typeof(TParam1), typeof(TParam2), typeof(TParam3) }, genericTypeArgs)
                    .Wrap<TParam1, TParam2, TParam3, TResult>();
        }

        /// <summary>
        /// Gets the static method.
        /// </summary>
        /// <typeparam name="TParam1">The type of the param1.</typeparam>
        /// <typeparam name="TParam2">The type of the param2.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="classType">Type of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam1, TParam2, TResult> GetStaticMethod<TParam1, TParam2, TResult>(this Type classType, string methodName, params Type[] genericTypeArgs)
        {
            return GetStaticMethod<TResult>(classType, methodName, new Type[] { typeof(TParam1), typeof(TParam2) }, genericTypeArgs)
                    .Wrap<TParam1, TParam2, TResult>();
        }

        /// <summary>
        /// Gets the static method.
        /// </summary>
        /// <typeparam name="TParam">The type of the param.</typeparam>
        /// <typeparam name="TResult">The type of the result.</typeparam>
        /// <param name="classType">Type of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="genericTypeArgs">The generic type args.</param>
        /// <returns></returns>
        public static Func<TParam, TResult> GetStaticMethod<TParam, TResult>(this Type classType, string methodName, params Type[] genericTypeArgs)
        {
            return GetStaticMethod<TResult>(classType, methodName, new Type[] { typeof(TParam) }, genericTypeArgs)
                    .Wrap<TParam, TResult>();
        }

        /// <summary>
        /// Gets the static method of a class
        /// </summary>
        /// <typeparam name="T">The return type of the method</typeparam>
        /// <param name="classType">Type of the class.</param>
        /// <param name="methodName">Name of the method.</param>
        /// <param name="parameterTypes">The types of objects the method receives as parameters.</param>
        /// <param name="genericTypeArgs">The type arguments if its a generic method definition.</param>
        /// <returns></returns>
        public static Func<object[], T> GetStaticMethod<T>(this Type classType, string methodName, Type[] parameterTypes, params Type[] genericTypeArgs)
        {
            return GetMethod<T>(null, classType, methodName, parameterTypes, genericTypeArgs, Flags.StaticAnyVisibility);
        }
    }
}
