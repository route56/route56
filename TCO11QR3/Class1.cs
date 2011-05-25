using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCO11QR3
{
	public class AllButOneDivisor
	{
		int[] PrimeNo = { 2, 3, 5, 7, 11, 13 };

		public int getMinimum(int[] divisors)
		{
			Array.Sort(divisors);

			for (int i = divisors.Length - 1; i >= 0; i--)
			{
				if (IsPrime(divisors[i]))
				{
					return LCM(divisors, i);
				}
			}


			int lcm = divisors[0];
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
				if ((num * lcm) % divisors[divisors.Length - 1] != 0)
				{
					ans = (int)(num * lcm);
					break;
				}
			}

			return ans;


		}

		private bool IsPrime(int p)
		{
			foreach (var prime in PrimeNo)
			{
				if (p % prime == 0 && p / prime == 1)
				{
					return true;
				}
			}

			return false;
		}

		private int LCM(int[] divisors, int inp)
		{
			int lcm = divisors[0];
			for (int i = 1; i < divisors.Length; i++)
			{
				if (i != inp)
				{
					lcm = LCM(lcm, divisors[i]);
				}
			}

			return lcm;
		}

		private int LCM(int a, int b)
		{
			return (a / GCD(a,b)) * b;
		}

		public int GCD(int a, int b) // zero case not handled
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
