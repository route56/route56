using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;


namespace Testground
{
	public interface IScanProblem
	{
		int Worst(int[] input);
		int Optimal(int[] input);
	}

	public class Inversions
	{
		public int CountBruteForce(int[] source)
		{
			int count = 0;

			for (int i = 0; i < source.Length - 1; i++)
			{
				for (int j = i + 1; j < source.Length; j++)
				{
					if (source[i] > source[j])
					{
						count++;
					}
				}
			}

			return count;
		}

		public int CountOptimal(int[] source)
		{
			int count = 0;

			var copy = source.ToList();

			copy.Sort();

			for (int i = 0; i < source.Length; i++)
			{
				int index = copy.BinarySearch(source[i]);
				count += index;
				copy.RemoveAt(index);
			}

			return count;
		}
	}

	[TestClass]
	public class InversionTest
	{
		[TestMethod]
		public void BasicBruteForce()
		{
			Assert.AreEqual(0, new Inversions().CountBruteForce(new int[] { 1, 2, 3, 4, 5, 6 }));

			Assert.AreEqual(1, new Inversions().CountBruteForce(new int[] { 1, 2, 4, 3, 5, 6 }));

			Assert.AreEqual(2, new Inversions().CountBruteForce(new int[] { 1, 4, 2, 3, 5, 6 }));
		}

		[TestMethod]
		public void BasicOptimal()
		{
			Assert.AreEqual(3, new Inversions().CountOptimal(new int[] { 3, 2, 1 }));

			Assert.AreEqual(1, new Inversions().CountOptimal(new int[] { 2, 1, 3 }));
			Assert.AreEqual(0, new Inversions().CountOptimal(new int[] { 1, 2, 3, 4, 5, 6 }));

			Assert.AreEqual(1, new Inversions().CountOptimal(new int[] { 1, 2, 4, 3, 5, 6 }));

			Assert.AreEqual(2, new Inversions().CountOptimal(new int[] { 1, 4, 2, 3, 5, 6 }));
		}
	}

	public class CircularProblem : IScanProblem
	{
		public int Worst(int[] input)
		{
			int max = int.MinValue;

			for (int i = 0; i < input.Length; i++)
			{
				int sum = 0;

				for (int j = 0; j < input.Length; j++)
				{
					sum += input[(i + j) % input.Length];

					max = Math.Max(sum, max);
				}
			}

			return max;
		}

		public int Optimal(int[] input)
		{
			if (input.Length == 0)
			{
				return int.MinValue;
			}

			// Find min element's index.
			int min = int.MaxValue;
			int minIndex = -1;

			for (int i = 0; i < input.Length; i++)
			{
				if (min > input[i])
				{
					min = input[i];
					minIndex = i;
				}
			}

			int maxSumTillNow = int.MinValue;
			int tailSum = int.MinValue;

			for (int i = 0; i < input.Length; i++)
			{
				tailSum = Math.Max(tailSum, 0);

				tailSum += input[(i + minIndex) % input.Length];

				maxSumTillNow = Math.Max(maxSumTillNow, tailSum);
			}

			return maxSumTillNow;
		}
	}

	class LinearProblem : IScanProblem
	{
		public int Worst(int[] input)
		{
			int max = int.MinValue;

			for (int i = 0; i < input.Length; i++)
			{
				int sum = 0;

				for (int j = i; j < input.Length; j++)
				{
					sum += input[j];

					max = Math.Max(sum, max);
				}
			}

			return max;
		}

		public int Optimal(int[] input)
		{
			if (input.Length == 0)
			{
				return int.MinValue;
			}

			int maxSumTillNow = input[0];
			int tailSum = maxSumTillNow;

			for (int i = 1; i < input.Length; i++)
			{
				if (tailSum < 0)
				{
					tailSum = 0;
				}

				tailSum += input[i];

				if (maxSumTillNow < tailSum)
				{
					maxSumTillNow = tailSum;
				}
			}

			return maxSumTillNow;
		}
	}

	[TestClass]
	public abstract class ScanProblemTest
	{
		public abstract IScanProblem Target { get; }

