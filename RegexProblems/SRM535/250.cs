using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.SRM535
{
	public class FoxAndIntegers
	{
		public int[] get(int x, int y, int z, int w)
		{
			double a = (x + z) / 2.0;
			double b = (y + w) / 2.0;
			double bb = (z - x) / 2.0;
			double c = (w - y) / 2.0;

			if (b == bb && IsInt(b) && IsInt(a) && IsInt(c))
			{
				return new int[] { (int)a, (int)b, (int)c };
			}
			else
			{
				return new int[] { };
			}
		}

		private bool IsInt(double b)
		{
			return (b - (int)b) == 0.0;
		}
	}
}
