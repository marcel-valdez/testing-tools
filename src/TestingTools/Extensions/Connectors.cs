namespace TestingTools
{
  using System;
  using TestingTools.Core;

  public static class Connectors
  {
    /// <summary>
    /// Appends a verification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="actual">The actual.</param>
    /// <returns></returns>
    public static IVerifiable<T> And<T>(this IVerifiable<T> actual)
    {
      return actual;
    }


    /// <summary>
    /// Appends the specified member verification.
    /// </summary>
    /// <typeparam name="T">Type of parent</typeparam>
    /// <typeparam name="TMember">The type of the member.</typeparam>
    /// <param name="actual">The parent verification.</param>
    /// <param name="memberVerification">The member verification.</param>
    /// <returns></returns>
    public static IVerifiable<T> And<T, TMember>(this IVerifiable<T> actual, IVerifiable<TMember> memberVerification)
    {
      return new Verifiable<T>(actual, target =>
      {
        if (memberVerification is ICommandable)
        {
          (memberVerification as ICommandable).Command(target);
        }

        memberVerification.Now();
      });

    }

    /// <summary>
    /// Appends a verification
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="actual">The actual.</param>
    /// <returns></returns>
    public static IAssertion<TTarget> And<T, TTarget>(this IVerifiable<T> actual, TTarget target)
    {
      return new AndConnector<TTarget, T>(actual, target);
    }

    /// <summary>
    /// Allows for either Verification to cause a valid assertion
    /// </summary>
    /// <typeparam name="T">Type of the target</typeparam>
    /// <param name="me">Target to be checked first</param>
    /// <param name="or">Target to be checked second</param>
    /// <returns></returns>
    public static IVerifiable<T> Or<T>(this IVerifiable<T> me, Func<IAssertion<T>, IVerifiable<T>> or)
    {
      var assertable = new Assertion<T>(me.Target);
      var verifiable = or(assertable);
      return new VerifiableOr<T>(me, verifiable);
    }
  }
}
