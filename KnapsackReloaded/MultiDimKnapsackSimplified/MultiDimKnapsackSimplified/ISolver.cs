using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MultiDimKnapsackSimplified
{
	interface ISolver
	{
		int[] SolveNonFancy(int[,] map, int[] weight);
	}
}
