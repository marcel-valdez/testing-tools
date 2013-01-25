namespace TestingTools
{
  using System;
  using System.Diagnostics;
  using System.Diagnostics.Contracts;
  using System.IO;
  using System.Reflection;
  using NUnit.Framework;

  public class TestEnvironment
  {
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
          "INEXISTANT_FILE.DOES_NOT_EXIST",
        testContext != null ? 
          BuildPathFromTestDir(filename, testContext) :
          "INEXISTANT_FILE.DOES_NOT_EXIST",
      };

      foreach (string path in paths)
      {
        if (FileExists(path))
        {
          return path;
        }
      }

      throw new FileNotFoundException(filename + " not found.");
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
      var stackFrameFile = new FileInfo(new StackFrame(true).GetFileName());
      return stackFrameFile.Directory.FullName
                            + Path.DirectorySeparatorChar
                            + filename;
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
      return Directory.GetCurrentDirectory()
                    + Path.DirectorySeparatorChar
                    + filename;
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
      return Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory
                              + Path.DirectorySeparatorChar
                              + filename);
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
      return Path.GetFullPath(codeBaseFilePath.Substring(0, stringEnd + 1)
                                              .Replace("file:///", "") + filename);
    }

    /// <summary>
    /// Builds the path from test deployment.
    /// </summary>
    /// <param name="filename">The filename.</param>
    /// <param name="testContext">The test context.</param>
    /// <returns>
    /// A full file path based on the test deployment directory.
    /// </returns>    
    private static string BuildPathFromTestDeployment(
      string filename, 
      TestContext testContext)
    {
      Contract.Requires(testContext != null);
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
    private static string BuildPathFromTestDir(
      string filename, 
      TestContext testContext)
    {
      return Path.GetFullPath(
              Path.Combine(
                testContext.TestDirectory, filename));
    }
  }
}