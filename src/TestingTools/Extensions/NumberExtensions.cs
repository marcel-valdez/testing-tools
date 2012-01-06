namespace TestingTools.Extensions
{
    using TestingTools.Core;

    public static class NumberExtensions
    {
        public static IVerifiable<int> IsGreaterThan(this IAssertion<int> number, int compared)
        {
            return new Verifiable<int>(number, target =>
            Verify.That(target > compared).IsTrue(string.Format("Value wasn't greater than {0}, it was {1}", compared, number)).Now());
        }

        public static IVerifiable<int> IsGreaterThanOrEqual(this IAssertion<int> number, int compared)
        {
            return new Verifiable<int>(number, target =>
            Verify.That(target >= compared).IsTrue(string.Format("Value wasn't greater than or equal {0}, it was {1}", compared, number)));   
        }

        public static IVerifiable<int> IsLessThanOrEqual(this IAssertion<int> number, int compared)
        {
            return new Verifiable<int>(number, target =>
            Verify.That(target <= compared).IsTrue(string.Format("Value wasn't less than or equal to {0}, it was {1}", compared, number)));   
        }

        public static IVerifiable<int> IsLessThan(this IAssertion<int> number, int compared)
        {
            return new Verifiable<int>(number, target =>
            Verify.That(target < compared).IsTrue(string.Format("Value wasn't less than {0}, it was {1}", compared, number)).Now());
        }
    }
}
