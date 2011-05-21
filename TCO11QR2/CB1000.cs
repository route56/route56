using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TCO11QR2
{
	class CB1000
	{
		public long count(long min, long max)
		{
			return solve(max) - solve(min - 1);
		}

		private long solve(long max)
		{
			long ret = 0;
			for (int msk = 1; msk < (1<<9); msk++)
			{
				//long product = 1;
			}
			return ret;
		}
	}
}
