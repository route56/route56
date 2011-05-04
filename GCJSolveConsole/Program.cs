using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using ProblemHelper;
using System.IO;

namespace GCJSolveConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 4 && args.Length != 5)
			{
				Console.WriteLine("usage: GCJSolveConsole ProjName ClassName InputFilePath OutputFilePath [ExpectedOutputFile]");
				return;
			}

			bool done = false;
			Thread runApp = new Thread(() => { done = RunAppThread(args); });

			runApp.Start();
			TimeSpan timePassed = new TimeSpan(0);
			TimeSpan oneSec = new TimeSpan(0, 0, 1);

			Console.WriteLine("Solving...");
			while (!done)
			{
				ConsoleClearCurrentLine();
				Console.Write(timePassed.ToString());
				Thread.Sleep(1000);
				timePassed += oneSec;
			}
			Console.WriteLine();

			if (args.Length == 5)
			{
				Console.WriteLine(TryFileCompare(args[3], args[4]) ? "Success" : "Failed" );
			}

		}

		private static bool TryFileCompare(string actual, string expected)
		{
			bool result = false;

			try
			{
				result = FileCompare(actual, expected);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return result;
		}

		private static void ConsoleClearCurrentLine()
		{
			Console.CursorLeft = 0;
			Console.Write("                                        ");
			Console.CursorLeft = 0;
		}

		private static bool RunAppThread(string[] args)
		{
			try
			{
				//Type t = typeof(OCAA2011.VanishingNumbers);
				//string s = t.Assembly.FullName.ToString();
				//Console.WriteLine("***");
				//Console.WriteLine(s);
				//Console.WriteLine("***");

				System.Runtime.Remoting.ObjectHandle oh = 
					Activator.CreateInstance(args[0] + ", Version=1.0.0.0, Culture=neutral, PublicKeyToken=null", args[0] + '.' + args[1]);

				IGCJSolution solver = (IGCJSolution)oh.Unwrap();
				solver.Solve(args[2], args[3]);
			}
			catch (Exception ex)
			{
				Console.WriteLine();
				Console.WriteLine(ex.Message);
			}

			return true;
		}

		private static bool FileCompare(string file1, string file2)
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
