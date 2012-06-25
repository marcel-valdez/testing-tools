namespace TestingTools.Extensions
{
    using System;
    using System.Diagnostics.Contracts;
    using NUnit.Framework;
    using TestingTools.Core;

    public static class GenericExtensions
    {
        public static IVerifiable<T> IsEqualTo<T>(this IAssertion<T> actual, T expected)
        {
            return new Verifiable<T>(actual, target => Assert.AreEqual(expected, target));
        }

        public static IVerifiable<T> IsEqualTo<T>(this IAssertion<T> actual, T expected, string message)
        {
            return new Verifiable<T>(actual, target => Assert.AreEqual(expected, target, message));
        }

        public static IVerifiable<T> IsNotEqualTo<T>(this IAssertion<T> actual, T notExpected)
        {
            return new Verifiable<T>(actual, target => Assert.AreNotEqual(notExpected, target));
        }

        public static IVerifiable<T> IsNotEqualTo<T>(this IAssertion<T> actual, T notExpected, string message)
        {
            return new Verifiable<T>(actual, target => Assert.AreNotEqual(notExpected, target, message));
        }

        public static IVerifiable<T> IsOfType<T>(this IAssertion<T> actual, Type type)
        {
            return new Verifiable<T>(actual, target => Assert.IsInstanceOfType(type, target));
        }

        public static IVerifiable<T> IsOfType<T>(this IAssertion<T> actual, Type type, string message)
        {
            return new Verifiable<T>(actual, target => Assert.IsInstanceOfType(type, target, message));
        }

        public static IVerifiable<T> IsNotOfType<T>(this IAssertion<T> actual, Type type, string message)
        {
            return new Verifiable<T>(actual, target => Assert.IsNotInstanceOfType(type, target, message));
        }

        public static IVerifiable<T> IsNotOfType<T>(this IAssertion<T> actual, Type type)
        {
            return new Verifiable<T>(actual, target => Assert.IsNotInstanceOfType(type, target));
        }

        public static IVerifiable<T> IsNotNull<T>(this IAssertion<T> actual, string message = "")
            where T : class
        {
            return new Verifiable<T>(actual, target => Assert.IsNotNull(target, message));
        }

        public static IVerifiable<T> IsNull<T>(this IAssertion<T> actual, string message = "")
            where T : class
        {
            return new Verifiable<T>(actual, target => Assert.IsNull(target, message));
        }

        /// <summary>
        /// Compares two objects by reference.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static IVerifiable<T> IsTheSameAs<T>(this IAssertion<T> actual, T expected)
        {
            return new Verifiable<T>(actual, target => Assert.AreSame(expected, target));
        }

        /// <summary>
        /// Compares two objects by reference with a message for failures.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="message"></param>
        public static IVerifiable<T> IsTheSameAs<T>(this IAssertion<T> actual, T expected, string message)
        {
            return new Verifiable<T>(actual, target => Assert.AreSame(expected, target, message));
        }

        /// <summary>
        /// Makes sure two objects are not the same reference.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        public static IVerifiable<T> IsDifferentFrom<T>(this IAssertion<T> actual, T expected)
        {
            return new Verifiable<T>(actual, target => Assert.AreNotSame(expected, target));
        }

        /// <summary>
        /// Makes sure two objects are not the same reference with a message for failures.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual"></param>
        /// <param name="expected"></param>
        /// <param name="message"></param>
        public static IVerifiable<T> IsDifferentFrom<T>(this IAssertion<T> actual, T expected, string message)
        {
            return new Verifiable<T>(actual, target => Assert.AreNotSame(expected, target, message));
        }

        /// <summary>
        /// Verifies that a predicate, applied to an instance is true.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="actual">The actual.</param>
        /// <param name="predicate">The predicate.</param>
        /// <param name="message">The message.</param>
        /// <returns></returns>
        public static IVerifiable<T> ItsTrueThat<T>(this IAssertion<T> actual, Predicate<T> predicate, string message = "")
        {
            Contract.Requires(predicate != null, "predicate is null.");
            return new Verifiable<T>(actual, target => Assert.IsTrue(predicate(target)));
        }

        /// <summary>
        /// Allows the client to perform a verification on a member of the parent
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TMember">The type of the member.</typeparam>
        /// <param name="parent">The parent.</param>
        /// <param name="getter">The getter.</param>
        /// <returns></returns>
        public static Func<Func<IAssertion<TMember>, IVerifiable<TMember>>, IVerifiable<T>> Member<T, TMember>(this IAssertion<T> parent, Func<TMember> getter)
        {
            Contract.Requires(getter != null, "getter must not be null");
            Func<Func<IAssertion<TMember>, IVerifiable<TMember>>, IVerifiable<T>> verificator =
                memberVerification =>
                    {
                        Action<T> nexus = _ =>
                            {
                                var memberVerifiable = new Assertion<TMember>(getter());
                                memberVerification(memberVerifiable)
                                    .Now();
                            };

                        return new Verifiable<T>(parent, nexus);
                    };

            return verificator;
        }

        public static Func<Func<IAssertion<TMember>, IVerifiable<TMember>>, IVerifiable<T>> Member<T, TMember>(this IAssertion<T> parent, TMember member)
        {
            Contract.Requires(member != null, "getter must not be null");
            Func<Func<IAssertion<TMember>, IVerifiable<TMember>>, IVerifiable<T>> verificator =
                memberVerification =>
                {
                    Action<T> nexus = _ =>
                    {
                        var memberVerifiable = new Assertion<TMember>(member);
                        memberVerification(memberVerifiable)
                            .Now();
                    };

                    return new Verifiable<T>(parent, nexus);
                };

            return verificator;
        }

        public static Func<Func<IAssertion<TMember>, IVerifiable<TMember>>, IVerifiable<T>> Member<T, TMember>(this IAssertion<T> parent, Func<T, TMember> getter)
        {
            Contract.Requires(getter != null, "getter must not be null");
            Func<Func<IAssertion<TMember>, IVerifiable<TMember>>, IVerifiable<T>> verificator =
                memberVerification =>
                {
                    Action<T> nexus = parentTarget =>
                    {
                        var memberVerifiable = new Assertion<TMember>(getter(parentTarget));
                        memberVerification(memberVerifiable)
                            .Now();
                    };

                    return new Verifiable<T>(parent, nexus);
                };

            return verificator;
        }
    }
}
