using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace GCJSolveConsole
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 3)
			{
				Console.WriteLine("usage: GCJSolveConsole ClassName InputFilePath OutputFilePath");
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
				Thread.Sleep(5000);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return true;
		}
	}
}
