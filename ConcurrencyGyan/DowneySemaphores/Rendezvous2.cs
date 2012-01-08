using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DowneySemaphores
{
	// Involves extra context switch, not efficient
	class Rendezvous2
	{
		private static Semaphore _aArrived;
		private static Semaphore _bArrived;

		public static void MainX(string[] args)
		{
			_aArrived = new Semaphore(0, 1);
			_bArrived = new Semaphore(0, 1);

			Thread tA = new Thread(ThreadA);
			tA.Start();

			Thread tB = new Thread(ThreadB);
			tB.Start();
		}

		private static void ThreadA()
		{
			Console.WriteLine("Thread A: Doing A1");
			Helper.RandomSleep();
			Console.WriteLine("Thread A: Waiting for bArrived");
			_bArrived.WaitOne(); // wait
			Console.WriteLine("Thread A: Signal aArrived");
			_aArrived.Release(); // signal
			Console.WriteLine("Thread A: Doing A2");
			Helper.RandomSleep();
			Console.WriteLine("Thread A: Exit");
		}

		private static void ThreadB()
		{
			Console.WriteLine("Thread B: Doing B1");
			Helper.RandomSleep();
			Console.WriteLine("Thread B: Signal bArrived");
			_bArrived.Release(); // signal
			Console.WriteLine("Thread B: Waiting for aArrived");
			_aArrived.WaitOne(); // wait
			Console.WriteLine("Thread B: Doing B2");
			Helper.RandomSleep();
			Console.WriteLine("Thread B: Exit");
		}
	}
}

/*
PS x:\ConcurrencyGyan\DowneySemaphores\bin\Debug> .\DowneySemaphores.exe
Thread A: Doing A1
Thread B: Doing B1
Thread A: Waiting for bArrived
Thread B: Signal bArrived
Thread B: Waiting for aArrived
Thread A: Signal aArrived
Thread A: Doing A2
Thread B: Doing B2
Thread A: Exit
Thread B: Exit
*/