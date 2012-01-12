using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DowneySemaphores
{
	delegate void MainX(string[] args);

	class TestHelper
	{
		public static bool VerifyStringOrderInConsoleOutput(MainX mainx, List<StringOrder> order)
		{
			StringBuilder sb = new StringBuilder();

			StringWriter strw = new StringWriter(sb);
			// First, save the standard output.
			TextWriter tmp = Console.Out;
			Console.SetOut(strw);
			mainx(null);
			Console.SetOut(tmp);
			strw.Close();

			return _ValidateRules(sb.ToString(), order);
		}

		private static bool _ValidateRules(string p, List<StringOrder> order)
		{
			foreach (var item in order)
			{
				if (_ValidateRule(p, item) == false)
				{
					return false;
				}
			}

			return true;
		}

		private static bool _ValidateRule(string p, StringOrder item)
		{
			// TODO not efficient, searching same string p 4 times!
			if (p.Contains(item.First) && p.Contains(item.Second))
			{
				return p.IndexOf(item.First) < p.IndexOf(item.Second);
			}

			return false;
		}
	}
}
