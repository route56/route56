using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Solver.Test
{
	class PowerSet
	{
		public IEnumerable<IEnumerable<T>> GetPowerSet<T>(List<T> list)
		{
			return from m in Enumerable.Range(0, 1 << list.Count)
				   select
					   from i in Enumerable.Range(0, list.Count)
					   where (m & (1 << i)) != 0
					   select list[i];
		}

		internal int[] Solve(int[,] map, int[] weight)
		{
			List<int> index = Enumerable.Range(0, map.GetLength(1)).ToList();

			List<IEnumerable<int>> solutions = new List<IEnumerable<int>>();

			foreach (var subSets in GetPowerSet<int>(index))
			{
				bool consider = true;

				for (int i = 0; i < weight.Length; i++)
				{
					int currWt = 0;

					foreach (var indx in subSets)
					{
						currWt += map[i, indx];

						if (currWt > weight[i])
						{
							break;
						}
					}

					if (currWt > weight[i])
					{
						consider = false;
						break;
					}
				}

				if (consider)
				{
					solutions.Add(subSets);
				}
			}

			int maxLen = solutions.Max(s => s.Count());

			return solutions.Where(s => s.Count() == maxLen).First().ToArray();
		}
	}
}
