using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SRM532_Div2.Test
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			DengklekTryingToSleep tg = new DengklekTryingToSleep();

			Assert.AreEqual(tg.minDucks(new int[] { 5, 3, 2 }), 1);
			Assert.AreEqual(tg.minDucks(new int[] { 58 } ), 0);
			Assert.AreEqual(tg.minDucks(new int[] { 9, 3, 6, 4 }), 3);
			Assert.AreEqual(tg.minDucks(new int[] {7, 4, 77, 47, 74, 44}), 68);
			Assert.AreEqual(tg.minDucks(new int[] {1, 2, 3, 4, 5, 6, 7, 8, 9, 10}), 0);
		}

		[TestMethod]
		public void DengklekMakingChainsTest()
		{
			TestThis(new string[] { ".15", "7..", "402", "..3" }, 19);
			//One possible solution: 
			//In the first step, concatenate the chain pieces in the order "..3", ".15", "402", "7.." to obtain the chain "..3.154027..".
			//In the second step, pick the subsequence "154027".
			//The beauty of the chain in this solution is 1+5+4+0+2+7 = 19.
			//1)


			TestThis(new string[] { "..1", "7..", "567", "24.", "8..", "234" }, 36);
			//One possible solution is to concatenate the chain pieces in this order:  "..1", "234", "567", "8..", "24.", "7.." -> "..12345678..24.7..",  and then to pick the subsequence "12345678". Its beauty is 1+2+3+4+5+6+7+8 = 36.
			//2)


			TestThis(new string[] { "...", "..." }, 0);
			//Mr. Dengklek cannot pick any links.
			//3)


			TestThis(new string[] { "16.", "9.8", ".24", "52.", "3.1", "532", "4.4", "111" }, 28);


			TestThis(new string[] { "..1", "3..", "2..", ".7." }, 7);

			TestThis(new string[] { "412", "..7", ".58", "7.8", "32.", "6..", "351", "3.9", "985", "...", ".46" }, 58);

		}

		private void TestThis(string[] sequence, int ans)
		{
			Assert.AreEqual(new DengklekMakingChains().maxBeauty(sequence), ans);
		}
	}
}
