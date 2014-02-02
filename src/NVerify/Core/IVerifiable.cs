namespace NVerify.Core
{
  public interface IVerifiable<T> : IAssertion<T>
  {
    void Now();
  }
}
