using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.SRM538
{
	public class LeftOrRight
	{
		public int maxDistance(string program)
		{
			throw new NotImplementedException();
			char[] arr = program.ToCharArray();

			int l = 0;
			int r = 0;
			int q = 0;

			for (int i = 0; i < arr.Length; i++)
			{
				switch (arr[i])
				{
					case 'L':
						l++;
						break;
					case 'R':
						r++;
						break;
					case '?':
						q++;
						break;
				}
			}

			if (q == 0)
			{
				return SimulateProgram(arr);
			}
			else
			{
				if (l == 0)
				{
					return r + q;
				}
				else
				{
					if (r == 0)
					{
						return l + q;
					}
					else
					{

					}
				}
			}
		}

		private int SimulateProgram(char[] arr)
		{
			throw new NotImplementedException();
		}
	}
}
