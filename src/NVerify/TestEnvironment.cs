namespace NVerify
{
  using System;
  using System.Diagnostics;
  using System.IO;
  using System.Reflection;
  using NUnit.Framework;

  public class TestEnvironment
  {
    private const string INEXISTANT_FILE = "INEXISTANT_FILE.DOES_NOT_EXIST";
    /// <summary>
    /// Gets the full file path of an existing output file in the execution environment
    /// </summary>
    /// <returns>The full path to the file.</returns>
    /// <param name="testContext">The test context from which to attempt to extract the path</param>
    /// <param name="filename">The filename without path</param>
    public static string GetExecutionFilepath(string filename, TestContext testContext = null)
    {

      string[] paths = new string[] {
        Path.GetFullPath(filename),
        BuildPathFromCodeBaseDir(filename),
        BuildPathFromCurrentDir(filename),
        BuildPathFromDomainDir(filename),
        BuildPathFromStackFrame(filename),
        testContext != null ? 
          BuildPathFromTestDeployment(filename, testContext) : 
          INEXISTANT_FILE,
        testContext != null ? 
          BuildPathFromTestDir(filename, testContext) :
          INEXISTANT_FILE,
      };

      foreach (string path in paths)
      {
        if (FileExists(path))
        {
          return path;
        }
      }

      throw new FileNotFoundException(string.Format("{0} not found.", filename));
    }

    /// <summary>
    /// Checks if the given file exists.
    /// </summary>
    /// <param name="filepath">The filepath.</param>
    /// <returns>true if the file exists, false otherwise.</returns>    
    private static bool FileExists(string filepath)
    {
      return new FileInfo(filepath).Exists;
    }

    /// <summary>
    /// Builds the path stack frame path.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns>The file path based on stack frame path</returns>    
    private static string BuildPathFromStackFrame(string filename)
    {
      var stackFrame = new StackFrame(true);
      string stackFrameFilePath = stackFrame.GetFileName();
      if (string.IsNullOrEmpty(stackFrameFilePath))
      {
        return INEXISTANT_FILE;
      }

      return Path.GetFullPath(
              Path.Combine(Path.GetDirectoryName(stackFrameFilePath), filename));
    }

    /// <summary>
    /// Builds the path from current working directory.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns>
    /// A full file path attempt from the current working directory.
    /// </returns>    
    private static string BuildPathFromCurrentDir(string filename)
    {
      return Path.GetFullPath(
              Path.Combine(Directory.GetCurrentDirectory(), filename));
    }

    /// <summary>
    /// Builds the path from domain base directory.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns>
    /// A full file path based on the app domain
    /// base directory.
    /// </returns>    
    private static string BuildPathFromDomainDir(string filename)
    {
      return Path.GetFullPath(
              Path.Combine(AppDomain.CurrentDomain.BaseDirectory, filename));
    }

    /// <summary>
    /// Builds the path from code base directory.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <returns>
    /// A full file path based on the code base directory
    /// </returns>
    private static string BuildPathFromCodeBaseDir(string filename)
    {
      string codeBaseFilePath = Assembly.GetCallingAssembly().EscapedCodeBase;
      int stringEnd = codeBaseFilePath.LastIndexOf(Path.DirectorySeparatorChar);
      return Path.GetFullPath(
                Path.Combine(codeBaseFilePath.Substring(0, stringEnd + 1)
                                              .Replace("file:///", ""),
                              filename));
    }

    /// <summary>
    /// Builds the path from test deployment.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <param name="testContext">The test context.</param>
    /// <returns>
    /// A full file path based on the test deployment directory.
    /// </returns>    
    private static string BuildPathFromTestDeployment(string filename, TestContext testContext)
    {
      if (testContext == null)
        throw new ArgumentException("Test context must not be null.");

      return Path.GetFullPath(
              Path.Combine(testContext.WorkDirectory, filename));
    }

    /// <summary>
    /// Builds the path from test directory.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <param name="testContext">The test context.</param>
    /// <returns>
    /// A full file path based on the test directory.
    /// </returns>    
    private static string BuildPathFromTestDir(string filename, TestContext testContext)
    {
      return Path.GetFullPath(
              Path.Combine(testContext.TestDirectory, filename));
    }
  }
}