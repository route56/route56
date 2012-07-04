using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;


namespace Testground
{
	public class IntScanProblems
	{
		public int MaxCircularSum(int[] input)
		{
			if (input.Length == 0)
			{
				return 0;
			}

			int maxSumTillNow = input[0];
			int maxIndex = 0;
			int tailIndex = 0;
			int tailSum = maxSumTillNow;

			for (int i = 1; i < input.Length * 2; i++)
			{
				Debug.WriteLine("{0} {1} {2} {3} {4}", i, tailSum, tailIndex, maxSumTillNow, maxIndex);
				if (tailSum < 0)
				{
					tailSum = 0;
					tailIndex = i;
				}

				if (i >= input.Length + tailIndex)
				{
					break;
				}

				tailSum += input[i % input.Length];

				if(maxSumTillNow < tailSum)
				{
					maxSumTillNow = tailSum;
					maxIndex = tailIndex;
				}

				Debug.WriteLine("{0} {1} {2} {3} {4}", i, tailSum, tailIndex, maxSumTillNow, maxIndex);
			}

			return maxSumTillNow;
		}
	}

	[TestClass()]
	public class IntScanProblemsTest
	{
		//public IEnumerable<IEnumerable<T>> GetPowerSet<T>(IList<T> list)
		//{
		//    foreach (var m in Enumerable.Range(0, 1 << list.Count))
		//    {
		//        yield return Enumerable.Range(0, list.Count).Where(i => (m & (1 << i)) != 0).Select(i => list[i]);
		//    }
		//}

		private int WorstAlgo(int[] input)
		{
			int max = int.MinValue;

			for (int i = 0; i < input.Length; i++)
			{
				int sum = 0;

				for (int j = 1; j < input.Length; j++)
				{
					sum += input[(i + j) % input.Length];

					max = Math.Max(sum, max);
				}
			}

			return max;
		}

		private int[] GetRandomDataSet(Random rand)
		{
			int rsize =   100;
			int rmin = -1000;
			int rmax =  1000;

			int size = rand.Next(rsize);

			int[] res = new int[size];

			for (int i = 0; i < res.Length; i++)
			{
				res[i] = rand.Next(rmin, rmax);
			}

			return res;
		}

		[TestMethod()]
		public void MaxCircularSumTest()
		{
			IntScanProblems target = new IntScanProblems();
			int[] input = { 8, -8, 9, -9, 10, -11, 12 };
			int expected = 22; // WorstAlgo(input); 
			int actual;
			actual = target.MaxCircularSum(input);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod]
		public void FailedTestCases()
		{
			//TestFor(new int[] { 148, 267, 475, -44, 813, -937, -326, 281, 499, -713, -415, 121, 912, 134, -252, 686, 729 });
			TestFor(new int[] { 100, -10, 100 });
		}

		private void TestFor(int[] input)
		{
			IntScanProblems target = new IntScanProblems();
			int expected = WorstAlgo(input); 
			int actual;
			actual = target.MaxCircularSum(input);
			Assert.AreEqual(expected, actual);
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
			IntScanProblems target = new IntScanProblems();
			int[] input = GetRandomDataSet(rand);
			int expected = WorstAlgo(input); 
			int actual;
			actual = target.MaxCircularSum(input);
			Assert.AreEqual(expected, actual);
		}
	}
}
