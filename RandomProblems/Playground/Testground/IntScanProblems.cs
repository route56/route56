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
}
