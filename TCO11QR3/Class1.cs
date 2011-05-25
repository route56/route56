using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCO11QR3
{
	public class AllButOneDivisor
	{
		public int getMinimum(int[] divisors)
		{
			Array.Sort(divisors);

			long lcm = divisors[0];
			for (int i = 1; i < divisors.Length - 1; i++)
			{
				lcm = LCM(lcm, divisors[i]);
			}

			if (lcm % divisors[divisors.Length - 1] == 0)
			{
				return -1;
			}

			int ans = 0;
			for (int num = 1; num < Int32.MaxValue; num++) // refactor this
			{
				if ((num*lcm)%divisors[divisors.Length - 1] != 0)
				{
					ans = (int)(num * lcm);
					break;
				}
			}

			return ans;
		}

		private long LCM(long a, long b)
		{
			return (a / GCD(a,b)) * b;
		}

		public long GCD(long a, long b) // zero case not handled
		{
			if (b == 0)
			{
				return Math.Abs(a);
			}
			else
			{
				return (GCD(b, a % b));
			}
		}

	}
}
