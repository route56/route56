using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace DowneySemaphores
{
	class Multiplex
	{
		private static Semaphore _multiplex;

		public static void MainX(string[] args)
		{
			int threadLimit = 3;
			int threadsToCreate = 7;
			_multiplex = new Semaphore(threadLimit, threadLimit);

			for (int i = 0; i < threadsToCreate; i++)
			{
				Thread tA = new Thread(ThreadCode);
				tA.Start();
			}
		}

		private static void ThreadCode()
		{
			Helper.ConsoleWriteLineThreadId("Started");
			Helper.RandomSleep();
			Helper.ConsoleWriteLineThreadId("Waiting to enter multiplex");
			_multiplex.WaitOne();
			Helper.ConsoleWriteLineThreadId("Entered multiplex");
			Helper.RandomSleep();
			Helper.ConsoleWriteLineThreadId("Quiting multiplex");
			_multiplex.Release();
			Helper.RandomSleep();
			Helper.ConsoleWriteLineThreadId("Exited");
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
TID 3: Waiting to enter multiplex
TID 3: Entered multiplex
TID 4: Waiting to enter multiplex
TID 4: Entered multiplex
TID 5: Waiting to enter multiplex
TID 5: Entered multiplex
TID 6: Waiting to enter multiplex
TID 7: Waiting to enter multiplex
TID 8: Waiting to enter multiplex
TID 9: Waiting to enter multiplex
TID 3: Quiting multiplex
TID 6: Entered multiplex
TID 4: Quiting multiplex
TID 5: Quiting multiplex
TID 7: Entered multiplex
TID 8: Entered multiplex
TID 3: Exited
TID 6: Quiting multiplex
TID 9: Entered multiplex
TID 4: Exited
TID 5: Exited
TID 7: Quiting multiplex
TID 8: Quiting multiplex
TID 6: Exited
TID 9: Quiting multiplex
TID 7: Exited
TID 8: Exited
TID 9: Exited
*/