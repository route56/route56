using SRM505Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM144Div1Test
{
    
    
    /// <summary>
    ///This is a test class for PerfectSequencesTest and is intended
    ///to contain all PerfectSequencesTest Unit Tests
    ///</summary>
	[TestClass()]
	public class PerfectSequencesTest
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
		///A test for fixIt
		///</summary>
		[TestMethod()]
		public void fixItTest()
		{
			RunFixItTest(
new int[] {1,3,4},
"Yes",
"If we change the last element to 2, we have {1,3,2}. 1+3+2 = 1*3*2.");

    
	RunFixItTest(
new int[] {1,2,3},
"No",
"This sequence is already perfect and it is not possible to change exactly one of its elements and keep it perfect.");
    
	RunFixItTest(
new int[] {1,4,2,4,2,4},
"No", "");

			RunFixItTest(
			new int[] {8},
"Yes",
"It is possible to change the first element to any non-negative number and the sequence will stay perfect.");
		

		
    RunFixItTest(
new int[] {1000000,1,1,1,1,2},
"Yes",
"It is possible to replace 1000000 with 6 to make the sequence become perfect.");
		
			RunFixItTest(new int[] { 100000, 2, 1 }, "Yes", "4 with 3");
		}


		[TestMethod()]
		public void fixItTest2()
		{

			RunFixItTest(
new int[] { 2, 0, 2 },
"No", "");
		}
	[TestMethod()]
		public void fixItTest3()
		{
			RunFixItTest(
	  new int[] { 2, 1, 0 },
	  "Yes", "");
}
		

		private void RunFixItTest(int[] seq,string expected,string p_3)
		{

			PerfectSequences target = new PerfectSequences(); // TODO: Initialize to an appropriate value
			string actual;
			actual = target.fixIt(seq);
			Assert.AreEqual(expected, actual, p_3);
		}
	}
}
