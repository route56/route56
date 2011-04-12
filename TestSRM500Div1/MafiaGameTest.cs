using SRM500Div1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSRM500Div1
{
    
    
    /// <summary>
    ///This is a test class for MafiaGameTest and is intended
    ///to contain all MafiaGameTest Unit Tests
    ///</summary>
	[TestClass()]
	public class MafiaGameTest
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
		///A test for probabilityToLose
		///</summary>
		[TestMethod()]
		public void probabilityToLoseTest()
		{
			RunProbabilityToLoseTest(3, new int[] { 1, 1, 1 }, 1.0, "After the first voting round, there will be 3 votes against player 1 and no votes against other players. Therefore player 1 will lose with probability 1.0.");
			RunProbabilityToLoseTest(5, new int[] { 1, 2, 3 }, 0.0, "The first round will proceed as follows. The first three votes will be against players 1, 2 and 3. The next vote will be against player 0 with probability 50% (in this case the last vote will be against player 4) or against player 4 with probability 50% (in this case the last vote will be against player 0). In both cases, after the end of the round there will be 1 vote against each player, so the set of \"vulnerable\" players will remain unchanged.  Each next round will proceed according to the same scenario, so there will be infinitely many rounds.");
			RunProbabilityToLoseTest(20, new int[] {1, 2, 3, 4, 5, 6, 7, 1, 2, 3, 4, 5, 6, 7, 18, 19, 0}, 0.0, "There can be different voting scenarios, but in each of them the number of \"vulnerable\" players after the first three rounds is 7, 6 and 2, respectively. Then, starting from the fourth round, there will be exactly 10 votes against each \"vulnerable\" player, so the set of \"vulnerable\" players will remain unchanged and there will be infinitely many rounds.");
			RunProbabilityToLoseTest(23, new int[] { 17, 10, 3, 14, 22, 5, 11, 10, 22, 3, 14, 5, 11, 17 }, 0.14285714285714285, "There can also be many different voting scenarios, but each of them consists of exactly 3 rounds. Each of players 3, 5, 10, 11, 14, 17, 22 can lose with the same probability of 1/7.");

		}

		private void RunProbabilityToLoseTest(int N, int[] decisions, double expected, string assertMsg)
		{
			MafiaGame target = new MafiaGame();
			double actual;
			actual = target.probabilityToLose(N, decisions);
			Assert.AreEqual(expected, actual, assertMsg);
		}
	}
}
