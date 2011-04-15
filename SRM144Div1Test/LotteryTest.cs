using SRM144Div1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestHelpers;

namespace SRM144Div1Test
{
    
    
    /// <summary>
    ///This is a test class for LotteryTest and is intended
    ///to contain all LotteryTest Unit Tests
    ///</summary>
	[TestClass()]
	public class LotteryTest
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


		public void sortByOddsTestRun(string[] rules, string[] expected)
		{
			Lottery target = new Lottery();
			string[] actual;
			actual = target.sortByOdds(rules);
			
			CollectionAssert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for sortByOdds
		///</summary>
		[TestMethod()]
		public void sortByOdds()
		{
			sortByOddsTestRun(
				new string[]
				{
					"PICK ANY TWO: 10 2 F F"
					,"PICK TWO IN ORDER: 10 2 T F"
					,"PICK TWO DIFFERENT: 10 2 F T"
					,"PICK TWO LIMITED: 10 2 T T"
				},
				new string[]
				{
					"PICK TWO LIMITED",
					"PICK TWO IN ORDER",
					"PICK TWO DIFFERENT",
					"PICK ANY TWO"
				});

			sortByOddsTestRun(
				new string[]
				{"INDIGO: 93 8 T F",
 "ORANGE: 29 8 F T",
 "VIOLET: 76 6 F F",
 "BLUE: 100 8 T T",
 "RED: 99 8 T T",
 "GREEN: 78 6 F T",
 "YELLOW: 75 6 F F"}
				,new string[] { "RED", "ORANGE", "YELLOW", "GREEN", "BLUE", "INDIGO", "VIOLET" }
				);

			sortByOddsTestRun(new string[] { }, new string[] { });
		}
	}
}
