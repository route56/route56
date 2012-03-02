using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.DynamicProgrammingProblems
{
	public class ZigZag
	{
		public int LongestZigZag(int[] sequence)
		{
			if (sequence.Length < 2)
			{
				return sequence.Length;
			}

			if (sequence[0] == sequence[1])
			{
				return 1;
			}

			bool goDown = sequence[0] < sequence[1];

			int maxLen = 2;

			for (int i = 2; i < sequence.Length; i++)
			{
				if (sequence[i-1] > sequence[i])
				{
					if (goDown)
					{
						goDown = false;
						maxLen++;
					}
				}
				else if (sequence[i-1] < sequence[i])
				{
					if (!goDown)
					{
						goDown = true;
						maxLen++;
					}
				}
			}

			return maxLen;
		}
	}
}
