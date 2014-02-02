namespace TestingTools
{
  using System;
  using System.IO;
  using NUnit.Framework;
  using TestingTools.Core;

  public static class MethodExtensions
  {
    /// <summary>
    /// Tests tha a Func<typeparamref name="T"></typeparamref> throws an exception.
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <param name="function">The function.</param>
    /// <param name="message">The message.</param>
    public static IVerifiable<Func<T>> ThrowsException<T>(
      this IAssertion<Func<T>> function,
      string message = "")
    {
      return new Verifiable<Func<T>>(function, target =>
              Assert.IsNotNull(target.GetThrownException(),
                              string.Format("The function should've not thrown an exception, but it did.\n{0}",
                                            message)));
    }

    /// <summary>
    /// Tests that an Action should throw an exception
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="message">The message.</param>
    public static IVerifiable<Action> ThrowsException(
      this IAssertion<Action> action,
      string message = "")
    {
      return new Verifiable<Action>(action, target =>
      {
        Assert.IsNotNull(target.GetThrownException(), message);
      });
    }

    /// <summary>
    /// Tests tha a Func<typeparamref name="T"/> does not throw an exception.
    /// </summary>
    /// <typeparam name="T">Return type</typeparam>
    /// <param name="function">The function.</param>
    /// <param name="message">The message.</param>
    public static IVerifiable<Func<T>> DoesNotThrowException<T>(
      this IAssertion<Func<T>> function,
      string message = "")
    {
      return new Verifiable<Func<T>>(function, target =>
              Assert.IsNull(
                  target.GetThrownException(),
                  string.Format(
                    "The function should've not thrown an exception, but it did.\n{0}",
                    message)));
    }

    /// <summary>
    /// Tests that an Action should not throw an exception
    /// </summary>
    /// <param name="action">The action.</param>
    /// <param name="message">The message.</param>
    public static IVerifiable<Action> DoesNotThrowException(
      this IAssertion<Action> action,
      string message = "")
    {
      return new Verifiable<Action>(action, target =>
      {
        Assert.IsNull(target.GetThrownException(), message);
      });
    }


    /// <summary>
    /// Verifies that a methods writes exactly some output to console.
    /// </summary>
    /// <param name="assertion">The action assertion.</param>
    /// <param name="expectedWritten">The expected written message to console.</param>
    /// <param name="message">The message for the error.</param>
    /// <returns>The verification continuation</returns>    
    public static IVerifiable<Action> WritesExactlyToConsole(
      this IAssertion<Action> assertion,
      string expectedWritten,
      string message = "")
    {
      return new Verifiable<Action>(assertion, target =>
      {

        StringWriter consoleOut = new StringWriter();
        Console.SetOut(consoleOut);
        target();
        string actualWritten = consoleOut.ToString();
        Console.SetOut(Console.Out);
        Assert.AreEqual(actualWritten,
                        expectedWritten,
                        message);
      });
    }

    /// <summary>
    /// Verifies that a methods does not write exactly some output to console.
    /// </summary>
    /// <param name="assertion">The action assertion.</param>
    /// <param name="expectedWritten">The expected message not written to console.</param>
    /// <param name="message">The message for the error.</param>
    /// <returns>The verification continuation</returns>    
    public static IVerifiable<Action> DoesntWriteExactlyToConsole(
      this IAssertion<Action> assertion,
      string expectedWritten,
      string message = "")
    {
      return new Verifiable<Action>(assertion, target =>
      {

        StringWriter consoleOut = new StringWriter();
        Console.SetOut(consoleOut);
        target();
        string actualWritten = consoleOut.ToString();
        Console.SetOut(Console.Out);
        Assert.AreNotEqual(actualWritten,
                           expectedWritten,
                           message);
      });
    }


    /// <summary>
    /// Verifies that a methods writes exactly some output to console.
    /// </summary>
    /// <param name="assertion">The action assertion.</param>
    /// <param name="expectedWritten">The expected written message to console.</param>
    /// <param name="message">The message for the error.</param>
    /// <returns>The verification continuation</returns>    
    public static IVerifiable<Action> WritesToConsole(
      this IAssertion<Action> assertion,
      string expectedWritten,
      string message = "")
    {
      return new Verifiable<Action>(assertion, target =>
      {

        StringWriter consoleOut = new StringWriter();
        Console.SetOut(consoleOut);
        target();
        string actualWritten = consoleOut.ToString();
        Console.SetOut(Console.Out);
        Assert.That(actualWritten,
                    Is.StringContaining(expectedWritten),
                    message);
      });
    }

    /// <summary>
    /// Verifies that a methods does not write some output to console.
    /// </summary>
    /// <param name="assertion">The action assertion.</param>
    /// <param name="expectedWritten">The written message not expected.</param>
    /// <param name="message">The message for the error.</param>
    /// <returns>The verification continuation</returns>    
    public static IVerifiable<Action> DoesntWriteToConsole(
      this IAssertion<Action> assertion,
      string expectedWritten,
      string message = "")
    {
      return new Verifiable<Action>(assertion, target =>
      {

        StringWriter consoleOut = new StringWriter();
        Console.SetOut(consoleOut);
        target();
        string actualWritten = consoleOut.ToString();
        Console.SetOut(Console.Out);
        Assert.That(actualWritten,
                    Is.Not.StringContaining(expectedWritten),
                    message);
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
