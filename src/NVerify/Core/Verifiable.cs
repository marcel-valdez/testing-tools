namespace NVerify.Core
{
  using System;

  internal class Verifiable<T> : IVerifiable<T>, ICommandable
  {
    private readonly IAssertion<T> mAssertion;
    private readonly Action<T> mPredicate;

    internal Verifiable(IAssertion<T> assertion, Action<T> predicate)
    {
      this.mAssertion = assertion;
      this.mPredicate = predicate;
    }

    /// <summary>
    /// Ejecuta todos los predicados encadenados, empezando por el primero, hasta el último.
    /// </summary>
    public virtual void Now()
    {
      if (this.mAssertion is IVerifiable<T>)
      {
        (this.mAssertion as IVerifiable<T>).Now();
      }

      this.mPredicate(this.mAssertion.Target);
    }

    #region IAssertion<T> Members
    /// <summary>
    /// Gets the target being tested.
    /// </summary>
    public virtual T Target
    {
      get
      {
        return this.mAssertion.Target;
      }
    }
    #endregion

    #region ICommandable Members

    public void Command(object command)
    {
      if (this.mAssertion is ICommandable)
      {
        (this.mAssertion as ICommandable).Command(command);
      }
    }
    #endregion
  }
}
