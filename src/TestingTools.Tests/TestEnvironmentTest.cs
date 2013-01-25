// -----------------------------------------------------------------------
// <copyright file="TestEnvironmentTest.cs" company="">
// Marcel Valdez Orozco. Copyright(r) 2012
// </copyright>
// -----------------------------------------------------------------------

namespace TestingTools.Tests
{
  using System.IO;
  using NUnit.Framework;

  /// <summary>
  /// This class contains the unit tests for the TestEnvironment class.
  /// </summary>
  [TestFixture]
  public class TestEnvironmentTest
  {
    /// <summary>
    /// Tests if it gets full file path for existing files.
    /// </summary>    
    [Test]
    public void TestIfItGetsFullFilePathForExistingFiles()
    {
      // Arrange
      string existingFile = @"out\existing_file.txt";
      string expected = Path.GetFullPath(existingFile);

      // Act
      string actual = TestEnvironment.GetExecutionFilepath(existingFile);

      // Assert
      Assert.That(actual, Is.EqualTo(expected));
    }
  }
}
