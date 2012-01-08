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
	}
}
