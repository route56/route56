using System;
using System.Collections.Generic;
using System.Text;

namespace SRM532_Div2
{
	public class DengklekTryingToSleep
	{
		public int minDucks(int[] ducks)
		{
			Array.Sort(ducks);
			int ans = 0;
			int A = ducks[0];
			for (int i = 1; i < ducks.Length; i++)
			{
				ans += (ducks[i] - A - 1);
				A = ducks[i];
			}

			return ans;
		}
	}
}