		[TestMethod]
		public void FailedTestCases()
		{
			TestFor(new int[] { 100, -10, 100 });
			TestFor(new int[] { 10, -100, 30, -100, 10 });
			TestFor(new int[] { 10, -9, 11, -5, -5, 10 });
		}

		[TestMethod]
		public void MaxCircularSumTextRandom()
		{
			Random rand = new Random();

			for (int i = 0; i < 200; i++)
			{
				RandomTest(rand);
			}
		}

		private void RandomTest(Random rand)
		{
			int[] input = GetRandomDataSet(rand);
			int expected = Target.Worst(input); 
			int actual;
			actual = Target.Optimal(input);
			Assert.AreEqual(expected, actual);
		}

		private void TestFor(int[] input)
		{
			int expected = Target.Worst(input);
			int actual;
			actual = Target.Optimal(input);
			Assert.AreEqual(expected, actual);
		}

		private int[] GetRandomDataSet(Random rand)
		{
			int rsize = 100;
			int rmin = -1000;
			int rmax = 1000;

			int size = rand.Next(rsize);

			int[] res = new int[size];

			for (int i = 0; i < res.Length; i++)
			{
				res[i] = rand.Next(rmin, rmax);
			}

			return res;
		}
	}

	[TestClass]
	public class CircularTest : ScanProblemTest
	{
		public override IScanProblem Target
		{
			get { return target; }
		}

		IScanProblem target = new CircularProblem();

		[TestMethod()]
		public void MaxSumTest()
		{
			int[] input = { 8, -8, 9, -9, 10, -11, 12 };
			int expected = 22;
			int actual;
			actual = target.Optimal(input);
			Assert.AreEqual(expected, actual);
		}
	}

	[TestClass]
	public class LinearTest : ScanProblemTest
	{
		public override IScanProblem Target
		{
			get { return target; }
		}

		IScanProblem target = new LinearProblem();
	}

	class IntToggleProblems
	{
		public void Transform(int[] a)
		{
			for (int i = 0; i < a.Length - 1; i++)
			{
				if (i % 2 == 0)
				{
					if (a[i] < a[i + 1])
					{
						continue;
					}
					else
					{
						int temp = a[i];
						a[i] = a[i + 1];
						a[i + 1] = temp;
					}
				}
				else
				{
					if (a[i] < a[i + 1])
					{
						int temp = a[i];
						a[i] = a[i + 1];
						a[i + 1] = temp;
					}
					else
					{
						continue;
					}
				}
			}
		}
	}

	[TestClass]
	public class IntToggleProblemsTest
	{
		[TestMethod]
		public void MyTestMethod()
		{
			var target = new IntToggleProblems();
			var a = new int[] { 0, 1, 2, 3 };
			var ex = new int[] { 0, 2, 1, 3 };

			var a2 = new int[] { 2, 1, 3, 0 };
			var ex2 = new int[] { 1, 3, 0, 2 };

			target.Transform(a);
			target.Transform(a2);

			CollectionAssert.AreEqual(ex, a);
			CollectionAssert.AreEqual(ex2, a2);
		}

		static IEnumerable<int> RandomNumberStream(Random rand)
		{
			while (true)
			{
				yield return rand.Next();
			}
		}

		[TestMethod]
		public void MyTestMethod2()
		{
			Random rand = new Random();

			var a = RandomNumberStream(rand).Take(101).ToArray();

			var target = new IntToggleProblems();

			target.Transform(a);

			for (int i = 0; i < a.Length - 1; i++)
			{
				if (i % 2 == 0)
				{
					Assert.IsTrue(a[i] < a[i + 1]);
				}
				else
				{
					Assert.IsTrue(a[i] > a[i + 1]);
				}
			}
		}
	}

	/*
	 * Natural numbers. No duplicate. Find smallest natural number missing in array size n.
	 * XOR and index Hopping
	 * Insertion sort, quick sort inspiration
	 * 1,2,3,4,...100, 102, ... 
	 * sort n scan
	 * scan . min and max.
	 * n numbers.
	 * 1, n... MAX
	 * i < n ? 
	 */
	class MissingNaturalNumber
	{
		void Swap(ref int a, ref int b)
		{
			int t = a;
			a = b;
			b = t;
		}

