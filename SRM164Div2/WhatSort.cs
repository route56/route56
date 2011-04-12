using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SRM164Div2
{
	public class WhatSort
	{
		public enum SortUsed
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
			bool isNameSorted = WhatSortUsed(nameCE, 0, nameCE.Count - 1);
			bool isAgeSorted = WhatSortUsed(ageCE, 0, ageCE.Count - 1);
			bool isWeightSorted = WhatSortUsed(wtCE, 0, wtCE.Count - 1);

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
				return GetSecondaryWithPrimary(nameCE, ageCE, wtCE).ToString();
			}

			if (isAgeSorted)
			{
				Debug.Assert(!(isWeightSorted || isNameSorted));
				return GetSecondaryWithPrimary(ageCE, nameCE, wtCE).ToString();
			}

			if (isWeightSorted)
			{
				Debug.Assert(!(isNameSorted || isAgeSorted));
				return GetSecondaryWithPrimary(wtCE, nameCE, ageCE).ToString();
			}

			Debug.Assert(false, "Unreachable code");
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

		/// <summary>
		/// Gets Secondary
		/// </summary>
		/// <param name="primary">Primary column</param>
		/// <param name="secCandidate1">Secondary candidate 1</param>
		/// <param name="secCandidate2">Secondary candidate 2</param>
		/// <returns></returns>
		public SortUsed GetSecondaryWithPrimary(List<IColumnElement> primary, List<IColumnElement> secCandidate1, List<IColumnElement> secCandidate2)
		{
			List<KeyValuePair<int, int>> ranges = FindRangesWithSameValues(primary);

			bool canSecCandidate1 = false;
			bool cansecCandidate2 = false;

			foreach (KeyValuePair<int, int> item in ranges)
			{
				bool secCandidate1Sorted = WhatSortUsed(secCandidate1, item.Key, item.Value);
				bool secCandidate2Sorted = WhatSortUsed(secCandidate2, item.Key, item.Value);

				if (secCandidate1Sorted)
				{
					if (secCandidate2Sorted)
					{
						continue;
					}
					else
					{
						if (cansecCandidate2)
						{
							return SortUsed.IND;
						}
						else
						{
							canSecCandidate1 = true;
						}
					}
				}
				else
				{
					if (secCandidate2Sorted)
					{
						if (canSecCandidate1)
						{
							return SortUsed.IND;
						}
						else
						{
							cansecCandidate2 = true;
						}
					}
					else
					{
						return SortUsed.NOT;
					}
				}
			}

			if (canSecCandidate1)
			{
				if (cansecCandidate2)
				{
					return SortUsed.IND;
				}
				else
				{
					return GetSortCode(primary, secCandidate1, secCandidate2);//SortUsed.WNA;
				}
			}
			else
			{
				if (cansecCandidate2)
				{
					return GetSortCode(primary, secCandidate2, secCandidate1);//SortUsed.WAN;
				}
				else
				{
					return SortUsed.IND; // NO ONE WAS EVER ASSIGNED, both can be candidates continue case
				}
			}
		}

		private SortUsed GetSortCode(List<IColumnElement> primary, List<IColumnElement> secCandidate1, List<IColumnElement> secCandidate2)
		{
			if (primary[0].GetType() == typeof(Age))
			{
				if (secCandidate1[0].GetType() == typeof(Name))
				{
					return SortUsed.ANW;
				}
				else
				{
					return SortUsed.AWN;
				}
			}

			if (primary[0].GetType() == typeof(Name))
			{
				if (secCandidate1[0].GetType() == typeof(Age))
				{
					return SortUsed.NAW;
				}
				else
				{
					return SortUsed.NWA;
				}
			}

			if (primary[0].GetType() == typeof(Weight))
			{
				if (secCandidate1[0].GetType() == typeof(Age))
				{
					return SortUsed.WAN;
				}
				else
				{
					return SortUsed.WNA;
				}
			}

			Debug.Assert(false, "Unreachable code");
			return SortUsed.NOT;
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
