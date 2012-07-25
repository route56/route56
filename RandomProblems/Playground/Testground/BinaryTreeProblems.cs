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
