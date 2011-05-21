using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCJSolver;

namespace GCJUnitTester
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			Assert.AreEqual(Program.gcd(56, -100), 4);
			Assert.AreEqual(Program.gcd(24, -56), 8);
			Assert.AreEqual(Program.gcd(4, 6), 2);
			Assert.AreEqual(Program.gcd(6, 4), 2);
			Assert.AreEqual(Program.gcd(10, 6), 2);
			Assert.AreEqual(Program.gcd(3, 17), 1);
			Assert.AreEqual(Program.gcd(2, 19), 1);
			Assert.AreEqual(Program.gcd(19, 2), 1);
		}

		[TestMethod]
		public void ForExample3()
		{
			//1 100 50
			Assert.AreEqual(true, Program.IsFreeCellStatCorrect(1, 100, 50));
			//10 10 100
			Assert.AreEqual(false, Program.IsFreeCellStatCorrect(10, 10, 100));
			//9 80 56
			Assert.AreEqual(true, Program.IsFreeCellStatCorrect(9, 80, 56));
		}

	}
}
