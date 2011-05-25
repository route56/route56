using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NumberSystem
{
	public class GCDCalc
	{
		private long _GetGCD(long a, long b)
		{
			if (b == 0)
			{
				return a;
			}
			else
			{
				return (_GetGCD(b, a % b));
			}
		}

		public long GetGCD(long a, long b)
		{
			if (a < 0 || b < 0)
			{
				throw new ArgumentOutOfRangeException();
			}

			return _GetGCD(a, b);
		}

		public long GetGCD(long[] list)
		{
			if (list.Length == 0)
			{
				throw new ArgumentException();
			}

			long gcd = list[0];

			for (int i = 1; i < list.Length; i++)
			{
				gcd = GetGCD(gcd, list[i]);
			}

			return gcd;
		}
	}
}
