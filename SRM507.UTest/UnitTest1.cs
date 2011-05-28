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
	}
}
