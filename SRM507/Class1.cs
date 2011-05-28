using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM507
{
	public class CubeAnts
	{
		public int getMinimumSteps(int[] pos)
		{
			int max = 0;
			foreach (var item in pos)
			{
				if (max < RealValue(item))
				{
					max = RealValue(item);
				}

				if (max == 3)
				{
					return max;
				}
			}

			return max;
		}

		private int RealValue(int item)
		{
			switch (item)
			{
				case 0:
					return 0;
				case 1:
				case 3:
				case 4:
					return 1;
				case 2:
				case 5:
				case 7:
					return 2;
				case 6:
					return 3;
				default:
					throw new ArgumentException();
			}
		}
	}
}
