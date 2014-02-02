// -----------------------------------------------------------------------
// <copyright file="TestEnvironmentTest.cs" company="">
// Marcel Valdez Orozco. Copyright(r) 2012
// </copyright>
// -----------------------------------------------------------------------

namespace NVerify.Tests
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
    /// <param name="existingFile">An existing file path</param>
    [Test]
    [TestCase(@"out\existing_file.txt")]
    [TestCase("out/existing_file.txt")]
    public void TestIfItGetsFullFilePathForExistingFiles(string existingFile)
    {
      // Arrange
      string expected = Path.GetFullPath(existingFile);

      // Act
      string actual = TestEnvironment.GetExecutionFilepath(existingFile);

      // Assert
      Assert.That(actual, Is.EqualTo(expected));
    }

    [Test]
    public void TestIfItThrowsOnNonExistantFile()
    {
      // Arrange
      string nonExisting = @"out\non_existing";
      string expected = Path.GetFullPath(nonExisting);

      // Act
      TestDelegate act = () => TestEnvironment.GetExecutionFilepath(nonExisting);

      // Assert
      Assert.Throws<FileNotFoundException>(act);
    }
  }
}
