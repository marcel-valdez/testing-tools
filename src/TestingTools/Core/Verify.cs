namespace TestingTools.Core
{
    public static class Verify
    {
        public static IAssertion<T> That<T>(T target)
        {
            return new Assertion<T>(target);
        }

        public static IAssertion<T> That<T>(IAssertion<T> assertion)
        {
            return assertion;
        }
    }
}
