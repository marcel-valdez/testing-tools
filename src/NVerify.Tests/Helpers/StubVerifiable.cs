namespace NVerify.Tests.Helpers
{
  using NVerify.Core;

  internal class StubVerifiable<T> : IVerifiable<T>
  {
    IVerifiable<T> mInner;
    public StubVerifiable(IVerifiable<T> inner)
    {
      this.mInner = inner;
    }

    public bool NowCalled
    {
      get;
      set;
    }

    public bool TargetGetCalled
    {
      get;
      set;
    }

    public virtual void Now()
    {
      NowCalled = true;
      mInner.Now();
    }

    public virtual T Target
    {
      get
      {
        TargetGetCalled = true;
        return mInner.Target;
      }
    }
  }
}
