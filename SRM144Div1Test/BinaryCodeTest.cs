using SRM144Div1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SRM144Div1Test
{
    
    
    /// <summary>
    ///This is a test class for BinaryCodeTest and is intended
    ///to contain all BinaryCodeTest Unit Tests
    ///</summary>
	[TestClass()]
	public class BinaryCodeTest
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


		void RunTestWithInputs(string message, string[] expected)
		{
			BinaryCode target = new BinaryCode();
			string[] actual;
			actual = target.decode(message);
			CollectionAssert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for decode
		///</summary>
		[TestMethod()]
		public void decodeTest()
		{
			RunTestWithInputs("123210122", new string[] {"011100011", "NONE"});
			RunTestWithInputs("11", new string[] { "01", "10" });
			RunTestWithInputs("22111", new string[] { "NONE", "11001" });
			RunTestWithInputs("123210120", new string[] { "NONE", "NONE" });
			RunTestWithInputs("3", new string[] { "NONE", "NONE" });
			RunTestWithInputs("12221112222221112221111111112221111", new string[] { "01101001101101001101001001001101001", "10110010110110010110010010010110010" });
			RunTestWithInputs("123210122", new string[] { "011100011", "NONE" });
		}
	}
}
