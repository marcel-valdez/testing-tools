namespace TestingTools.Extensions
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using TestingTools.Core;

    public static class Method
    {
        /// <summary>
        /// Tests tha a Func<typeparamref name="T" throws an exception./>
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <param name="function">The function.</param>
        /// <param name="message">The message.</param>
        public static IVerifiable<Func<T>> ThrowsException<T>(this IAssertion<Func<T>> function, string message = "")
        {
            return new Verifiable<Func<T>>(function, target =>
                    Assert.IsNotNull(
                        target.GetThrownException(),
                        "La función debió lanzar una excepción, pero no fue así.\n" + message));
        }

        /// <summary>
        /// Tests that an Action should throw an exception
        /// </summary>
        /// <param name="action">The action.</param>
        /// <param name="message">The message.</param>
        public static IVerifiable<Action> ThrowsException(this IAssertion<Action> action, string message = "")
        {
            return new Verifiable<Action>(action, target =>
            {
                Assert.IsNull(target.GetThrownException(), message);
            });
        }

        /// <summary>
        /// Gets the exception thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="function">The function.</param>
        /// <returns></returns>
        private static Exception GetThrownException<T>(this Func<T> function)
        {
            Exception x = null;
            try
            {
                function();
            }
            catch (Exception ex)
            {
                x = ex;
            }

            return x;
        }

        /// <summary>
        /// Gets the exception thrown.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="action">The action.</param>
        /// <returns></returns>
        private static Exception GetThrownException(this Action action)
        {
            Exception x = null;
            try
            {
                action();
            }
            catch (Exception ex)
            {
                x = ex;
            }

            return x;
        }
    }
}
