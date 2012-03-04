using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.SRMPractice
{
	public class EllysCheckers
	{
		public string getWinner(string board)
		{
			return GetWinnerMain(board) ? "YES" : "NO";
		}

		public bool GetWinnerMain(string board)
		{
			int state = 0;

			char[] arr = board.ToCharArray();
			arr[arr.Length - 1] = '.';
			for (int i = 0; i < arr.Length; i++)
			{
				switch (state)
				{
					case 0:
						if (arr[i] == 'o')
						{
							state = 0;
						}
						else
						{
							state = 1;
						}
						break;

					case 1:
						if (arr[i] == 'o')
						{
							state = 3;
						}
						else
						{
							state = 2;
						}
						break;

					case 2:
						if (arr[i] == 'o')
						{
							state = 3;
						}
						else
						{
							state = 1;
						}

						break;
					case 3:
						if (arr[i] == 'o')
						{
							state = 4;
						}
						else
						{
							state = 2;
						}
						break;
					case 4:
						if (arr[i] == 'o')
						{
							state = 1;
						}
						else
						{
							state = 3;
						}
						break;
				}
			}

			return state == 2;
		}
	}
}
