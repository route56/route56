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

		public interface IColumnElement
		{
			bool IsGreaterThan(IColumnElement other);
			bool IsEqualTo(IColumnElement other);
			bool IsLessThan(IColumnElement other);
		}

		class Name : IColumnElement
		{
			public string data;

			public bool IsGreaterThan(IColumnElement other)
			{
				Name otherAsName = other as Name;

				return this.data.CompareTo(otherAsName.data) > 0;
			}

			public bool IsEqualTo(IColumnElement other)
			{
				Name otherAsName = other as Name;

				return this.data.CompareTo(otherAsName.data) == 0;
			}

			public bool IsLessThan(IColumnElement other)
			{
				Name otherAsName = other as Name;

				return this.data.CompareTo(otherAsName.data) < 0;
			}
		}

		class Age : IColumnElement
		{
			public int data;

			public bool IsGreaterThan(IColumnElement other)
			{
				Age otherAsName = other as Age;

				return this.data > otherAsName.data;
			}

			public bool IsEqualTo(IColumnElement other)
			{
				Age otherAsName = other as Age;

				return this.data == otherAsName.data;
			}

			public bool IsLessThan(IColumnElement other)
			{
				Age otherAsName = other as Age;

				return this.data < otherAsName.data;
			}
		}

		class Weight : IColumnElement
		{
			public int data;

			public bool IsGreaterThan(IColumnElement other)
			{
				Weight otherAsName = other as Weight;

				return this.data < otherAsName.data; // Weight sorted in descending order
			}

			public bool IsEqualTo(IColumnElement other)
			{
				Weight otherAsName = other as Weight;

				return this.data == otherAsName.data;
			}

			public bool IsLessThan(IColumnElement other)
			{
				Weight otherAsName = other as Weight;

				return this.data > otherAsName.data; // Weight sorted in descending order
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name">Assend</param>
		/// <param name="age">Assend</param>
		/// <param name="wt">Descend</param>
		/// <returns></returns>
		public string sortType(string[] name, int[] age, int[] wt)
		{
			List<IColumnElement> nameCE = new List<IColumnElement>();
			List<IColumnElement> ageCE = new List<IColumnElement>();
			List<IColumnElement> wtCE = new List<IColumnElement>();

			TransformInput(name, age, wt, nameCE, ageCE, wtCE);

			// GetPrimary
			bool isNameSorted = WhatSortUsed(nameCE, 0, nameCE.Length - 1);
			bool isAgeSorted = WhatSortUsed(ageCE, 0, age.Length - 1);
			bool isWeightSorted = WhatSortUsed(wtCE, 0, wt.Length - 1);

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

			return null;
		}

		public void TransformInput(string[] name, int[] age, int[] wt, List<IColumnElement> nameCE, List<IColumnElement> ageCE, List<IColumnElement> wtCE)
		{
			//TransformInput
			foreach (string item in name)
			{
				nameCE.Add(new Name() { data = item });
			}

			foreach (int item in age)
			{
				ageCE.Add(new Age() { data = item });
			}

			foreach (int item in wt)
			{
				wtCE.Add(new Weight() { data = item });
			}
		}

		public SortUsed GetSecondaryWithWeightAsPrimary(List<IColumnElement> wt, List<IColumnElement> name, List<IColumnElement> age)
		{
			List<KeyValuePair<int, int>> ranges = FindRangesWithSameValues(age);

			bool canNameBeSecKey = false;
			bool canAgeBeSecKey = false;

			foreach (KeyValuePair<int, int> item in ranges)
			{
				bool nameSorted = WhatSortUsed(name, item.Key, item.Value);
				bool ageSorted = WhatSortUsed(wt, item.Key, item.Value);

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

		public SortUsed GetSecondaryWithAgeAsPrimary(List<IColumnElement> age, List<IColumnElement> name, List<IColumnElement> wt)
		{
			List<KeyValuePair<int, int>> ranges = FindRangesWithSameValues(age);

			bool canNameBeSecKey = false;
			bool canWtBeSecKey = false;

			foreach (KeyValuePair<int, int> item in ranges)
			{
				bool nameSorted = WhatSortUsed(name, item.Key, item.Value);
				bool wtSorted = WhatSortUsed(wt, item.Key, item.Value);

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

		public SortUsed GetSecondaryWithNameAsPrimary(List<IColumnElement> name, List<IColumnElement> age, List<IColumnElement> wt)
		{
			List<KeyValuePair<int,int>> ranges = FindRangesWithSameValues(name);

			bool canAgeBeSecKey = false;
			bool canWtBeSecKey = false;

			foreach (KeyValuePair<int,int> item in ranges)
			{
				bool ageSorted = WhatSortUsed(age, item.Key, item.Value);
				bool wtSorted = WhatSortUsed(wt, item.Key, item.Value);

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

		public List<KeyValuePair<int, int>> FindRangesWithSameValues(List<IColumnElement> array)
		{
			List<KeyValuePair<int, int>> result = new List<KeyValuePair<int, int>>();

			IColumnElement currentName = array[0];
			int key = -1;
			for (int i = 1; i < array.Count; i++)
			{
				if (currentName.IsEqualTo(array[i]))
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
				result.Add(new KeyValuePair<int, int>(key, array.Count - 1));
			}

			return result;
		}

		/// <summary>
		/// Sorted?
		/// </summary>
		/// <param name="array"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		/// <returns></returns>
		public bool WhatSortUsed(List<IColumnElement> array, int start, int end)
		{
			bool result = true;

			for (int i = start; i < end; i++)
			{
				if (array[i].IsGreaterThan(array[i + 1]))
				{
					result = false;
					break;
				}
			}

			return result;
		}
	}
}
