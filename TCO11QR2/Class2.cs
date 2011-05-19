using System;
using System.Collections.Generic;
using System.Text;

namespace TCO11QR2
{
	public class FoxIntegerLevelThree
	{
		public long count(long min, long max)
		{
			long found = 0;
			for (long num = min; num <= max; num++)
			{
				for (int y = 1; y < 10; y++)
				{
					if (num % y == 0)
					{
						if (Dfn(num/y) == y)
						{
							found++;
							break;
						}
					}
				}
			}

			return found;
		}

		private int Dfn(long p)
		{
			string s = p.ToString();

			if (s.Length == 1)
			{
				return (int)p;
			}

			long num = 0;
			foreach (var ch in s.ToCharArray())
			{
				num += Int32.Parse(ch.ToString());
			}

			return Dfn(num);
		}
	}
}
