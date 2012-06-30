using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace XmlToTeX
{
	class XmlQuerier : IQuerier
	{
		public XmlQuerier(string xmlFilePath)
		{
			doc.Load(xmlFilePath);
		}

		public IEnumerable<string> GetData(string key)
		{
			XmlNodeList nodes = doc.SelectNodes(key);

			foreach (XmlNode item in nodes)
			{
				yield return item.InnerText;
			}
		}

		private XmlDocument doc = new XmlDocument();
	}
}
