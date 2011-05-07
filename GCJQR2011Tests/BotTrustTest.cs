using QR2011;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GCJQR2011Tests
{
    
    
    /// <summary>
    ///This is a test class for BotTrustTest and is intended
    ///to contain all BotTrustTest Unit Tests
    ///</summary>
	[TestClass()]
	public class BotTrustTest
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
		///A test for Solve
		///</summary>
		[TestMethod()]
		public void SolveTest()
		{
			RunSolveTestFor("4 O 2 B 1 B 2 O 4", 6, "ex1");

			RunSolveTestFor("2 B 2 B 1", 4, "ex3");
			
			RunSolveTestFor("1 B 2", 2, "only one button press at 2");
		
			RunSolveTestFor("3 O 5 O 8 B 100", 100, "ex2");
			RunSolveTestFor("1 B 1", 1, "only one button press at 1");
			RunSolveTestFor("4 B 1 O 1 B 2 O 2", 4, "Alternate and close");
			RunSolveTestFor("4 B 1 O 10 B 10 O 1", 20, "X type");
			RunSolveTestFor("4 B 1 B 10 O 1 O 10", 22, "= type 1");
			RunSolveTestFor("4 B 1 B 10 O 10 O 1", 22, "= type 2");
			RunSolveTestFor("6 B 2 O 4 O 9 B 5 O 7 B 10", 17, "my complicated ex");
		}

		private void RunSolveTestFor(string seq, int expected, string msg)
		{
			BotTrust target = new BotTrust();
			int actual;
			actual = target.RunAlgo(seq);
			Assert.AreEqual(expected, actual, msg);
		}
	}
}
