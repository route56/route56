using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

namespace Testground
{
	[DebuggerDisplay("Value = {Value} Left = {Left.Value} Right = {Right.Value}")]
	class Node
	{
		public int Value { get; set; }
		public Node Left { get; set; }
		public Node Right { get; set; }

		public List<int> LevelList { get; set; }
		public bool VistedMe { get; set; }
	}

	class BSTFlatteningProblem
	{
		/*
		 * Write a program to convert a BST to sorted list. 
		 * You must use the same tree and make the right child pointer 
		 * as the next pointer in the list.
		 */
		internal void TransformSortedList(ref Node root)
		{
			Node parent = new Node();

			Explore(root, ref parent, null);

			root = parent.Right;
		}

		/// <summary>
		/// Explores and transforms into sorted list
		/// </summary>
		/// <param name="me">node to explore. Can't be null</param>
		/// <param name="parent">Parent of leftmost inner node</param>
		/// <param name="rightEnd">right end node for right most</param>
		private void Explore(Node me, ref Node parent, Node rightEnd)
		{
			if (me.Left == null)
			{
				parent.Right = me;
			}
			else
			{
				Explore(me.Left, ref parent, me);
			}

			if (me.Right == null)
			{
				me.Right = rightEnd;
			}
			else
			{
				Explore(me.Right, ref me, rightEnd);
			}
		}
	}

	[TestClass]
	public class BSTFlattehingProblemTest
	{
		[TestMethod]
		public void FullBalancedBST()
		{
			var target = new BSTFlatteningProblem();

			Node root = new Node() { Value = 4 };

			root.Left = new Node() { Value = 2 };
			root.Right = new Node() { Value = 6 };

			root.Left.Left = new Node() { Value = 1 };
			root.Left.Right = new Node() { Value = 3 };

			root.Right.Left = new Node() { Value = 5 };
			root.Right.Right = new Node() { Value = 7 };

			target.TransformSortedList(ref root);

			var next = root;

			for (int i = 1; i < 8; i++)
			{
				Assert.AreEqual(i, next.Value);
				next = next.Right;
			}

			Assert.IsNull(next);
		}

		[TestMethod]
		public void SkewedLeftBST()
		{
			var target = new BSTFlatteningProblem();

			Node root = new Node() { Value = 6 };
			root.Left = new Node() { Value = 5 };
			root.Left.Left = new Node() { Value = 4 };
			root.Left.Left.Left = new Node() { Value = 3 };
			root.Left.Left.Left.Left = new Node() { Value = 2 };
			root.Left.Left.Left.Left.Left = new Node() { Value = 1 };

			target.TransformSortedList(ref root);

			var next = root;

			for (int i = 1; i < 7; i++)
			{
				Assert.AreEqual(i, next.Value);
				next = next.Right;
			}

			Assert.IsNull(next);
		}

		[TestMethod]
		public void SkewedRightBST()
		{
			var target = new BSTFlatteningProblem();

			Node root = new Node() { Value = 1 };
			root.Right = new Node() { Value = 2 };
			root.Right.Right = new Node() { Value = 3 };
			root.Right.Right.Right = new Node() { Value = 4 };
			root.Right.Right.Right.Right = new Node() { Value = 5 };
			root.Right.Right.Right.Right.Right = new Node() { Value = 6 };

			target.TransformSortedList(ref root);

			var next = root;

			for (int i = 1; i < 7; i++)
			{
				Assert.AreEqual(i, next.Value);
				next = next.Right;
			}

			Assert.IsNull(next);
		}

		[TestMethod]
		public void PartialBalancedBST()
		{
			var target = new BSTFlatteningProblem();

			Node root = new Node() { Value = 5 };

			root.Left = new Node() { Value = 1 };
			root.Right = new Node() { Value = 7 };

			root.Left.Right = new Node() { Value = 3 };

			root.Left.Right.Left = new Node() { Value = 2 };
			root.Left.Right.Right = new Node() { Value = 4 };

			root.Right.Left = new Node() { Value = 6 };
			root.Right.Right = new Node() { Value = 9 };

			root.Right.Right.Left = new Node() { Value = 8 };
			root.Right.Right.Right = new Node() { Value = 10 };

			target.TransformSortedList(ref root);

			var next = root;

			for (int i = 1; i < 11; i++)
			{
				Assert.AreEqual(i, next.Value);
				next = next.Right;
			}

			Assert.IsNull(next);
		}

		[TestMethod]
		public void BoundaryBST()
		{
			var target = new BSTFlatteningProblem();

			Node root = new Node() { Value = 1 };

			target.TransformSortedList(ref root);

			var next = root;

			for (int i = 1; i < 2; i++)
			{
				Assert.AreEqual(i, next.Value);
				next = next.Right;
			}

			Assert.IsNull(next);
		}
	}

