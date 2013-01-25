namespace TestingTools.Extensions
{
  using NUnit.Framework;
    using TestingTools.Core;

    public static class ObjectExtensions
    {

        public static IVerifiable<object> IsNull(this IAssertion<object> actual)
        {
            return new Verifiable<object>(actual, target => Assert.IsNull(target));
        }

        public static IVerifiable<object> IsNull(this IAssertion<object> actual, string message)
        {
            return new Verifiable<object>(actual, target => Assert.IsNull(target, message));
        }

        public static IVerifiable<object> IsNotNull(this IAssertion<object> actual)
        {
            return new Verifiable<object>(actual, target => Assert.IsNotNull(target));
        }

        public static IVerifiable<object> IsNotNull(this IAssertion<object> actual, string message)
        {
            return new Verifiable<object>(actual, target => Assert.IsNotNull(target, message));
        }
    }
}
