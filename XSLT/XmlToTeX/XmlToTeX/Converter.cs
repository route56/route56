using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace XmlToTeX
{
	public class Keywords
	{
		public string XPath { get; set; }
		public string ForEach { get; set; }
	}

	public class Converter
	{
		public bool ValidateSource(string source, Keywords keyword)
		{
			string[] splitSource = GetSplitByForEach(source, keyword);

			for (int i = 0; i < splitSource.Length; i++)
			{
				if (splitSource[i].Split(new string[] { keyword.XPath }, StringSplitOptions.None).Length % 2 == 0)
				{
					return false;
				}
			}

			return true;
		}

		public string Convert(string source, IQuerier data, Keywords keyword)
		{
			if (ValidateSource(source, keyword) == false)
			{
				throw new ArgumentException("Invalid source");
			}

			StringBuilder sb = new StringBuilder();

			string[] splitSource = GetSplitByForEach(source, keyword);

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
							sb.Append(EscapeLaTeX(data.GetData(xpathSplit[j]).First()));
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
						listEnum.Add(data.GetData(xpathSplit[j]).GetEnumerator());
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
								sb.Append(EscapeLaTeX(listEnum[j / 2].Current));
							}
						}
					}

					foreach (var enumerators in listEnum)
					{
						enumerators.Dispose();
					}
				}
			}

			return sb.ToString();
		}

		private static StringBuilder EscapeLaTeX(string source)
		{
			StringBuilder result = new StringBuilder(source);

			for (int i = 0; i < escapeMapping.GetLength(0); i++)
			{
				result = result.Replace(escapeMapping[i,0], escapeMapping[i,1]);
			}

			return result;
		}

		private static string[] GetSplitByForEach(string source, Keywords keyword)
		{
			string[] splitSource;
			if (string.IsNullOrWhiteSpace(keyword.ForEach))
			{
				splitSource = new string[] { source };
			}
			else
			{
				splitSource = source.Split(new string[] { keyword.ForEach }, StringSplitOptions.None);
			}
			return splitSource;
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

		public void Convert(string source, string dest, string xmlData)
		{
			Keywords keywords = null;

			string sourceText = null;

			using (StreamReader sr = File.OpenText(source))
			{
				string readLine1 = sr.ReadLine();

				if (string.IsNullOrEmpty(readLine1))
				{
					return;
				}

				string readLine2 = sr.ReadLine();

				if (string.IsNullOrEmpty(readLine2))
				{
					return;
				}

				keywords = new Keywords();

				string xpathEscape = "%XPATH";
				string foreachEscape = "%FOREACH";

				if (readLine1.StartsWith(xpathEscape, StringComparison.OrdinalIgnoreCase))
				{
					keywords.XPath = readLine1.Substring(xpathEscape.Length);
				}

				if (readLine2.StartsWith(foreachEscape, StringComparison.OrdinalIgnoreCase))
				{
					keywords.ForEach = readLine2.Substring(foreachEscape.Length);
				}

				if (string.IsNullOrEmpty(keywords.XPath) && string.IsNullOrEmpty(keywords.ForEach))
				{
					return;
				}

				sourceText = sr.ReadToEnd();
			}

			File.WriteAllText(dest, Convert(sourceText, new XmlQuerier(xmlData), keywords));

			//var node = doc.SelectSingleNode(split[i]);
			//sb.Append(node.InnerText);

		}

		// Ordering of below items in table matter!
		private static string[,] escapeMapping = {
												 {"\\", "\\textbackslash"},
												 {"&", "\\&"},
												 {"%", "\\%"},
												 {"$", "\\$"},
												 {"#", "\\#"},
												 {"_", "\\_"},
												 {"{", "\\{"},
												 {"}", "\\}"},
												 {"~", "\\textasciitilde"},
												 {"^", "\\textasciicircum"},
												 };
	}
}

