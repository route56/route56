using SRM504._5Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM504._5.Test
{
    
    
    /// <summary>
    ///This is a test class for TheJackpotDivTwoTest and is intended
    ///to contain all TheJackpotDivTwoTest Unit Tests
    ///</summary>
	[TestClass()]
	public class TheJackpotDivTwoTest
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
		///A test for find
		///</summary>
		[TestMethod()]
		public void findTest2()
		{
			RunIneffFindTestFor(
				new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000 },
				1000000,
				new int[] { 
		185185,
		185185,
		185185,
		185185,
		185185,
		185186,
		1000000,});

		}
		[TestMethod()]
		public void findTest()
		{
			RunFindTestFor(
				new int[] { 1, 10, 100, 1000, 10000, 100000, 1000000 },
				1000000,
				new int[] { 
		185185,
		185185,
		185185,
		185185,
		185185,
		185186,
		1000000,});

		}

		private void RunIneffFindTestFor(int[] money, int jackpot, int[] expected )
		{
			TheJackpotDivTwo target = new TheJackpotDivTwo(); // TODO: Initialize to an appropriate value
			int[] actual;
			int[] moneyCopy = new int[money.Length];

			Array.Copy(money, moneyCopy, money.Length);
			actual = target.InEfficientfind(moneyCopy, jackpot);
			CollectionAssert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void findTest3()
		{

			RunFindTestFor(
				new int[] { 1, 10 },
				1000000,
				new int[] { 500005, 500006 });


			RunFindTestFor(
				new int[] { 1, 1, 1, 1 },
				1000000,
				new int[] { 250001, 250001, 250001, 250001 });


			RunFindTestFor(
				new int[] { 1, 1, 1, 1 },
				1000,
				new int[] { 251,251,251,251});

			RunFindTestFor(
				new int[] { 1, 1, 1, 10000 },
				300,
				new int[] { 101, 101, 101, 10000 });

			RunFindTestFor(
new int[] {1, 2, 3, 4},
2,
new int[] {2, 3, 3, 4 });

			RunFindTestFor(
			new int[] {4, 7},
1,
new int[] {5, 7 });

			RunFindTestFor(
			new int[] {1},
100,
new int[] {101 });
			RunFindTestFor(
new int[] { 21, 85, 6, 54, 70, 100, 91, 60, 71 },
15,
new int[] { 21, 21, 54, 60, 70, 71, 85, 91, 100 });

		}

		private void RunFindTestFor(int[] money, int jackpot, int[] expected )
		{
			TheJackpotDivTwo target = new TheJackpotDivTwo(); // TODO: Initialize to an appropriate value
			int[] actual;
			int[] moneyCopy = new int[money.Length];

			Array.Copy(money, moneyCopy, money.Length);
			actual = target.find(moneyCopy, jackpot);
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
