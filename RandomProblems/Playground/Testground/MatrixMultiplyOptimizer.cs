using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading;

namespace Testground
{
	class MatrixMultiplyOptimizer
	{
		internal long Solve(int[] dimentionChain)
		{
			_dimentionChain = dimentionChain;
			InitMemory(_dimentionChain.Length);

			return OptimalMultiply(0, _dimentionChain.Length);
		}

		private void InitMemory(int length)
		{
			_memory = new long[length, length];
			for (int i = 0; i < _memory.GetLength(0); i++)
			{
				for (int j = 0; j < _memory.GetLength(1); j++)
				{
					_memory[i, j] = -1;
				}
			}
		}

		private long[,] _memory;
		private int[] _dimentionChain;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="start">inclusive start</param>
		/// <param name="end">exclusive end</param>
		/// <returns></returns>
		private long OptimalMultiply(int start, int end)
		{
			if (start + 1 >= end)
			{
				throw new ArgumentException();
			}

			if (_memory[start, end-1] == -1)
			{
				if (start + 2 == end)
				{
					_memory[start, end-1] = 0;
				}
				else if(start + 3 == end)
				{
					_memory[start, end - 1] = _dimentionChain[start] * _dimentionChain[start + 1] * _dimentionChain[start + 2];
				}
				else
				{
					long min = long.MaxValue;

					for (int k = start + 1; k < end - 1; k++)
					{
						long sum =
							OptimalMultiply(start, k + 1)
							+ OptimalMultiply(k, end)
							+ _dimentionChain[start] * _dimentionChain[k] * _dimentionChain[end-1];

						if (min > sum)
						{
							min = sum;
						}
					}

					_memory[start, end-1] = min;
				}
			}

			return _memory[start, end-1];
		}
	}

	[TestClass]
	public class OptimalMatrixTest
	{
		[TestMethod]
		public void BasicTest()
		{
			int[] dimentionChain = { 10, 100, 5, 50 };

			var target = new MatrixMultiplyOptimizer();

			Assert.AreEqual(7500, target.Solve(dimentionChain));
		}

		[TestMethod]
		public void CLRExampleTest()
		{
			int[] dimentionChain = { 30, 35, 15, 5, 10, 20, 25 };

			var target = new MatrixMultiplyOptimizer();

			Assert.AreEqual(15125, target.Solve(dimentionChain));
		}
	}

	class Paranthesis
	{
		// paranthesis
		// XXXYYY, XYXXYY
		// (((AB)C)D)
		// (AB)((CD))
		// ABC _A_B_C_
		// XXYY ((AB)C)
		// A(BC) XXYY (A(BC))
		// (AB)C XYXY (AB)(C)
		// ABCD XXXYYY => (A(B(CD)))
		// ABCD XYXYXY => (AB)(CD)() ??

		// Tree travarsal. Full tree
		// Root node first, then left, then right
		// ABC => A B(left) c (right)
		// ABCD ? A B(l) C(l) D(r)

		// XXYXYY
		// X X AB Y X CD Y Y

		internal static long GetCatalanNumber(int n)
		{
			if (n >= catalanNumber.Length || n < 0)
			{
				throw new ArgumentException(" 0 <= n < " + catalanNumber.Length.ToString());
			}

			return catalanNumber[n];
		}

		internal static long GetCatalanNumberLength()
		{
			return catalanNumber.Length;
		}

		private static long[] catalanNumber = new long[]
		{
			1,1,2,5,14,42,132,429,1430,4862,16796,58786,
			 208012,742900,2674440,9694845,35357670,129644790,
			 477638700,1767263190,6564120420,24466267020,
			 91482563640,343059613650,1289904147324,
			 4861946401452,18367353072152,69533550916004,
			 263747951750360,1002242216651368,3814986502092304
		};

		internal List<string> FactorParanthesis(string factors)
		{
			return Kaay(factors);
		}

		// P(1) = 1;
		// P(n) = Sig(k = 1, n - 1, P(k)*P(n-k))
		private List<string> Kaay(string input)
		{
			List<string> bigList = new List<string>();
			

			if (input.Length == 1)
			{
				bigList.Add(input);
			}
			else if (input.Length == 2)
			{
				bigList.Add("(" + input + ")");
			}
			else
			{
				for (int k = 0; k < input.Length - 1; k++)
				{
					string first = input.Substring(0, k + 1);
					string second = input.Substring(k + 1, input.Length - k - 1);

					bigList.AddRange(CrossProduct<string>(Kaay(first), Kaay(second), (l, r) => "(" + l + r + ")"));
				}
			}

			return bigList;
		}

		private IEnumerable<T> CrossProduct<T>(IEnumerable<T> left, IEnumerable<T> right, Func<T, T, T> combine)
		{
			foreach (var lt in left)
			{
				foreach (var rt in right)
				{
					yield return combine(lt, rt);
				}
			}
		}
	}

	[TestClass]
	public class FactorsParanthesisTest
	{
		[TestMethod]
		public void FactorsParanthesis()
		{
			Paranthesis target = new Paranthesis();

			string factors = "ABC";
			string[] expected = 
			{
				"((AB)C)",
				"(A(BC))"
			};

			TestFactorParanthesis(target, factors, expected);

			factors = "ABCD";
			expected = new string[]
			{
				"(((AB)C)D)",
				"((A(BC))D)",
				"(A(B(CD)))",
				"((AB)(CD))",
				"(A((BC)D))"
			};

			TestFactorParanthesis(target, factors, expected);
		}

		private static void TestFactorParanthesis(Paranthesis target, string factors, string[] expected)
		{
			var actual = target.FactorParanthesis(factors);

			Assert.AreEqual(expected.Count(), actual.Count());

			foreach (var item in expected)
			{
				Assert.IsTrue(actual.Contains(item));
			}
		}

		[TestMethod]
		public void CatalanCountTest()
		{
			Paranthesis target = new Paranthesis();
			StringBuilder sb = new StringBuilder();

			//Thread thread = new Thread(
			//    () =>
				{
					for (int i = 0; i < 14; i++)
					{
						sb.Append('A');
						var watch = new System.Diagnostics.Stopwatch();
						watch.Start();
						Assert.AreEqual(Paranthesis.GetCatalanNumber(i), target.FactorParanthesis(sb.ToString()).Count);
						watch.Stop();
						Console.WriteLine("i = {0} watch = {1}", i, watch.Elapsed.ToString());
					}
				}
			//);

			//thread.Start();
			//Thread.Sleep(10000);
			//thread.Abort();
		}
	}
}
