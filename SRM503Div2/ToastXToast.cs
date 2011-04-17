using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM503Div2
{
	class ToastXToast
	{
		public int bake(int[] un, int[] ov)
		{
			int result = -1;

			Array.Sort(un);
			Array.Sort(ov);

			if (un[un.Length - 1] > ov[ov.Length - 1] || un[0] > ov[0])
			{
				result = -1;
			}
			else if (un[un.Length - 1] < ov[0])
			{
				result = 1;
			}
			else
			{
				result = 2;
			}
			
			return result;
		}
	}
}
