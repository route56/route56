using MultiDimKnapsackSimplified;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Solver.Test
{
	[TestClass()]
	public class SolverTest
	{
		//public void SolveTestHelper<TItem, TDim>()
		//{
		//    Solver<TItem, TDim> target = new Solver<TItem, TDim>();
		//    IList<TItem> items = null; // TODO: Initialize to an appropriate value
		//    IList<TDim> dim = null; // TODO: Initialize to an appropriate value
		//    Func<TItem, TDim, bool> map = null; // TODO: Initialize to an appropriate value
		//    IList<uint> weight = null; // TODO: Initialize to an appropriate value
		//    IList<TItem> expected = null; // TODO: Initialize to an appropriate value
		//    IList<TItem> actual;
		//    actual = target.Solve(items, dim, map, weight);
		//    Assert.AreEqual(expected, actual);
		//    Assert.Inconclusive("Verify the correctness of this test method.");
		//}

		//[TestMethod()]
		//public void SolveTest()
		//{
		//    SolveTestHelper<GenericParameterHelper, GenericParameterHelper>();
		//}

		[TestMethod()]
		public void SolveTest_BasicCases()
		{
			int[,] map = 
			{
				{1, 1, 0},
				{0, 1, 1}
			};

			int[] weight = 
			{
				1,
				1
			};

			int[] expected = { 0, 2 };

			SolverNonFancy target = new SolverNonFancy();

			CollectionAssert.AreEqual(expected, target.SolveNonFancy(map, weight));

			weight = new int[] { 2, 2 };

			var actual = target.SolveNonFancy(map, weight);
			Assert.AreEqual(2, actual.Length);
			CollectionAssert.AreNotEqual(expected, actual);
		}

		[TestMethod()]
		public void SolveTest_5X3Case()
		{
			int[,] map = 
			{
				{1, 1, 0, 1, 1},
				{0, 1, 1, 1, 0},
				{0, 1, 1, 0, 0},
			};

			int[] weight = 
			{
				3,
				2,
				1
			};

			int[] expected = { 0, 2, 3, 4};
			int[] powerSolver = new PowerSet().Solve(map, weight);

			SolverNonFancy target = new SolverNonFancy();
			var actual = target.SolveNonFancy(map, weight);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void RandomUnder32Sets()
		{
			Random rand = new Random();

			RandomTest(rand, (e,a) => CollectionAssert.AreEqual(e,a));
		}

		[TestMethod]
		public void RandomTestCalledMultipleTimes()
		{
			Random rand = new Random();

			for (int i = 0; i < 100; i++)
			{
				RandomTest(rand,
					(e, a) =>
					{
						if (e.Length - a.Length > 1)
						{
							Assert.Fail();
						}
					});
			}
		}

		private void RandomTest(Random rand, Action<int[], int[]> assert)
		{
			int[,] map = GetRandomMap(rand);

			int[] weight = GetRandomWeight(rand, map.GetLength(0));

			int[] expected = new PowerSet().Solve(map, weight);

			DebugDump(map, weight, expected);

			SolverNonFancy target = new SolverNonFancy();
			var actual = target.SolveNonFancy(map, weight);
			Debug.WriteLineIf(actual.Length < expected.Length, "Diff = " + (expected.Length - actual.Length).ToString());
			assert(expected, actual);
		}

		[TestMethod]
		public void FailedTestDetectedByRandom()
		{
			int[,] map = 
			{
				{0, 1, 1, 1, 0, 0, 0, 1, 0, 0, 1, },
				{1, 1, 1, 1, 0, 1, 0, 0, 0, 0, 0, },
				{0, 0, 0, 0, 1, 0, 1, 0, 0, 1, 1, },
				{1, 0, 1, 1, 1, 0, 0, 0, 0, 1, 0, },
				{1, 1, 0, 1, 0, 0, 0, 1, 0, 1, 0, },
				{0, 1, 0, 1, 0, 0, 1, 0, 1, 1, 0, },
				{0, 1, 1, 0, 0, 0, 0, 0, 0, 1, 0, },
				{1, 0, 0, 1, 0, 1, 0, 0, 1, 1, 0, },
				{0, 1, 0, 1, 1, 1, 0, 0, 0, 1, 0, },
				{1, 1, 1, 0, 0, 0, 0, 0, 1, 0, 1, },
				{1, 1, 1, 0, 0, 1, 0, 0, 1, 0, 0, },
			};

			int[] weight = 
			{
				14, 
				2, 
				10, 
				1, 
				9, 
				2, 
				6, 
				3, 
				15, 
				12, 
				9, 
			};

			int[] expected =   { 1, 4, 5, 6, 7, 10, };
							// { 0, 1, 6, 7, 10};

			SolverNonFancy target = new SolverNonFancy();
			var actual = target.SolveNonFancy(map, weight);
			CollectionAssert.AreEqual(expected, actual);
		}

		private void DebugDump(int[,] map, int[] weight, int[] expected)
		{
			Debug.WriteLine("int[,] map = {");
			for (int i = 0; i < map.GetLength(0); i++)
			{
				Debug.Write("\t{");
				for (int j = 0; j < map.GetLength(1); j++)
				{
					Debug.Write(map[i, j]);
					Debug.Write(", ");
				}
				Debug.WriteLine("},");
			}
			Debug.WriteLine("};");

			Debug.WriteLine("int[] weight = {");
			for (int i = 0; i < weight.Length; i++)
			{
				Debug.Write(weight[i]);
				Debug.WriteLine(", ");
			}
			Debug.WriteLine("};");

			Debug.WriteLine("int[] expected = {");
			for (int i = 0; i < expected.Length; i++)
			{
				Debug.Write(expected[i]);
				Debug.Write(", ");
			}
			Debug.WriteLine("};");
		}

		private int[] GetRandomWeight(Random rand, int size)
		{
			if (size > _MaxSize)
			{
				Assert.Inconclusive();
			}
			
			int[] result = new int[size];

			for (int i = 0; i < size; i++)
			{
				result[i] = rand.Next(1, _MaxSize);
			}

			return result;
		}

		private int[,] GetRandomMap(Random rand)
		{
			int row = rand.Next(1, _MaxSize);
			int col = rand.Next(1, _MaxSize);

			int[,] result = new int[row, col];

			for (int i = 0; i < row; i++)
			{
				for (int j = 0; j < col; j++)
				{
					result[i, j] = rand.Next(0, 2);
				}
			}

			return result;
		}

		private const int _MaxSize = 16;
	}
}
