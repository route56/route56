using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	/*
	 * Given a boolean expression with n predicates separated by n-1 operators, operator can be either & or | or ^, based on how you group the expression with paranthesis, the value will be evaluated to True or False. Design an algo to count number of such combinations which will evaluate to true.
	 */

	class SatisfiabilityReloaded
	{
		internal void Parse(string expression)
		{
			throw new NotImplementedException();
		}

		public string[] Predicates { get; private set; }
	}


	class SatisfiabilityOnlyZeroOne
	{
		internal void Parse(string expression)
		{
			throw new NotImplementedException();
		}
	}


	[TestClass]
	public class SatisfiabilityReloadedTest
	{
		[TestMethod]
		public void MyTestMethod()
		{
			string expression = "a+b*!c";

			SatisfiabilityReloaded target = new SatisfiabilityReloaded();

			target.Parse(expression);

			CollectionAssert.AreEqual(new string[] { "a", "b", "c" }, target.Predicates);

			// NP Complete!

			// combination count.
		}

		[TestMethod]
		public void MyTestMethod2()
		{
			string expression = "0+1*!1";

			SatisfiabilityOnlyZeroOne target = new SatisfiabilityOnlyZeroOne();

			target.Parse(expression);

			// how many ways paranthesis can be done.
			// catalan number. Code to do that?
			// (1/(n+1))*(2nCn)
		}
	}
}
