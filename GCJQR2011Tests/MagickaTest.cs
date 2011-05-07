using QR2011;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace GCJQR2011Tests
{
    
    
    /// <summary>
    ///This is a test class for MagickaTest and is intended
    ///to contain all MagickaTest Unit Tests
    ///</summary>
	[TestClass()]
	public class MagickaTest
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
		///A test for RunAlgo
		///</summary>
		[TestMethod()]
		public void RunAlgoTestBasics()
		{
			MagickaRunAlgoTestFor(
				new string[] { },
				new string[] { },
				"EA",
				"EA",
				"Ex 1");

			MagickaRunAlgoTestFor(
				new string[] { "QRI" },
				new string[] { },
				"RRQR",
				"RIR",
				"ex 2");

			MagickaRunAlgoTestFor(
				new string[] { "QFT" },
				new string[] { "QF" },
				"FAQFDFQ",
				"FDT",
				"ex 3");

			MagickaRunAlgoTestFor(
				new string[] { "EEZ" },
				new string[] { "QE" },
				"QEEEERA",
				"ZERA",
				"ex 4");

			MagickaRunAlgoTestFor(
				new string[] { },
				new string[] { "QW" },
				"QW",
				"",
				"ex 5");
		}

		[TestMethod()]
		public void RunAlgoTestMyFalsePositives()
		{
			MagickaRunAlgoTestFor(
				new string[] { "QWT", "ASG" },
				new string[] { "ER", "DF" },
				"QSEFAWDR",
				"R", // ""
				"");//"ER/DF will dest");

			MagickaRunAlgoTestFor(
				new string[] { "QWT", "ASG" },
				new string[] { "ER", "DF" },
				"QQQWWW",
				"QQTWW", // "TTT"
				"");//"basic case");
		}

		[TestMethod()]
		public void RunAlgoTestMyTests()
		{
			MagickaRunAlgoTestFor(
				new string[] { "QWT", "ASG" },
				new string[] { "ER", "DF" },
				"QWDFASER",
				"",
				"ER in end destroys stuff");

			MagickaRunAlgoTestFor(
				new string[] { "QWT", "ASG" },
				new string[] { "ER", "DF" },
				"QAQWASSW",
				"QATGSW",
				"simple case");

			MagickaRunAlgoTestFor(
				new string[] { },
				new string[] { "QW", "ER", "AS", "DF"},
				"QEADQEAD",
				"QEADQEAD",
				"no dest");

			MagickaRunAlgoTestFor(
				new string[] { },
				new string[] { "QW", "ER", "AS", "DF" },
				"WRSFWRSF",
				"WRSFWRSF",
				"no dest");

			MagickaRunAlgoTestFor(
				new string[] { },
				new string[] { "QW", "ER", "AS", "DF"},
				"EDAFEQ",
				"EQ",
				"EQ in end");
		}

		private void MagickaRunAlgoTestFor(string[] combineRules, string[] opposedRules, string invokeSeq, string expected, string msg)
		{
			Magicka target = new Magicka();
			string actual;
			actual = target.RunAlgo(combineRules, opposedRules, invokeSeq);
			Assert.AreEqual(expected, actual, msg);
		}
	}
}
