using SRM211Div1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSRM211Div1
{
    
    
    /// <summary>
    ///This is a test class for CircuitsTest and is intended
    ///to contain all CircuitsTest Unit Tests
    ///</summary>
	[TestClass()]
	public class CircuitsTest
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
		///A test for howLong
		///</summary>
		[TestMethod()]
		public void howLongTest()
		{
			RunHowLongTestFor(
new string[] {"1 2",
 "2",
 ""},
new string[] {"5 3",
 "7",
 ""},
12, "From above");

		}
			[TestMethod()]
		public void howLongTest2()
		{
			RunHowLongTestFor(
			new string[] { "1 2 3 4 5", "2 3 4 5", "3 4 5", "4 5", "5", "" }
			, new string[] { "2 2 2 2 2", "2 2 2 2", "2 2 2", "2 2", "2", "" }
			, 10,
			"The longest path goes from 0-1-2-3-4-5 for a cost of 10.");
				}
			[TestMethod()]
			public void howLongTest3()
			{
			RunHowLongTestFor(
			new string[] { "1", "2", "3", "", "5", "6", "7", "" }
			, new string[] { "2", "2", "2", "", "3", "3", "3", "" }
			, 9
			, "The 0-1-2-3 path costs 6 whereas the 4-5-6-7 path costs 9");
				}
			[TestMethod()]
			public void howLongTest4()
			{
			RunHowLongTestFor(
			new string[] 
{"","2 3 5","4 5","5 6","7","7 8","8 9","10",
 "10 11 12","11","12","12",""}
			, new string[] {"","3 2 9","2 4","6 9","3","1 2","1 2","5",
 "5 6 9","2","5","3",""}
			, 22, "");
				}
			[TestMethod()]
			public void howLongTest5()
			{
			RunHowLongTestFor(
			new string[] { "", "2 3", "3 4 5", "4 6", "5 6", "7", "5 7", "" }
			, new string[] { "", "30 50", "19 6 40", "12 10", "35 23", "8", "11 20", "" }
			, 105, "");
		}

			[TestMethod()]
			public void howLongTestTCFailed1()
	{
//        Problem: 275
//Test Case: 5
//Succeeded: No
//Execution Time: 15 ms
		RunHowLongTestFor(
new string[] {"", "", "", "38 35 14 40 21 19 27 4 31", "", "", "", "", "", "", "", "", "", "1 27 37", "", "29 6 38", "", "1 45 28", "20 26 24 10 17 6", "", "47 1 24", "", "", "13 7 40 43 8 30 48 10 19", "", "", "7 45 0 2 47", "", "", "", "", "", "", "", "", "", "", "45 44", "7 12 28", "4 19 35 9", "6 22 46", "", "48 26 3 22 19 25 40 28", "18", "", "", "13 30", "", "8 15 40 12 45 41 7 0 22", ""}, new string[] {"", "", "", "51 45 94 86 94 16 4 36 66", "", "", "", "", "", "", "", "", "", "10 39 33", "", "83 89 12", "", "13 75 97", "52 24 30 87 21 72", "", "49 15 67", "", "", "90 53 63 36 14 62 81 36 55", "", "", "96 36 87 86 90", "", "", "", "", "", "", "", "", "", "", "69 100", "48 57 47", "71 27 21 36", "17 39 49", "", "74 3 17 69 100 90 3 84", "16", "", "", "66 52", "", "40 31 97 67 85 89 90 62 87", ""},
426, "TC FAILED 1");

//Received:
//334
	}

			[TestMethod()]
			public void howLongTestTCFailed2()
			{
//                Problem: 275
//Test Case: 6
//Succeeded: No
//Execution Time: 0 ms
RunHowLongTestFor(
new string[] {"", "", "", "46", "", "21 29 15 43 30 16", "", "35 18", "", "", "", "", "", "", "47 38", "", "", "", "", "", "", "28 35 30 32 44", "24 18 14 47 1", "", "", "40 29 43 31 13", "", "", "33 34", "13 17 4 40 8 27 16 14 31", "3 18", "6 33 23 14 36", "", "", "", "", "", "39 40", "", "3 14 20 38 0 42 35 22 15", "", "", "14 5 23 8", "", "", "", "48 19 41", "", "23", "1 37 12 28 4 43"}, new string[] {"", "", "", "3", "", "19 78 78 19 40 81", "", "2 6", "", "", "", "", "", "", "25 37", "", "", "", "", "", "", "89 17 31 80 70", "74 5 17 38 94", "", "", "66 20 18 18 100", "", "", "95 60", "19 57 39 28 93 96 71 21 2", "60 48", "56 94 50 11 20", "", "", "", "", "", "10 5", "", "22 8 39 70 68 64 73 99 93", "", "", "93 56 70 49", "", "", "", "77 29 72", "", "54", "56 78 54 48 93 47"}
,452, "TC FAILED 2");

//Received:
//203
			}

		private void RunHowLongTestFor(string[] connects,string[] costs,int expected ,string assertMsg)
		{
			Circuits target = new Circuits();
			int actual;
			actual = target.howLong(connects, costs);
			Assert.AreEqual(expected, actual, assertMsg);
		}
	}
}
