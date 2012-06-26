using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiDimKnapsackSimplified
{
	public class Solver<TItem, TDim>
	{
		public IList<TItem> Solve(IList<TItem> items, IList<TDim> dim, Func<TItem, TDim, bool> map, IList<UInt32> weight)
		{
			throw new NotImplementedException();
		}
	}

	class PairData
	{
		public int Max { get; set; }
		public int Index { get; set; }
		public int[] Weight { get; set; }
	}

	public class SolverNonFancy : ISolver
	{
		public int[] SolveNonFancy(int[,] map, int[] weight)
		{
			if (map.GetLength(0) != weight.Length)
			{
				throw new ArgumentException("map and weight's dimention doesn't match");
			}

			PairData[] space = new PairData[map.GetLength(1)];

			for (int i = 0; i < map.GetLength(1); i++) // Main loop
			{
				PairData max = new PairData() { Index = i, Max = 1, Weight = new int[weight.Length] };

				for (int k = 0; k < weight.Length; k++)
				{
					max.Weight[k] = map[k, i];
				}

				for (int j = 0; j < i; j++) // Max finding loop
				{
					bool allDimSatisfied = true;

					for (int k = 0; k < weight.Length; k++) // dimention loop
					{
						if (space[j].Weight[k] + map[k, i] > weight[k])
						{
							allDimSatisfied = false;
							break;
						}
					}

					if (allDimSatisfied)
					{
						if (max.Max < space[j].Max + 1)
						{
							max.Max = space[j].Max + 1;
							max.Index = j;
						}
					}
				}

				if (max.Index != i)
				{
					for (int k = 0; k < weight.Length; k++)
					{
						max.Weight[k] += space[max.Index].Weight[k];
					}
				}

				space[i] = max;
			}

			PairData overallMax = new PairData() { Index = -1, Max = -1};

			for (int i = 0; i < space.Length; i++)
			{
				if (overallMax.Max < space[i].Max)
				{
					overallMax.Max = space[i].Max;
					overallMax.Index = i;
				}
			}

			List<int> result = new List<int>();

			do
			{
				result.Add(overallMax.Index);
				overallMax = space[overallMax.Index];
			} while (overallMax.Index != result.Last());

			result.Sort();

			return result.ToArray();
		}
	}
}
