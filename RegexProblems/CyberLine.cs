using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RegexProblems
{
	// CyberLine (SRM 187 div 1, level 1)
	public class CyberLine
	{
		public string LastCyberword(string cyberline)
		{
			
			StringBuilder sb = new StringBuilder(cyberline);
			sb.Replace("-", "");
			string[] arr = Regex.Replace(sb.ToString(), "[^a-zA-Z0-9@]", " ").Trim().Split(' ');
			return arr[arr.Length - 1];
		}
	}
}
