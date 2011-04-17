using SRM503Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM503
{
    
    
    /// <summary>
    ///This is a test class for KingdomXCitiesandVillagesAnotherTest and is intended
    ///to contain all KingdomXCitiesandVillagesAnotherTest Unit Tests
    ///</summary>
	[TestClass()]
	public class KingdomXCitiesandVillagesAnotherTest
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
		///A test for determineLength
		///</summary>
		[TestMethod()]
		[DeploymentItem("SRM503Div2.dll")]
		public void determineLengthTest()
		{
a(
new int[] {1},
new int[] {1},
new int[] {2,3},
new int[] {1,1},
2.0);
//If you pick village 0 first, the total length is 2.0. Otherwise, it's 3.0. So, the minimum possible total length is 2.0

a(
new int[] {1,2},
new int[] {1,1},
new int[] {1,2},
new int[] {2,2},
2.0);


a(    
new int[] {0},
new int[] {0},
new int[] {2},
new int[] {2},
2.8284271247461903);

			// failed one
			a(
//				Problem: 900
//Test Case: 5
//Succeeded: No
//Execution Time: 16 ms
//Args:
//{
			new int[] {920978}, new int[] {375185}, new int[] {228692}, new int[] {991009},
			//}

//Expected:
926552.2676956762);

//Received:
//NaN
		}
		[TestMethod()]
		[DeploymentItem("SRM503Div2.dll")]
		public void determineLengthTest2()
		{
			// Another failed one
			a(
//                Problem: 900
//Test Case: 8
//Succeeded: No
//Execution Time: 0 ms
//Args:
//{
			new int[] {299590}, 
			new int[] {877251}, 
			new int[] {481343, 726215, 152216, 471768}, 
			new int[] {1957, 659416, 729639, 83045},
		//}

//Expected:
1399296.0268321058);

//Received:
//1977574.6117114008

		}

		private void a(int[] cityX,int[] cityY,int[] villageX,int[] villageY,double expected)
		{
			KingdomXCitiesandVillagesAnother_Accessor target = new KingdomXCitiesandVillagesAnother_Accessor(); // TODO: Initialize to an appropriate value
			double actual;
			actual = target.determineLength(cityX, cityY, villageX, villageY);
			Assert.AreEqual(expected, actual);
		}
	}
}
