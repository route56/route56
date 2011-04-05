using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM144Div1
{
	public class BinaryCode
	{
		public string[] decode(string message)
		{
			/*
			 * org[i] => [-1, n]
			 * msg => [0, n-1]
			 * org[-1] = 0.
			 * verify org[n] = 0
			 * org[i] = m [i - 1] - (org[i - 1] org [ i -2])
			 * assume org[0] = 0 and org[0] 1 for two cases
			 * */

			int[] org = new int[message.Length + 1];
			int[] org2 = new int[message.Length + 1];

			org[0] = 0;
			org2[0] = 1;

			DecodeMessage(message, org);
			DecodeMessage(message, org2);

			return new string[] { ValidateAndGetResult(org), ValidateAndGetResult(org2) };
		}

		private static void DecodeMessage(string message, int[] org)
		{
			org[1] = Int32.Parse(message[0].ToString()) - org[0];

			for (int i = 2; i < message.Length + 1; i++)
			{
				org[i] = Int32.Parse(message[i - 1].ToString()) - org[i - 1] - org[i - 2];
			}
		}

		private string ValidateAndGetResult(int[] org)
		{
			StringBuilder one = new StringBuilder(org.Length - 1);
			String none = "NONE";

			if (org[org.Length - 1] != 0)
			{
				one.Append(none);
			}
			else
			{
				for (int i = 0; i < org.Length - 1; i++)
				{
					if (org[i] != 0 && org[i] != 1)
					{
						one.Clear();
						one.Append(none);
						break;
					}

					one.Append(org[i].ToString());
				}
			}

			return one.ToString();
		}
	}
}
