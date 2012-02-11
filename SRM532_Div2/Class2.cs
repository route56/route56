using System;
using System.Collections.Generic;
using System.Text;

namespace SRM532_Div2
{
	public class DengklekMakingChains
	{
		enum MyEnum
		{
			Left, Mid, Right, Both, None
		}

		public int maxBeauty(string[] chains)
		{
			int left = 0;
			int right = 0;
			int mid = 0;

			List<string> bothSide = new List<string>();

			foreach (var item in chains)
			{
				int val = 0;

				switch (GetValue(item, out val))
				{
					case MyEnum.Left:
						if (val > left)
						{
							left = val;
						}
						break;
					case MyEnum.Mid:
						mid += val;
						break;
					case MyEnum.Right:
						if (val > right)
						{
							right = val;
						}
						break;
					case MyEnum.None:
						break;
					case MyEnum.Both:
						bothSide.Add(item);
						break;
					default:
						break;
				}
			}

			FindLeftRight(bothSide, ref left, ref right);

			return left + right + mid;
		}

		private void FindLeftRight(List<string> bothSide, ref int leftX, ref int rightX)
		{
			if (bothSide.Count == 0)
			{
				return;
			}

			int left = leftX;
			int right = rightX;
			int left2nd = 0;
			int right2nd = 0;
			int lIndex = 0;
			int rIndex = 0;

			for (int i = 0; i < bothSide.Count; i++)
			{
				int l = Int32.Parse(bothSide[i].ToCharArray()[2].ToString());
				int r = Int32.Parse(bothSide[i].ToCharArray()[0].ToString());

				if ( l > left2nd)
				{
					if (l > left)
					{
						left2nd = left;
						left = l;
					}
				}

			}


		}

		private MyEnum GetValue(string item, out int val)
		{
			val = 0;
			if (item == "...")
			{
				return MyEnum.None;
			}

			char[] arr = item.ToCharArray();

			if (arr[0] == '.')
			{
				if (arr[1] == '.')
				{
					val = Int32.Parse(arr[2].ToString());
				}
				else
				{
					val = Int32.Parse(arr[1].ToString()) + Int32.Parse(arr[2].ToString());
				}
				return MyEnum.Left;
			}

			if (arr[2] == '.')
			{
				if (arr[1] == '.')
				{
					val = Int32.Parse(arr[0].ToString());
				}
				else
				{
					val = Int32.Parse(arr[0].ToString()) + Int32.Parse(arr[1].ToString());
				}
				return MyEnum.Right;
			}

			if (arr[1] == '.')
			{
				return MyEnum.Both;
			}

			val = Int32.Parse(arr[0].ToString()) + Int32.Parse(arr[1].ToString()) + Int32.Parse(arr[2].ToString());

			return MyEnum.Mid;
		}
	}
}
