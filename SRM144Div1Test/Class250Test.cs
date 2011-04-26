using SRM504Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM144Div1Test
{
    
    
    /// <summary>
    ///This is a test class for Class250Test and is intended
    ///to contain all Class250Test Unit Tests
    ///</summary>
	[TestClass()]
	public class Class250Test
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
		///A test for makeProgram
		///</summary>
		[TestMethod()]
		public void makeProgramTest()
		{
			RunMmakeProgramTest(
			new int[] { 1 },
			new int[] { 2 },
			new int[] { 2 },
			1);

			RunMmakeProgramTest(
		new int[] { 1, 3 },
		new int[] { 2, 1 },
		new int[] { 2, 3 },
		7);

			RunMmakeProgramTest(
		new int[] { 1, 3, 5 },
		new int[] { 2, 1, 7 },
		new int[] { 2, 3, 5 },
		-1);


			RunMmakeProgramTest(
		new int[] { 1, 3, 5 },
		new int[] { 2, 1, 7 },
		new int[] { 1, 3, 5 },
		1);

			RunMmakeProgramTest(
		new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 },
		new int[] { 5, 4, 7, 8, 3, 1, 1, 2, 3, 4, 6 },
		new int[] { 1, 2, 3, 4, 3, 1, 1, 2, 3, 4, 6 },
		7);


			RunMmakeProgramTest(
		new int[] { 1, 5, 6, 7, 8 },
		new int[] { 1, 5, 6, 7, 8 },
		new int[] { 1, 5, 6, 7, 8 },
		1);

		}

		private void RunMmakeProgramTest(int[] A, int[] B, int[] wanted, int expected)
		{
			////Class250 target = new Class250(); // TODO: Initialize to an appropriate value
			//int actual;
			//actual = target.makeProgram(A, B, wanted);
			//Assert.AreEqual(expected, actual);
		}
	}
}
