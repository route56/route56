using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Testground
{
	enum NodeColor
	{
		White,
		Grey,
		Black
	}

	class NodeBFSData<T>
	{
		public NodeColor Color { get; set; }
		public int Distance { get; set; }
		public T ParentPath { get; set; }
	}

	class NodeDFSData<T>
	{
		public NodeColor Color { get; set; }

		/// <summary>
		/// d[u]
		/// </summary>
		public int StartTime { get; set; }
		/// <summary>
		/// f[u]
		/// </summary>
		public int EndTime { get; set; }

		public T ParentPath { get; set; }
	}

	class GraphSearch
	{
		internal static Dictionary<T, NodeBFSData<T>> BFSTraversal<T>(Dictionary<T, List<T>> adjGraph, T source)
		{
			var nodeData = new Dictionary<T, NodeBFSData<T>>();

			foreach (var item in adjGraph.Keys)
			{
				nodeData.Add(item, new NodeBFSData<T>()
				{
					Color = NodeColor.White,
					Distance = int.MaxValue,
					ParentPath = default(T),
				});
			}

			Queue<T> queue = new Queue<T>();

			nodeData[source].Color = NodeColor.Grey;
			nodeData[source].Distance = 0;

			queue.Enqueue(source);

			while (queue.Count != 0)
			{
				T node = queue.Dequeue();

				foreach (var item in adjGraph[node])
				{
					if (nodeData[item].Color == NodeColor.White)
					{
						nodeData[item].Color = NodeColor.Grey;
						nodeData[item].Distance = nodeData[node].Distance + 1;
						nodeData[item].ParentPath = node;
						queue.Enqueue(item);
					}
				}

				nodeData[node].Color = NodeColor.Black;
			}

			return nodeData;
		}

		// TODO classification of edges as Tree, Back, Forward, Cross. DAG => No Back. Undirected, Only Tree and Back.
		internal static Dictionary<T, NodeDFSData<T>> DFSTraversal<T>(Dictionary<T, List<T>> adjGraph)
		{
			return _DFSTraversalIterative<T>(adjGraph);
		}

		private static Dictionary<T, NodeDFSData<T>> _DFSTraversalRecursive<T>(Dictionary<T, List<T>> adjGraph)
		{
			var result = new Dictionary<T, NodeDFSData<T>>();

			foreach (var item in adjGraph.Keys)
			{
				result.Add(item, new NodeDFSData<T>()
					{
						Color = NodeColor.White,
					});
			}

			int time = 0;

			foreach (var item in adjGraph.Keys)
			{
				if (result[item].Color == NodeColor.White)
				{
					_DFSVisit<T>(item, adjGraph, result, ref time);
				}
			}

			return result;
		}

		private static void _DFSVisit<T>(T current, Dictionary<T, List<T>> adjGraph, Dictionary<T, NodeDFSData<T>> result, ref int time)
		{
			result[current].Color = NodeColor.Grey;
			result[current].StartTime = ++time;

			foreach (var item in adjGraph[current])
			{
				if (result[item].Color == NodeColor.White)
				{
					result[item].ParentPath = current;

					_DFSVisit<T>(item, adjGraph, result, ref time);
				}
			}

			result[current].EndTime = ++time;
			result[current].Color = NodeColor.Black;
		}

		private static Dictionary<T, NodeDFSData<T>> _DFSTraversalIterative<T>(Dictionary<T, List<T>> adjGraph)
		{
			var result = new Dictionary<T, NodeDFSData<T>>();

			foreach (var item in adjGraph.Keys)
			{
				result.Add(item, new NodeDFSData<T>()
				{
					Color = NodeColor.White,
				});
			}

			int time = 0;

			foreach (var item in adjGraph.Keys)
			{
				if (result[item].Color == NodeColor.White)
				{
					//_DFSVisit<T>(item, adjGraph, result, ref time);
					var stack = new Stack<T>();
					var flag = new List<T>();

					stack.Push(item);
					while (stack.Count > 0)
					{
						var current = stack.Pop();

						if (flag.Contains(current) == false)
						{
							result[current].Color = NodeColor.Grey;
							result[current].StartTime = ++time;

							foreach (var adjItem in adjGraph[current])
							{
								if (result[adjItem].Color == NodeColor.White)
								{
									result[adjItem].ParentPath = current;

									// _DFSVisit<T>(adjItem, adjGraph, result, ref time);
									flag.Add(current);
									stack.Push(current);
									stack.Push(adjItem);
								}
							}
						}
						else
						{
							result[current].EndTime = ++time;
							result[current].Color = NodeColor.Black;
						}
					}
				}
			}

			return result;
		}
	}
}
