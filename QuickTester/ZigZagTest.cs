using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexProblems.DynamicProgrammingProblems;

namespace QuickTester
{
	[TestClass]
	public class ZigZagTest
	{
		[TestMethod]
		public void KnownTests()
		{
			Assert.AreEqual(6,
				new ZigZag().LongestZigZag(new int[] { 1, 7, 4, 9, 2, 5 }),
				"The entire sequence is a zig-zag sequence.");

			Assert.AreEqual(7,
				new ZigZag().LongestZigZag(new int[] { 1, 17, 5, 10, 13, 15, 10, 5, 16, 8 }),
				"There are several subsequences that achieve this length. One is 1,17,10,13,10,16,8.");

			Assert.AreEqual(1,
				new ZigZag().LongestZigZag(new int[] { 44 }));

			Assert.AreEqual(2,
				new ZigZag().LongestZigZag(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 }));

			Assert.AreEqual(8,
				new ZigZag().LongestZigZag(new int[] { 70, 55, 13, 2, 99, 2, 80, 80, 80, 80, 100, 19, 7, 5, 5, 5, 1000, 32, 32 }));

			Assert.AreEqual(36,
				new ZigZag().LongestZigZag(new int[] { 374, 40, 854, 203, 203, 156, 362, 279, 812, 955, 
600, 947, 978, 46, 100, 953, 670, 862, 568, 188, 
67, 669, 810, 704, 52, 861, 49, 640, 370, 908, 
477, 245, 413, 109, 659, 401, 483, 308, 609, 120, 
249, 22, 176, 279, 23, 22, 617, 462, 459, 244 }));
		}
	}
}

/*
//Constraints
//-	sequence contains between 1 and 50 elements, inclusive.
//-	Each element of sequence is between 1 and 1000, inclusive.
*/