		internal int FindMissing(int[] arr)
		{
			int m = 0;

			// find min
			for (int i = 1; i < arr.Length; i++)
			{
				if (arr[m] > arr[i])
				{
					m = i;
				}
			}

			// early exit
			if (arr[m] != 1)
			{
				return 1;
			}

			int j = 0;
			int k = arr.Length - 1;

			// find quick sort index of n + 1
			while (j < k)
			{
				if (arr[j] <= arr.Length + 1)
				{
					j++;
				}
				else if(arr[k] > arr.Length + 1)
				{
					k--;
				}
				else
				{
					Swap(ref arr[j], ref arr[k]);
					j++;
					k--;
				}
			}

			// sorting natural numbers
			SortNaturalNumbers(arr, 0, k);

			// find the missing guy!
			for (int i = 0; i < arr.Length; i++)
			{
				if (arr[i] != i + 1)
				{
					return i + 1;
				}
			}

			return -1;
		}

		internal void SortNaturalNumbers(int[] arr, int j, int k)
		{
			while (j <= k)
			{
				if (arr[j] == j + 1)
				{
					j++;
				}
				else
				{
					if (arr[j] - 1 <= k)
					{
						Swap(ref arr[j], ref arr[arr[j] - 1]);
					}
					else
					{
						j++;
					}
				}
			}
		}

		internal int FindMissingBruteForce(int[] arr)
		{
			Array.Sort(arr);

			for (int i = 0; i < arr.Length; i++)
			{
				if (i + 1 != arr[i])
				{
					return i + 1;
				}
			}

			return -1;
		}
	}

	[TestClass]
	public class MissingNaturalNumTest
	{
		[TestMethod]
		public void MyTestMethod()
		{
			var target = new MissingNaturalNumber();

			Assert.AreEqual(6, target.FindMissing(
				Enumerable.Range(10, 3)
				.Union(Enumerable.Range(1, 3))
				.Union(Enumerable.Range(100, 10))
				.Union(Enumerable.Range(4, 2))
				.ToArray()));

			Assert.AreEqual(6, target.FindMissing(
				Enumerable.Range(100, 3)
				.Union(Enumerable.Range(90, 3))
				.Union(Enumerable.Range(80, 10))
				.Union(Enumerable.Range(1, 5))
				.ToArray()));

			// passed
			Assert.AreEqual(4, target.FindMissing(
				Enumerable.Range(10, 3).Union(Enumerable.Range(1, 3)).ToArray()));

			Assert.AreEqual(11, target.FindMissing(
				Enumerable.Range(12, 10).Union(Enumerable.Range(1, 10)).ToArray()));

			Assert.AreEqual(11, target.FindMissing(
				Enumerable.Range(1, 10).Union(Enumerable.Range(12, 20)).ToArray()));

			Assert.AreEqual(4, target.FindMissing(
				Enumerable.Range(1, 3).Union(Enumerable.Range(10, 3)).ToArray()));

		}

		[TestMethod]
		public void MyTestMethod2()
		{
			var target = new MissingNaturalNumber();

			var arr = new int[] { 4,2,1,3 };

			target.SortNaturalNumbers(arr, 0, arr.Length - 1);

			CollectionAssert.AreEqual(Enumerable.Range(1, 4).ToArray(), arr);

			arr = new int[] { 4, 2, 1, 3, 5, 7, 6 };
			target.SortNaturalNumbers(arr, 0, arr.Length - 1);
			CollectionAssert.AreEqual(Enumerable.Range(1, 7).ToArray(), arr);

			arr = new int[] { 1 };
			target.SortNaturalNumbers(arr, 0, arr.Length - 1);
			CollectionAssert.AreEqual(Enumerable.Range(1, 1).ToArray(), arr);

			arr = new int[] { };
			target.SortNaturalNumbers(arr, 0, arr.Length - 1);
			CollectionAssert.AreEqual(new int[] { }, arr);

			arr = new int[] { 1, 2 };
			target.SortNaturalNumbers(arr, 0, arr.Length - 1);
			CollectionAssert.AreEqual(Enumerable.Range(1, 2).ToArray(), arr);
		}
	}
}

