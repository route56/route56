using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	class Node
	{
		public int Value { get; set; }
		public Node Left { get; set; }
		public Node Right { get; set; }
		public List<int> LevelList { get; set; }
	}

	class BinaryTreeProblems
	{
		/*
		 * Given a binary tree , 
		 * find the sum of all the nodes , 
		 * which are at a particular level from all of its existing leaves. 
		 * ( note:- it might be possible a single such node would exist for two different leaf , in that case we need to sum it up only once )
		 */

		internal int SumNodesBeforeLeafLevel(Node root, int level)
		{
/*
 * 1. Allocate a global bit vector of size h where h is height of the tree.
2. Traverse the tree in post-order fashion.
3. At each node do the following:
a) if leaf node: set the kth bit of the vector as 1 where k is the height of the current node.
b) else check if the k+l bit is set and add the value of the current node to sum.

Time complexity: O(n)
Space: O(logn)

The idea is to figure out the height of all leaf nodes and then choose all the nodes that are level 'l' from every leaf node.
 */
			header = true;
			visitCallCount = 0;

			Dictionary<int, bool> boolList = new Dictionary<int, bool>();
			int sum = 0;

			Visit(root, boolList, level, 0, ref sum);

			return sum;
		}

		static int visitCallCount = 0;

		private void Visit(Node node, Dictionary<int, bool> boolList, int level, int nodeDepth, ref int sum)
		{
			int functionId = visitCallCount++;

			Dump("before", functionId, visitCallCount, boolList, node, sum);

			if (node != null)
			{
				Visit(node.Left, boolList, level, nodeDepth + 1, ref sum);
				Visit(node.Right, boolList, level, nodeDepth + 1, ref sum);

				if (node.Left == null && node.Right == null)
				{
					if (level == 0)
					{
						sum += node.Value;
					}
					else
					{
						boolList[nodeDepth] = true;
					}
				}
				else
				{
					if (boolList.ContainsKey(nodeDepth + level))
					{
						sum += node.Value;
					}
				}
			}

			Dump("after", functionId, visitCallCount, boolList, node, sum);
		}

		static bool header = true;

		private void Dump(string message, int functionId, int visitCallCount, Dictionary<int, bool> boolList, Node node, int sum)
		{
			if (header)
			{
				Console.WriteLine("Message\tfunctionId\tcallcount\tboolList\tNode\tsum");
				header = false;
			}

			Console.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t{5}",
				message,
				functionId,
				visitCallCount,
				String.Join(",", boolList.Keys.Select(s => s.ToString())),
				node == null ? "Null" : node.Value.ToString(),
				sum
				);
		}

		/// <summary>
		/// O(n^2) time, O(n^2) space
		/// </summary>
		/// <param name="root"></param>
		/// <param name="level"></param>
		/// <returns></returns>
		internal int SumNodesBeforeLeafLevel2(Node root, int level)
		{
			if (root == null || level < 0)
			{
				return 0;
			}

			LevelBand(root);

			int sum = 0;

			GetSum(root, level, ref sum);

			return sum;
		}

		private void GetSum(Node node, int level, ref int sum)
		{
			if (node != null)
			{
				if (node.LevelList.Contains(level))
				{
					sum += node.Value;
				}

				GetSum(node.Left, level, ref sum);
				GetSum(node.Right, level, ref sum);
			}
		}

		private List<int> LevelBand(Node node)
		{
			if (node == null)
			{
				return new List<int>();
			}

			node.LevelList = new List<int>();

			if (node.Left == null && node.Right == null)
			{
				node.LevelList.Add(0);
				return node.LevelList;
			}

			node.LevelList.AddRange(LevelBand(node.Left).Union(LevelBand(node.Right)));

			for (int i = 0; i < node.LevelList.Count; i++)
			{
				node.LevelList[i]++;
			}

			return node.LevelList;
		}
	}

	[TestClass]
	public class BinaryTreeTest
	{
		[TestMethod]
		public void BalancedTree()
		{
			BinaryTreeProblems target = new BinaryTreeProblems();

			Node root = new Node() { Value = 1 };

			root.Left = new Node() { Value = 2 };
			root.Right = new Node() { Value = 3 };

			root.Left.Left = new Node() { Value = 4 };
			root.Left.Right = new Node() { Value = 5 };

			root.Right.Left = new Node() { Value = 6 };
			root.Right.Right = new Node() { Value = 7 };

			Assert.AreEqual(1, target.SumNodesBeforeLeafLevel(root, 2));
			Assert.AreEqual(2+3, target.SumNodesBeforeLeafLevel(root, 1));
			Assert.AreEqual(4+5+6+7, target.SumNodesBeforeLeafLevel(root, 0));

			Assert.AreEqual(0, target.SumNodesBeforeLeafLevel(root, 3));
			Assert.AreEqual(0, target.SumNodesBeforeLeafLevel(root, 10));
			Assert.AreEqual(0, target.SumNodesBeforeLeafLevel(root, -10));
		}

		[TestMethod]
		public void SkewedTree()
		{
			BinaryTreeProblems target = new BinaryTreeProblems();

			Node root = new Node() { Value = 1 };
				root.Left = new Node() { Value = 2 };
				root.Left.Left = new Node() { Value = 3 };
				root.Left.Left.Left = new Node() { Value = 4 };
				root.Left.Left.Left.Left = new Node() { Value = 5 };

			Assert.AreEqual(1, target.SumNodesBeforeLeafLevel(root, 4));
			Assert.AreEqual(2, target.SumNodesBeforeLeafLevel(root, 3));
			Assert.AreEqual(3, target.SumNodesBeforeLeafLevel(root, 2));
			Assert.AreEqual(4, target.SumNodesBeforeLeafLevel(root, 1));
			Assert.AreEqual(5, target.SumNodesBeforeLeafLevel(root, 0));

			Assert.AreEqual(0, target.SumNodesBeforeLeafLevel(root, -1));
			Assert.AreEqual(0, target.SumNodesBeforeLeafLevel(root, 5));
		}

		[TestMethod]
		public void UnBalancedTree()
		{
			BinaryTreeProblems target = new BinaryTreeProblems();

			Node root = new Node() { Value = 1 };

			root.Left = new Node() { Value = 2 };
				root.Left.Left = new Node() { Value = 3 };
				root.Left.Right = new Node() { Value = 4 };
					root.Left.Right.Left = new Node() { Value = 5 };
					root.Left.Right.Right = new Node() { Value = 6 };

			Assert.AreEqual(1, target.SumNodesBeforeLeafLevel(root, 3));
			Assert.AreEqual(1 + 2, target.SumNodesBeforeLeafLevel(root, 2));
			Assert.AreEqual(2 + 4, target.SumNodesBeforeLeafLevel(root, 1));
			Assert.AreEqual(3 + 5 + 6, target.SumNodesBeforeLeafLevel(root, 0));

			Assert.AreEqual(0, target.SumNodesBeforeLeafLevel(root, 4));
		}
	}
}
