using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using ProblemHelper;

namespace GCJTest
{
	[TestClass]
	public class FileTester
	{
		[TestMethod]
		public void FileTest_VanishingNumber_Small()
		{
			ProblemMeta info = new ProblemMeta()
			{
				InputFile = "VNS.in",
				ActualOutputFile = "VNS_Actual.out",
				ExpectedOutputFile = "VNS.out"
			};

			RunTest(info);
		}

		private void RunTest(ProblemMeta info)
		{
			info.RunProblem();
			Assert.IsTrue(FileCompare(info.ExpectedOutputFile, info.ActualOutputFile), "Expected and Actual files don't match");
		}

		private bool FileCompare(string file1, string file2)
		{
			int file1byte;
			int file2byte;
			FileStream fs1;
			FileStream fs2;

			// Determine if the same file was referenced two times.
			if (file1 == file2)
			{
				// Return true to indicate that the files are the same.
				return true;
			}

			// Open the two files.
			fs1 = new FileStream(file1, FileMode.Open);
			fs2 = new FileStream(file2, FileMode.Open);

			// Check the file sizes. If they are not the same, the files 
			// are not the same.
			if (fs1.Length != fs2.Length)
			{
				// Close the file
				fs1.Close();
				fs2.Close();

				// Return false to indicate files are different
				return false;
			}

			// Read and compare a byte from each file until either a
			// non-matching set of bytes is found or until the end of
			// file1 is reached.
			do
			{
				// Read one byte from each file.
				file1byte = fs1.ReadByte();
				file2byte = fs2.ReadByte();
			}
			while ((file1byte == file2byte) && (file1byte != -1));

			// Close the files.
			fs1.Close();
			fs2.Close();

			// Return the success of the comparison. "file1byte" is 
			// equal to "file2byte" at this point only if the files are 
			// the same.
			return ((file1byte - file2byte) == 0);
		}
	}
}
