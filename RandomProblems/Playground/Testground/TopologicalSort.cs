using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	class TopologicalSort
	{
		internal const string Seprator = ",";

		internal string Solve(string[] dependency)
		{
			Dictionary<string, List<string>> adjGraph  = null;
			try
			{
				adjGraph = ComputeAdjGraph(dependency);
			}
			catch (Exception ex)
			{
				throw new ArgumentException("dependency", ex);
			}

			var result = GraphSearch.DFSTraversal<string>(adjGraph);

			return string.Join(Seprator,
				result.OrderByDescending(s => s.Value.EndTime).Select(s => s.Key));
		}

		private Dictionary<string, List<string>> ComputeAdjGraph(string[] dependency)
		{
			var result = new Dictionary<string, List<string>>();
			var splitArr = new string[] { Seprator };

			foreach (var item in dependency)
			{
				string[] pair = item.Split(splitArr, StringSplitOptions.None);

				if (result.ContainsKey(pair[0]))
				{
					result[pair[0]].Add(pair[1]); // TODO use sets instead of list. possibility to add duplicate here. //HashSet<string> f = new HashSet<string>();
				}
				else
				{
					result.Add(pair[0], new List<string>() { pair[1] });
				}

				if (result.ContainsKey(pair[1]) == false)
				{
					result.Add(pair[1], new List<string>());
				}
			}

			return result;
		}
	}

	[TestClass]
	public class TopoSortTest
	{
		[TestMethod]
		public void BasicDependencyCase()
		{
			var dependency = new string[]
				{
					"a,b",
					"b,c",
					"c,d"
				};

			string expected = "a,b,c,d";

			var target = new TopologicalSort();

			string actual = target.Solve(dependency);

			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void MultiplePossibleDependencyCase()
		{
			var dependency = new string[]
				{
					"a,b",
					"c,b",
				};

			string[] expected = 
				{
					"a,c,b",
					"c,a,b",
				};

			var target = new TopologicalSort();

			string actual = target.Solve(dependency);

			Assert.IsTrue(expected.Contains(actual));
		}

		[TestMethod]
		public void ComplexDependencyCase()
		{
			var dependency = new string[]
				{
					"a,b",
					"c,b",
					"a,d",
					"d,c",
					"d,e",
					"b,f",
					"e,f",
					"g,h",
					"h,i"
				};

			_ComplexCases(dependency);
		}

		[TestMethod]
		public void RandomDependencyCase()
		{
			Assert.Inconclusive("todo random DAG generation");

			//Random rand = new Random();

			//int nodeCount = rand.Next(1, 1000);

			//var dependency = List<string>();

			//for (int i = 0; i < nodeCount; i++)
			//{
			//    var edgeCount = rand.Next(1, nodeCount);

			//    for (int j = 0; j < nodeCount; j++)
			//    {

			//    }
			//}

			//_ComplexCases(dependency);
		}

		private static void _ComplexCases(string[] dependency)
		{
			var target = new TopologicalSort();

			string actual = target.Solve(dependency);

			foreach (var item in dependency)
			{
				string[] split = item.Split(',');

				Assert.IsTrue(actual.IndexOf(split[0]) < actual.IndexOf(split[1]));
			}
		}
	}

}
