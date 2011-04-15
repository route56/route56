using SRM164Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using UnitTestHelpers;

namespace TestJustifier
{
    
    
    /// <summary>
    ///This is a test class for JustifierTest and is intended
    ///to contain all JustifierTest Unit Tests
    ///</summary>
	[TestClass()]
	public class JustifierTest
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
		///A test for justify
		///</summary>
		[TestMethod()]
		public void justifyTest1()
		{
			Justifier target = new Justifier();
			string[] textIn = { "BOB", "TOMMY", "JIM" };
			string[] expected = { "  BOB", "TOMMY", "  JIM" };
			string[] actual;
			actual = target.justify(textIn);
			CollectionAssert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for justify
		///</summary>
		[TestMethod()]
		public void justifyTest2()
		{
			Justifier target = new Justifier();
			string[] textIn = { "JOHN", "JAKE", "ALAN", "BLUE" };
			string[] expected = { "JOHN", "JAKE", "ALAN", "BLUE" };
			string[] actual;
			actual = target.justify(textIn);
			CollectionAssert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for justify
		///</summary>
		[TestMethod()]
		public void justifyTest3()
		{
			Justifier target = new Justifier();
			string[] textIn = { "LONGEST", "A", "LONGER", "SHORT" };
			string[] expected = { "LONGEST", "      A", " LONGER", "  SHORT" };
			string[] actual;
			actual = target.justify(textIn);
			CollectionAssert.AreEqual(expected, actual);
		}
	}
}
