using SRM505Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM144Div1Test
{
    
    
    /// <summary>
    ///This is a test class for SentenceCapitalizerInatorTest and is intended
    ///to contain all SentenceCapitalizerInatorTest Unit Tests
    ///</summary>
	[TestClass()]
	public class SentenceCapitalizerInatorTest
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
		///A test for fixCaps
		///</summary>
		[TestMethod()]
		public void fixCapsTest()
		{
			RunFixCapsTestFor("hello programmer. welcome to topcoder.", "Hello programmer. Welcome to topcoder.");

			RunFixCapsTestFor(
			"one.", "One.");

			RunFixCapsTestFor("not really. english. qwertyuio. a. xyz.", "Not really. English. Qwertyuio. A. Xyz.");

			RunFixCapsTestFor("example four. the long fourth example. a. b. c. d.", "Example four. The long fourth example. A. B. C. D.");
		}

		private void RunFixCapsTestFor(string paragraph, string expected)
		{
			SentenceCapitalizerInator target = new SentenceCapitalizerInator(); // TODO: Initialize to an appropriate value
			string actual;
			actual = target.fixCaps(paragraph);
			Assert.AreEqual(expected, actual);
		}
	}
}
