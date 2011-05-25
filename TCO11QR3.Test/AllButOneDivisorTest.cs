using TCO11QR3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TCO11QR3.Test
{
    
    
    /// <summary>
    ///This is a test class for AllButOneDivisorTest and is intended
    ///to contain all AllButOneDivisorTest Unit Tests
    ///</summary>
	[TestClass()]
	public class AllButOneDivisorTest
	{


		private TestContext testContextInstance;

		/// <summary>
		///Gets or sets the test context which provides
		///information about and functionality for the current test run.
		///</summary>
		public TestContext TestContext
		{
			get
			{
				return testContextInstance;
			}
			set
			{
				testContextInstance = value;
			}
		}

		#region Additional test attributes
		// 
		//You can use the following additional attributes as you write your tests:
		//
		//Use ClassInitialize to run code before running the first test in the class
		//[ClassInitialize()]
		//public static void MyClassInitialize(TestContext testContext)
		//{
		//}
		//
		//Use ClassCleanup to run code after all tests in a class have run
		//[ClassCleanup()]
		//public static void MyClassCleanup()
		//{
		//}
		//
		//Use TestInitialize to run code before running each test
		//[TestInitialize()]
		//public void MyTestInitialize()
		//{
		//}
		//
		//Use TestCleanup to run code after each test has run
		//[TestCleanup()]
		//public void MyTestCleanup()
		//{
		//}
		//
		#endregion


		/// <summary>
		///A test for getMinimum
		///</summary>
		[TestMethod()]
		public void getMinimumTest()
		{
			RunGMTFor(
new int[] {2,3,5},
6,
"There are many possible values for n in this case. For example: 6, 15, 75 and 12. 6 is the smallest of them.");

    RunGMTFor(
new int[] {2,4,3,9},
12,"");

			RunGMTFor(
new int[] {3,2,6},
-1,
"Every multiple of 3 and 2 is also a multiple of 6. Every multiple of 6 is also a multiple of 2 and 3.  Therefore, a number that is a multiple of exactly 2 out of the three elements in this array cannot exist.");
		}

		[TestMethod()]
		public void getMinimumTest2()
		{
			RunGMTFor(
				new int[] {6,7,8,9,10},
360,
"");
    RunGMTFor(
new int[] {10,6,15},
-1,
			"");
		}

		private void RunGMTFor(int[] divisors, int expected, string msg)
		{
			AllButOneDivisor target = new AllButOneDivisor(); // TODO: Initialize to an appropriate value
			int actual;
			actual = target.getMinimum(divisors);
			Assert.AreEqual(expected, actual, msg);
		}
	}
}
