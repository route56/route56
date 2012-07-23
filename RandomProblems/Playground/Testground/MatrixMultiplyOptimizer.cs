using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	class MatrixMultiplyOptimizer
	{
		internal long Solve(int[] dimentionChain)
		{
			throw new NotImplementedException();
		}

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
	public class OptimizerTest
	{
		[TestMethod]
		public void BasicTest()
		{
			int[] dimentionChain = { 10, 100, 5, 50 };

			MatrixMultiplyOptimizer target = new MatrixMultiplyOptimizer();

			Assert.AreEqual(7500, target.Solve(dimentionChain));
		}

		[TestMethod]
		public void FactorsParanthesis()
		{
			MatrixMultiplyOptimizer target = new MatrixMultiplyOptimizer();

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

		private static void TestFactorParanthesis(MatrixMultiplyOptimizer target, string factors, string[] expected)
		{
			var actual = target.FactorParanthesis(factors);

			Assert.AreEqual(expected.Count(), actual.Count());

			foreach (var item in expected)
			{
				Assert.IsTrue(actual.Contains(item));
			}
		}
	}
}
