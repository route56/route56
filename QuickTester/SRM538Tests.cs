using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexProblems.SRM538;

namespace QuickTester
{
	[TestClass]
	public class SRM538Tests
	{

		[TestMethod]
		public void Test250()
		{
			Assert.AreEqual(new LeftOrRight().maxDistance("LLLRLRRR"), 3);
			Assert.AreEqual(new LeftOrRight().maxDistance("R???L"), 4);
			Assert.AreEqual(new LeftOrRight().maxDistance("??????"), 6);
			Assert.AreEqual(new LeftOrRight().maxDistance("LL???RRRRRRR???"), 11);
			Assert.AreEqual(new LeftOrRight().maxDistance("L?L?"), 4);
		}

		[TestMethod]
		public void MyTestMethod()
		{
			Assert.AreEqual(
				new EvenRoute().isItPossible(
				new int[] {-1,-1,1,1},
				new int[] {-1,1,1,-1},
				0),
				"CAN");

			Assert.AreEqual(
				new EvenRoute().isItPossible(
				new int[] {-5,-3,2},
				new int[] {2,0,3},
				1),
				"CAN");


			Assert.AreEqual(
				new EvenRoute().isItPossible(
				new int[] {1001, -4000},
				new int[] {0, 0},
				1),
				"CAN");


			Assert.AreEqual(
				new EvenRoute().isItPossible(
				new int[] {11, 21, 0},
				new int[] {-20, 42, 7},
				0),
				"CANNOT");

			Assert.AreEqual(
				new EvenRoute().isItPossible(
				new int[] {0, 6},
				new int[] {10, -20},
				1),
				"CANNOT");
		}

		[TestMethod]
		public void MyTestMethod_Others()
		{
			Assert.AreEqual(
				new Other().isItPossible(
				new int[] { -1, -1, 1, 1 },
				new int[] { -1, 1, 1, -1 },
				0),
				"CAN");

			Assert.AreEqual(
				new Other().isItPossible(
				new int[] { -5, -3, 2 },
				new int[] { 2, 0, 3 },
				1),
				"CAN");


			Assert.AreEqual(
				new Other().isItPossible(
				new int[] { 1001, -4000 },
				new int[] { 0, 0 },
				1),
				"CAN");


			Assert.AreEqual(
				new Other().isItPossible(
				new int[] { 11, 21, 0 },
				new int[] { -20, 42, 7 },
				0),
				"CANNOT");

			Assert.AreEqual(
				new Other().isItPossible(
				new int[] { 0, 6 },
				new int[] { 10, -20 },
				1),
				"CANNOT");
		}

		[TestMethod]
		public void CompareRandomly()
		{
			Random rand = new Random();

			int size = rand.Next() % 50 + 1;
			int parity = rand.Next() % 2;

			int[] x = new int[size];
			int[] y = new int[size];

			for (int i = 0; i < size; i++)
			{
				x[i] = GetSign(rand) * (rand.Next() % 100000);
				y[i] = GetSign(rand) * (rand.Next() % 100000);
			}

			RemoveDuplicates(x, y);

			Assert.AreEqual(new EvenRoute().isItPossible(x,y,parity),
				new Other().isItPossible(x,y,parity));
		}

		[TestMethod]
		public void CompareRand_100Times()
		{
			for (int i = 0; i < 100; i++)
			{
				CompareRandomly();
			}
		}

		private void RemoveDuplicates(int[] x, int[] y)
		{
			//Dictionary<int, List<int>> dups = new Dictionary<int, List<int>>();

			//for (int i = 0; i < x.Length; i++)
			//{

			//}
			// TODO 
			return;
		}

		private int GetSign(Random rand)
		{
			return rand.Next() % 2 == 0 ? -1 : 1;
		}
	}
}
