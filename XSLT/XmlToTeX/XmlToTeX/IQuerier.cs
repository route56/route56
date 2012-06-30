using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace XmlToTeX
{
	public interface IQuerier
	{
		IEnumerable<string> GetData(string key);
	}
}
