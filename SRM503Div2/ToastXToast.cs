using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM503Div2
{
	class ToastXToast
	{
		public int bake(int[] undertoasted, int[] overtoasted)
		{
			int result = -1;

			List<int> un = new List<int>(undertoasted);
			List<int> ov = new List<int>(overtoasted);

			un.Sort();
			ov.Sort();

			if (un[un.Count - 1] > ov[ov.Count -1])
			{
				result = -1;
			}
			else if (un[un.Count - 1] < ov[0])
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
