namespace NVerify.Tests.Helpers
{
  using NVerify.Core;

  internal class MockVerifiable<T> : StubVerifiable<T>
  {
    internal MockVerifiable(IVerifiable<T> inner)
      : base(inner)
    {
    }
  }
}