	class BinaryTreeProblemsSumNodesEquidistantFromLeaves 
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
	public class BinaryTreeTestSumNodesEquidistantFromLeaves
	{
		[TestMethod]
		public void BalancedTree()
		{
			BinaryTreeProblemsSumNodesEquidistantFromLeaves target = new BinaryTreeProblemsSumNodesEquidistantFromLeaves();

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
			BinaryTreeProblemsSumNodesEquidistantFromLeaves target = new BinaryTreeProblemsSumNodesEquidistantFromLeaves();

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
			BinaryTreeProblemsSumNodesEquidistantFromLeaves target = new BinaryTreeProblemsSumNodesEquidistantFromLeaves();

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

	class TreeTraversal
	{
		#region inorder
		internal void InorderRecursive(Node node, IList<int> toAdd)
		{
			if (node != null)
			{
				InorderRecursive(node.Left, toAdd);
				toAdd.Add(node.Value);
				InorderRecursive(node.Right, toAdd);
			}
		}

		// http://stackoverflow.com/questions/2116662/help-me-understand-inorder-traversal-without-using-recursion
		internal void InorderIterativeSO1(Node node, IList<int> toAdd)
		{
			var stack = new Stack<Node>();

			while (stack.Count > 0 || node != null)
			{
				if (node != null)
				{
					stack.Push(node);
					node = node.Left;
				}
				else
				{
					node = stack.Pop();
					toAdd.Add(node.Value);
					node = node.Right;
				}
			}
		}

		enum FunctionState
		{
			Entered,
			AfterLeftTraversal
		}

		internal void InorderIterativeSO2(Node node, IList<int> toAdd)
		{
			var stack = new Stack<Tuple<Node, FunctionState>>();
			stack.Push(new Tuple<Node, FunctionState>(node, FunctionState.Entered)); // add in advance.

			while (stack.Count > 0)
			{
				var state = stack.Pop();

				switch (state.Item2)
				{
					case FunctionState.Entered:
						if (state.Item1 != null)
						{
							// imp. save state
							stack.Push(new Tuple<Node, FunctionState>(state.Item1, FunctionState.AfterLeftTraversal));
							// recurse
							stack.Push(new Tuple<Node, FunctionState>(state.Item1.Left, FunctionState.Entered));
						}
						break;

					case FunctionState.AfterLeftTraversal:
						toAdd.Add(state.Item1.Value);

						stack.Push(new Tuple<Node,FunctionState>(state.Item1.Right, FunctionState.Entered));

						break;
				}
			}
		}

		internal void InorderIterative(Node root, IList<int> toAdd)
		{
			if (root == null)
			{
				return;
			}

			var mainStack = new Stack<Node>();

			mainStack.Push(root);

			var dyNodeColor = new Dictionary<Node, bool>();

			while (mainStack.Count > 0)
			{
				var current = mainStack.Pop();

				if (current == null)
				{
					continue;
				}

				if (dyNodeColor.ContainsKey(current))
				{

					if (dyNodeColor[current])
					{
						toAdd.Add(current.Value);
						dyNodeColor[current] = false;
						mainStack.Push(current.Right);
					}
				}
				else
				{
					mainStack.Push(current);
					mainStack.Push(current.Left);
					dyNodeColor.Add(current, true);
				}
			}
		}
		#endregion

		#region Preorder

		internal void PreorderRecursive(Node node, IList<int> toAdd)
		{
			if (node != null)
			{
				toAdd.Add(node.Value);
				PreorderRecursive(node.Left, toAdd);
				PreorderRecursive(node.Right, toAdd);
			}
		}

		enum FunctionPreorder
		{
			Entered,
			Left,
		}

		internal void PreorderIterative(Node node, IList<int> toAdd)
		{
			var stack = new Stack<Tuple<Node, FunctionPreorder>>();

			stack.Push(new Tuple<Node,FunctionPreorder>(node, FunctionPreorder.Entered));

			while (stack.Count > 0)
			{
				var context = stack.Pop();

				switch (context.Item2)
				{
					case FunctionPreorder.Entered:
						if (context.Item1 != null)
						{
							toAdd.Add(context.Item1.Value);
							stack.Push(new Tuple<Node, FunctionPreorder>(context.Item1, FunctionPreorder.Left));
							stack.Push(new Tuple<Node, FunctionPreorder>(context.Item1.Left, FunctionPreorder.Entered));
						}
						break;

					case FunctionPreorder.Left:
						stack.Push(new Tuple<Node, FunctionPreorder>(context.Item1.Right, FunctionPreorder.Entered));
						break;
				}
			}
		}

		internal void PreorderIterativeOptimized(Node node, IList<int> toAdd)
		{
			var stack = new Stack<Node>();

			while (stack.Count > 0 || node != null)
			{
				if (node != null)
				{
					toAdd.Add(node.Value);

					stack.Push(node);
					node = node.Left;
				}
				else
				{
					node = stack.Pop();
					node = node.Right;
				}
			}
		}

		#endregion

		#region Postorder

		internal void PostorderRecursive(Node node, IList<int> toAdd)
		{
			if (node != null)
			{
				PostorderRecursive(node.Left, toAdd);
				PostorderRecursive(node.Right, toAdd);
				toAdd.Add(node.Value);
			}
		}

		enum FunctionPostorder
		{
			Entered,
			Left,
			Right
		}

		internal void PostorderIterative(Node node, IList<int> toAdd)
		{
			var stack = new Stack<Tuple<Node, FunctionPostorder>>();

			stack.Push(new Tuple<Node, FunctionPostorder>(node, FunctionPostorder.Entered));

			while (stack.Count > 0)
			{
				var context = stack.Pop();

				switch (context.Item2)
				{
					case FunctionPostorder.Entered:
						if (context.Item1 != null)
						{
							stack.Push(new Tuple<Node, FunctionPostorder>(context.Item1, FunctionPostorder.Left));
							stack.Push(new Tuple<Node, FunctionPostorder>(context.Item1.Left, FunctionPostorder.Entered));
						}
						break;

					case FunctionPostorder.Left:
						stack.Push(new Tuple<Node, FunctionPostorder>(context.Item1, FunctionPostorder.Right));
						stack.Push(new Tuple<Node, FunctionPostorder>(context.Item1.Right, FunctionPostorder.Entered));
						break;

					case FunctionPostorder.Right:
						toAdd.Add(context.Item1.Value);
						break;
				}
			}
		}

		internal void PostorderIterativeOptimized(Node node, IList<int> toAdd)
		{
			var stack = new Stack<Node>();

			while (stack.Count > 0 || node != null)
			{
				if (node != null)
				{
					stack.Push(node);
					node = node.Left;
				}
				else
				{
					node = stack.Pop();

					if (node.VistedMe)
					{
						toAdd.Add(node.Value);
						node = null; // wow this is imp. else infinite loop.
					}
					else
					{
						node.VistedMe = true;
						stack.Push(node);
						node = node.Right;
					}
				}
			}
		}

		#endregion
	}

	[TestClass]
	public class TreeTraversalTest
	{
		[TestMethod]
		public void InorderRecursiveIterativeMatch()
		{
			Node root = InitNode();

			var target = new TreeTraversal();

			var recurse = new List<int>();
			var iterate = new List<int>();

			target.InorderRecursive(root, recurse);
			target.InorderIterative(root, iterate);
			CollectionAssert.AreEqual(recurse, iterate);

			iterate.Clear();
			target.InorderIterativeSO1(root, iterate);
			CollectionAssert.AreEqual(recurse, iterate);

			iterate.Clear();
			target.InorderIterativeSO2(root, iterate);
			CollectionAssert.AreEqual(recurse, iterate);
		}

		[TestMethod]
		public void PreorderRecursiveIterativeMatch()
		{
			Node root = InitNode();

			var target = new TreeTraversal();

			var recurse = new List<int>();
			var iterate = new List<int>();

			target.PreorderRecursive(root, recurse);
			target.PreorderIterative(root, iterate);
			CollectionAssert.AreEqual(recurse, iterate);

			iterate.Clear();
			target.PreorderIterativeOptimized(root, iterate);
			CollectionAssert.AreEqual(recurse, iterate);
		}

		[TestMethod]
		public void PostorderRecursiveIterativeMatch()
		{
			Node root = InitNode();

			var target = new TreeTraversal();

			var recurse = new List<int>();
			var iterate = new List<int>();

			target.PostorderRecursive(root, recurse);
			target.PostorderIterative(root, iterate);
			CollectionAssert.AreEqual(recurse, iterate);

			iterate.Clear();
			target.PostorderIterativeOptimized(root, iterate);
			CollectionAssert.AreEqual(recurse, iterate);
		}



		private static Node InitNode()
		{
			Node root = new Node() { Value = 4 };

			root.Left = new Node() { Value = 2 };
			root.Right = new Node() { Value = 6 };

			root.Left.Left = new Node() { Value = 1 };
			root.Left.Right = new Node() { Value = 3 };

			root.Right.Left = new Node() { Value = 5 };
			root.Right.Right = new Node() { Value = 7 };
			return root;
		}
	}
}
