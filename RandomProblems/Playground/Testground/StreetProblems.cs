using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	class StreetProblems
	{
		static void Main()
		{
			string[] dictionary;
			SourceDest[] sourceDestPair;

			try
			{
				ParseInput(out dictionary, out sourceDestPair);

				foreach (var item in sourceDestPair)
				{
					if (dictionary.Where(s => s == item.Source).Count() != 1)
					{
						throw new ArgumentException("Missing " + item.Source);
					}

					if (dictionary.Where(s => s == item.Destination).Count() != 1)
					{
						throw new ArgumentException("Missing " + item.Destination);
					}
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine("Invalid input");
				Console.WriteLine(ex.ToString());
				return;
			}

			StreetProblems target = new StreetProblems();

			target.Dictionary = dictionary;

			foreach (var pairs in sourceDestPair)
			{
				Console.WriteLine(target.Solve(pairs.Source, pairs.Destination));
			}
		}

		private static void ParseInput(out string[] dictionary, out SourceDest[] sourceDestPair)
		{
			int nodes = int.Parse(Console.ReadLine());

			dictionary = new string[nodes];

			for (int i = 0; i < nodes; i++)
			{
				dictionary[i] = Console.ReadLine();
			}

			int pair = int.Parse(Console.ReadLine());

			sourceDestPair = new SourceDest[pair];

			for (int i = 0; i < pair; i++)
			{
				string[] pairNodes = Console.ReadLine().Split();
				sourceDestPair[i] = new SourceDest();
				sourceDestPair[i].Source = pairNodes[0];
				sourceDestPair[i].Destination = pairNodes[1];
			}
		}

		public int Solve(string source, string dest)
		{
			if (Dictionary.Where(s => s == source).Count() != 1)
			{
				throw new ArgumentException("source missing in dictionary");
			}

			if (Dictionary.Where(s => s == dest).Count() != 1)
			{
				throw new ArgumentException("dest missing in dictionary");
			}

			if (AdjGraph == null) // lazy init
			{
				AdjGraph = ConstructAdjancyGraph(Dictionary);
			}

			Dictionary<string, NodeBFSData> data = BFSTraversal(AdjGraph, source);

			return data[dest].Distance + 1; // including source node
		}

		private static Dictionary<string, NodeBFSData> BFSTraversal(Dictionary<string, List<string>> adjGraph, string source)
		{
			Dictionary<string, NodeBFSData> nodeData = new Dictionary<string, NodeBFSData>();

			foreach (var item in adjGraph.Keys)
			{
				nodeData.Add(item, new NodeBFSData() 
					{ 
						Color = NodeColor.White, 
						Distance = int.MaxValue 
					});
			}

			nodeData[source].Color = NodeColor.Grey;
			nodeData[source].Distance = 0;

			Queue<string> queue = new Queue<string>();

			queue.Enqueue(source);

			while (queue.Count != 0)
			{
				string node = queue.Dequeue();

				foreach (var item in adjGraph[node])
				{
					if (nodeData[item].Color == NodeColor.White)
					{
						nodeData[item].Color = NodeColor.Grey;
						nodeData[item].Distance = nodeData[node].Distance + 1;
						queue.Enqueue(item);
					}
				}

				nodeData[node].Color = NodeColor.Black;
			}

			return nodeData;
		}

		enum NodeColor
		{
			White,
			Grey,
			Black
		}

		class NodeBFSData
		{
			public NodeColor Color { get; set; }
			public int Distance { get; set; }
		}

		class SourceDest
		{
			public string Source { get; set; }
			public string Destination { get; set; }
		}

		internal Dictionary<string, List<string>> ConstructAdjancyGraph(string[] dictionary)
		{
			Dictionary<string, List<string>> graph = new Dictionary<string, List<string>>();

			foreach (var node in dictionary)
			{
				List<string> links = new List<string>();

				foreach (var dest in dictionary)
				{
					if (node != dest) // assuming no duplicates
					{
						if (CanMoveInOneSetp(node, dest))
						{
							links.Add(dest);
						}
					}
				}

				graph.Add(node, links);
			}

			return graph;
		}

		internal bool CanMoveInOneSetp(string node, string dest)
		{
			if (node.Length >= dest.Length)
			{
				return node.Where(s => dest.Contains(s) == false).Count() == 1;
			}
			else
			{
				return dest.Where(s => node.Contains(s) == false).Count() == 1;
			}
		}

		public string[] Dictionary { get; set; }

		public Dictionary<string, List<string>> AdjGraph { get; set; }
	}

	[TestClass]
	public class StreetTest
	{
		string[] dictionary = 
		{
"BAG",
"BANG",
"BAT",
"BIG",
"BIT",
"BUG",
"BUT",
"CAR",
"CAT",
		};

		[TestMethod]
		public void MainTestCase()
		{
			StreetProblems target = new StreetProblems();

			target.Dictionary = dictionary;

			Assert.AreEqual(4, target.Solve("BIG", "CAT"));
			Assert.AreEqual(3, target.Solve("BAT", "CAR"));
		}

		[TestMethod]
		public void CanMoveInOneStepTestAllTrue()
		{
			StreetProblems target = new StreetProblems();

			Assert.IsTrue(target.CanMoveInOneSetp("ABC", "ABF"));
			Assert.IsTrue(target.CanMoveInOneSetp("AB", "ABF"));
			Assert.IsTrue(target.CanMoveInOneSetp("ABC", "AB"));
			Assert.IsTrue(target.CanMoveInOneSetp("BIG", "BIT"));
			Assert.IsTrue(target.CanMoveInOneSetp("BAT", "BIT"));
			Assert.IsTrue(target.CanMoveInOneSetp("BAT", "BUT"));
			Assert.IsTrue(target.CanMoveInOneSetp("BAT", "CAT"));
		}

		[TestMethod]
		public void CanMoveInOneStepTestAllFalse()
		{
			StreetProblems target = new StreetProblems();

			Assert.IsFalse(target.CanMoveInOneSetp("ABC", "ABECD"));
			Assert.IsFalse(target.CanMoveInOneSetp("ABC", "A"));
			Assert.IsFalse(target.CanMoveInOneSetp("ABCD", "ABCD"));
		}
	}
}
