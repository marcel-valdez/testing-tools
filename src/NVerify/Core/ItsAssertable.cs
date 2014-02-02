namespace NVerify.Core
{
  using System;

  public class ItsAssertable<TParent, TMember> : IAssertion<TMember>, ICommandable
  {
    private TParent mParent;
    private readonly Func<TParent, TMember> mGetter;

    internal ItsAssertable(Func<TParent, TMember> getter)
    {
      this.mGetter = getter;
    }

    #region IAssertion<T> Members

    public TMember Target
    {
      get
      {
        return this.mGetter(mParent);
      }
    }

    #endregion

    public void Command(object command)
    {
      if (command is TParent)
      {
        this.mParent = (TParent)command;
      }
    }
  }
}
