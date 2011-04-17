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

		public double determineLength(int[] cityX, int[] cityY, int[] villageX, int[] villageY)
		{
			List<Data> cvGrd = new List<Data>();
			List<Data> vvGrd = new List<Data>();

			for (int i = 0; i < cityX.Length; i++)
			{
				for (int j = 0; j < villageX.Length; j++)
				{
					Data dt = new Data();

					dt.distance = ComputeDistance(cityX[i], cityY[i], villageX[j], villageY[j]);
					dt.i = i;
					dt.j = j;
					cvGrd.Add(dt);
				}
			}

			for (int i = 0; i < villageX.Length; i++)
			{
				for (int j = i; j < villageX.Length; j++)
				{
					
					Data dt = new Data();

					dt.distance = ComputeDistance(villageX[i], villageY[i], villageX[j], villageY[j]);
					dt.i = i;
					dt.j = j;
					vvGrd.Add(dt);
				}
			}

			cvGrd.Sort();
			vvGrd.Sort();

			double ans = 0;

			while (vvGrd.Count > 0 && cvGrd.Count > 0)
			{
				int villageAdded = cvGrd[0].j;
				ans += cvGrd[0].distance;

				List<int> toRemove = new List<int>();
				for (int i = 0; i < cvGrd.Count; i++)
				{
					if (cvGrd[i].j == villageAdded)
					{
						toRemove.Add(i);
					}
				}

				for (int i = toRemove.Count - 1; i >= 0; i--)
				{
					cvGrd.RemoveAt(toRemove[i]);
				}

				toRemove.Clear();

				for (int i = 0; i < vvGrd.Count; i++)
				{
					if (vvGrd[i].i == villageAdded)
					{
						toRemove.Add(i);
						cvGrd.Add(vvGrd[i]);
					}
					else if (vvGrd[i].j == villageAdded)
					{
						toRemove.Add(i);
						cvGrd.Add(new Data() { distance = vvGrd[i].distance, i = vvGrd[i].j, j = vvGrd[i].i });
					}
				}

				for (int i = toRemove.Count - 1; i >= 0; i--)
				{
					vvGrd.RemoveAt(toRemove[i]);
				}

				cvGrd.Sort();
			}

			return ans;
		}

		private double ComputeDistance(int p, int p_2, int p_3, int p_4)
		{
			return Math.Sqrt(Math.Pow((p_3 - p), 2) + Math.Pow((p_4 - p_2),2)); // int overflow happened!!!
		}
	}
}
