using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GCJSolver;

namespace GCJUnitTester
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void TestMethod1()
		{
			Assert.AreEqual(Program.ProblemSpecificUnit("Hi"), "Hello world", "case 1");
			Assert.AreEqual(Program.ProblemSpecificUnit("Bye"), "Bye world", "case 2");
		}
	}
}
