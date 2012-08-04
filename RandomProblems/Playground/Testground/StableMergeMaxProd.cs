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
		public List<int[]> AllStableMerge(int[] left, int[] right)
		{
			if (left.Length == 0)
			{
				if (right.Length == 0)
				{
					return new List<int[]>();
				}
				else
				{
					return new List<int[]>() { right };
				}
			}
			else
			{
				if (right.Length == 0)
				{
					return new List<int[]>() { left };
				}
				else
				{
					var result = new List<int[]>();

					int[] dLeft = new int[left.Length - 1];
					Array.Copy(left, 1, dLeft, 0, dLeft.Length);
					var downLef = AllStableMerge(dLeft, right);

					foreach (var item in downLef)
					{
						int[] dAdd = new int[left.Length + right.Length];

						dAdd[0] = left[0];
						Array.Copy(item, 0, dAdd, 1, item.Length);

						result.Add(dAdd);
					}

					int[] dRight = new int[right.Length - 1];
					Array.Copy(right, 1, dRight, 0, dRight.Length);
					var downRt = AllStableMerge(left, dRight);

					foreach (var item in downRt)
					{
						int[] dAdd = new int[left.Length + right.Length];

						dAdd[0] = right[0];
						Array.Copy(item, 0, dAdd, 1, item.Length);

						result.Add(dAdd);
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

			var actual = target.AllStableMerge(a, b);

			Assert.AreEqual(Combination(a.Length + b.Length, a.Length), actual.Count);

			foreach (var item in actual)
			{
				Assert.AreEqual(a.Length + b.Length, item.Length);

				int j = 0, k = 0;

				for (int i = 0; i < item.Length; i++)
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
