using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace QR2011
{
	public class GogoSort
	{
		public double SortIt(int[] arr)
		{
			// find list of loops.
			List<int> loops;

			GetLoops(arr, out loops);

			double hits = 0;

			foreach (var len in loops)
			{
				if (len == 1)
				{
					continue;
				}

				// n = log (.5) / log (1-p)
				// p = 1/len! 
				// log (n! - 1) - log n!

				hits += GetN(len);
			}

			return hits;
		}

		private void GetLoops(int[] arr, out List<int> loops)
		{
			loops = new List<int>();
			bool[] visited = new bool[arr.Length];
			int start = 0;

			while (start < visited.Length)
			{
				loops.Add(GetLoopLen(start, visited, arr));

				start++;

				for (int i = start; i < visited.Length; i++)
				{
					if (visited[i] == false)
					{
						start = i;
						break;
					}
				}
			}

			loops.Sort();
		}

		private double GetN(int len)
		{
			double n = Math.Log(.5);

			double fact = 1;
			for (int i = 1; i <= len; i++)
			{
				fact *= i;
			}

			n = n / (Math.Log(fact - 1) - Math.Log(fact));

			return n;
		}

		private int GetLoopLen(int start, bool[] visited, int[] arr)
		{
			// 3 1 5 2 6 4 7
			int run = start;
			int count = 1;
			visited[run] = true;
			int match = arr[run] - 1; // 3
			run = arr[run] - 1; // 5

			while (match != run) // 3 != 5
			{
				visited[run] = true;
				count++;
				run = arr[run] - 1; // 5
			}

			return count;
		}
	}
}
