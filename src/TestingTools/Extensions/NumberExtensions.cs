namespace TestingTools.Extensions
{
  using NUnit.Framework;
  using TestingTools.Core;

    public static class NumberExtensions
    {
        public static IVerifiable<int> IsGreaterThan(this IAssertion<int> assertion, int compared, string message = "")
        {
            return new Verifiable<int>(assertion, target =>
            Assert.That(target, 
                        Is.GreaterThan(compared), 
                        string.Format("Value wasn't greater than {0}, it was {1}\n{2}", 
                            compared, 
                            target, 
                            message)));
        }

        public static IVerifiable<int> IsGreaterThanOrEqual(this IAssertion<int> assertion, int compared, string message = "")
        {
            return new Verifiable<int>(assertion, target =>
                Assert.That(target, 
                            Is.GreaterThanOrEqualTo(compared), 
                            string.Format(
                                    "Value wasn't greater than or equal {0}, it was {1}\n{2}",
                                    compared,
                                    target,
                                    message)));
        }

        public static IVerifiable<int> IsLessThanOrEqual(this IAssertion<int> assertion, int compared, string message = "")
        {
            return new Verifiable<int>(assertion, target =>
                Assert.That(target, 
                            Is.LessThanOrEqualTo(compared), 
                            string.Format(
                                    "Value wasn't less than or equal {0}, it was {1}\n{2}",
                                    compared,
                                    target,
                                    message)));
        }

        public static IVerifiable<int> IsLessThan(this IAssertion<int> assertion, int compared, string message = "")
        {
            return new Verifiable<int>(assertion, target =>
            {
                Assert.That(target, 
                            Is.LessThan(compared), 
                            string.Format(
                                    "Value wasn't less than {0}, it was {1}\n{2}",
                                    compared,
                                    target,
                                    message));
            });
        }
    }
}
