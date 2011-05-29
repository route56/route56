using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SRM507
{
	public class CubeRoll
	{
		public int getMinimumSteps(int initPos, int goalPos, int[] holePos)
		{
			Array.Sort(holePos);

			int dist = Math.Abs(goalPos - initPos);

			if (dist == 0)
			{
				return 0;
			}

			if (IsAnyHoleBetween(holePos, initPos, goalPos))
				return -1;

			// dist = a^2 +/- b^2 +/- c^2...
			throw new NotImplementedException();
			
		}

		public bool IsAnyHoleBetween(int[] holePos, int x, int y)
		{
			Debug.Assert(x != y);
			if (Array.BinarySearch(holePos, x) == Array.BinarySearch(holePos, y))
			{
				return false;
			}
			else
			{
				return true;
			}
		}
		// This is also correct, but above one is simplified refactored truth!
		//{
		//    int xx = Array.BinarySearch(holePos, Math.Min(x,y));
		//    int yy = Array.BinarySearch(holePos, Math.Max(x,y));

		//    // source or dest is itself a hole
		//    if (xx > 0 || yy > 0)
		//    {
		//        //return true;
		//    }

		//    xx = ~xx;
		//    yy = ~yy;

		//    // no element larger than x
		//    if (xx >= holePos.Length)
		//    {
		//        //return false;
		//    }

		//    // no element smaller than y
		//    if (yy == 0)
		//    {
		//        //return false;
		//    }

		//    if (xx == yy)
		//    {
		//        return false;
		//    }
		//    else
		//    {
		//        return true;
		//    }
		//}
	}
}
