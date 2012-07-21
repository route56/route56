using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	class AssemblyLineProblem
	{
		public int Lines { get; private set; }

		public int Nodes { get; private set; }

		public long[] Enter { get; set; }

		public long[] Exit { get; set; }

		public long[,] StationCost { get; set; }

		public long[,] SwitchCost { get; set; }

		internal void Solve(bool bruteForce)
		{
			Validation();

			if (bruteForce)
			{
				BruteForceMethod();
			}
			else
			{
				OptimizedMethod();
			}
		}

		private void Validation()
		{
			Lines = StationCost.GetLength(0);
			Nodes = StationCost.GetLength(1);

			if (Lines <= 0)
			{
				throw new InvalidOperationException("Invalid lines");
			}

			if (Nodes <= 0)
			{
				throw new InvalidOperationException("Invalid nodes");
			}

			if (Lines != Enter.Length || Lines != Exit.Length || Lines != SwitchCost.GetLength(0))
			{
				throw new InvalidOperationException("Lines mismatch");
			}

			if (Nodes != SwitchCost.GetLength(1) + 1)
			{
				throw new InvalidOperationException("Node count mismatch");
			}
		}

		private void BruteForceMethod()
		{
			// powerset of path
			// 0 pipe 0. 1 pipe 1. With each node, it grows exponentially
			var powerSet = GetPowerSet<int>(Enumerable.Range(0, Nodes).ToList());

			MinTime = long.MaxValue;

			foreach (var item in powerSet)
			{
				int[] tempPath = GetPath(item);
				long tempMinTime = GetMinTime(tempPath);

				if (MinTime > tempMinTime)
				{
					MinTime = tempMinTime;
					Path = tempPath;
				}
			}
		}

		private void OptimizedMethod()
		{
			// Current min
			long[] currMin = new long[Lines];
			int[][] pathLines = new int[Lines][];

			for (int i = 0; i < Lines; i++)
			{
				pathLines[i] = new int[Nodes];
			}

			for (int i = 0; i < Nodes; i++)
			{
				pathLines[1][i] = 1;
			}

			Enter.CopyTo(currMin, 0);

			currMin[0] += StationCost[0, 0];
			currMin[1] += StationCost[1, 0];

			for (int i = 1; i < Nodes; i++)
			{
				long[] next = new long[Lines];

				for (int j = 0; j < Lines; j++)
				{
					long sameLine = StationCost[j, i] + currMin[j];
					long switchLine = StationCost[j, i] 
						+ currMin[(j + 1) % Lines] 
						+ SwitchCost[(j + 1) % Lines, i - 1];

					if ( sameLine > switchLine)
					{
						next[j] = switchLine;
						pathLines[j][i] = (j + 1) % Lines;
					}
					else
					{
						next[j] = sameLine;
					}
				}

				for (int j = 0; j < Lines; j++)
				{
					currMin[j] = next[j];
				}
			}

			currMin[0] += Exit[0];
			currMin[1] += Exit[1];

			if (currMin[0] < currMin[1])
			{
				MinTime = currMin[0];
				Path = pathLines[0];
			}
			else
			{
				MinTime = currMin[1];
				Path = pathLines[1];
			}
		}

		private long GetMinTime(int[] tempPath)
		{
			long minTime = 0;

			minTime += Enter[tempPath.First()];


			for (int i = 0; i < tempPath.Length; i++)
			{
				minTime += StationCost[tempPath[i], i];
			}

			for (int i = 0; i < tempPath.Length - 1; i++)
			{
				if (tempPath[i] != tempPath[i+1])
				{
					minTime += SwitchCost[tempPath[i], i];
				}
			}

			minTime += Exit[tempPath.Last()];

			return minTime;
		}

		private int[] GetPath(IEnumerable<int> item)
		{
			int[] result = new int[Nodes];

			foreach (var index in item)
			{
				result[index] = 1;
			}

			return result;
		}

		public long MinTime { get; set; }

		public int[] Path { get; set; }

		private IEnumerable<IEnumerable<T>> GetPowerSet<T>(IList<T> list)
		{
			foreach (var m in Enumerable.Range(0, 1 << list.Count))
			{
				yield return Enumerable.Range(0, list.Count).Where(i => (m & (1 << i)) != 0).Select(i => list[i]);
			}
		}
	}

	[TestClass]
	public class AssemblyLineTest
	{
		[TestMethod]
		public void BasicTest()
		{
			long[] enter = new long[] { 1, 1 };
			long[] exit = new long[] { 2, 2 };

			long[,] stationCost = new long[,]
				{
					{ 1, 1, 1},
					{ 2, 2, 2}
				};

			long[,] switchCost = new long[,]
				{
					{ 1, 1},
					{ 2, 2},
				};

			long expectedTime = 6;
			int[] expectedPath = new int[] {0, 0, 0 };

			Solve(enter, exit, stationCost, switchCost, expectedTime, expectedPath);
		}

		[TestMethod]
		public void OneFlipTest()
		{
			long[] enter = new long[] { 1, 1 };
			long[] exit = new long[] { 1, 1 };

			long[,] stationCost = new long[,]
				{
					{ 1, 4, 1},
					{ 4, 1, 4}
				};

			long[,] switchCost = new long[,]
				{
					{ 1, 1},
					{ 1, 1},
				};

			long expectedTime = 7;
			int[] expectedPath = new int[] { 0, 1, 0 };

			Solve(enter, exit, stationCost, switchCost, expectedTime, expectedPath);
		}

		[TestMethod]
		public void TwentyNodes()
		{
			long[] enter = new long[] { 1, 1 };
			long[] exit = new long[] { 1, 1 };

			long[,] stationCost = new long[,]
				{
					{ 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4},
					{ 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1, 4, 1}
				};

			long[,] switchCost = new long[,]
				{
					{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
					{ 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1},
				};

			long expectedTime = 41;
			int[] expectedPath = new int[] 
				{ 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1, 0, 1 };

			Solve(enter, exit, stationCost, switchCost, expectedTime, expectedPath);
		}

		[TestMethod]
		public void RandomTestFailed()
		{
			long[] enter = new long[] { 10, 20 };
			long[] exit = new long[] { 24, 5 };

			long[,] stationCost = new long[,]
				{
					{10, 20, 7, 38, 8, 9},
					{11, 2, 34, 52, 9, 10}
				};

			long[,] switchCost = new long[,]
				{
					{2, 5, 100, 2, 88},
					{89, 23, 1, 23, 94},
				};

			AssemblyLineProblem target = new AssemblyLineProblem();

			target.Enter = enter;
			target.Exit = exit;
			target.StationCost = stationCost;
			target.SwitchCost = switchCost;

			target.Solve(true);
			long expectedTime = target.MinTime;
			WritePath(target);

			target.Solve(false);
			long actualTime = target.MinTime;
			WritePath(target);

			Assert.AreEqual(expectedTime, actualTime);
		}

		[TestMethod]
		public void RandomTest()
		{
			Random rand = new Random();

			for (int i = 0; i < 100; i++)
			{
				RandomTestA(rand);
			}
		}

		private static void RandomTestA(Random rand)
		{
			int lines = 2;
			int nodeCount = rand.Next(1, 20);
			int min = 1;
			int max = 100000;

			long[] enter = new long[lines];
			long[] exit = new long[lines];
			long[,] stationCost = new long[lines, nodeCount];
			long[,] switchCost = new long[lines, nodeCount - 1];

			for (int i = 0; i < lines; i++)
			{
				enter[i] = rand.Next(min,max);
				exit[i] = rand.Next(min, max);

				for (int j = 0; j < nodeCount; j++)
				{
					stationCost[i, j] = rand.Next(min, max);

					if (j < nodeCount - 1)
					{
						switchCost[i, j] = rand.Next(min, max);
					}
				}
			}

			AssemblyLineProblem target = new AssemblyLineProblem();

			target.Enter = enter;
			target.Exit = exit;
			target.StationCost = stationCost;
			target.SwitchCost = switchCost;

			WriteInput(target);

			target.Solve(true);
			long expectedTime = target.MinTime;
			WritePath(target);

			target.Solve(false);
			long actualTime = target.MinTime;
			WritePath(target);

			Console.WriteLine("Expected = " + expectedTime.ToString());
			Console.WriteLine("Actual = " + actualTime.ToString());

			Assert.AreEqual(expectedTime, actualTime);
		}

		private static void WriteInput(AssemblyLineProblem target)
		{
			StringBuilder sb = new StringBuilder();

			sb.Append("Enter");
			target.Enter.Aggregate<long, StringBuilder>(sb, (s, i) =>
			{
				s.Append(",");
				s.Append(i.ToString());
				return s;
			});
			sb.AppendLine();

			sb.Append("Exit");
			target.Exit.Aggregate<long, StringBuilder>(sb, (s, i) =>
			{
				s.Append(",");
				s.Append(i.ToString());
				return s;
			});
			sb.AppendLine();

			Console.WriteLine(sb.ToString());

			Console.Write("StationCost");
			for (int i = 0; i < target.StationCost.GetLength(0); i++)
			{
				for (int j = 0; j < target.StationCost.GetLength(1); j++)
				{
					Console.Write(target.StationCost[i, j].ToString() + ",");
				}
				Console.WriteLine();
			}

			Console.Write("switchCost");
			for (int i = 0; i < target.SwitchCost.GetLength(0); i++)
			{
				for (int j = 0; j < target.SwitchCost.GetLength(1); j++)
				{
					Console.Write(target.SwitchCost[i, j].ToString() + ",");
				}
				Console.WriteLine();
			}
		}

		private static void WritePath(AssemblyLineProblem target)
		{
			StringBuilder sb = new StringBuilder();

			target.Path.Aggregate<int, StringBuilder>(sb, (s, i) =>
			{
				s.Append(",");
				s.Append(i.ToString());
				return s;
			});

			Console.WriteLine(sb.ToString());
		}

		private static void Solve(long[] enter, long[] exit, long[,] stationCost, long[,] switchCost, long expectedTime, int[] expectedPath)
		{
			AssemblyLineProblem target = new AssemblyLineProblem();

			target.Enter = enter;
			target.Exit = exit;
			target.StationCost = stationCost;
			target.SwitchCost = switchCost;

			target.Solve(false);

			Assert.AreEqual(expectedTime, target.MinTime);
			
			//CollectionAssert.AreEqual(expectedPath, target.Path);
		}
	}
}
