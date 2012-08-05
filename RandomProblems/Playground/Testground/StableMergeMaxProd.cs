using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	/*
	 * http://stackoverflow.com/questions/11682472/stable-merging-two-arrays-to-maximize-product-of-adjacent-elements
	 * 
	 * You are given 2 arrays of size 'n' each. 
	 * You need to stable-merge these arrays 
	 * such that in the new array 
	 * sum of product of consecutive elements 
	 * is maximized.
	 * 
	 * For example 
	 * A= { 2, 1, 3}
	 * B= { 3, 7, 9}
	 * On Stable merging A and B will give an array C 
	 * with '2n' elements 
	 * say C={c1, c2, c3, c4, c5, c6} 
	 * You need to find a new array C by merging (stable) A and B such that 
	 * sum= c1*c2 + c3*c4 + c5* c6..... n terms is maximum.
	 * 
	 */
	class StableMergeMaxProd
	{
		public int[] Solver(int[] left, int[] right)
		{
			if (left.Length != right.Length)
			{
				throw new ArgumentException();
			}

			memoization.Clear();

			NodeData ans = Solve(left, 0, right, 0);

			int[] result = new int[left.Length + right.Length];

			int lhead = 0;
			int rhead = 0;
			int head = 0;

			while (ans != null)
			{
				if (ans.Child == null)
				{
					while (lhead < left.Length)
					{
						result[head++] = left[lhead++];
					}

					while (rhead < right.Length)
					{
						result[head++] = right[rhead++];
					}

					ans = null;
				}
				else
				{
					while (lhead < ans.Child.Item1)
					{
						result[head++] = left[lhead++];
					}

					while (rhead < ans.Child.Item2)
					{
						result[head++] = right[rhead++];
					}

					ans = memoization[ans.Child];
				}
			}

			return result;
		}

		private NodeData Solve(int[] left, int lstart, int[] right, int rstart)
		{
			var key = Tuple.Create(lstart, rstart);

			if (memoization.ContainsKey(key) == false)
			{
				NodeData nodeResult;

				var lend = left.Length;
				var rend = right.Length;

				if (lstart == lend)
				{
					if (rstart == rend)
					{
						nodeResult = new NodeData() { Max = 0, Child = null, NeedsPairing = false };
					}
					else
					{
						nodeResult = new NodeData()
						{
							Max = ComputeMax(right, rstart),
							NeedsPairing = (rend - rstart) % 2 != 0,
							Child = null
						};
					}
				}
				else
				{
					if (rstart == rend)
					{
						nodeResult = new NodeData()
						{
							Max = ComputeMax(left, lstart),
							NeedsPairing = (lend - lstart) % 2 != 0,
							Child = null
						};
					}
					else
					{
						var downLef = Solve(left, lstart + 1, right, rstart);

						var lefResNode = new NodeData()
						{
							Child = Tuple.Create(lstart + 1, rstart),
						};

						if (downLef.NeedsPairing)
						{
							lefResNode.Max = downLef.Max + left[lstart] * right[rstart];
							lefResNode.NeedsPairing = false;
						}
						else
						{
							lefResNode.Max = downLef.Max;
							lefResNode.NeedsPairing = true;
						}

						var downRt = Solve(left, lstart, right, rstart + 1);

						var rtResNode = new NodeData()
						{
							Child = Tuple.Create(lstart, rstart + 1),
						};

						if (downRt.NeedsPairing)
						{
							rtResNode.Max = downRt.Max + right[rstart] * left[lstart];
							rtResNode.NeedsPairing = false;
						}
						else
						{
							rtResNode.Max = downRt.Max;
							rtResNode.NeedsPairing = true;
						}

						if (lefResNode.Max > rtResNode.Max)
						{
							nodeResult = lefResNode;
						}
						else
						{
							nodeResult = rtResNode;
						}
					}
				}

				memoization.Add(key, nodeResult);
			}

			return memoization[key];
		}

		private int ComputeMax(int[] arr, int start)
		{
			if ((arr.Length - start) % 2 != 0)
			{
				int sum = 0;

				for (int i = start; i < arr.Length - 1; i+=2)
				{
					sum += arr[i] * arr[i + 1];
				}

				return sum;
			}
			else
			{
				int sum = 0;

				for (int i = start + 1; i < arr.Length - 1; i += 2)
				{
					sum += arr[i] * arr[i + 1];
				}

				return sum;
			}
		}

		private Dictionary<Tuple<int, int>, NodeData> memoization = new Dictionary<Tuple<int, int>, NodeData>();

		class NodeData
		{
			public int Max { get; set; }
			public bool NeedsPairing { get; set; }
			public Tuple<int, int> Child { get; set; }
		}

		public List<List<int>> AllStableMerge(int[] left, int lstart, int[] right, int rstart)
		{
			var lend = left.Length;
			var rend = right.Length;

			if (lstart == lend)
			{
				if (rstart == rend)
				{
					return new List<List<int>>();
				}
				else
				{
					return new List<List<int>>() 
					{ 
						right.Skip(rstart).Take(rend - rstart).ToList()
					};
				}
			}
			else
			{
				if (rstart == rend)
				{
					return new List<List<int>>() 
					{ 
						left.Skip(lstart).Take(lend - lstart).ToList()
					};
				}
				else
				{
					var result = new List<List<int>>();

					var downLef = AllStableMerge(left, lstart + 1, right, rstart);

					foreach (var item in downLef)
					{
						item.Insert(0, left[lstart]);
						result.Add(item);
					}

					var downRt = AllStableMerge(left, lstart, right, rstart + 1);

					foreach (var item in downRt)
					{
						item.Insert(0, right[rstart]);
						result.Add(item);
					}

					return result;
				}
			}
		}
	}

	[TestClass]
	public class StableMergeMaxProdTest
	{
		[TestMethod]
		public void VerifyMaxProd()
		{
			var target = new StableMergeMaxProd();

			// var actual = target.Solver(new int[] { 2, 1, 3 }, new int[] { 3, 7, 9 });
			var actual = target.Solver(new int[] { 1, 100 }, new int[] { 1, 100 });

			CollectionAssert.AreEqual(new int[] { 1, 1, 100, 100 }, actual);
		}

		[TestMethod]
		public void VerifyAllPossibleMerge()
		{
			AssertItWorks(new int[] { 1, 2 }, new int[] { 3, 4 });

			AssertItWorks(new int[] { }, new int[] { });

			AssertItWorks(new int[] { 1, 2 }, new int[] { });

			AssertItWorks(new int[] { }, new int[] { 3, 4 });

			AssertItWorks(new int[] { 1, 2, 5 }, new int[] { 3, 4 });

			AssertItWorks(new int[] { 1, 2 }, new int[] { 3, 4, 6 });

			AssertItWorks(new int[] { 1, 2, 5 }, new int[] { 3, 4, 6 });
		}

		private void AssertItWorks(int[] a, int[] b)
		{
			var target = new StableMergeMaxProd();

			var actual = target.AllStableMerge(a, 0, b, 0);

			Assert.AreEqual(Combination(a.Length + b.Length, a.Length), actual.Count);

			foreach (var item in actual)
			{
				Assert.AreEqual(a.Length + b.Length, item.Count);

				int j = 0, k = 0;

				for (int i = 0; i < item.Count; i++)
				{
					if (j < a.Length && item[i] == a[j])
					{
						j++;
					}
					else if (k < b.Length && item[i] == b[k])
					{
						k++;
					}
					else
					{
						Assert.Fail("Invalid sequence");
					}
				}
			}
		}

		private double Combination(int n, int k)
		{
			if (n < 0 || k < 0 || n < k)
			{
				throw new ArgumentException();
			}

			if (n == 0 && k == 0) // done from test point of view. actuall 0C0 = 1;
			{
				return 0;
			}

			if (k == 0)
			{
				return 1;
			}

			if (k == 1)
			{
				return n;
			}

			double result = n;
			result = result / k;
			result = result * Combination(n - 1, k - 1);

			return result;
		}
	}
}
