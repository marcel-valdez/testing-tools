#if NET35
public delegate TResult Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(TParam1 arg1, TParam2 arg2, TParam3 arg3, TParam4 arg4, TParam5 arg5);

public delegate TResult Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>(TParam1 arg1, TParam2 arg2, TParam3 arg3, TParam4 arg4, TParam5 arg5, TParam6 arg6);
#endif

namespace NVerify.Helpers
{
  using System;

  public static class Casting
  {
    /// <summary>
    /// Wraps the function.
    /// </summary>
    /// <typeparam name="TParam">The type of the param.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The func.</param>
    /// <returns></returns>
    public static Func<TParam, TResult> Wrap<TParam, TResult>(this Func<object[], TResult> func)
    {
      return (arg) =>
      {
        return func.Call(arg);
      };
    }

    /// <summary>
    /// Wraps the function.
    /// </summary>
    /// <typeparam name="TParam">The type of the param.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The func.</param>
    /// <returns></returns>
    public static Func<TParam1, TParam2, TResult> Wrap<TParam1, TParam2, TResult>(this Func<object[], TResult> func)
    {
      return (arg1, arg2) =>
      {
        return func.Call(arg1, arg2);
      };
    }

    /// <summary>
    /// Wraps the function.
    /// </summary>
    /// <typeparam name="TParam">The type of the param.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The func.</param>
    /// <returns></returns>
    public static Func<TParam1, TParam2, TParam3, TResult> Wrap<TParam1, TParam2, TParam3, TResult>(this Func<object[], TResult> func)
    {
      return (arg1, arg2, arg3) =>
      {
        return func.Call(arg1, arg2, arg3);
      };
    }

    /// <summary>
    /// Wraps the function.
    /// </summary>
    /// <typeparam name="TParam">The type of the param.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The func.</param>
    /// <returns></returns>
    public static Func<TParam1, TParam2, TParam3, TParam4, TResult> Wrap<TParam1, TParam2, TParam3, TParam4, TResult>(this Func<object[], TResult> func)
    {
      return (arg1, arg2, arg3, arg4) =>
      {
        return func.Call(arg1, arg2, arg3, arg4);
      };
    }

    /// <summary>
    /// Wraps the function.
    /// </summary>
    /// <typeparam name="TParam">The type of the param.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The func.</param>
    /// <returns></returns>
    public static Func<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>
        Wrap<TParam1, TParam2, TParam3, TParam4, TParam5, TResult>(
            this Func<object[], TResult> func)
    {
      return (arg1, arg2, arg3, arg4, arg5) =>
      {
        return func.Call(arg1, arg2, arg3, arg4, arg5);
      };
    }

    /// <summary>
    /// Wraps the function.
    /// </summary>
    /// <typeparam name="TParam">The type of the param.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The func.</param>
    /// <returns></returns>
    public static Func<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult> Wrap<TParam1, TParam2, TParam3, TParam4, TParam5, TParam6, TResult>(this Func<object[], TResult> func)
    {
      return (arg1, arg2, arg3, arg4, arg5, arg6) =>
      {
        return func.Call(arg1, arg2, arg3, arg4, arg5, arg6);
      };
    }

    /// <summary>
    /// Calls the specified func.
    /// </summary>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    /// <param name="func">The func.</param>
    /// <param name="args">The args.</param>
    /// <returns></returns>
    public static TResult Call<TResult>(this Func<object[], TResult> func, params object[] args)
    {
      return func(new object[] { args });
    }

    /// <summary>
    /// Casts the specified result.
    /// </summary>
    /// <typeparam name="TCast">The type of the cast.</typeparam>
    /// <param name="result">The result.</param>
    /// <returns></returns>
    public static TCast Cast<TCast>(this object instance)
    {
      return typeof(TCast).IsValueType || instance != null ? (TCast)instance : default(TCast);
    }
  }
}
