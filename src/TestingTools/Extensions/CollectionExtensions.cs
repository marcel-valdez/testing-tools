namespace TestingTools.Extensions
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Linq;
    using NUnit.Framework;
    using TestingTools.Core;

    public static class CollectionExtensions
    {
        public static IVerifiable<IEnumerable<T>> IsEmpty<T>(this IAssertion<IEnumerable<T>> collection)
        {
            return new Verifiable<IEnumerable<T>>(collection, target =>
                Verify.That(target).IsNotNull()
                .And()
                    .ItsTrueThat(col => col.Count() == 0)
                .Now());
        }

        public static IVerifiable<IEnumerable<T>> IsEmpty<T>(this IAssertion<IEnumerable<T>> collection, string message)
        {
            return new Verifiable<IEnumerable<T>>(collection, target =>
                    Verify.That(target).IsNotNull(message)
                    .And()
                        .ItsTrueThat(col => col.Count() == 0, message)
                    .Now());
        }

        public static IVerifiable<IEnumerable<T>> IsNotEmpty<T>(this IAssertion<IEnumerable<T>> collection)
        {
            return new Verifiable<IEnumerable<T>>(collection, target =>
                Verify.That(target).IsNotNull()
                .And()
                    .ItsTrueThat(col => col.Count() > 0)
                .Now());
        }

        public static IVerifiable<IEnumerable<T>> IsNotEmpty<T>(this IAssertion<IEnumerable<T>> collection, string message)
        {
            return new Verifiable<IEnumerable<T>>(collection, target =>

                Verify.That(target).IsNotNull(message)
                .And()
                    .ItsTrueThat(col => col.Count() > 0, message)
                .Now());
        }

        public static IVerifiable<IEnumerable> DoesContain(this IAssertion<IEnumerable> list, object expected)
        {
            return list.DoesContain(expected, null);
        }

        public static IVerifiable<IEnumerable> DoesContain(this IAssertion<IEnumerable> list, object expected, string message)
        {
            return new Verifiable<IEnumerable>(list, target =>
                {
                    bool found = false;
                    if (list != null)
                    {
                        foreach (object o in target)
                        {

                            if (o == expected)
                            {
                                found = true;
                                break;
                            }
                        }
                    }

                    Verify.That(found)
                          .IsTrue(string.Format("The <{0}> should have contained: <{1}>. {2}",
                                        (target ?? "NULL"),
                                        (expected ?? "NULL"),
                                        (message ?? string.Empty)))
                          .Now();
                });
        }

        public static IVerifiable<IEnumerable<T>> DoesContain<T>(this IAssertion<IEnumerable<T>> list, T expected)
        {
            return new Verifiable<IEnumerable<T>>(list, target => Verify.That(target.Contains(expected)).IsTrue().Now());
        }

        public static IVerifiable<IEnumerable<T>> DoesContain<T>(this IAssertion<IEnumerable<T>> list, T expected, string message)
        {

            return new Verifiable<IEnumerable<T>>(list,
                target => Verify.That(target.Contains(expected))
                          .IsTrue(string.Format("The <{0}> should have contained: <{1}>. {2}",
                                    (list.ToString() ?? "NULL"),
                                    (expected.ToString() ?? "NULL"),
                                    (message ?? string.Empty)))
                          .Now());
        }

        public static IVerifiable<IEnumerable> DoesNotContain(this IAssertion<IEnumerable> list, object expected)
        {
            return list.DoesNotContain(expected, "");
        }

        public static IVerifiable<IEnumerable> DoesNotContain(this IAssertion<IEnumerable> list, object expected, string message)
        {
            return new Verifiable<IEnumerable>(list,
                target =>
                {
                    bool found = false;

                    if (list != null)
                        foreach (var o in target)
                            if (o == expected)
                                found = true;

                    Verify.That(found)
                        .IsFalse(string.Format("The <{0}> should not have contained: <{1}>. {2}",
                                            (target ?? "NULL"),
                                            (expected ?? "NULL"),
                                            (message ?? string.Empty)))
                        .Now();
                });
        }

        public static IVerifiable<IEnumerable<T>> DoesNotContain<T>(this IAssertion<IEnumerable<T>> list, T expected)
        {
            return list.DoesNotContain(expected, "");
        }

        public static IVerifiable<IEnumerable<T>> DoesNotContain<T>(this IAssertion<IEnumerable<T>> list, T expected, string message)
        {
            return new Verifiable<IEnumerable<T>>(list, target =>
                Verify.That(target.Contains(expected))
                      .IsFalse(string.Format(
                            "The <{0}> should not have contained: <{1}>. {2}",
                            (list.ToString() ?? "NULL"),
                            (expected.ToString() ?? "NULL"),
                            (message ?? string.Empty)))
                        .Now());
        }

        /// <summary>
        /// Asserts that all elements comply with a Predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="evaluator">The evaluator.</param>
        public static IVerifiable<IEnumerable<T>> IsTrueForAll<T>(this IAssertion<IEnumerable<T>> list, Predicate<T> evaluator, string message = "")
        {
            return new Verifiable<IEnumerable<T>>(list, target =>
                {
                    bool result = false;
                    int index = 0;
                    foreach(T item in target)
                    {
                        result = evaluator(item);
                        if (!result)
                        {
                            message = "Item at index: " + index + " did not comply with condition.\n" + message;
                            break;
                        }

                        index++;
                    }

                    Verify.That(result)
                          .IsTrue(message)
                          .Now();
                });
        }

        /// <summary>
        /// Asserts that all elements DO NOT comply with a Predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="evaluator">The evaluator.</param>
        public static IVerifiable<IEnumerable<T>> IsFalseForAll<T>(this IAssertion<IEnumerable<T>> list, Predicate<T> evaluator, string message = "")
        {
            return list.IsTrueForAll(value => !evaluator(value), message);
        }

        /// <summary>
        /// Asserts that at least one element complies with a Predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="evaluator">The evaluator.</param>
        public static IVerifiable<IEnumerable<T>> IsTrueForAny<T>(this IAssertion<IEnumerable<T>> list, Predicate<T> evaluator, string message = "")
        {
            return new Verifiable<IEnumerable<T>>(list, target =>
                Verify.That(
                target.Any(t => evaluator(t)))
                      .IsTrue(message)
                      .Now());
        }

        /// <summary>
        /// Asserts that at least one element complies with a Predicate
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="evaluator">The evaluator.</param>
        public static IVerifiable<IEnumerable<T>> IsFalseForAny<T>(this IAssertion<IEnumerable<T>> list, Predicate<T> evaluator, string message = "")
        {
            return list.IsTrueForAny(value => !evaluator(value), message);
        }

        public static IVerifiable<IEnumerable<T>> IsOfSize<T>(this IAssertion<IEnumerable<T>> list, int elementCount)
        {
            return new Verifiable<IEnumerable<T>>(list, target =>
            Assert.AreEqual(elementCount, target.Count(), string.Format("Number of elements should be: {0}", elementCount)));

        }

        public static IVerifiable<IEnumerable<T>> HasLessThan<T>(this IAssertion<IEnumerable<T>> list, int elementCount)
        {
            int listCount = list.Target.Count();
            return new Verifiable<IEnumerable<T>>(list, target =>
                Verify.That(listCount < elementCount)
                      .IsTrue(
                            string.Format("Number of elements should be less than: {0} but was {1}",
                            elementCount,
                            listCount))
                       .Now());
        }

        /// <summary>
        /// Verify that a collection should have more than a certain amount of elements
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">The list.</param>
        /// <param name="elementCount">The count.</param>
        public static IVerifiable<IEnumerable<T>> HasMoreThan<T>(this IAssertion<IEnumerable<T>> list, int elementCount)
        {
            int listCount = list.Target.Count();
            return new Verifiable<IEnumerable<T>>(list, target =>
                Verify.That(listCount > elementCount)
                .IsTrue(
                    string.Format("Number of elements should be more than: {0} but was {1}",
                    elementCount,
                    listCount))
                .Now());
        }
    }
}