using System;
using System.Collections.Generic;
using System.Text;

namespace SRM507
{
	public class CubeStickers
	{
		public string isPossible(string[] sticker)
		{
			return isPossibleImpl(sticker) ? "YES" : "NO";
		}

		public bool isPossibleImpl(string[] sticker)
		{
			Dictionary<string, int> dict = new Dictionary<string, int>();

			foreach (string item in sticker)
			{
				if (dict.ContainsKey(item) == false)
				{
					dict.Add(item, 1);
				}
				else
				{
					dict[item]++;
				}
			}

			if (dict.Count < 3)
			{
				return false;
			}
			else
			{
				if (dict.Count < 5)
				{
					int[] values = new int[dict.Count];
					dict.Values.CopyTo(values, 0);

					Array.Sort(values);

					if (dict.Count == 3)
					{
						// 3 with 1,1,4
						// 1,2,3
						if (values[0] == 1)
						{
							return false;
						}
						else
						{
							return true;
						}
						// 3 with 2,2,2 true;
					}
					else if (dict.Count == 4)
					{
						if (values[2] == 1)
						{
							return false;
						}
						else
						{
							return true;
						}
						// 4 with 1,1,1,3 false

						// 4 with 1,1,2,2 true
					}
				}

				// 5 with 1,1,1,1,2 always true
				// 6 with 1,1,1,1,1,1 always true

				return true;
			}
		}
	}
}
