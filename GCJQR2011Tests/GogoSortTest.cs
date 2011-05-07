using QR2011;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace GCJQR2011Tests
{
    
    
    /// <summary>
    ///This is a test class for GogoSortTest and is intended
    ///to contain all GogoSortTest Unit Tests
    ///</summary>
	[TestClass()]
	public class GogoSortTest
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
		///A test for SortIt
		///</summary>
		[TestMethod()]
		public void SortItTest()
		{
			RunTestFor(new int[] { 2, 1 }, 2.0);
		}

		private void RunTestFor(int[] arr, double expected)
		{
			GogoSort target = new GogoSort();
			double actual;
			actual = target.SortIt(arr);
			Assert.AreEqual(expected, actual, 0.000001);
		}

		/// <summary>
		///A test for GetLoops
		///</summary>
		[TestMethod()]
		[DeploymentItem("QR2011.dll")]
		public void GetLoopsTest()
		{
			RunGetLoops(new int[] { 3, 1, 2 }, new int[] { 3 });
			RunGetLoops(new int[] { 2, 1 }, new int[] { 2 });
		}

		private void RunGetLoops(int[] arr, int[] expected)
		{
			GogoSort_Accessor target = new GogoSort_Accessor();
			List<int> loops = null;

			target.GetLoops(arr, out loops);

			CollectionAssert.AreEqual(expected, loops.ToArray());
		}
	}
}
