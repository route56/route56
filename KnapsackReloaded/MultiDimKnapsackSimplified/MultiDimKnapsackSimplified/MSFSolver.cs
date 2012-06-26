using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.SolverFoundation.Common;
using Microsoft.SolverFoundation.Services;
using System.Diagnostics;


namespace MultiDimKnapsackSimplified
{
	public class MSFSolver : ISolver
	{
		public int[] SolveNonFancy(int[,] map, int[] weight)
		{
			SolverContext context = SolverContext.GetContext();
			Model model = context.CreateModel();

			Decision[] xVar = new Decision[weight.Length];
			//List<Term> termVar = new List<Term>();

			for (int i = 0; i < xVar.Length; i++)
			{
				xVar[i] = new Decision(Domain.IntegerNonnegative, "xVar_" + i); // Domain.IntegerRange(0,1) // Domain.Boolean

				model.AddDecision(xVar[i]);

				//termVar.Add(0 <= xVar[i] <= 1);
			}

			//model.AddConstraints("limits", termVar.ToArray());

			//List<Term> termList = new List<Term>();

			for (int i = 0; i < weight.Length; i++)
			{
				Term cummulative = 0;

				for (int j = 0; j < map.GetLength(1); j++)
				{
					Term temp = map[i, j] * xVar[i] <= weight[i];

					cummulative += temp;
				}

				//termList.Add(cummulative);

				model.AddConstraint("constraint_" + i, cummulative);
			}

			// model.AddConstraints("constraint", termList.ToArray());

			Term goal = 0;

			for (int i = 0; i < xVar.Length; i++)
			{
				goal += xVar[i];
			}

			model.AddGoal("count", GoalKind.Maximize, goal);

			Solution solution = context.Solve();//new ConstraintProgrammingDirective()); //InteriorPointMethodDirective()); //SimplexDirective());  // Microsoft.SolverFoundation.Common.UnsolvableModelException was unhandled by user code ??
			
			Report report = solution.GetReport();

			List<int> answer = new List<int>();

			for (int i = 0; i < xVar.Length; i++)
			{
				Debug.WriteLine("xVar[{0}] = {1}", i, xVar[i]);

				if (int.Parse(xVar[i].ToString()) == 1)
				{
					answer.Add(i);
				}
			}

			Debug.Write(report.ToString());

			return answer.ToArray();
		}

		/// <summary>
		/// http://msdn.microsoft.com/en-us/library/ff628587(v=vs.93).aspx
		/// </summary>
		public void CallMe()
		{
			SolverContext context = SolverContext.GetContext();
			Model model = context.CreateModel();

			Decision vz = new Decision(Domain.RealNonnegative, "barrels_venezuela");
			Decision sa = new Decision(Domain.RealNonnegative, "barrels_saudiarabia");
			model.AddDecisions(vz, sa);

			model.AddConstraints("limits",
				  0 <= vz <= 9000,
				  0 <= sa <= 6000);

			model.AddConstraints("production",
				  0.3 * sa + 0.4 * vz >= 2000,
				  0.4 * sa + 0.2 * vz >= 1500,
				  0.2 * sa + 0.3 * vz >= 500);

			model.AddGoal("cost", GoalKind.Minimize,
				20 * sa + 15 * vz);

			Solution solution = context.Solve(new SimplexDirective());

			Report report = solution.GetReport();
			Console.WriteLine("vz: {0}, sa: {1}", vz, sa);
			Console.Write("{0}", report);
			Console.ReadLine();
		}
	}
}
