using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.SRM535
{
	public class FoxAndGCDLCM
	{
		public long get(long G, long L)
		{
			if (G > L && L % G != 0)
			{
				return -1;
			}

			long left = L / G;

			for (long i = (long)Math.Sqrt(left); i > 1; i--)
			{
				if (left % i == 0)
				{
					long a = (left / i);
					return a + G * L / a;
				}
			}

			return -1;
		}

		public long getV2(long G, long L)
		{
			if (G > L && L % G != 0)
			{
				return -1;
			}

			for (long i = (long)Math.Sqrt(L * G); i > G; i--)
			{
				if (L % i == 0 || i % G == 0)
				{
					return i + G * L / i;
				}
			}

			return -1;
		}
	}
}
