using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DowneySemaphores
{
	class Mutex
	{
		private static Semaphore _mutex;
		private static int _sharedCount = 0;

		public static void MainX(string[] args)
		{
			_mutex = new Semaphore(1, 1);

			Thread tA = new Thread(ThreadA);
			tA.Start();

			Thread tB = new Thread(ThreadB);
			tB.Start();
		}

		private static void ThreadA()
		{
			Console.WriteLine("Thread A: Doing A1");
			Helper.RandomSleep();
			Console.WriteLine("Thread A: Getting lock to critical section");
			_mutex.WaitOne();
			Console.WriteLine("Thread A: Aquired lock. In critical section");
			_sharedCount++;
			Helper.RandomSleep();
			Console.WriteLine("Thread A: Releasing Lock");
			_mutex.Release();
			Helper.RandomSleep();
			Console.WriteLine("Thread A: Exit");
		}

		private static void ThreadB()
		{
			Console.WriteLine("Thread B: Doing B1");
			Helper.RandomSleep();
			Console.WriteLine("Thread B: Getting lock to critical section");
			_mutex.WaitOne();
			Console.WriteLine("Thread B: Aquired lock. In critical section");
			_sharedCount++;
			Helper.RandomSleep();
			Console.WriteLine("Thread B: Releasing Lock");
			_mutex.Release();
			Helper.RandomSleep();
			Console.WriteLine("Thread B: Exit");
		}
	}
}
/*
PS x:\ConcurrencyGyan\DowneySemaphores\bin\Debug> .\DowneySemaphores.exe
Thread A: Doing A1
Thread B: Doing B1
Thread A: Getting lock to critical section
Thread A: Aquired lock. In critical section
Thread B: Getting lock to critical section
Thread A: Releasing Lock
Thread B: Aquired lock. In critical section
Thread A: Exit
Thread B: Releasing Lock
Thread B: Exit
*/