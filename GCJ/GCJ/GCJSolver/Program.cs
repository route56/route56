using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCJSolver
{
	public class Program
	{
		class Data
		{
			public double RPI { get; set; }
			public double WP { get; set; }
			public double OWP { get; set; }
			public double OOWP { get; set; }
			public double Win { get; set; }
			public double Count { get; set; }
		}

		public static void Main(string[] args)
		{
			string input = Console.ReadLine();
			for (int caser = 0; caser < Int32.Parse(input); caser++)
			{
				string ip = Console.ReadLine();
				int n = Int32.Parse(ip);
				bool?[,] grid = new bool?[n, n];

				for (int row = 0; row < n; row++)
				{
					string ip2 = Console.ReadLine();
					for (int col = 0; col < n; col++)
					{
						switch (ip2[col])
						{
							case '1':
								grid[row, col] = true;
								break;
							case '0':
								grid[row, col] = false;
								break;
							default:
								grid[row, col] = null;
								break;
						}
					}
				}

				double[] res = Program.GetRPI(grid, n);

				Console.WriteLine("Case #{0}:", caser+1);

				foreach (double item in res)
				{
					Console.WriteLine(item);
				}
			}
		}

		private static double[] GetRPI(bool?[,] grid, int n)
		{
			List<Data> dt = new List<Data>(n);

			for (int i = 0; i < n; i++)
			{
				dt.Add(new Data());
				dt[i].Count = 0;
				dt[i].Win = 0;
				for (int j = 0; j < n; j++)
				{
					if (grid[i,j].HasValue)
					{
						dt[i].Count++;
						dt[i].Win += (grid[i, j].Value ? 1 : 0);
					}
				}

				dt[i].WP = dt[i].Win / dt[i].Count;
			}

			for (int i = 0; i < n; i++)
			{
				// D
				dt[i].OWP = 0;
				for (int j = 0; j < n; j++)
				{
					//= dt[].WP;
					// B
					if (grid[i, j].HasValue)
					{
						if (grid[i, j].Value)
						{
							dt[i].OWP += (dt[j].Win / (dt[j].Count - 1));// b.win / (b.Count -1)
						}
						else
						{
							dt[i].OWP += ((dt[j].Win -1)/(dt[j].Count - 1));// c.Win - 1 / c.Count -1
						}
					}
				}

				dt[i].OWP = dt[i].OWP / dt[i].Count;
			}

			for (int i = 0; i < n; i++)
			{
				dt[i].OOWP = 0;
				for (int j = 0; j < n; j++)
				{
					if (grid[i, j].HasValue)
					{
						dt[i].OOWP += dt[j].OWP;
					}
				}

				dt[i].OOWP = dt[i].OOWP/ dt[i].Count;
			}

			for (int i = 0; i < n; i++)
			{
				dt[i].RPI = 0.25 * dt[i].WP + 0.50 * dt[i].OWP + 0.25 * dt[i].OOWP;
			}

			double[] res = new double[n];
			for (int i = 0; i < n; i++)
			{
				res[i] = dt[i].RPI;
			}

			return res;
		}
	}
}