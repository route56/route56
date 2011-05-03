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
			long sum = 0;
			long prod = 1;

			for (int i = 0; i < seq.Length; i++)
			{
				sum += seq[i];
				prod *= seq[i];
			}

			if (sum == prod)
			{
				if (seq.Length == 1)
					return "Yes";
				else
					return "No";
			}
			//else if (prod == 0 || prod == 1)
			//{
			//    return TryChangingZeroOne(seq, sum, prod);
			//}

			long[] sumArr = new long[seq.Length];
			long[] prodArr = new long[seq.Length];

			for (int i = 0; i < seq.Length; i++)
			{
				sumArr[i] = sum - seq[i];
				if (seq[i] == 0)
				{
					prodArr[i] = long.MaxValue;
				}
				else
				{
					prodArr[i] = prod / seq[i];
				}
			}

			for (int i = 0; i < seq.Length; i++)
			{
				if (prodArr[i] != 0 && (prodArr[i] - 1) != 0 && (sumArr[i] % (prodArr[i] - 1) == 0))
				{
					return "Yes";
				}
			}

			if (prod == 0)
			{
				int count = 0;
				int index = 0;
				for (int i = 0; i < seq.Length; i++)
				{
					if (prodArr[i] == 0)
						count++;
					else
					{
						index = i;
					}
				}

				if (count == seq.Length)
				{
					return "No";
				}
				else
				{

				}
			}

			return "No";
		}

		//private string TryChangingZeroOne(int[] seq, long sum, long prod)
		//{
		//    throw new NotImplementedException();
		//}

	}
}


