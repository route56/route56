using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DowneySemaphores
{
	class ReusableBarrier
	{
		private static Semaphore _mutex;
		private static Semaphore _mutex2;
		private static Semaphore _gogogo;
		private static int _threadRemainingCount;
		private static int _threadsToCreate;
		private static int _threadGoGoGoCount;

		public static void MainX(string[] args)
		{
			_threadsToCreate = _threadRemainingCount = 4;
			_mutex = new Semaphore(1, 1);
			_mutex2 = new Semaphore(1, 1);
			_gogogo = new Semaphore(0, 1);

			for (int i = 0; i < _threadsToCreate; i++)
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

			Helper.ConsoleWriteLineThreadName("Entering barrier code");
			BarrierCode();
			Helper.ConsoleWriteLineThreadName("Exiting barrier code");

			Helper.RandomSleep();

			Helper.ConsoleWriteLineThreadName("Entering barrier code again");
			BarrierCode();
			Helper.ConsoleWriteLineThreadName("Exiting barrier code again");

			Helper.RandomSleep();
			Helper.ConsoleWriteLineThreadName("Exited");
		}

		private static void BarrierCode()
		{
			_mutex.WaitOne();
			_threadRemainingCount--;
			Helper.ConsoleWriteLineThreadName(string.Format("Critical section: ThreadCount = {0}", _threadRemainingCount));
			_mutex.Release();

			if (_threadRemainingCount == 0)
			{
				Helper.ConsoleWriteLineThreadName("Trigering GoGoGo");
				_threadRemainingCount = _threadsToCreate;
				_threadGoGoGoCount = 0;
				_gogogo.Release();
			}

			_mutex2.WaitOne();
			if (_threadGoGoGoCount == _threadsToCreate)
			{
				Helper.ConsoleWriteLineThreadName("_threadGoGoGoCount reached _threadsToCreate, calling wait to make barrier reusable");
				_threadGoGoGoCount = 0;
				_mutex2.Release();
				_gogogo.WaitOne();
			}
			else
			{
				_mutex2.Release();
			}

			// below is called turnstile.
			Helper.ConsoleWriteLineThreadName("Waiting for the GoGoGo");
			_gogogo.WaitOne();
			Helper.ConsoleWriteLineThreadName("Got GoGoGo, Signaling same.");
			_gogogo.Release();

			_mutex2.WaitOne();
			_threadGoGoGoCount++;
			Helper.ConsoleWriteLineThreadName(string.Format("Critical section: ThreadGoGoGoCount = {0}", _threadGoGoGoCount));
			_mutex2.Release();
		}
	}
}
/*
PS x:\ConcurrencyGyan\DowneySemaphores\bin\Debug> .\DowneySemaphores.exe
TName 0: Started
TName 1: Started
TName 2: Started
TName 3: Started
TName 0: Entering barrier code
TName 0: Critical section: ThreadCount = 3
TName 0: Waiting for the GoGoGo
TName 1: Entering barrier code
TName 1: Critical section: ThreadCount = 2
TName 1: Waiting for the GoGoGo
TName 2: Entering barrier code
TName 2: Critical section: ThreadCount = 1
TName 2: Waiting for the GoGoGo
TName 3: Entering barrier code
TName 3: Critical section: ThreadCount = 0
TName 3: Trigering GoGoGo
TName 3: Waiting for the GoGoGo
TName 3: Got GoGoGo, Signaling same.
TName 3: Critical section: ThreadGoGoGoCount = 1
TName 3: Exiting barrier code
TName 0: Got GoGoGo, Signaling same.
TName 0: Critical section: ThreadGoGoGoCount = 2
TName 0: Exiting barrier code
TName 1: Got GoGoGo, Signaling same.
TName 1: Critical section: ThreadGoGoGoCount = 3
TName 1: Exiting barrier code
TName 2: Got GoGoGo, Signaling same.
TName 2: Critical section: ThreadGoGoGoCount = 4
TName 2: Exiting barrier code
TName 3: Entering barrier code again
TName 3: Critical section: ThreadCount = 3
TName 3: _threadGoGoGoCount reached _threadsToCreate, calling wait to make barrier reusable
TName 3: Waiting for the GoGoGo
TName 0: Entering barrier code again
TName 1: Entering barrier code again
TName 1: Critical section: ThreadCount = 2
TName 1: Waiting for the GoGoGo
TName 0: Critical section: ThreadCount = 1
TName 0: Waiting for the GoGoGo
TName 2: Entering barrier code again
TName 2: Critical section: ThreadCount = 0
TName 2: Trigering GoGoGo
TName 3: Got GoGoGo, Signaling same.
TName 3: Critical section: ThreadGoGoGoCount = 1
TName 3: Exiting barrier code again
TName 1: Got GoGoGo, Signaling same.
TName 1: Critical section: ThreadGoGoGoCount = 2
TName 1: Exiting barrier code again
TName 0: Got GoGoGo, Signaling same.
TName 0: Critical section: ThreadGoGoGoCount = 3
TName 0: Exiting barrier code again
TName 2: Waiting for the GoGoGo
TName 2: Got GoGoGo, Signaling same.
TName 2: Critical section: ThreadGoGoGoCount = 4
TName 2: Exiting barrier code again
TName 3: Exited
TName 1: Exited
TName 0: Exited
TName 2: Exited
PS x:\ConcurrencyGyan\DowneySemaphores\bin\Debug>
*/