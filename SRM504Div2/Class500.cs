using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM504Div2
{
	public class MathContest
	{
		public int countBlackMy(string ballSequence, int repetitions)
		{
			char[] tape = ballSequence.ToCharArray();

			int blackCount = 0;
			int size = tape.Length * repetitions;
			bool flipped = false;
			bool fromHead = true;
			int head = 0;
			int tail = size - 1;
			int current = head;

			while (size > 0)
			{
				if ( (tape[current % tape.Length] == 'B' && flipped == false)
					|| (tape[current % tape.Length] == 'W' && flipped == true))
				{
					blackCount++;
					flipped = !flipped;

					if (fromHead)
					{
						head++;
					}
					else
					{
						tail--;
					}

				}
				else
				{
					if (fromHead)
					{
						head++;
					}
					else
					{
						tail--;
					}

					fromHead = !fromHead;
				}

				if (fromHead)
				{
					current = head;
				}
				else
				{
					current = tail;
				}

				size--;
			}

			return blackCount;
		}


		public int countBlack(string str, int rep)
		{
			int p1 = 0;
			int p2 = str.Length - 1;
			int countW = 0;
			int countB = 0;

			while (countW + countB != str.Length * rep)
			{
				CheckMethod(str, ref p1, ref p2, ref countW, ref countB);
			}

			return countB;
		}

		private void CheckMethod(string str, ref int p1, ref int p2, ref int countW, ref int countB)
		{
			if (countW % 2 == 0) // p1
			{
				if ((str[p1] == 'W' && countB % 2 == 0) || (str[p1] == 'B' && countB % 2 == 1)) // white
				{
					countW++;
				}
				else
				{
					countB++;
				}

				p1++;
				if (p1 == str.Length)
				{
					p1 = 0;
				}
			}
			else // p2
			{
				if ((str[p2] == 'W' && countB % 2 == 0) || (str[p2] == 'B' && countB % 2 == 1)) // white
				{
					countW++;
				}
				else
				{
					countB++;
				}
				p2--;
				if (p2 == -1)
				{
					p2 = str.Length - 1;
				}
			}
		}

	}
}
