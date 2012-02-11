using System;
using System.Collections.Generic;
using System.Text;

namespace SRM532_Div2
{
	public class DengklekMakingChains
	{
		enum MyEnum
		{
			Left, Right, Mid, None, DotEnds, DotMid
		}

		class Pair
		{
			public int Index { get; set; }
			public int Value { get; set; }
		}

		public int maxBeauty(string[] chains)
		{
			Pair leftMax = null;
			Pair left2ndMax = null;

			Pair rightMax = null;
			Pair right2ndMax = null;

			int dotEndsMax = 0;

			int midSum = 0;

			for (int i = 0; i < chains.Length; i++)
			{
				int val = 0;

				switch (GetValue(chains[i], out val))
				{
					case MyEnum.Left:
						InsertIfMaxAnd2ndMax(ref leftMax, ref left2ndMax, val, i);
						break;
					case MyEnum.Right:
						InsertIfMaxAnd2ndMax(ref rightMax, ref right2ndMax, val, i);
						break;
					case MyEnum.Mid:
						midSum += val;
						break;
					case MyEnum.None:
						break;
					case MyEnum.DotEnds:
						if (val > dotEndsMax)
						{
							dotEndsMax = val;
						}
						break;
					case MyEnum.DotMid:
						InsertIfMaxAnd2ndMax(ref leftMax, ref left2ndMax, Int32.Parse(chains[i][2].ToString()), i);
						InsertIfMaxAnd2ndMax(ref rightMax, ref right2ndMax, Int32.Parse(chains[i][0].ToString()), i);
						break;
					default:
						break;
				}
			}

			int chain = midSum + GetAnswer(leftMax, left2ndMax, rightMax, right2ndMax);

			return GetMaxValue(chain, dotEndsMax);
		}

		private int GetAnswer(Pair leftMax, Pair left2ndMax, Pair rightMax, Pair right2ndMax)
		{
			if (leftMax == null)
			{
				if (rightMax == null)
				{
					return 0;
				}
				else
				{
					return rightMax.Value;
				}
			}
			else
			{
				if (rightMax == null)
				{
					return leftMax.Value;
				}
				else
				{
					// if same link
					if (leftMax.Index == rightMax.Index)
					{
						if (left2ndMax == null)
						{
							if (right2ndMax == null)
							{
								// 2 non nulls
								// only one link in same places
								return GetMaxValue(leftMax.Value, rightMax.Value);
							}
							else
							{
								// 3 non nulls, but 2 links
								// leftMax; rightMax; right2ndMax;
								// 2.3 1..
								// 3.1 1..
								return GetMaxValue(leftMax.Value + right2ndMax.Value, rightMax.Value);
							}
						}
						else
						{
							if (right2ndMax == null)
							{
								// 3 non nulls, but 2 links
								// leftMax; left2ndMax; rightMax;
								return GetMaxValue(leftMax.Value, left2ndMax.Value + rightMax.Value);
							}
							else
							{
								//// 4 non nulls
								//// leftMax; left2ndMax; rightMax; right2ndMax;
								return GetMaxValue(leftMax.Value + right2ndMax.Value, left2ndMax.Value + rightMax.Value);
							}
						}
					}
					else
					{
						return leftMax.Value + rightMax.Value;
					}
				}
			}
		}

		private int GetMaxValue(int one, int two)
		{
			if (one > two)
			{
				return one;
			}
			else
			{
				return two;
			}
		}

		private void InsertIfMaxAnd2ndMax(ref Pair pairMax, ref Pair pair2ndMax, int val, int i)
		{
			if (pairMax == null)
			{
				pairMax = new Pair() { Index = i, Value = val };
				return;
			}

			if (val > pairMax.Value)
			{
				if (pair2ndMax == null)
				{
					pair2ndMax = pairMax;
					pairMax = new Pair() { Index = i, Value = val };
				}
				else
				{
					pair2ndMax.Index = pairMax.Index;
					pair2ndMax.Value = pairMax.Value;

					pairMax.Index = i;
					pairMax.Value = val;
				}
			}
			else
			{
				if (pair2ndMax == null)
				{
					pair2ndMax = new Pair() { Index = i, Value = val };
				}
				else
				{
					if (val > pair2ndMax.Value)
					{
						pair2ndMax.Index = i;
						pair2ndMax.Value = val;
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
					// ..1
					val = Int32.Parse(arr[2].ToString());
				}
				else
				{
					if (arr[2] == '.')
					{
						// .1.
						val = Int32.Parse(arr[1].ToString());
						return MyEnum.DotEnds;
					}
					else
					{
						// .11
						val = Int32.Parse(arr[1].ToString()) + Int32.Parse(arr[2].ToString());
					}
				}
				return MyEnum.Left;
			}

			// 1XX
			if (arr[1] == '.')
			{
				if (arr[2] == '.')
				{
					// 1..
					val = Int32.Parse(arr[0].ToString());
					return MyEnum.Right;
				}
				else
				{
					// 1.1
					return MyEnum.DotMid;
				}
			}
			else
			{
				if (arr[2] == '.')
				{
					// 11.
					val = Int32.Parse(arr[0].ToString()) + Int32.Parse(arr[1].ToString());
					return MyEnum.Right;
				}
				else
				{
					// 111
					val = Int32.Parse(arr[0].ToString()) + Int32.Parse(arr[1].ToString()) + Int32.Parse(arr[2].ToString());
					return MyEnum.Mid;
				}
			}
		}
	}
}
