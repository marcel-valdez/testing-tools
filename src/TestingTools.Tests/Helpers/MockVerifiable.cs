namespace TestingTools.Tests.Helpers
{
  using TestingTools.Core;

  internal class MockVerifiable<T> : StubVerifiable<T>
  {
    internal MockVerifiable(IVerifiable<T> inner)
      : base(inner)
    {
    }
  }
}
