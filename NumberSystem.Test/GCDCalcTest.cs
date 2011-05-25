using NumberSystem;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace NumberSystem.Test
{
    
    
    /// <summary>
    ///This is a test class for GCDCalcTest and is intended
    ///to contain all GCDCalcTest Unit Tests
    ///</summary>
	[TestClass()]
	public class GCDCalcTest
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
		///A test for GetGCD
		///</summary>
		[TestMethod()]
		public void GetGCDTestTwoNumbers()
		{
			RunGetGCDTestTwoNumbersFor(2, 3, 1, "two prime nos");
			RunGetGCDTestTwoNumbersFor(2, 2, 2, "same nums");
			RunGetGCDTestTwoNumbersFor(2, 2*2, 2, "power of same num");
			RunGetGCDTestTwoNumbersFor(2*2*2, 2*5*7, 2, "2 is common");
			RunGetGCDTestTwoNumbersFor(5*7*11, 11*13*19, 11, "beyond 2 test cases");
			RunGetGCDTestTwoNumbersFor(2 * 5 * 7 * 11, 7 * 11 * 13, 7*11, "gcd not a prime no");
		}

		private void RunGetGCDTestTwoNumbersFor(int a, int b, int expected, string msg)
		{
			GCDCalc target = new GCDCalc();
			long actual;
			actual = target.GetGCD(a, b);
			Assert.AreEqual(expected, actual, msg);
		}

		/// <summary>
		///A test for GetGCD
		///</summary>
		[TestMethod()]
		public void GetGCDTestForListOfNumbers()
		{
			RunGetGCDTestForListOfNumbersFor(
				new long[] { 2, 3, 5, 7 },
				1,
				"All primes");

			RunGetGCDTestForListOfNumbersFor(
				new long[] { 2 * 2, 2 * 5, 2 * 7, 2 * 3 },
				2,
				"2 is common");

			RunGetGCDTestForListOfNumbersFor(
				new long[] { 2 * 3 * 5 * 7 * 7 * 11, 2 * 2 * 2 * 5 * 7 * 7, 3 * 3 * 5 * 7 * 7, 5 * 5 * 5 * 7 * 7, 5 * 7 * 7 * 11 * 13 },
				5 * 7 * 7,
				"5,7 common in all");
		}

		private void RunGetGCDTestForListOfNumbersFor(long[] list, int expected, string msg)
		{
			GCDCalc target = new GCDCalc(); // TODO: Initialize to an appropriate value
			long actual;
			actual = target.GetGCD(list);
			Assert.AreEqual(expected, actual,msg);
		}
	}
}
