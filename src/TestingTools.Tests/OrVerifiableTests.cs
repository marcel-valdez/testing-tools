namespace TestingTools.Tests
{
  using System;
  using NUnit.Framework;

  /// <summary>
  /// Summary description for OrVerifiableTests
  /// </summary>
  [TestFixture]
  public class OrVerifiableTests
  {
    public OrVerifiableTests()
    {
    }

    /// <summary>
    ///Gets or sets the test context which provides
    ///information about and functionality for the current test run.
    ///</summary>
    public TestContext TestContext
    {
      get;
      set;
    }

    [Test]
    public void TestIfCanVerifyATrueOrAssertionOnInnerPredicament()
    {
      // Arrange
      object target = new object();

      // Act
      Verify.That(target)
            .IsNull()
            .Or(t => t.IsNotNull())
            .Now();

      // Assert
      // Empty
    }

    [Test]
    public void TestIfCanVerifyAFailingOrAssertionWithInnerPredicament()
    {
      // Arrange
      object target = new object();
      Exception expected = null;

      // Act
      try
      {
        Verify.That(target)
              .IsNull()
              .Or(t => t.IsNull())
              .Now();
      }
      catch (Exception ex)
      {
        expected = ex;
      }

      // Assert
      Assert.IsNotNull(expected);
    }

    [Test]
    public void TestIfCanVerifyAValidOrAssertionWithOuterPredicament()
    {
      // Arrange
      object target = new object();
      Exception expected = null;

      // Act
      try
      {
        Verify.That(target)
              .IsNotNull()
              .Or(t => t.IsNull())
              .Now();
      }
      catch (Exception ex)
      {
        expected = ex;
      }

      // Assert
      Assert.IsNull(expected);
    }
  }
}
