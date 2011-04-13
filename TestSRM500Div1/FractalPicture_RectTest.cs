using SRM500Div1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSRM500Div1
{
    
    
    /// <summary>
    ///This is a test class for FractalPicture_RectTest and is intended
    ///to contain all FractalPicture_RectTest Unit Tests
    ///</summary>
	[TestClass()]
	public class FractalPicture_RectTest
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
		///A test for OverlapWRT
		///</summary>
		[TestMethod()]
		public void OverlapWRTTest()
		{
			RunOverlapWRTTestFor(
				0,0,10,10,
				20,20,40,40,
				FractalPicture.RectOverlap.Disjoint);

			RunOverlapWRTTestFor(
				0,0,10,10,
				1,1,5,5,
				FractalPicture.RectOverlap.SuperRect);

			RunOverlapWRTTestFor(
				1,1,5,5,
				0,0,10,10,
				FractalPicture.RectOverlap.SubRect);

			RunOverlapWRTTestFor(
				0,0,10,10,
				5,5,20,20,
				FractalPicture.RectOverlap.Intersecting);
		}

		private void RunOverlapWRTTestFor(int p, int p_2, int p_3, int p_4, int p_5, int p_6, int p_7, int p_8, FractalPicture.RectOverlap expected)
		{
			FractalPicture.Rect target = new FractalPicture.Rect() { X1 = p, Y1 = p_2, X2 = p_3, Y2 = p_4 };
			FractalPicture.Rect other = new FractalPicture.Rect() { X1 = p_5, Y1 = p_6, X2 = p_7, Y2 = p_8 };
			FractalPicture.RectOverlap actual;
			actual = target.OverlapWRT(other);
			Assert.AreEqual(expected, actual, "Expecting "+ expected.ToString());
		}
	}
}
