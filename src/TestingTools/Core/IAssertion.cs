namespace TestingTools.Core
{
    public interface IAssertion<T> 
    {
        T Target
        {
            get;
        }
    }
}
