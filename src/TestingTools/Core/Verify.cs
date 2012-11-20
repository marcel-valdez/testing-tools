namespace TestingTools.Core
{
    using System.Collections.Generic;
    using System.Linq;

    public static class Verify
    {
        /// <summary>
        /// Starts a new fluent test assertion, using <paramref name="target"/> as
        /// the target under test.
        /// </summary>
        /// <typeparam name="T">The type of the target under test</typeparam>
        /// <param name="target">The target.</param>
        /// <returns>The assertion to build</returns>
        public static IAssertion<IEnumerable<T>> That<T>(T[] target)
        {
            return new Assertion<IEnumerable<T>>(target.AsEnumerable());
        }

        /// <summary>
        /// Starts a new fluent test assertion, using <paramref name="target"/> as
        /// the target under test.
        /// </summary>
        /// <typeparam name="T">The type of the target under test</typeparam>
        /// <param name="target">The target.</param>
        /// <returns>The assertion to build</returns>
        public static IAssertion<T> That<T>(T target)
        {
            return new Assertion<T>(target);
        }

        /// <summary>
        /// Creates a continuation of a fluent test assertion, using 
        /// <paramref name="target"/> as the target under test.
        /// </summary>
        /// <typeparam name="T">The type of the target under test</typeparam>
        /// <param name="target">The target.</param>
        /// <returns>The assertion to build</returns>
        public static IAssertion<T> That<T>(IAssertion<T> assertion)
        {
            return assertion;
        }
    }
}
