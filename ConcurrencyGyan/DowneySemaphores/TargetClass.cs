using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DowneySemaphores
{
	class TargetClass
	{
		public static void MainX(string[] args)
		{
			Console.WriteLine("Foo");
			Console.WriteLine("Bar");
			Console.WriteLine("Baz");
			Console.WriteLine("Qux");
		}

		public static bool TestThis()
		{
			return TestHelper.VerifyStringOrderInConsoleOutput(MainX,
				new List<StringOrder>()
				{
					new StringOrder() { First = "Foo", Second = "Bar" },
					new StringOrder() { First = "Baz", Second = "Qux" }
				});
		}
	}
}
