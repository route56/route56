using SRM503Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM503
{
    
    
    /// <summary>
    ///This is a test class for ToastXToastTest and is intended
    ///to contain all ToastXToastTest Unit Tests
    ///</summary>
	[TestClass()]
	public class ToastXToastTest
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
		///A test for bake
		///</summary>
		[TestMethod()]
		public void bakeTest()
		{
	RunBakeTest(    
new int[] {2,4},
new int[] {5,6,3},
2, "Two types of bread is sufficient as illustrated below.  ");
    
				RunBakeTest(    
new int[] {5},
new int[] {4}, -1, "");

RunBakeTest(    

    
new int[] {1,2,3},
new int[] {5,6,7},
1, "");



    RunBakeTest(    
new int[] {1,3,5},
new int[] {2,4,6},2, "");

	RunBakeTest(
new int[] { 1, 3, 5 },
new int[] { 2, 4, }, 
-1, 
"");

	RunBakeTest(
new int[] { 1, 3 },
new int[] { 2, 4, 6, 8 }, 2, "");

	RunBakeTest(
new int[] { 1 },
new int[] { 2 }, 1, "");

	RunBakeTest(
new int[] { 1 },
new int[] { 2, 3, 4 }, 1, "");

			RunBakeTest(

//Succeeded: No
//Execution Time: 0 ms
//Args:
//{
			new int[] {5}, new int[] {1, 9},
		//}

//Expected:
-1, "FAILED System test case!");

//Received:
//2

		}

		private void RunBakeTest(int[] undertoasted,int[] overtoasted,int expected,string p_4)
		{
			ToastXToast target = new ToastXToast(); // TODO: Initialize to an appropriate value
			int actual;
			actual = target.bake(undertoasted, overtoasted);
			Assert.AreEqual(expected, actual, p_4);
		}
	}
}
