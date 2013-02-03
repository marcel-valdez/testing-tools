// ***********************************************************************
// Assembly         : TestingTools.Tests
// Author           : Marcel Valdez
// Created          : 02-02-2013
//
// Last Modified By : Marcel Valdez
// Last Modified On : 02-03-2013
// ***********************************************************************
// <copyright file="MethodExtensionsTest.cs" company="Marcel Valdez">
//     Marcel Valdez. All rights reserved.
// </copyright>
// ***********************************************************************

namespace TestingTools.Tests
{
  using System;
  using NUnit.Framework;
  using TestingTools.Core;
  using TestingTools.Extensions;

  /// <summary>
  /// This class contains the unit tests for the MethodExtensions class
  /// </summary>
  [TestFixture]
  public class MethodExtensionsTest
  {

    /// <summary>
    /// Tests if it can verify console output.
    /// </summary>
    /// <param name="expectedWritten">The expected written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>
    /// <param name="actualWritten">The actual written.</param>
    [Test, Category("IntegrationTest")]
    [TestCase("bad", false, new[] { "test" })]
    [TestCase("test", true, new[] { "test" })]
    public void TestIfItCanVerifyExactConsoleOutput(string expectedWritten, bool shouldPass, params string[] actualWritten)
    {
      // Arrange
      Action targetAct = () =>
      {
        foreach (string msg in actualWritten)
        {
          Console.Write(msg);
        }
      };
      var target = Verify.That(targetAct).WritesExactlyToConsole(expectedWritten);

      // Act
      TestDelegate act = () => target.Now();

      // Assert
      if (shouldPass)
      {
        Assert.DoesNotThrow(act);
      }
      else
      {
        Assert.Throws<AssertionException>(act);
      }
    }

    /// <summary>
    /// Tests if it can verify console output.
    /// </summary>
    /// <param name="expectedWritten">The expected written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>
    /// <param name="actualWritten">The actual written.</param>
    [Test, Category("IntegrationTest")]
    [TestCase("bad", false, new[] { "test" })]
    [TestCase("bad", false, new[] { "" })]
    [TestCase("test", true, new[] { "test" })]
    [TestCase("test", true, new[] { "testA" })]
    [TestCase("test", true, "test", "A")]
    [TestCase("test", true, new[] { "Atest" })]
    [TestCase("test", true, "A", "test")]
    [TestCase("test", true, new[] { "AtestA" })]
    [TestCase("test", true, "A", "test", "A")]
    [TestCase("test", true, new[] { "A\ntest\nA" })]
    [TestCase("test", true, "A\n", "test\n", "A")]
    public void TestIfItCanVerifyConsoleOutput(string expectedWritten, bool shouldPass, params string[] actualWritten)
    {
      // Arrange
      Action targetAct = () =>
      {
        foreach (string msg in actualWritten)
        {
          Console.Write(msg);
        }
      };

      var target = Verify.That(targetAct).WritesToConsole(expectedWritten);

      // Act
      TestDelegate act = () => target.Now();

      // Assert
      if (shouldPass)
      {
        Assert.DoesNotThrow(act);
      }
      else
      {
        Assert.Throws<AssertionException>(act);
      }
    }

    /// <summary>
    /// Tests if it can verify console output.
    /// </summary>
    /// <param name="expectedWritten">The expected written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>
    /// <param name="actualWritten">The actual written.</param>
    [Test, Category("IntegrationTest")]
    [TestCase("bad", true, new[] { "test" })]
    [TestCase("test", false, new[] { "test" })]
    public void TestIfItCanVerifyExactBadConsoleOutput(string expectedWritten, bool shouldPass, params string[] actualWritten)
    {
      // Arrange
      Action targetAct = () =>
      {
        foreach (string msg in actualWritten)
        {
          Console.Write(msg);
        }
      };
      var target = Verify.That(targetAct).DoesntWriteExactlyToConsole(expectedWritten);

      // Act
      TestDelegate act = () => target.Now();

      // Assert
      if (shouldPass)
      {
        Assert.DoesNotThrow(act);
      }
      else
      {
        Assert.Throws<AssertionException>(act);
      }
    }

    /// <summary>
    /// Tests if it can verify console output.
    /// </summary>
    /// <param name="expectedWritten">The expected written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>
    /// <param name="actualWritten">The actual written.</param>
    [Test, Category("IntegrationTest")]
    [TestCase("bad", true, new[] { "test" })]
    [TestCase("bad", true, new[] { "" })]
    [TestCase("test", false, new[] { "test" })]
    [TestCase("test", false, new[] { "testA" })]
    [TestCase("test", false, new[] { "Atest" })]
    [TestCase("test", false, new[] { "AtestA" })]
    [TestCase("test", false, new[] { "A\ntest\nA" })]
    public void TestIfItCanVerifyBadConsoleOutput(string expectedWritten, bool shouldPass, params string[] actualWritten)
    {
      // Arrange
      Action targetAct = () =>
      {
        foreach (string msg in actualWritten)
        {
          Console.Write(msg);
        }
      };
      var target = Verify.That(targetAct).DoesntWriteToConsole(expectedWritten);

      // Act
      TestDelegate act = () => target.Now();

      // Assert
      if (shouldPass)
      {
        Assert.DoesNotThrow(act);
      }
      else
      {
        Assert.Throws<AssertionException>(act);
      }
    }

    [Test]
    public void TestIfItSendsExceptionMessage()
    {
      // Arrange
      string expectedMsg = "test message";
      Action targetAct = () => Console.Write("0");
      var target = Verify.That(targetAct).WritesExactlyToConsole("1", expectedMsg);

      // Act
      TestDelegate act = (TestDelegate)(() => target.Now());
      AssertionException exception = Assert.Throws<AssertionException>(act);

      // Assert      
      Assert.That(exception.Message, Is.StringContaining(expectedMsg));
    }
  }
}
