using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.SRM538
{
	public class Other
	{
		public string isItPossible(int[] x, int[] y, int wantedParity)
		{
			int t = Math.Abs(x[0]) + Math.Abs(y[0]);
			for (int i = 1; i < x.Length; i++)
			{
				t = (t + Math.Abs(x[i] - x[i - 1]) + Math.Abs(y[i] - y[i - 1]));
			}
			int n = x.Length;

			for (int i = 0; i < x.Length; i++)
			{
				int tl = (t + Math.Abs(x[n - 1] - x[i]) + Math.Abs(y[n - 1] - y[i]));
				if (tl % 2 == wantedParity)
				{
					return "CAN";
				}
			}

			return "CANNOT";
		}
	}
}
