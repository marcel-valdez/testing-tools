﻿namespace TestingTools.Extensions
{
    using TestingTools.Core;

    public static class Connectors
    {
        /// <summary>
        /// Appends a verification
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual">The actual.</param>
        /// <returns></returns>
        public static IVerifiable<T> And<T>(this IVerifiable<T> actual)
        {
            return actual;
        }

        /// <summary>
        /// Allows for either Verification to cause a valid assertion
        /// </summary>
        /// <typeparam name="T">Type of the target</typeparam>
        /// <param name="me">Target to be checked first</param>
        /// <param name="or">Target to be checked second</param>
        /// <returns></returns>
        public static IVerifiable<T> Or<T>(this IVerifiable<T> me, IVerifiable<T> or)
        {
            return new VerifiableOr<T>(me, or);
        }
    }
}
