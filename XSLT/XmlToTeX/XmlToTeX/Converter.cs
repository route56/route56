using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace XmlToTeX
{
	public class Keywords
	{
		public string XPath { get; set; }
		public string ForEach { get; set; }
	}

	public class Converter
	{
		public string Convert(string source, IDictionary<string, IEnumerable<string>> data, Keywords keyword)
		{
			StringBuilder sb = new StringBuilder();

			string[] splitSource;

			if (string.IsNullOrWhiteSpace(keyword.ForEach))
			{
				splitSource = new string[] { source };
			}
			else
			{
				splitSource = source.Split(new string[] { keyword.ForEach }, StringSplitOptions.None);
			}

			for (int i = 0; i < splitSource.Length; i++)
			{
				if (i % 2 == 0)
				{
					// Simple Add
					string[] xpathSplit = splitSource[i].Split(new string[] { keyword.XPath }, StringSplitOptions.None);

					for (int j = 0; j < xpathSplit.Length; j++)
					{
						if (j % 2 ==0)
						{
							sb.Append(xpathSplit[j]);
						}
						else
						{
							sb.Append(data[xpathSplit[j]].First());
						}
					}
				}
				else
				{
					string[] xpathSplit = splitSource[i].Split(new string[] { keyword.XPath }, StringSplitOptions.None);

					IList<IEnumerator<string>> listEnum = new List<IEnumerator<string>>();

					// Get odd ones
					for (int j = 1; j < xpathSplit.Length; j+=2)
					{
						listEnum.Add(data[xpathSplit[j]].GetEnumerator());
					}

					while (MoveNext(listEnum))
					{
						for (int j = 0; j < xpathSplit.Length; j++)
						{
							if (j % 2 == 0)
							{
								sb.Append(xpathSplit[j]);
							}
							else
							{
								sb.Append(listEnum[j / 2].Current);
							}
						}
					}
				}
			}

			return sb.ToString();
		}

		private bool MoveNext(IList<IEnumerator<string>> listEnum)
		{
			foreach (var item in listEnum)
			{
				if (item.MoveNext() == false)
				{
					return false;
				}
			}

			return true;
		}
	}
}
