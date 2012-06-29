using XmlToTeX;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace XmlToTex.Test
{
	[TestClass()]
	public class ConverterTest
	{
		[TestMethod()]
		public void ConvertTestXpathSingleLine()
		{
			Converter target = new Converter();
			string source = "abc$foo$def";

			IDictionary<string, IEnumerable<string>> data = new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux"} }
			};

			Keywords keys = new Keywords() { XPath = "$" };

			string expected = "abcbardef";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ConvertTestXpathMultiLine()
		{
			Converter target = new Converter();
			string source = 
@"abc
def$foo$ghi
jkl";

			IDictionary<string, IEnumerable<string>> data = new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux"} }
			};

			Keywords keys = new Keywords() { XPath = "$" };

			string expected = 
@"abc
defbarghi
jkl";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ConvertTestXpathMultipleTokens()
		{
			Converter target = new Converter();
			string source = "abc$foo$def$baz$ghi$foo$jkl";

			IDictionary<string, IEnumerable<string>> data = new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux"} }
			};

			Keywords keys = new Keywords() { XPath = "$" };

			string expected = "abcbardefquxghibarjkl";

			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ConvertTestXpathMultipleKeys()
		{
			Converter target = new Converter();
			string source = "$#foo$#";

			IDictionary<string, IEnumerable<string>> data = new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} },
				{ "baz", new string[] {"qux"} }
			};

			Keywords keys = new Keywords() { XPath = "$#" };

			string expected = "bar";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ConvertTestForEachSingleLine()
		{
			Converter target = new Converter();
			string source = "some#foo$baz$#text";

			IDictionary<string, IEnumerable<string>> data = new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux", "fox"} }
			};

			Keywords keys = new Keywords() { XPath = "$", ForEach = "#" };

			string expected = "somefooquxfoofoxtext";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ConvertTestXpathXPathInNonLoop()
		{
			Converter target = new Converter();
			string source = "a$b$c#d$e$f#h$i$";

			IDictionary<string, IEnumerable<string>> data = new Dictionary<string, IEnumerable<string>>()
			{
			    { "b", new string[] {"B"} },
				{ "e", new string[] {"E", "G"}},
				{ "i", new string[] {"I"} }
			};

			Keywords keys = new Keywords() { XPath = "$", ForEach = "#"};

			string expected = "aBcdEfdGfhI";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ConvertTestExpectsException()
		{
			Converter target = new Converter();

			// equal number of begin end.
			// equal match
			// nested looping ???? #abc#def##

			Keywords keys = new Keywords() { XPath = "$", ForEach = "#" };

			Assert.IsFalse(target.ValidateSource("a#$foo#$", keys), "$ $ should be conatined inside # #");
			Assert.IsTrue(target.ValidateSource("a#$foo$#", keys), "$ $ is conatined inside # #");

			Assert.IsFalse(target.ValidateSource("a$foo", keys), "Equal no of $");
			Assert.IsTrue(target.ValidateSource("a$foo$", keys), "Equal no of $");
			Assert.IsFalse(target.ValidateSource("a$foo$bar$aaa", keys), "Equal no of $");
			Assert.IsTrue(target.ValidateSource("a$foo$bar$aaa$aaaaa", keys), "Equal no of $");

			Assert.IsTrue(target.ValidateSource("aaa#aaaa$aaaaaa$aaaa$aa$aa#aaaa$a$##a##a$a$#", keys));
		}

		// Loop count doesn't match # $key1$ $key2$ # where key1 returns 1, key2 returns 2 items should throw exception. Count should MATCH.
	}
}
