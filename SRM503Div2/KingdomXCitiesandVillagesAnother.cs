using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM503Div2
{
	class KingdomXCitiesandVillagesAnother
	{
		class Data : IComparable<Data>
		{
			public double distance;
			public int i;
			public int j;

			public int CompareTo(Data other)
			{
				if (distance > other.distance)
					return 1;
				else if (distance == other.distance)
				{
					return 0;
				}
				else
				{
					return -1;
				}
			}
		}

		double determineLength(int[] cityX, int[] cityY, int[] villageX, int[] villageY)
		{
			List<Data> grid = new List<Data>();

			for (int i = 0; i < cityX.Length; i++)
			{
				for (int j = 0; j < villageX.Length; j++)
				{
					Data dt = new Data();

					dt.distance = ComputeDistance(cityX[i], cityY[i], villageX[j], villageY[j]);
					dt.i = i;
					dt.j = j;
				}
			}

			grid.Sort();
		}

		private double ComputeDistance(int p, int p_2, int p_3, int p_4)
		{
			return Math.Sqrt((p_3 - p) * (p_3 - p) + (p_4 - p_2) * (p_4 - p_2));
		}
	}
}
