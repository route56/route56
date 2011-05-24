using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GCJSolver
{
	public class Program
	{
//3
//2 3
//###
//###
//1 1
//.
//4 5
//.##..
//.####
//.####
//.##..
		public static void MainOld(string[] args)
		{
			int caser = Int32.Parse(Console.ReadLine());
			TextWriter tw = new StreamWriter(args[0]);
			for (int i = 0; i < caser; i++)
			{
				Console.WriteLine("Case #{0}:",i+1);
				tw.WriteLine("Case #{0}:", i+1);
				new Program().DoIt(tw);
			}
			tw.Close();
		}

		private void DoIt(TextWriter tw)
		{
			string[] rowcol = Console.ReadLine().Split();
			int row = Int32.Parse(rowcol[0]);
			int col = Int32.Parse(rowcol[1]);

			StringBuilder[] grid = new StringBuilder[row];

			for (int i = 0; i < row; i++)
			{
				grid[i] = new StringBuilder(Console.ReadLine());
			}

			bool impossible = false;
			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					switch (grid[i][j])
					{
						case '#':
							if ((i+1 < row && j+1 < col) &&
								(grid[i][j+1] == '#' && grid[i+1][j] == '#' && grid[i+1][j+1] == '#'))
							{
								grid[i][j] = '/';
								grid[i+1][j+1] = '/';
								grid[i][j+1] = '\\';
								grid[i+1][j] = '\\';
							}
							else
							{
								impossible = true;
							}
							break;
						default:
							break;
					}

					if (impossible)
					{
						break;
					}
				}

				if (impossible)
				{
					break;
				}
			}

			if (impossible)
			{
				Console.WriteLine("Impossible");
				tw.WriteLine("Impossible");
			}
			else
			{
				for (int i = 0; i < row; i++)
				{
					Console.WriteLine(grid[i].ToString());
					tw.WriteLine(grid[i].ToString());
				}
			}

		}
	}
}
