using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlToTeX;

namespace XmlToTex.Test
{
	class StubQuerier : IQuerier
	{
		private Dictionary<string, IEnumerable<string>> _data;

		public StubQuerier(Dictionary<string, IEnumerable<string>> data)
		{
			_data = data;
		}

		public IEnumerable<string> GetData(string key)
		{
			return _data[key];
		}
	}
}
