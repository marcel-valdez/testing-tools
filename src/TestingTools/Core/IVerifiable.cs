namespace TestingTools.Core
{
    public interface IVerifiable<T> : IAssertion<T>
    {
        void Now();
    }
}
