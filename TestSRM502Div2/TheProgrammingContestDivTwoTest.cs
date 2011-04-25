using SRM502Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSRM502Div2
{


	/// <summary>
	///This is a test class for TheProgrammingContestDivTwoTest and is intended
	///to contain all TheProgrammingContestDivTwoTest Unit Tests
	///</summary>
	[TestClass()]
	public class TheProgrammingContestDivTwoTest
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
		public void findTest()
		{
			//0)


			RunFindTestFor(74,
			new int[] { 47 },
				//Returns: 
						new int[] { 1, 47 },
			"They can solve the task. solved will be 1 and penalty will be 47.");
			//1)


			RunFindTestFor(74,
			new int[] { 4747 },
				//Returns: 
						new int[] { 0, 0 },
			"They don't have time enough to solve the task.");
			//2)


			RunFindTestFor(47,
			new int[] { 8, 5 },
				//Returns: 
						new int[] { 2, 18 },
			"The order is important. If they solve task 0 first and task 1 second, solved will be 2 and penalty will be 21. If they solve task 1 first and task 0 second, solved will be 2 and penalty will be 18.");
			//3)


			RunFindTestFor(47,
			new int[] { 12, 3, 21, 6, 4, 13 },
				//Returns: 
						new int[] { 5, 86 }, "");

			//4)


			RunFindTestFor(58,
			new int[] { 4, 5, 82, 3, 4, 65, 7, 6, 8, 7, 6, 4, 8, 7, 6, 37, 8 },
				//Returns: 
						new int[] { 10, 249 }, "");

			//5)


			RunFindTestFor(100000,
			new int[] { 100000 },
				//Returns: 
						new int[] { 1, 100000 }, "");
		}

		private void RunFindTestFor(int T, int[] requiredTime, int[] expected, string p_4)
		{
			TheProgrammingContestDivTwo target = new TheProgrammingContestDivTwo(); // TODO: Initialize to an appropriate value
			int[] actual;
			actual = target.find(T, requiredTime);
			CollectionAssert.AreEqual(expected, actual, p_4);
		}
	}
}
