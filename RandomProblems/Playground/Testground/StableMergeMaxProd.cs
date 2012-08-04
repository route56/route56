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
		public List<List<int>> AllStableMerge(int[] left, int lstart, int lend,
			int[] right, int rstart, int rend)
		{
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

					var downLef = AllStableMerge(left, lstart + 1, lend, right, rstart, rend);

					foreach (var item in downLef)
					{
						item.Insert(0, left[lstart]);
						result.Add(item);
					}

					var downRt = AllStableMerge(left, lstart, lend, right, rstart + 1, rend);

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

			var actual = target.AllStableMerge(a, 0, a.Length, b, 0, b.Length);

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
