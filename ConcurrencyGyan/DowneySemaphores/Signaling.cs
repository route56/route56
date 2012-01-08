using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DowneySemaphores
{
	class Signaling
	{
		private static Semaphore _a1IsDone;

		public static void MainX(string[] args)
		{
			_a1IsDone = new Semaphore(0, 1);

			Thread tA = new Thread(ThreadA);
			tA.Start();

			Thread tB = new Thread(ThreadB);
			tB.Start();
		}

		private static void ThreadA()
		{
			Console.WriteLine("Thread A: Doing A1");
			Helper.RandomSleep();
			Console.WriteLine("Thread A: Sending Signal");
			_a1IsDone.Release();
			Console.WriteLine("Thread A exiting.");
		}

		private static void ThreadB()
		{
			Console.WriteLine("Thread B: Waiting for A1 to finish");
			_a1IsDone.WaitOne();
			Console.WriteLine("Thread B: Got Signal");
			Console.WriteLine("Thread B: Doing B1");
			Helper.RandomSleep();
			Console.WriteLine("Thread B: Exiting");
		}
	}
}

/*
PS x:\ConcurrencyGyan\DowneySemaphores\bin\Debug> .\DowneySemaphores.exe
Thread A: Doing A1
Thread B: Waiting for A1 to finish
Thread A: Sending Signal
Thread B: Got Signal
Thread B: Doing B1
Thread A exiting.
Thread B: Exiting
*/