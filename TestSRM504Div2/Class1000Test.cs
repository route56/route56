using SRM504Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSRM504Div2
{
    
    
    /// <summary>
    ///This is a test class for Class1000Test and is intended
    ///to contain all Class1000Test Unit Tests
    ///</summary>
	[TestClass()]
	public class Class1000Test
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
		///A test for Class1000 Constructor
		///</summary>
		[TestMethod()]
		public void Class1000ConstructorTest()
		{
			Class1000 target = new Class1000();
			Assert.Inconclusive("TODO: Implement code to verify target");
		}
	}
}
