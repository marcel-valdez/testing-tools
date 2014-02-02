namespace TestingTools
{
  using System;
  using NUnit.Framework;
  using TestingTools.Core;

  public static class StringExtensions
  {
    public static IVerifiable<string> DoesStartWith(this IAssertion<string> actual, string expected)
    {
      return new Verifiable<string>(actual, value => Assert.IsTrue(value.StartsWith(expected)));
    }

    public static IVerifiable<string> DoesStartWith(this IAssertion<string> actual, string expected, string message)
    {
      return new Verifiable<string>(actual, value =>
          {
            if (value == null || !value.StartsWith(expected))
            {
              Assert.Fail(
                  string.Format(
                      "Actual: <{0}> did not start with the Expected: <{1}>. {2}",
                          (actual.Target ?? "NULL"),
                          (expected ?? "NULL"),
                          (message ?? string.Empty)));
            }
          });
    }

    public static IVerifiable<string> DoesEndWith(this IAssertion<string> actual, string expected)
    {
      return new Verifiable<string>(actual, target => Assert.IsTrue(target.EndsWith(expected)));
    }

    public static IVerifiable<string> DoesEndWith(this IAssertion<string> actual, string expected, string message)
    {
      return new Verifiable<string>(actual, target =>
          {
            if (target == null || !target.EndsWith(expected))
            {
              Assert.Fail(
                  string.Format(
                      "Actual: <{0}> did not end with the Expected: <{1}>. {2}",
                          (target ?? "NULL"),
                          (expected ?? "NULL"),
                          (message ?? string.Empty)));
            }
          });
    }

    public static IVerifiable<string> EqualsIgnoringCase(this IAssertion<string> actual, string expected)
    {
      return new Verifiable<string>(actual, target =>
      {
        if (target.ToLower() != expected.ToLower())
        {
          Assert.AreEqual(expected, target);
        }
      });
    }

    public static IVerifiable<string> EqualsIgnoringCase(this IAssertion<string> actual, string expected, string message)
    {
      return new Verifiable<string>(actual, target =>
          {
            if (target.ToLower() != expected.ToLower())
            {
              Assert.AreEqual(expected, target, message);
            }
          });
    }

    /// <summary>
    /// Verifies that a string contains another
    /// </summary>
    /// <param name="actual">The actual.</param>
    /// <param name="expected">The expected.</param>
    /// <param name="message">The message.</param>
    public static IVerifiable<string> DoesContain(this IAssertion<string> actual, string expected, string message = null)
    {
      return new Verifiable<string>(actual, target =>
          {
            string defaultMessage = string.Format("<{0}> should have contained <{1}>", actual.Target ?? "", expected ?? "");
            message = message == null ? "" : message + "\n";
            Assert.IsTrue(target.Contains(expected), String.Format("{0}{1}", message, defaultMessage));
          });
    }

    /// <summary>
    /// Verifies that a string doesn't contain another
    /// </summary>
    /// <param name="actual">The actual.</param>
    /// <param name="expected">The expected.</param>
    /// <param name="message">The message.</param>
    public static IVerifiable<string> DoesNotContain(this IAssertion<string> actual, string expected, string message = null)
    {
      return new Verifiable<string>(actual, target =>
          {
            string defaultMessage = string.Format("<{0}> should have not contained <{1}>", actual.Target ?? "", expected ?? "");
            message = message == null ? "" : message + "\n";
            Assert.IsTrue(target.Contains(expected), String.Format("{0}{1}", message, defaultMessage));
          });
    }
  }
}
