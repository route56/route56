using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM503Div2
{
	public class ToastXRaspberry
	{
		int apply(int upper_limit, int layer_count)
		{
			if (layer_count % upper_limit == 0)
				return layer_count / upper_limit;
			else
			{
				return layer_count / upper_limit + 1;
			}
		}
	}
}
