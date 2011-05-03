using System;
using System.Collections.Generic;
//using System.Linq;
using System.Text;

//namespace SRM505Div2
//{
	public class SentenceCapitalizerInator
	{
		public string fixCaps(string paragraph)
		{
			StringBuilder sb = new StringBuilder(paragraph);
			bool makeCap = true;
			for (int i = 0; i < paragraph.Length; i++)
			{
				if (makeCap && sb[i] != '.' && sb[i] != ' ')
				{
					sb[i] = sb[i].ToString().ToUpper()[0];
					makeCap = false;
				}

				if (sb[i] == '.')
				{
					makeCap = true;
				}
			}

			return sb.ToString();
		}
	}
//}
