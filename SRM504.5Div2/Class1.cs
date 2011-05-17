using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SRM504._5Div2
{
	public class TheJackpotDivTwo
	{
		public int[] InEfficientfind(int[] money, int jackpot)
		{
			Array.Sort(money);

			while (jackpot > 0)
			{
				money[0]++;
				Array.Sort(money);
				jackpot--;
			}

			return money;
		}

		public int[] find(int[] money, int jackpot)
		{
			Array.Sort(money);

			if (money.Length == 1)
			{
				money[0] += jackpot;
				return money;
			}

			int sameValues = 0;

			while (jackpot > 0 && sameValues < money.Length - 1)
			{
				if (money[sameValues + 1] > money[sameValues])
				{
					if (jackpot >= GetDelta(money, sameValues))
					{
						jackpot -= GetDelta(money, sameValues);

						for (int i = 0; i <= sameValues; i++)
						{
							money[i] = money[sameValues + 1];
						}

						sameValues++;
					}
					else
					{
						break;
					}
				}
				else
				{
					sameValues++;
				}
			}

			if (jackpot > 0)
			{
				//money[0] += jackpot;
				int each = jackpot / (sameValues + 1);
				for (int i = 0; i <= sameValues; i++)
				{
					money[i] += each;
				}

				jackpot -= each * (sameValues + 1);
				money[sameValues] += jackpot;
			}

			return money;
			
		}

		private static int GetDelta(int[] money, int sameValues)
		{
			return (sameValues + 1) * (money[sameValues + 1] - money[sameValues]);
		}

	}
}
