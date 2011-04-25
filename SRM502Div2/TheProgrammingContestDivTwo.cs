using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SRM502Div2
{
	public class TheProgrammingContestDivTwo
	{
		public int[] find(int T, int[] requiredTime)
		{
			Array.Sort(requiredTime);

			for (int i = 1; i < requiredTime.Length; i++)
			{
				requiredTime[i] += requiredTime[i - 1];
			}

			int[] result = { 0, 0 };

			if (T < requiredTime[0])
			{
				return result;
			}
			else if (T >= requiredTime[requiredTime.Length - 1])
			{
				result[0] = requiredTime.Length;
				for (int i = 0; i < requiredTime.Length; i++)
				{
					result[1] += requiredTime[i];
				}
				return result;
			}

			int? found = null;
			for (int i = 0; i < requiredTime.Length; i++)
			{
				if (requiredTime[i] > T)
				{
					found = i - 1;
					break;
				}
			}

			Debug.Assert(found.HasValue, "should have value as other checks already done");

			result[0] = found.Value + 1;

			for (int i = 0; i <= found.Value; i++)
			{
				result[1] += requiredTime[i];
			}

			return result;
		}
	}
}
