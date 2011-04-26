using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM504Div2
{
	public class MathContest
	{
		public int countBlack(string ballSequence, int repetitions)
		{
			StringBuilder str = new StringBuilder();

			for (int i = 0; i < repetitions; i++)
			{
				str.Append(ballSequence);
			}

			char[] tape = str.ToString().ToCharArray();

			int blackCount = 0;
			int size = tape.Length;
			bool flipped = false;
			bool fromHead = true;
			int head = 0;
			int tail = size - 1;
			int current = head;

			while (size > 0)
			{
				if ( (tape[current] == 'B' && flipped == false) 
					|| (tape[current] == 'W' && flipped == true))
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
	}
}
