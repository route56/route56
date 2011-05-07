using QR2011;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GCJQR2011Tests
{
    
    
    /// <summary>
    ///This is a test class for CandySplittingTest and is intended
    ///to contain all CandySplittingTest Unit Tests
    ///</summary>
	[TestClass()]
	public class CandySplittingTest
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
		///A test for RunIneffificentAlgo
		///</summary>
		[TestMethod()]
		public void RunIneffificentAlgoTestForExamples()
		{
			RunAlgoForExamples(false);
		}

		[TestMethod()]
		public void RunEffificentAlgoTestForExamples()
		{
			RunAlgoForExamples(true);
		}

		[TestMethod()]
		public void RunIneffificentAlgoTestForMyExamples()
		{
			RunAlgoForMyExamples(false);
		}

		[TestMethod()]
		public void RunEffificentAlgoTestForMyExamples()
		{
			RunAlgoForMyExamples(true);
		}

		[TestMethod()]
		public void RunEffificentAlgoTestAgainstResultByInefficient()
		{
			RunEvsIEFor(new int[] { 3, 10, 100, 75, 2 });
			RunEvsIEFor(new int[] { 1, 2, 4 });
			RunEvsIEFor(new int[] { 6, 2, 9, 10, 30, 30, 20});
			RunEvsIEFor(new int[] { 10, 7, 9 });
		}


		[TestMethod()]
		public void RunEffificentAlgoTestAgainstResultByInefficientRandom()
		{
			Random rand = new Random();

			// Configure
			int loopCount = 10;
			int arraySize = 10;
			int valueMax = 100;

			for (int i = 0; i < loopCount; i++)
			{
				int size = rand.Next() % arraySize;
				size += 2; // minimum size

				int[] input = new int[size];

				for (int j = 0; j < input.Length; j++)
				{
					input[j] = rand.Next() % valueMax;
				}

				RunEvsIEFor(input, "Random test");
			}
		}

		/// <summary>
		/// Assumes IE is correct and tests E against it's output
		/// </summary>
		/// <param name="input"></param>
		private void RunEvsIEFor(int[] input, string msg)
		{
			CandySplitting ieff = new CandySplitting();
			CandySplitting target = new CandySplitting();
			int[] inputCopy = new int[input.Length];

			Array.Copy(input, inputCopy, input.Length);

			int expected = ieff.RunIneffificentAlgo(input);
			int actual = target.RunEffificentAlgo(inputCopy);

			Assert.AreEqual(expected, actual, msg + ":Efficient doesn't match InEfficient");
		}

		private void RunEvsIEFor(int[] input)
		{
			RunEvsIEFor(input, "");
		}

		private void RunAlgoForMyExamples(bool runEfficientAlgo)
		{
			//RunAlgoTestFor(runEfficientAlgo, new int[] { 5 }, 0, "Not allowed. min 2 elements");
			RunAlgoTestFor(runEfficientAlgo, new int[] { 5, 5 }, 5, "And not 10. Asked this question @ GCJ");
			RunAlgoTestFor(runEfficientAlgo, new int[] { 5, 5, 5}, 0, "no way to do this");
			RunAlgoTestFor(runEfficientAlgo, new int[] { 5, 5, 5, 5 }, 15, "");//10, "10 was false +ve");

			RunAlgoTestFor(runEfficientAlgo, new int[] { 1, 5, 1, 5 }, 11, "S = 5,5, 1 and P has 1");
			// NEED FOR MORE TEST CASES???
			//RunAlgoTestFor(runEfficientAlgo, new int[] { 1, 3, 5, 7, 9, 11, 13}, , "");
			//RunAlgoTestFor(runEfficientAlgo, new int[] { }, , "");
			//RunAlgoTestFor(runEfficientAlgo, new int[] { }, , "");
			//RunAlgoTestFor(runEfficientAlgo, new int[] { }, , "");
			//RunAlgoTestFor(runEfficientAlgo, new int[] { }, , "");
		}

		private void RunAlgoForExamples(bool runEfficientAlgo)
		{
			RunAlgoTestFor(runEfficientAlgo, new int[] { 3, 5, 6 }, 11, "ex 2");
			RunAlgoTestFor(runEfficientAlgo, new int[] { 1, 2, 3, 4, 5 }, 0, "ex 1");
		}

		private void RunAlgoTestFor(bool runEfficientAlgo, int[] candyArr, int expected, string msg)
		{
			CandySplitting target = new CandySplitting();
			int actual;
			actual = runEfficientAlgo ? target.RunEffificentAlgo(candyArr) : target.RunIneffificentAlgo(candyArr);
			Assert.AreEqual(expected, actual, msg);
		}
	}
}
