using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Reflection;
using ProblemHelper;

namespace GCJSolveConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 4)
			{
				Console.WriteLine("usage: GCJSolveConsole ProjName ClassName InputFilePath OutputFilePath");
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
				Console.WriteLine(ex.Message);
			}

			return true;
		}
	}
}
