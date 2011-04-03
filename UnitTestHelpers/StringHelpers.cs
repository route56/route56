using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UnitTestHelpers
{
	public class StringHelpers
	{
		public static bool AreEqualStringsArrays(string[] expected, string[] actual)
		{
			if (expected.Length != actual.Length)
			{
				return false;
			}

			int i = 0;
			foreach (string s in expected)
			{
				if (s.CompareTo(actual[i]) != 0)
				{
					return false;
				}
				i++;
			}

			return true;
		}
	}
}
