namespace NVerify
{
  using NVerify.Core;
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Reflection;

  public static class Verify
  {
    static Verify()
    {
      // This method tries to find any version of NUnit in its path,
      // in case the one used by the developer is not the exact
      // version against which this was compiled (2.6.012...)
      AppDomain.CurrentDomain.AssemblyResolve += (sender, e) =>
      {
        string partialName = e.Name.Substring(0, e.Name.IndexOf(','));
        if (partialName.ToLower().Contains("nunit"))
        {
          // Put code here to load whatever version of the assembly you actually have
          return Assembly.Load(new AssemblyName(partialName));
        }
        else
        {
          return null;
        }
      };
    }

    /// <summary>
    /// Starts a new fluent test assertion, using <paramref name="target"/> as
    /// the target under test.
    /// </summary>
    /// <typeparam name="T">The type of the target under test</typeparam>
    /// <param name="target">The target.</param>
    /// <returns>The assertion to build</returns>
    public static IAssertion<IEnumerable<T>> That<T>(T[] target)
    {
      return new Assertion<IEnumerable<T>>(target.AsEnumerable());
    }

    /// <summary>
    /// Starts a new fluent test assertion, using <paramref name="target"/> as
    /// the target under test.
    /// </summary>
    /// <typeparam name="T">The type of the target under test</typeparam>
    /// <param name="target">The target.</param>
    /// <returns>The assertion to build</returns>
    public static IAssertion<T> That<T>(T target)
    {
      return new Assertion<T>(target);
    }

    /// <summary>
    /// Creates a continuation of a fluent test assertion, using 
    /// <paramref name="target"/> as the target under test.
    /// </summary>
    /// <typeparam name="T">The type of the target under test</typeparam>
    /// <param name="target">The target.</param>
    /// <returns>The assertion to build</returns>
    public static IAssertion<T> That<T>(IAssertion<T> assertion)
    {
      return assertion;
    }
  }
}
