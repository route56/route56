using SRM500Div1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSRM500Div1
{
    
    
    /// <summary>
    ///This is a test class for FractalPictureTest and is intended
    ///to contain all FractalPictureTest Unit Tests
    ///</summary>
	[TestClass()]
	public class FractalPictureTest
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
		///A test for getLength
		///</summary>
		[TestMethod()]
		public void getLengthTest()
		{
			RunGetLengthTestFor(
			-1,
0,
1,
53,
53.0,
"Only one part of fractal segments belongs to this rectangle: (0, 0) - (0, 53).");

//            RunGetLengthTestFor(
//            1,
//            1,
//            10,
//            10,
//            0.0,
//            "No parts of fractal segments belong to this rectangle.");

//            RunGetLengthTestFor(
//-10,
//54,
//10,
//55,
//21.0,
//"Two parts of fractal segments belong to this rectangle: (-10, 54) - (10, 54) and (0, 54) - (0, 55). Note that parts that lie on the rectangle's border also belong to the rectangle.");
//            RunGetLengthTestFor(
//            14,
//            45,
//            22,
//            54,
//            2999.0,
//            "");
		}

		private void RunGetLengthTestFor(int x1, int y1, int x2, int y2, double expected, string assertmsg)
		{
			FractalPicture target = new FractalPicture();
			double actual;
			actual = target.getLength(x1, y1, x2, y2);
			Assert.AreEqual(expected, actual, assertmsg);
		}
	}
}
