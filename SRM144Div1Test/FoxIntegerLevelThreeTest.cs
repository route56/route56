using TCO11QR2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM144Div1Test
{
    
    
    /// <summary>
    ///This is a test class for FoxIntegerLevelThreeTest and is intended
    ///to contain all FoxIntegerLevelThreeTest Unit Tests
    ///</summary>
	[TestClass()]
	public class FoxIntegerLevelThreeTest
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
		///A test for count
		///</summary>
		[TestMethod()]
		public void countTest()
		{
//            RunCT(10,
//16,2,
//"The representable numbers are 10 (= 10 * d(10)) and 16 (= 4 * d(4)).");

//            RunCT(
//123,
//123,
//0,
//"123 is not representable.");
    
//RunCT(160,
//160,
//1,
//"160 can be represented by two ways:");
    
//RunCT(47,
//58,
//4,"");

    RunCT(
123456789,
9876543210,
2618024258,"");

		}

		private void RunCT(long min, long max, long expected, string p_4)
		{
			FoxIntegerLevelThree target = new FoxIntegerLevelThree(); // TODO: Initialize to an appropriate value
			long actual;
			actual = target.count(min, max);
			Assert.AreEqual(expected, actual, p_4);
		}
	}
}
