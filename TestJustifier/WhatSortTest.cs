using SRM164Div2;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace TestJustifier
{
    
    
    /// <summary>
    ///This is a test class for WhatSortTest and is intended
    ///to contain all WhatSortTest Unit Tests
    ///</summary>
	[TestClass()]
	public class WhatSortTest
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
		///A test for sortType
		///</summary>
		[TestMethod()]
		public void sortTypeTest1()
		{
			WhatSort target = new WhatSort(); // TODO: Initialize to an appropriate value
			string[] name = { "BOB", "BOB", "DAVE", "JOE" };
			int[] age = { 22, 35, 35, 30 };
			int[] wt = { 122, 122, 195, 200 };
			string expected = "IND";
			string actual;
			actual = target.sortType(name, age, wt);
			Assert.IsTrue(expected.CompareTo(actual) == 0);
		}

		/// <summary>
		///A test for sortType
		///</summary>
		[TestMethod()]
		public void sortTypeTest2()
		{
			WhatSort target = new WhatSort(); // TODO: Initialize to an appropriate value
			string[] name = { "BOB", "BOB", "DAVE", "DAVE" };
			int[] age = { 22, 35, 35, 30 };
			int[] wt = { 122, 122, 195, 200 };
			string expected = "NOT";
			string actual;
			actual = target.sortType(name, age, wt);
			Assert.IsTrue(expected.CompareTo(actual) == 0);
		}

		/// <summary>
		///A test for sortType
		///</summary>
		[TestMethod()]
		public void sortTypeTest3()
		{
			WhatSort target = new WhatSort(); // TODO: Initialize to an appropriate value
			string[] name = { "BOB", "BOB", "DAVE", "DAVE" };
			int[] age = { 22, 35, 35, 30 };
			int[] wt = { 122, 122, 195, 190 };
			string expected = "NWA";
			string actual;
			actual = target.sortType(name, age, wt);
			Assert.IsTrue(expected.CompareTo(actual) == 0);
		}

		[TestMethod()]
		public void sortTypeTest4()
		{
			WhatSort target = new WhatSort(); // TODO: Initialize to an appropriate value
			string[] name = { "A", "B", "C", "D" };
			int[] age = { 1, 2, 3, 4 };
			int[] wt = { 6, 5, 4, 3 };
			string expected = "IND"; // NAW, NWA, and more
			string actual;
			actual = target.sortType(name, age, wt);
			Assert.IsTrue(expected.CompareTo(actual) == 0);
		}

		[TestMethod()]
		public void sortTypeTest5()
		{
			WhatSort target = new WhatSort(); // TODO: Initialize to an appropriate value
			string[] name = { "B", "B", "C", "C" };
			int[] age = { 1, 2, 3, 4 };
			int[] wt = { 6, 5, 4, 3 };
			string expected = "IND"; // NAW, NWA, and more
			string actual;
			actual = target.sortType(name, age, wt);
			Assert.IsTrue(expected.CompareTo(actual) == 0);
		}

		[TestMethod()]
		public void sortTypeTest6()
		{
			WhatSort target = new WhatSort(); // TODO: Initialize to an appropriate value
			string[] name = { "A", "A", "A", "A" };
			int[] age = { 1, 2, 3, 4 };
			int[] wt = { 6, 5, 4, 3 };
			string expected = "IND"; // NAW, NWA, and more
			string actual;
			actual = target.sortType(name, age, wt);
			Assert.IsTrue(expected.CompareTo(actual) == 0);
		}

		/// <summary>
		///A test for FindRangesWithSameValues
		///</summary>
		[TestMethod()]
		public void FindRangesWithSameValuesTest()
		{
			WhatSort target = new WhatSort(); // TODO: Initialize to an appropriate value
			string[] name = { "one", "two", "two", "three", "four", "four", "four" };
			List<KeyValuePair<int, int>> expected = new List<KeyValuePair<int, int>>()
					{
						new KeyValuePair<int, int>(1,2),
						new KeyValuePair<int,int>(4,6)
					};

			string[] name2 = { "one", "two", "three", "four" };
			List<KeyValuePair<int, int>> expected2 = new List<KeyValuePair<int, int>>();

			string[] name3 = { "one", "one", "one", "one", "two", "two", "three", "four", "four", "four", "four"};
			List<KeyValuePair<int, int>> expected3 = new List<KeyValuePair<int, int>>()
					{
						new KeyValuePair<int, int>(0,3),
						new KeyValuePair<int,int>(4,5),
						new KeyValuePair<int,int>(7,10)
					};

			string[] name4 = { "one", "one", "two", "two"};
			List<KeyValuePair<int, int>> expected4 = new List<KeyValuePair<int, int>>()
					{
						new KeyValuePair<int, int>(0,1),
						new KeyValuePair<int,int>(2,3)
					};

			List<SRM164Div2.WhatSort.IColumnElement> nameCE = new List<SRM164Div2.WhatSort.IColumnElement>();
			List<SRM164Div2.WhatSort.IColumnElement> nameCE2 = new List<SRM164Div2.WhatSort.IColumnElement>();
			List<SRM164Div2.WhatSort.IColumnElement> nameCE3 = new List<SRM164Div2.WhatSort.IColumnElement>();
			List<SRM164Div2.WhatSort.IColumnElement> nameCE4 = new List<SRM164Div2.WhatSort.IColumnElement>();

			target.TransformInput(name, null, null, nameCE, null, null);
			target.TransformInput(name2, null, null, nameCE2, null, null);
			target.TransformInput(name3, null, null, nameCE3, null, null);
			target.TransformInput(name4, null, null, nameCE4, null, null);

			List<KeyValuePair<int, int>> actual;
			actual = target.FindRangesWithSameValues(nameCE);
			Assert.IsTrue(AreSameRanges(expected, actual));

			actual = target.FindRangesWithSameValues(nameCE2);
			Assert.IsTrue(AreSameRanges(expected2, actual));

			actual = target.FindRangesWithSameValues(nameCE3);
			Assert.IsTrue(AreSameRanges(expected3, actual));

			actual = target.FindRangesWithSameValues(nameCE4);
			Assert.IsTrue(AreSameRanges(expected4, actual));
		}

		private bool AreSameRanges(List<KeyValuePair<int, int>> expected, List<KeyValuePair<int, int>> actual)
		{
			if (expected.Count == actual.Count)
			{
				for (int i = 0; i < actual.Count; i++)
				{
					if (expected[i].Key != actual[i].Key
					|| expected[i].Value != actual[i].Value)
					{
						return false;
					}
				}
			}
			else
			{
				return false;
			}

			return true;
		}
	}
}
