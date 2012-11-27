namespace TestingTools.Extensions
{
    using TestingTools.Core;
    using NUnit.Framework;

    public static class NumberExtensions
    {
        public static IVerifiable<int> IsGreaterThan(this IAssertion<int> number, int compared, string message = "")
        {
            return new Verifiable<int>(number, target =>
            Assert.That(number, 
                        Is.GreaterThan(compared), 
                        string.Format("Value wasn't greater than {0}, it was {1}\n{2}", 
                            compared, 
                            number, 
                            message)));
        }

        public static IVerifiable<int> IsGreaterThanOrEqual(this IAssertion<int> number, int compared, string message = "")
        {
            return new Verifiable<int>(number, target =>
                Assert.That(number, 
                            Is.GreaterThanOrEqualTo(compared), 
                            string.Format(
                                    "Value wasn't greater than or equal {0}, it was {1}\n{2}",
                                    compared,
                                    number,
                                    message)));
        }

        public static IVerifiable<int> IsLessThanOrEqual(this IAssertion<int> number, int compared, string message = "")
        {
            return new Verifiable<int>(number, target =>
                Assert.That(number, 
                            Is.LessThanOrEqualTo(compared), 
                            string.Format(
                                    "Value wasn't less than or equal {0}, it was {1}\n{2}",
                                    compared,
                                    number,
                                    message)));
        }

        public static IVerifiable<int> IsLessThan(this IAssertion<int> number, int compared, string message = "")
        {
            return new Verifiable<int>(number, target =>
            {
                Assert.That(target, 
                            Is.LessThan(compared), 
                            string.Format(
                                    "Value wasn't less than {0}, it was {1}\n{2}",
                                    compared,
                                    number,
                                    message));
            });
        }
    }
}
