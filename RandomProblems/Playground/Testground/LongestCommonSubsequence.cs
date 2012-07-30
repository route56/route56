using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Testground
{
	class LongestCommonSubsequence
	{
		internal string Solve(string leftSequence, string rightSequence)
		{
			if (leftSequence.Length == 0 || rightSequence.Length == 0)
			{
				return string.Empty;
			}

			// http://stackoverflow.com/questions/2877660/composite-key-dictionary
			var compositKey = new Tuple<string, string>(leftSequence, rightSequence);

			if (_cache.ContainsKey(compositKey) == false)
			{
				var lcs = new StringBuilder();
				var xm = leftSequence.Last();
				var ym = rightSequence.Last();

				if (xm == ym)
				{
					lcs.Append(Solve(_OneDown(leftSequence), _OneDown(rightSequence)));
					lcs.Append(xm);
				}
				else
				{
					var oneDownLeft = Solve(_OneDown(leftSequence), rightSequence);
					var oneDownRight = Solve(leftSequence, _OneDown(rightSequence));

					if (oneDownLeft.Length > oneDownRight.Length)
					{
						lcs.Append(oneDownLeft);
					}
					else
					{
						lcs.Append(oneDownRight);
					}
				}

				_cache.Add(compositKey, lcs.ToString());
			}

			return _cache[compositKey];
		}

		private string _OneDown(string input)
		{
			return input.Substring(0, input.Length - 1);
		}

		private Dictionary<Tuple<string, string>, string> _cache = new Dictionary<Tuple<string, string>, string>();

	}

	[TestClass]
	public class LCSTest
	{
		[TestMethod]
		public void BasicCase()
		{
			string leftSequence = "abcdefgh";
			string rightSequence = "12ab3def456h";

			string expected = "abdefh";

			var target = new LongestCommonSubsequence();

			string actualLCS = target.Solve(leftSequence, rightSequence);

			Assert.AreEqual(expected, actualLCS);
		}

		[TestMethod]
		public void CLRExample()
		{
			string leftSequence =	"ACCGGTCGAGTGCGCGGAAGCCGGCCGAA";
			//						"    GTCG  T CG  GAAGCCGGCCGAA"
			string rightSequence =	"GTCGTTCGGAATGCCGTTGCTCTGTAAA";
			//						"GTCGT CGGAA GCCG  GC C G AA "

			string expected =		"GTCGTCGGAAGCCGGCCGAA";

			var target = new LongestCommonSubsequence();

			string actualLCS = target.Solve(leftSequence, rightSequence);

			Assert.AreEqual(expected, actualLCS);
		}
	}
}
