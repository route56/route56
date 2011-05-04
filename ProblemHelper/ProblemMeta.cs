using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ProblemHelper
{
	public class ProblemMeta
	{
		public string InputFile { get; set; }
		public string ExpectedOutputFile { get; set; }
		public string ActualOutputFile { get; set; }
		public IGCJSolution Solution { get; set; }

		public void RunProblem()
		{
			Solution.Solve(InputFile, ActualOutputFile);
		}
	}
}
