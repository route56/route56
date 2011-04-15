using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM211Div1
{
	public class Circuits
	{
		public int howLong(string[] connects, string[] costs)
		{
			int[,] costFunction = new int[connects.Length, connects.Length];
			bool[] graphVisited = new bool[connects.Length];

			ConstructGraph(connects, costs, costFunction);

			bool foundOne = false;

			do
			{

				foundOne = SearchCriticalPath(costFunction, graphVisited);

			} while (foundOne);

			int max = 0;
			for (int i = 0; i < costFunction.GetLength(0); i++)
			{
				for (int j = 0; j < costFunction.GetLength(1); j++)
				{
					if (max < costFunction[i, j])
					{
						max = costFunction[i, j];
					}
				}
			}

			return max;
		}

		private static bool SearchCriticalPath(int[,] costFunction, bool[] graphVisited)
		{
			bool found = false;
			int index;
			for (index = 0; index < graphVisited.Length; index++)
			{
				if (graphVisited[index] == false)
				{
					found = true;
					break;
				}
			}

			Queue<int> toVisit = new Queue<int>();
			if (found)
			{
				toVisit.Enqueue(index);
				graphVisited[index] = true;
			}

			while (toVisit.Count > 0)
			{
				int current = toVisit.Dequeue();

				for (int i = 0; i < costFunction.GetLength(0); i++)
				{
					if (graphVisited[i] == false && costFunction[current, i] != int.MinValue)
					{
						toVisit.Enqueue(i);
						graphVisited[i] = true;
					}

					if (costFunction[current, i] != int.MinValue)
					{
						if (costFunction[index, i] < costFunction[index, current] + costFunction[current, i])
						{
							costFunction[index, i] = costFunction[index, current] + costFunction[current, i];
						}
					}
				}
			}

			return found;
		}

		private static void ConstructGraph(string[] connects, string[] costs, int[,] costFunction)
		{
			for (int i = 0; i < costFunction.GetLength(0); i++)
			{
				for (int j = 0; j < costFunction.GetLength(1); j++)
				{
					costFunction[i, j] = int.MinValue;
				}
			}

			for (int i = 0; i < connects.GetLength(0); i++)
			{
				if (String.IsNullOrEmpty(connects[i]))
				{
					continue;
				}

				string[] row = connects[i].Split(' ');
				string[] cost = costs[i].Split(' ');

				for (int j = 0; j < row.Length; j++)
				{
					costFunction[i, int.Parse(row[j])] = int.Parse(cost[j]);
				}
			}
		}
	}
}
