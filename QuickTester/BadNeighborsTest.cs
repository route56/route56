using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexProblems.DynamicProgrammingProblems;

namespace QuickTester
{
	[TestClass]
	public class BadNeighborsTest
	{
		[TestMethod]
		public void KnownTests()
		{
			Assert.AreEqual(19,
				new BadNeighbors().MaxDonations(new int[] { 10, 3, 2, 5, 7, 8 }),
				"The maximum donation is 19, achieved by 10+2+7. It would be better to take 10+5+8 except that the 10 and 8 donations are from neighbors.");

			Assert.AreEqual(15,
				new BadNeighbors().MaxDonations(new int[] { 11, 15 }));

			Assert.AreEqual(11,
				new BadNeighbors().MaxDonations(new int[] { 1, 11, 2 }));

			Assert.AreEqual(11,
				new BadNeighbors().MaxDonations(new int[] { 10, 2, 1, 4 }));

			Assert.AreEqual(11,
				new BadNeighbors().MaxDonations(new int[] { 2, 10, 4, 1 }));

			Assert.AreEqual(11,
				new BadNeighbors().MaxDonations(new int[] { 2, 5, 4, 1, 6 }));

			Assert.AreEqual(11,
				new BadNeighbors().MaxDonations(new int[] { 5, 2, 1, 6, 4 }));

			Assert.AreEqual(21,
				new BadNeighbors().MaxDonations(new int[] { 7, 7, 7, 7, 7, 7, 7 }));

			Assert.AreEqual(16,
				new BadNeighbors().MaxDonations(new int[] { 1, 2, 3, 4, 5, 1, 2, 3, 4, 5 }));

			Assert.AreEqual(2926,
				new BadNeighbors().MaxDonations(new int[] { 94, 40, 49, 65, 21, 21, 106, 80, 92, 81, 679, 4, 61,  
  6, 237, 12, 72, 74, 29, 95, 265, 35, 47, 1, 61, 397,
  52, 72, 37, 51, 1, 81, 45, 435, 7, 36, 57, 86, 81, 72 }));
		}
	}
}
