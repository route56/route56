using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace QR2011
{
	public class Magicka : ProblemHelper.IGCJSolution
	{
		public void Solve(string InputFile, string ActualOutputFile)
		{
			using (System.IO.StreamReader rd = new System.IO.StreamReader(InputFile))
			using (System.IO.StreamWriter wr = new System.IO.StreamWriter(ActualOutputFile))
			{
				int dataItems = Int32.Parse(rd.ReadLine());

				for (int i = 0; i < dataItems; i++)
				{
					string[] currentLine = rd.ReadLine().Split();
					List<string> combineRules = new List<string>();
					List<string> opposedRules = new List<string>();
					string invokeSeqStr = null;

					//"1 QRI 0 4 RRQR"
					int index = 0;
					int cRuleCount = Int32.Parse(currentLine[index++]);
					
					for (int j = 0; j < cRuleCount; j++)
					{
						combineRules.Add(currentLine[index++]);
					}

					int dRuleCount = Int32.Parse(currentLine[index++]);
					for (int j = 0; j < dRuleCount; j++)
					{
						opposedRules.Add(currentLine[index++]);
					}

					Debug.Assert(index + 2 == currentLine.Length);
					
					invokeSeqStr = currentLine[currentLine.Length - 1];

					Debug.Assert(invokeSeqStr.Length == Int32.Parse(currentLine[index]));

					string answer = this.RunAlgo(combineRules.ToArray(), opposedRules.ToArray(), invokeSeqStr);

					//"Case #2: [R, I, R]"
					StringBuilder result = new StringBuilder();

					result.Append("[");

					for (int j = 0; j < answer.Length; j++)
					{
						result.Append(answer[j]);

						if (j + 1 != answer.Length) // seperator logic
						{
							result.Append(", ");
						}
					}

					result.Append("]");

					wr.WriteLine("Case #{0}: {1}", i + 1, result.ToString());
				}

				rd.Close();
				wr.Close();
			}
		}

		public string RunAlgo(string[] combineRules, string[] opposedRules, string invokeSeqStr)
		{
			char[] invokeSeq = invokeSeqStr.ToCharArray();
			char[] retSeq = new char[invokeSeq.Length];

			int j = -1;

			Dictionary<string, char> combine = GetCombine(combineRules);
			Dictionary<string, bool> oppose = GetOppose(opposedRules);

			for (int i = 0; i < invokeSeq.Length; i++)
			{
				if (j == -1)
				{
					retSeq[++j] = invokeSeq[i];
					continue;
				}

				char? nonBase = null;
				nonBase = CanCombine(combine, retSeq[j], invokeSeq[i]);

				if (nonBase.HasValue)
				{
					retSeq[j] = nonBase.Value; // overwrite current one.
				}
				else
				{
					if (CanDestroySeq(oppose, retSeq, j+1, invokeSeq[i]))
					{
						j = -1;
					}
					else
					{
						retSeq[++j] = invokeSeq[i];
					}
				}
			}

			return new string(retSeq, 0, j + 1);//(j == -1)? "" : new string(retSeq, 0, j+1);
		}

		private char? CanCombine(Dictionary<string, char> combine, char left, char right)
		{
			char? ret = null;

			string lr = new string(new char[] { left, right });
			string rl = new string(new char[] { right, left });//(new char[] { right, left }).ToString();

			if (combine.ContainsKey(lr))
			{
				ret = combine[lr];
			}
			else if(combine.ContainsKey(rl))
			{
				ret = combine[rl];
			}

			return ret;
		}

		private bool CanDestroySeq(Dictionary<string, bool> oppose, char[] retSeq, int j, char right)
		{
			bool result = false;

			for (int i = 0; i < j; i++)
			{
				if (CanDestroy(oppose, retSeq[i], right))
				{
					result = true;
					break;
				}
			}

			return result;
		}

		private bool CanDestroy(Dictionary<string, bool> oppose, char left, char right)
		{
			string lr = new string(new char[] { left, right });
			string rl = new string(new char[] { right, left });

			if (oppose.ContainsKey(lr) || oppose.ContainsKey(rl))
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private Dictionary<string, char> GetCombine(string[] combineRules)
		{
			Dictionary<string, char> dict = new Dictionary<string, char>();

			foreach (string rule in combineRules)
			{
				dict.Add(new string(rule.ToCharArray(), 0, 2), rule[2]);
			}

			return dict;
		}

		private Dictionary<string, bool> GetOppose(string[] opposedRules)
		{
			Dictionary<string, bool> dict = new Dictionary<string, bool>();

			foreach (string rule in opposedRules)
			{
				dict.Add(new string(rule.ToCharArray(), 0, 2), true);
			}

			return dict;
		}

	}
}
