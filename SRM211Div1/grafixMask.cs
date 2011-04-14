using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SRM211Div1
{
	public class grafixMask
	{
		struct Rect
		{
			public int Top;
			public int Left;
			public int Bottom;
			public int Right;
		}

		private const int RowMax = 400;
		private const int ColMax = 600;

		public int[] sortedAreas(string[] rectangles)
		{
			List<Rect> rectList = new List<Rect>();

			foreach (string str in rectangles)
			{
				string[] coords = str.Split(' ');
				rectList.Add(new Rect() { Top = Int32.Parse(coords[0]), Left = Int32.Parse(coords[1]), Bottom = Int32.Parse(coords[2]), Right = Int32.Parse(coords[3]) });
			}

			bool[,] visited = new bool[RowMax, ColMax];

			foreach (Rect re in rectList)
			{
				for (int i = re.Top; i <= re.Bottom; i++)
				{
					for (int j = re.Left; j <= re.Right; j++)
					{
						visited[i, j] = true;
					}
				}
			}

			List<int> areaList = new List<int>();

			int area = GetContiniousArea(visited);

			while (area > 0)
			{
				areaList.Add(area);
				area = GetContiniousArea(visited);
			}

			areaList.Sort();

			return areaList.ToArray();
		}

		private int GetContiniousArea(bool[,] visited)
		{
			Stack<Tuple<int, int>> tovisit = new Stack<Tuple<int, int>>();
			int area = 0;

			bool found = false;
			int row = 0, col = 0;

			for (row = 0; row < RowMax; row++)
			{
				for (col = 0; col < ColMax; col++)
				{
					if (visited[row, col] == false)
					{
						found = true;
						break;
					}
				}

				if (found)
				{
					break;
				}
			}

			if (found)
			{
				tovisit.Push(new Tuple<int, int>(row, col));
				visited[row, col] = true;
			}

			while (tovisit.Count > 0)
			{
				Tuple<int, int> current = tovisit.Pop();

				Debug.Assert(visited[current.Item1, current.Item2] == true);
				area++;

				Tuple<int, int>[] sides = 
					{ 
						new Tuple<int,int>(current.Item1 - 1, current.Item2),
						new Tuple<int, int>(current.Item1, current.Item2 - 1),
						new Tuple<int, int>(current.Item1 + 1, current.Item2),
						new Tuple<int, int>(current.Item1, current.Item2 + 1)
					};

				foreach (Tuple<int, int> side in sides)
				{
					if (side.Item1 >= 0 && side.Item1 < RowMax
						&& side.Item2 >= 0 && side.Item2 < ColMax)
					{
						if (visited[side.Item1, side.Item2] == false)
						{
							visited[side.Item1, side.Item2] = true;
							tovisit.Push(side);
						}
					}
				}
			}

			return area;
		}
	}
}
