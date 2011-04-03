using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SRM164Div2
{
	public class WhatSort
	{

		private enum SortUsed
		{
			/// <summary>
			/// name was the primary, age secondary, weight the final field
			/// </summary>
			NAW, // -- 
			/// <summary>
			/// name was the primary, weight secondary, age final
			/// </summary>
			NWA, // -- 
			/// <summary>
			/// age was primary, name was secondary, weight final
			/// </summary>
			ANW, // -- 
			/// <summary>
			/// age was primary, weight was secondary, name final
			/// </summary>
			AWN, // -- 
			/// <summary>
			/// weight primary, age secondary, name final
			/// </summary>
			WAN, // -- 
			/// <summary>
			/// weight primary, name secondary, age final
			/// </summary>
			WNA, // -- 
			/// <summary>
			/// indeterminate: more than one of the above is possible
			/// </summary>
			IND, // -- 
			/// <summary>
			/// none of the above sorting methods was used
			/// </summary>
			NOT 
		};

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">Assend</param>
		/// <param name="age">Assend</param>
		/// <param name="wt">Descend</param>
		/// <returns></returns>
		public string sortType(string[] name, int[] age, int[] wt)
		{
			// GetPrimary
			bool isNameSorted = WhatSortUsed(name, true, 0, name.Length - 1);
			bool isAgeSorted = WhatSortUsed(age, true, 0, age.Length - 1);
			bool isWeightSorted = WhatSortUsed(wt, false, 0, wt.Length - 1);

			// If nothing is sorted bail
			if (!(isNameSorted || isAgeSorted || isWeightSorted))
			{
				return SortUsed.NOT.ToString();
			}

			if ((isNameSorted && (isAgeSorted  || isWeightSorted))
			|| (isAgeSorted && (isWeightSorted || isNameSorted))
			|| (isWeightSorted && (isNameSorted  || isAgeSorted)))
			{
				return SortUsed.IND.ToString();
			}

			if (isNameSorted)
			{
				Debug.Assert(!(isAgeSorted || isWeightSorted));
				//GetSecondary
				return GetSecondaryWithNameAsPrimary(name, age, wt).ToString();
			}

			if (isAgeSorted)
			{
				Debug.Assert(!(isWeightSorted || isNameSorted));
				return GetSecondaryWithAgeAsPrimary(age, name, wt).ToString();
			}

			if (isWeightSorted)
			{
				Debug.Assert(!(isNameSorted || isAgeSorted));
				return GetSecondaryWithWeightAsPrimary(wt, name, age).ToString();
			}

			throw new NotImplementedException();
		}

		private SortUsed GetSecondaryWithWeightAsPrimary(int[] wt, string[] name, int[] age)
		{
			List<KeyValuePair<int, int>> ranges = FindRangesWithSameValues(age);

			bool canNameBeSecKey = false;
			bool canAgeBeSecKey = false;

			foreach (KeyValuePair<int, int> item in ranges)
			{
				bool nameSorted = WhatSortUsed(name, true, item.Key, item.Value);
				bool ageSorted = WhatSortUsed(wt, true, item.Key, item.Value);

				if (nameSorted)
				{
					if (ageSorted)
					{
						continue;
					}
					else
					{
						if (canAgeBeSecKey)
						{
							return SortUsed.IND;
						}
						else
						{
							canNameBeSecKey = true;
						}
					}
				}
				else
				{
					if (ageSorted)
					{
						if (canNameBeSecKey)
						{
							return SortUsed.IND;
						}
						else
						{
							canAgeBeSecKey = true;
						}
					}
					else
					{
						return SortUsed.NOT;
					}
				}
			}

			if (canNameBeSecKey)
			{
				if (canAgeBeSecKey)
				{
					return SortUsed.IND;
				}
				else
				{
					return SortUsed.WNA;
				}
			}
			else
			{
				if (canAgeBeSecKey)
				{
					return SortUsed.WAN;
				}
				else
				{
					return SortUsed.NOT;
				}
			}
		}

		private SortUsed GetSecondaryWithAgeAsPrimary(int[] age, string[] name, int[] wt)
		{
			List<KeyValuePair<int, int>> ranges = FindRangesWithSameValues(age);

			bool canNameBeSecKey = false;
			bool canWtBeSecKey = false;

			foreach (KeyValuePair<int, int> item in ranges)
			{
				bool nameSorted = WhatSortUsed(name, true, item.Key, item.Value);
				bool wtSorted = WhatSortUsed(wt, false, item.Key, item.Value);

				if (nameSorted)
				{
					if (wtSorted)
					{
						continue;
					}
					else
					{
						if (canWtBeSecKey)
						{
							return SortUsed.IND;
						}
						else
						{
							canNameBeSecKey = true;
						}
					}
				}
				else
				{
					if (wtSorted)
					{
						if (canNameBeSecKey)
						{
							return SortUsed.IND;
						}
						else
						{
							canWtBeSecKey = true;
						}
					}
					else
					{
						return SortUsed.NOT;
					}
				}
			}
			
			if (canNameBeSecKey)
			{
				if (canWtBeSecKey)
				{
					return SortUsed.IND;
				}
				else
				{
					return SortUsed.ANW;
				}
			}
			else
			{
				if (canWtBeSecKey)
				{
					return SortUsed.AWN;
				}
				else
				{
					return SortUsed.NOT;
				}
			}
		}

		private SortUsed GetSecondaryWithNameAsPrimary(string[] name, int[] age, int[] wt)
		{
			List<KeyValuePair<int,int>> ranges = FindRangesWithSameValues(name);

			bool canAgeBeSecKey = false;
			bool canWtBeSecKey = false;

			foreach (KeyValuePair<int,int> item in ranges)
			{
				bool ageSorted = WhatSortUsed(age, true, item.Key, item.Value);
				bool wtSorted = WhatSortUsed(wt, false, item.Key, item.Value);

				if (ageSorted)
				{
					if (wtSorted)
					{
						continue;
					}
					else
					{
						if (canWtBeSecKey)
						{
							return SortUsed.IND;
						}
						else
						{
							canAgeBeSecKey = true;
						}
					}
				}
				else
				{
					if (wtSorted)
					{
						if (canAgeBeSecKey)
						{
							return SortUsed.IND;
						}
						else
						{
							canWtBeSecKey = true;
						}
					}
					else
					{
						return SortUsed.NOT;
					}
				}
			}

			if (canAgeBeSecKey)
			{
				if (canWtBeSecKey)
				{
					return SortUsed.IND;
				}
				else
				{
					return SortUsed.NAW;
				}
			}
			else
			{
				if (canWtBeSecKey)
				{
					return SortUsed.NWA;
				}
				else
				{
					return SortUsed.IND;
				}
			}
		}

		public List<KeyValuePair<int, int>> FindRangesWithSameValues(int[] array)
		{
			List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();

			int currentName = array[0];
			int key = -1;
			for (int i = 1; i < array.Length; i++)
			{
				if (currentName == array[i])
				{
					if (key == -1)
					{
						key = i - 1;
					}
				}
				else
				{
					currentName = array[i];
					if (key != -1)
					{
						result.Add(new KeyValuePair<int, int>(key, i - 1));
						key = -1;
					}
				}
			}

			// Logic to include last value if any
			if (key != -1)
			{
				result.Add(new KeyValuePair<int, int>(key, array.Length - 1));
			}

			return result;
		}

		public List<KeyValuePair<int, int>> FindRangesWithSameValues(string[] name)
		{
			List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();

			string currentName = name[0];
			int key = -1;
			for (int i = 1; i < name.Length; i++)
			{
				if (currentName.CompareTo(name[i]) == 0)
				{
					if (key == -1)
					{
						key = i - 1;
					}
				}
				else
				{
					currentName = name[i];
					if (key != -1)
					{
						result.Add(new KeyValuePair<int, int>(key, i - 1));
						key = -1;
					}
				}
			}

			// Logic to include last value if any
			if (key != -1)
			{
				result.Add(new KeyValuePair<int, int>(key, name.Length - 1));
			}

			return result;
		}

		/// <summary>
		/// Generic
		/// </summary>
		/// <param name="array"></param>
		/// <param name="isAscending"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		private bool WhatSortUsed(int[] array, bool isAscending, int start, int end)
		{
			bool result = true;

			for (int i = start; i < end; i++)
			{
				if ( (isAscending && array[i] > array[i + 1])
					|| (!isAscending && array[i] < array[i + 1]))
				{
					result = false;
					break;
				}
			}

			return result;
		}

		/// <summary>
		/// Generic
		/// </summary>
		/// <param name="array"></param>
		/// <param name="isAscending"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		private bool WhatSortUsed(string[] array, bool isAscending, int start, int end)
		{
			bool result = true;

			for (int i = start; i < end; i++)
			{
				if ((isAscending && array[i].CompareTo(array[i + 1]) > 0)
					|| (!isAscending && array[i].CompareTo(array[i + 1]) < 0))
				{
					result = false;
					break;
				}
			}

			return result;
		}

	}
}
