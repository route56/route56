﻿using SRM211Div1;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace TestSRM211Div1
{
    
    
    /// <summary>
    ///This is a test class for grafixMaskTest and is intended
    ///to contain all grafixMaskTest Unit Tests
    ///</summary>
	[TestClass()]
	public class grafixMaskTest
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
		///A test for sortedAreas
		///</summary>
		[TestMethod()]
		public void sortedAreasTest()
		{
			RunSortedAreasTestFor(
				new string[] {"0 292 399 307"},
				new int[] { 116800,  116800 },
				"");
		
			RunSortedAreasTestFor(
				new string[] { "48 192 351 207", "48 392 351 407", "120 52 135 547", "260 52 275 547" },
				new int[] { 22816, 192608 },
				"");

			RunSortedAreasTestFor(
				new string[] {"0 192 399 207", "0 392 399 407", "120 0 135 599", "260 0 275 599"},
new int[] { 22080,  22816,  22816,  23040,  23040,  23808,  23808,  23808,  23808 }, "");

			RunSortedAreasTestFor(
				new string[] {"50 300 199 300", "201 300 350 300", "200 50 200 299", "200 301 200 550"},
new int[] { 1,  239199 }, "");

			RunSortedAreasTestFor(
							new string[] {"0 20 399 20", "0 44 399 44", "0 68 399 68", "0 92 399 92",
 "0 116 399 116", "0 140 399 140", "0 164 399 164", "0 188 399 188",
 "0 212 399 212", "0 236 399 236", "0 260 399 260", "0 284 399 284",
 "0 308 399 308", "0 332 399 332", "0 356 399 356", "0 380 399 380",
 "0 404 399 404", "0 428 399 428", "0 452 399 452", "0 476 399 476",
 "0 500 399 500", "0 524 399 524", "0 548 399 548", "0 572 399 572",
 "0 596 399 596", "5 0 5 599", "21 0 21 599", "37 0 37 599",
 "53 0 53 599", "69 0 69 599", "85 0 85 599", "101 0 101 599",
 "117 0 117 599", "133 0 133 599", "149 0 149 599", "165 0 165 599",
 "181 0 181 599", "197 0 197 599", "213 0 213 599", "229 0 229 599",
 "245 0 245 599", "261 0 261 599", "277 0 277 599", "293 0 293 599",
 "309 0 309 599", "325 0 325 599", "341 0 341 599", "357 0 357 599",
 "373 0 373 599", "389 0 389 599"},
			new int[] 
{ 15,  30,  45,  45,  45,  45,  45,  45,  45,  45,  45,  45,  45,  45,
  45,  45,  45,  45,  45,  45,  45,  45,  45,  45,  45,  45,  100,  115,
  115,  115,  115,  115,  115,  115,  115,  115,  115,  115,  115,  115,
  115,  115,  115,  115,  115,  115,  115,  115,  115,  115,  115,  200,
  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,
  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,  230,
  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,
  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,  300,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,
  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345,  345 }, "");

		}

		private void RunSortedAreasTestFor(string[] rectangles, int[] expected, string assertMsg)
		{
			grafixMask target = new grafixMask();
			int[] actual;
			actual = target.sortedAreas(rectangles);
			CollectionAssert.AreEqual(expected, actual, assertMsg);
		}
	}
}
