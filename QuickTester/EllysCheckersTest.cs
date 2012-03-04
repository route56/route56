using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RegexProblems.DynamicProgrammingProblems;
using RegexProblems.SRMPractice;

namespace QuickTester
{
	[TestClass]
	public class EllysCheckersTest
	{
		[TestMethod]
		public void KnownTests()
		{
			Assert.AreEqual(
				new EllysCheckers().GetWinnerMain(".o..."),
				true,
				"With only one checker it is pretty obvious who will win.");

			Assert.AreEqual(
				new EllysCheckers().GetWinnerMain("..o..o"),
				true,
				"Don't forget to ignore checkers on the rightmost cell.");

			Assert.AreEqual(
				new EllysCheckers().GetWinnerMain(".o...ooo..oo.."),
				false,
				"Here one can jump the checker from cell 5 to cell 8.");

			Assert.AreEqual(
				new EllysCheckers().GetWinnerMain("......o.ooo.o......"),
				true);

			Assert.AreEqual(
				new EllysCheckers().GetWinnerMain(".o..o...o....o.....o"),
				false);
		}
	}
}
