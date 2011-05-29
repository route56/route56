using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SRM507.UTest
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			RunCubeAntsFor(new int[] { 0, 1, 1 }, 1);

			RunCubeAntsFor(new int[] { 5, 4 }, 2);

			RunCubeAntsFor(new int[] { 0 }, 0);
			RunCubeAntsFor(new int[] { 6, 1, 6 }, 3);
			RunCubeAntsFor(new int[] { 7, 7, 3, 3, 7, 7, 3, 3 }, 2);
		}

		[TestMethod]
		public void StickerTest()
		{
			    
			RunStickerTestFor(new string[] {"cyan","magenta","yellow","purple","black","white","purple"}, "YES");
    
RunStickerTestFor(new string[] {"blue","blue","blue","blue","blue","blue","blue","blue","blue","blue"},"NO");

			//f1 < 5
RunStickerTestFor(new string[] {"red","yellow","blue","red","yellow","blue","red","yellow","blue"},"YES");

    //f2 < 3
RunStickerTestFor(new string[] {"purple","orange","orange","purple","black","orange","purple"},"NO");

    
RunStickerTestFor(new string[] {"white","gray","green","blue","yellow","red","target","admin"},"YES");
		}

		[TestMethod]
		public void MyTestForSticker()
		{
			RunStickerTestFor(new string[] { "one", "two", "three", "four", "four", "four"}, "NO");
		}

		private void RunStickerTestFor(string[] p, string p_2)
		{
			Assert.AreEqual(new CubeStickers().isPossible(p), p_2);
		}

		private void RunCubeAntsFor(int[] p, int p_2)
		{
			Assert.AreEqual(new CubeAnts().getMinimumSteps(p), p_2);
		}

		[TestMethod]
		public void CubeRoll()
		{
			RunCubeRollFor(
5,
1,
new int[] { 3 },
-1,
"There is a hole between initPos and goalPos, so the cube cannot be moved from initPos to goalPos.");

			RunCubeRollFor(
36,
72,
new int[] { 300, 100, 200, 400 },
1,
"The distance between initPos and goalPos is a perfect square, and there are no holes between them, so the cube can be moved in a single turn.");

			RunCubeRollFor(
10,
21,
new int[] { 38, 45 },
2,
"One of the optimal solutions is to move the cube to 10 -> -15 -> 21. (Note that the coordinate of the cube can be a negative integer.");


			RunCubeRollFor(
98765,
4963,
new int[] { 10, 20, 40, 30 },
2, "");

			RunCubeRollFor(
68332,
825,
new int[] { 99726, 371, 67, 89210 },
2, "");

		}

		private void RunCubeRollFor(int p, int p_2, int[] p_3, int p_4, string p_5)
		{
			throw new NotImplementedException();
		}

		[TestMethod]
		public void IsHolePresent()
		{
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, 1, 8, true, "everyone is in between");
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, 8, 10, false, "upper side");
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, -5, -2, false, "lower side");
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, 3, 4, false, "in range but no hole");
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, 6, 9, true, "between and higher");
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, 1, 3, true, "lower and between");
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, 5, 6, true, "start at hole");
			RunIsHolePresentFor(new int[] { 2, 5, 7 }, 4, 5, true, "end at hole");
		}

		private void RunIsHolePresentFor(int[] holes, int x, int y, bool expected, string msg)
		{
			Assert.AreEqual(new CubeRoll().IsAnyHoleBetween(holes, x, y), expected, msg);
		}
	}
}
