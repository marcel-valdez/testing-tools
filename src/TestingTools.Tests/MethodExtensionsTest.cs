// -----------------------------------------------------------------------
// <copyright file="MethodExtensionsTest.cs" company="">
// Marcel Valdez, Copyright (r) 2012
// </copyright>
// -----------------------------------------------------------------------

namespace TestingTools.Tests
{
  using System;
  using System.Collections.Generic;
  using System.Linq;
  using System.Text;
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
    /// <param name="actualWritten">The actual written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>    
    [Test, Category("IntegrationTest")]
    [TestCase("bad", "test", false)]
    [TestCase("test", "test", true)]
    public void TestIfItCanVerifyExactConsoleOutput(string expectedWritten, string actualWritten, bool shouldPass)
    {
      // Arrange
      Action targetAct = () => Console.Write(actualWritten);
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
        AssertionException execption = Assert.Throws<AssertionException>(act);
      }
    }

    /// <summary>
    /// Tests if it can verify console output.
    /// </summary>
    /// <param name="expectedWritten">The expected written.</param>
    /// <param name="actualWritten">The actual written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>    
    [Test, Category("IntegrationTest")]
    [TestCase("bad", "test", false)]
    [TestCase("bad", "", false)]
    [TestCase("test", "test", true)]
    [TestCase("test", "testA", true)]
    [TestCase("test", "Atest", true)]
    [TestCase("test", "AtestA", true)]
    public void TestIfItCanVerifyConsoleOutput(string expectedWritten, string actualWritten, bool shouldPass)
    {
      // Arrange
      Action targetAct = () => Console.Write(actualWritten);
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
        AssertionException execption = Assert.Throws<AssertionException>(act);
      }
    }

    /// <summary>
    /// Tests if it can verify console output.
    /// </summary>
    /// <param name="expectedWritten">The expected written.</param>
    /// <param name="actualWritten">The actual written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>    
    [Test, Category("IntegrationTest")]
    [TestCase("bad", "test", true)]
    [TestCase("test", "test", false)]
    public void TestIfItCanVerifyExactBadConsoleOutput(string expectedWritten, string actualWritten, bool shouldPass)
    {
      // Arrange
      Action targetAct = () => Console.Write(actualWritten);
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
        AssertionException execption = Assert.Throws<AssertionException>(act);
      }
    }

    /// <summary>
    /// Tests if it can verify console output.
    /// </summary>
    /// <param name="expectedWritten">The expected written.</param>
    /// <param name="actualWritten">The actual written.</param>
    /// <param name="shouldPass">if set to <c>true</c> [should pass].</param>    
    [Test, Category("IntegrationTest")]
    [TestCase("bad", "test", true)]
    [TestCase("bad", "", true)]
    [TestCase("test", "test", false)]
    [TestCase("test", "testA", false)]
    [TestCase("test", "Atest", false)]
    [TestCase("test", "AtestA", false)]
    public void TestIfItCanVerifyBadConsoleOutput(string expectedWritten, string actualWritten, bool shouldPass)
    {
      // Arrange
      Action targetAct = () => Console.Write(actualWritten);
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
        AssertionException execption = Assert.Throws<AssertionException>(act);
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
