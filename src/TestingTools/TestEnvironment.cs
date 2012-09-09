namespace TestingTools
{
    using NUnit.Framework;
    using System;    
    using System.IO;
    using System.Diagnostics;
	using System.Reflection;
	
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
			if (FileExists(filename))
			{
				return filename;
			}
				
			if (testContext != null)
			{
                string deploymentPath = testContext.TestDirectory + "/" + filename;
				if (FileExists(deploymentPath))
				{
					return deploymentPath;
				}

				string testDeploymentPath = testContext.WorkDirectory + "/" + filename;
				if (FileExists(testDeploymentPath))
				{
					return testDeploymentPath;
				}				
			}

			string codeBaseFilePath = Assembly.GetCallingAssembly().EscapedCodeBase;
			int stringEnd = codeBaseFilePath.LastIndexOf('/');
			string codebaseFilePath = codeBaseFilePath.Substring(0, stringEnd + 1)
								   .Replace("file:///", "") + filename;

			if (FileExists(codebaseFilePath))
			{
				return codebaseFilePath;
			}

			string appDomainPath = AppDomain.CurrentDomain.BaseDirectory + "/" + filename;
			if (FileExists(appDomainPath))
			{
				return appDomainPath;
			}


			string currentPath = Directory.GetCurrentDirectory() + "/" + filename;
			if (FileExists(currentPath))
			{
				return currentPath;
			}

			var stackFrameFile = new FileInfo(new StackFrame(true).GetFileName());
			string stackFramePath = stackFrameFile.Directory.FullName + "/" + filename;
			if (FileExists(stackFramePath))
			{
				return stackFramePath;
			}

			throw new Exception(filename + " not found.");
		}
		
		private static bool FileExists(string filepath)
        {
            return new FileInfo(filepath).Exists;
        }
	}
}