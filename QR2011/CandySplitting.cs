using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace QR2011
{
	public class CandySplitting : ProblemHelper.IGCJSolution
	{
		public int RunEffificentAlgo(int[] candyArr)
		{
			Array.Sort(candyArr);
			return RunWithBounds(candyArr, 0, candyArr.Length - 1);
		}

		/// <summary>
		/// BOUNDS are inclusive
		/// </summary>
		/// <param name="candyArr"></param>
		/// <param name="low"></param>
		/// <param name="high"></param>
		/// <returns></returns>
		private int RunWithBounds(int[] candyArr, int low, int high)
		{
			if (low >= high) // equality ok?
			{
				return 0;
			}
			else if (high - low == 1) // 2 elements
			{
				if (candyArr[low] == candyArr[high])
				{
					return candyArr[low];
				}
				else
				{
					return 0;
				}
			}

			int xor;
			int sum;
			ComputeXorAndSum(candyArr, low, high, out xor, out sum);

			if (xor == 0)
			{
				return sum - candyArr[low]; // give lowest element to cry baby and rest for big bro
			}
			else
			{
				return 0; // really this simple??
			}
			//{
			//    // binary search xor in array.
			//    int index = Array.BinarySearch(candyArr, low, high - low + 1, xor);

			//    if (index < low)
			//    {
			//        index = ~index;

			//        //int nextLow;
			//        //int nextHigh;

			//        int left = RunWithBounds(candyArr, low, index);
			//        int right = RunWithBounds(candyArr, index + 1, high);

			//        if (left != 0 && right != 0)
			//        {
			//            return (left + right);
			//        }
			//        else
			//        {
			//            return 0;
			//        }

			//        //if (index == low)
			//        //{
			//        //    nextLow
			//        //}
			//        //else if (index == high + 1)
			//        //{
						
			//        //}
			//        //if (index == 0)
			//        //    Console.Write("beginning of array and ");
			//        //else
			//        //    Console.Write("{0} and ", candyArr[index - 1]);

			//        //if (index == candyArr.Length)
			//        //    Console.WriteLine("end of array.");
			//        //else
			//        //    Console.WriteLine("{0}.", candyArr[index]);
			//    }
			//    else
			//    {
			//        //found!
			//        return 0;// TODO Really 0??//(sum - candyArr[index]); // give everything to big bro and xor to cry baby
			//    }
			//}
		}

		private void ComputeXorAndSum(int[] candyArr, int low, int high, out int xor, out int sum)
		{
			xor = 0;
			sum = 0;
			for (int i = low; i <= high; i++)
			{
				xor ^= candyArr[i];
				sum += candyArr[i];
			}
		}

		/// <summary>
		/// This is to verify efficient algo works by testing for small values :)
		/// </summary>
		public int RunIneffificentAlgo(int[] candyArr)
		{
			/* for each set in powerset
			 * compute xor of s and it's complement
			 * if xor match, find the set with max sum. Remember the sum [this can be answer]
			 * if this sum is > global max sum, set global max sum to this value
			 * return global max (0 => no such possible)
			 */

			List<int> candies = new List<int>(candyArr);
			List<int> index = new List<int>(candies.Count);

			for (int i = 0; i < candies.Count; i++)
			{
				index.Add(i);
			}

			var powerSet = (new PowerSet()).GetPowerSet<int>(index);
			int maxSum = 0; // our answer
			foreach (var subset in powerSet)
			{
				if (subset.Count() == 0 || subset.Count() == index.Count)
				{
					continue;
				}

				var complement = GetComplement(subset, index);

				int xorSub = 0;
				int sumSub = 0;
				foreach (var pos in subset)
				{
					xorSub ^= candies[pos];
					sumSub += candies[pos];
				}

				int xorComp = 0;
				int sumComp = 0;
				foreach (var pos in complement)
				{
					xorComp ^= candies[pos];
					sumComp += candies[pos];
				}

				if (xorSub == xorComp)
				{
					int max = Math.Max(sumSub, sumComp);
					maxSum = Math.Max(maxSum, max);
				}
			}

			return maxSum;
		}

		private IEnumerable<int> GetComplement(IEnumerable<int> subset, List<int> index)
		{
			List<int> complement = new List<int>();

			// assume poweset doesn't change order
			int j = 0;
			foreach (var ele in index)
			{
				if (j >= subset.Count() || ele != subset.ElementAt(j))
				{
					complement.Add(ele);
				}
				else
				{
					j++;
				}
			}

			return complement;
		}

		public void Solve(string InputFile, string ActualOutputFile)
		{
			/*
2
5
1 2 3 4 5
3
3 5 6
			 */
			using (System.IO.StreamReader rd = new System.IO.StreamReader(InputFile))
			using (System.IO.StreamWriter wr = new System.IO.StreamWriter(ActualOutputFile))
			{
				int dataItems = Int32.Parse(rd.ReadLine());

				for (int i = 0; i < dataItems; i++)
				{
					int arrCount = Int32.Parse(rd.ReadLine());
					int[] arr = new int[arrCount];
					string[] strarr = rd.ReadLine().Split();
					Debug.Assert(strarr.Length == arrCount);

					for (int j = 0; j < strarr.Length; j++)
					{
						arr[j] = Int32.Parse(strarr[j]);
					}

					int result = this.RunEffificentAlgo(arr);

					/*
Case #1: NO
Case #2: 11
					 */
					string ans;
					if (result == 0)
					{
						ans = "NO";
					}
					else
					{
						ans = result.ToString();
					}
					wr.WriteLine("Case #{0}: {1}", i + 1, ans);
				}

				rd.Close();
				wr.Close();
			}

		}
	}

	class PowerSet
	{
		public IEnumerable<IEnumerable<T>> GetPowerSet<T>(List<T> list)
		{
			// http://rosettacode.org/wiki/Power_set#C.23
			return from m in Enumerable.Range(0, 1 << list.Count)
				   select
					   from i in Enumerable.Range(0, list.Count)
					   where (m & (1 << i)) != 0
					   select list[i];
		}
	}
}
