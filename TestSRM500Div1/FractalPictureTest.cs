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
		}
		[TestMethod()]
		public void getLengthTest2()
		{
			RunGetLengthTestFor(
			1,
			1,
			10,
			10,
			0.0,
			"No parts of fractal segments belong to this rectangle.");

		}
		[TestMethod()]
		public void getLengthTest3()
		{
			RunGetLengthTestFor(
-10,
54,
10,
55,
21.0,
"Two parts of fractal segments belong to this rectangle: (-10, 54) - (10, 54) and (0, 54) - (0, 55). Note that parts that lie on the rectangle's border also belong to the rectangle.");
		}
		[TestMethod()]
		public void getLengthTest4()
		{
			RunGetLengthTestFor(
			14,
			45,
			22,
			54,
			2999.0,
			"");
		}

		private void RunGetLengthTestFor(int x1, int y1, int x2, int y2, double expected, string assertmsg)
		{
			FractalPicture target = new FractalPicture();
			double actual;
			actual = target.getLength(x1, y1, x2, y2);
			Assert.AreEqual(expected, actual, assertmsg);
		}

		/// <summary>
		///A test for GetNextGenLines
		///</summary>
		[TestMethod()]
		public void GetNextGenLinesTest()
		{
			FractalPicture target = new FractalPicture();
			FractalPicture.Line line = new FractalPicture.Line() { RootX = 0, RootY = 0, EndX = 0, EndY = 81 };
			FractalPicture.Line[] expected = new FractalPicture.Line[]
			{
				new FractalPicture.Line()
				{
					RootX = 0,
					RootY = 81 * 2 / 3,
					EndX = 0,
					EndY = 81
				},

				new FractalPicture.Line()
				{
					RootX = 0,
					RootY = 81 * 2 / 3,
					EndX = 81/3,
					EndY = 81 * 2 / 3
				},

				new FractalPicture.Line()
				{
					RootX = 0,
					RootY = 81 * 2 / 3,
					EndX = -81/3,
					EndY = 81 * 2 / 3
				}
			};
			
			FractalPicture.Line[] actual;
			actual = target.GetNextGenLines(line);

			Assert.IsTrue(actual.Length == 3);
			for (int i = 0; i < actual.Length; i++)
			{
				Assert.AreEqual(actual[i], expected[i]);
			}
		}

		/// <summary>
		///A test for GetNextGenRect
		///</summary>
		[TestMethod()]
		public void GetNextGenRectTest()
		{
			FractalPicture target = new FractalPicture();
			FractalPicture.Line line = new FractalPicture.Line() { RootX = 0, RootY = 0, EndX = 0, EndY = 81 };
			FractalPicture.Rect expected = new FractalPicture.Rect() { X1 = -81 / 3, Y1 = 0, X2 = 81 / 3, Y2 = 81 };
			FractalPicture.Rect actual;
			actual = target.GetNextGenRect(line);
			Assert.AreEqual(expected, actual);
		}

		/// <summary>
		///A test for GetLineIntersectionWithRect
		///</summary>
		[TestMethod()]
		public void GetLineIntersectionWithRectTest()
		{
			RunGetLineIntersectionWithRectTestFor(
				5, 5, 10, 5,
				0, 0, 20, 20,
				5, "whole line is inside");

			RunGetLineIntersectionWithRectTestFor(
				5, 5, 10, 5,
				0, 0, 8, 8,
				3, "Partial line");

			RunGetLineIntersectionWithRectTestFor(
				5, 5, 10, 5,
				5, 5, 8, 8,
				3, "partial line with touching border");

			RunGetLineIntersectionWithRectTestFor(
				5, 5, 10, 5,
				0, 0, 4, 4,
				0, "Line is outside the box");

			RunGetLineIntersectionWithRectTestFor(
				-5, 5, 10, 5,
				0, 0, 8, 8,
				8, "End Points outside but intercept the rectangle");
		}

		private void RunGetLineIntersectionWithRectTestFor(int p, int p_2, int p_3, int p_4, int p_5, int p_6, int p_7, int p_8, int expected, string assertMsg)
		{
			FractalPicture target = new FractalPicture();
			FractalPicture.Line line = new FractalPicture.Line() { RootX = p, RootY = p_2, EndX = p_3, EndY = p_4 };
			FractalPicture.Rect frame = new FractalPicture.Rect() { X1 = p_5, Y1 = p_6, X2 = p_7, Y2 = p_8 };
			double actual;
			actual = target.GetLineIntersectionWithRect(line, frame);
			Assert.AreEqual(expected, actual, assertMsg);
		}
	}
}
