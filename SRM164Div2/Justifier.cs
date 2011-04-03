using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM164Div2
{
	public class Justifier
	{
		public string[] justify(string[] textIn)
		{
			int maxLength = 0;
			foreach (string  s in textIn)
			{
				if (s.Length > maxLength)
				{
					maxLength = s.Length;
				}
			}

			List<string> strList = new List<string>();
			foreach (string s in textIn)
			{
				strList.Add(s.PadLeft(maxLength));
			}
			return strList.ToArray();
		}
	}
}
