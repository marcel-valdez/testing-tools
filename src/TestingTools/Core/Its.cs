namespace TestingTools.Core
{
  using System;

  public static class Its<TTarget>
  {
    public static IAssertion<TMember> Member<TMember>(Func<TTarget, TMember> getter)
    {
      return new ItsAssertable<TTarget, TMember>(getter);
    }
  }
}