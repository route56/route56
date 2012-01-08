using System;
using System.Threading;

namespace DowneySemaphores
{
	class Helper
	{
		public static void RandomSleep()
		{
			Random r = new Random();
			Thread.Sleep(100*r.Next(2,10));
		}

		public static void ConsoleWriteLineThreadId(string msg)
		{
			Console.WriteLine("TID {0}: {1}", Thread.CurrentThread.ManagedThreadId, msg);
		}
	}
}
