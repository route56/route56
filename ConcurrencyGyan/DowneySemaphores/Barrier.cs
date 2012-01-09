using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DowneySemaphores
{
	class Barrier
	{
		private static Semaphore _mutex;
		private static Semaphore _gogogo;
		private static int _threadCount;

		public static void MainX(string[] args)
		{
			int threadsToCreate = _threadCount = 7;
			_mutex = new Semaphore(1, 1);
			_gogogo = new Semaphore(0, 1);

			for (int i = 0; i < threadsToCreate; i++)
			{
				Thread tA = new Thread(ThreadCode);
				tA.Name = i.ToString();
				tA.Start();
			}
		}

		private static void ThreadCode()
		{
			Helper.ConsoleWriteLineThreadName("Started");
			Helper.RandomSleep();

			Helper.ConsoleWriteLineThreadName("Acquiring mutex...");
			_mutex.WaitOne();
			_threadCount--;
			Helper.ConsoleWriteLineThreadName(string.Format("Critical section: ThreadCount = {0}", _threadCount));
			Helper.ConsoleWriteLineThreadName("Released mutex.");
			_mutex.Release();

			// below check needs to be in critical section? No.
			// but below can be signaled multiple times if context switch happens between above mutex release and this check.
			if (_threadCount == 0)
			{
				Helper.ConsoleWriteLineThreadName("Trigering GoGoGo");
				_gogogo.Release();
			}
			else // this else isn't really needed
			{
				// below is called turnstile.
				Helper.ConsoleWriteLineThreadName("Waiting for the GoGoGo");
				_gogogo.WaitOne();
				Helper.ConsoleWriteLineThreadName("Got GoGoGo, Signaling same.");
				_gogogo.Release();
				// by end of n't thread turnstile will be 1.
			}

			Helper.RandomSleep();
			Helper.ConsoleWriteLineThreadName("Exited");
		}
	}
}
/*
PS x:\ConcurrencyGyan\DowneySemaphores\bin\Debug> .\DowneySemaphores.exe
TID 3: Started
TID 4: Started
TID 5: Started
TID 6: Started
TID 7: Started
TID 8: Started
TID 9: Started
TID 3: Acquiring mutex...
TID 3: Critical section: ThreadCount = 6
TID 3: Released mutex.
TID 3: Waiting for the GoGoGo
TID 4: Acquiring mutex...
TID 4: Critical section: ThreadCount = 5
TID 4: Released mutex.
TID 4: Waiting for the GoGoGo
TID 5: Acquiring mutex...
TID 5: Critical section: ThreadCount = 4
TID 5: Released mutex.
TID 5: Waiting for the GoGoGo
TID 6: Acquiring mutex...
TID 6: Critical section: ThreadCount = 3
TID 6: Released mutex.
TID 6: Waiting for the GoGoGo
TID 7: Acquiring mutex...
TID 7: Critical section: ThreadCount = 2
TID 7: Released mutex.
TID 7: Waiting for the GoGoGo
TID 8: Acquiring mutex...
TID 8: Critical section: ThreadCount = 1
TID 8: Released mutex.
TID 8: Waiting for the GoGoGo
TID 9: Acquiring mutex...
TID 9: Critical section: ThreadCount = 0
TID 9: Released mutex.
TID 9: Trigering GoGoGo
TID 3: Got GoGoGo, Signaling same.
TID 4: Got GoGoGo, Signaling same.
TID 5: Got GoGoGo, Signaling same.
TID 6: Got GoGoGo, Signaling same.
TID 7: Got GoGoGo, Signaling same.
TID 8: Got GoGoGo, Signaling same.
TID 9: Exited
TID 3: Exited
TID 4: Exited
TID 5: Exited
TID 6: Exited
TID 7: Exited
TID 8: Exited
*/