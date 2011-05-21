using TCO11QR2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM144Div1Test
{
    
    
    /// <summary>
    ///This is a test class for CB1000Test and is intended
    ///to contain all CB1000Test Unit Tests
    ///</summary>
	[TestClass()]
	public class CB1000Test
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
		///A test for count
		///</summary>
		[TestMethod()]
		public void countTest()
		{
			CB1000 target = new CB1000(); // TODO: Initialize to an appropriate value
			long min = 123456789;
			long max = 9876543210;
			long expected = 2618024258;
			long actual;
			actual = target.count(min, max);
			Assert.AreEqual(expected, actual);
		}
	}
}
