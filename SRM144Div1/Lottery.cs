using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SRM144Div1
{
	public class Lottery
	{
		public class LotteryRule: IComparable<LotteryRule>
		{
			public String Name { get; set; }
			public double Choices { get; set; }

			public double Blanks { get; set; }

			public bool Sorted { get; set; }

			public bool Unique { get; set; }

			public double LotteryCount { get; set; }

			internal double GetLotteryCount()
			{
				double result = 0;
				if (Sorted)
				{
					if (Unique)
					{
						result = AlgoSortedAndUnique();
					}
					else
					{
						result = AlgoSortedAndNotUnique();
					}
				}
				else
				{
					if (Unique)
					{
						result = AlgoUnSortedAndUnique();
					}
					else
					{
						result = AlgoUnSortedAndNotUnique();
					}
				}

				return result;
			}

			private double AlgoUnSortedAndNotUnique()
			{
				// 10, 2 => 10*10
				// 10, 3 => 10*10*10
				return Math.Pow(Choices, Blanks);
			}

			private double AlgoUnSortedAndUnique()
			{
				//10*9
				// 10, 3 => 10*9*8
				//result = Factorial(Choices) / Factorial(Blanks);
				double result = 1;

				Debug.Assert(Choices >= Blanks);
				for (double i = Choices; i > Choices - Blanks; i--)
				{
					result *= i;
				}

				return result;
			}

			private double AlgoSortedAndNotUnique()
			{
				// 10+9+...1
				// (10+9+...1)*10+...
				// Use DP A(n,r) = Sigma A(i, r-1) i = 1-> n : Recurrence relation. Generating fn?
				double result = 0;
				double[,] cached = InitializeCache();

				result = GeneratingFunction(cached, (long)Choices, (long)Blanks, false);

				return result;
			}

			private double[,] InitializeCache()
			{
				double[,] cached = new double[(long)Choices + 1, (long)Blanks + 1];

				for (int i = 0; i < Choices; i++)
				{
					for (int j = 0; j < Blanks; j++)
					{
						cached[i, j] = 0;
					}
				}
				return cached;
			}

			private double GeneratingFunction(double[,] cached, long nn, long rr, bool isUnique)
			{
				double result = 0;

				if (cached[nn, rr] != 0)
				{
					result = cached[nn, rr];
				}
				else
				{
					if (rr == 1)
					{
						result = nn;
					}
					else
					{
						long upperLimit = isUnique ? nn - 1: nn;

						for (long i = 1; i <= upperLimit; i++)
						{
							result += GeneratingFunction(cached, i, rr - 1, isUnique);
						}
					}

					cached[nn, rr] = result;
				}

				return result;
			}

			private double AlgoSortedAndUnique()
			{
				// 9+8+...1
				// Use DP A(n,r) = Sigma A(i, r-1) i = 1 -> n- 1 : Recurrence relation. Generating fn?
				double result = 0;
				double[,] cached = InitializeCache();

				result = GeneratingFunction(cached, (long)Choices, (long)Blanks, true);

				return result;
			}

			public int CompareTo(LotteryRule other)
			{
				double dblResult = this.LotteryCount - other.LotteryCount;
				if (dblResult == 0) // to break the tie
				{
					dblResult = this.Name.CompareTo(other.Name);
				}

				// dblResult NEEDED as int overflow is caused for high values leading to wrong sort order!
				int result = 0;

				if (dblResult > 0)
					result = 1;
				else if (dblResult == 0)
					result = 0;
				else
					result = -1;

				return result;
			}
		}


		public string[] sortByOdds(string[] rules)
		{
			List<LotteryRule> ruleList = new List<LotteryRule>(rules.Length);

			TransformInput(rules, ruleList);

			foreach (LotteryRule item in ruleList)
			{
				item.LotteryCount = item.GetLotteryCount();
			}

			ruleList.Sort();

			string[] results = new string[ruleList.Count];

			int i = 0;
			foreach (LotteryRule item in ruleList)
			{
				results[i] = item.Name;
				i++;
			}

			return results;
		}

		private static void TransformInput(string[] rules, List<LotteryRule> ruleList)
		{
			foreach (string item in rules)
			{
				string[] str = item.Split(':');
				Debug.Assert(str.Length == 2);
				string[] data = str[1].Trim().Split(' ');
				Debug.Assert(data.Length == 4);

				LotteryRule rule = new LotteryRule()
				{
					Name = str[0],
					Choices = Double.Parse(data[0]),
					Blanks = Double.Parse(data[1]),
					Sorted = data[2].CompareTo("T") == 0,
					Unique = data[3].CompareTo("T") == 0
				};

				ruleList.Add(rule);
			}
		}
	}
}
