using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

namespace SRM505Div2
{
	public class PerfectSequences
	{
		public string fixIt(int[] seq)
		{
			for (int i = 0; i < seq.Length; i++)
			{
				long s = CalcSum(i, seq);
				long m = CalcMult(i, seq);

				if (m == 1)
				{
					if (seq.Length == 1)
					{
						return "Yes";
					}

					continue;
				}

				long v = s / (m - 1);

				if (v >=0 && s+v == v*m && v != seq[i])
				{
					return "Yes";
				}
			}

			return "No";
		}

		private long CalcMult(int i, int[] seq)
		{
			long mult = 1;
			for (int index = 0; index < seq.Length; index++)
			{
				if (index == i)
				{
					continue;
				}

				mult *= seq[index];
			}

			return mult;
		}

		private long CalcSum(int i, int[] seq)
		{
			long sum = 0;
			for (int index = 0; index < seq.Length; index++)
			{
				if (index == i)
				{
					continue;
				}

				sum += seq[index];
			}

			return sum;
		}
	}
}


