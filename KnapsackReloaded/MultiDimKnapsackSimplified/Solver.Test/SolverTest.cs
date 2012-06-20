using MultiDimKnapsackSimplified;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;

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
	}
}
