using XmlToTeX;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.IO;
using System.Diagnostics;

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

			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux"} }
			});

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

			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux"} }
			});

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

			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux"} }
			});

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

			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} },
				{ "baz", new string[] {"qux"} }
			});

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

			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
			    { "foo", new string[] {"bar"} }, 
				{ "baz", new string[] {"qux", "fox"} }
			});

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

			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
			    { "b", new string[] {"B"} },
				{ "e", new string[] {"E", "G"}},
				{ "i", new string[] {"I"} }
			});

			Keywords keys = new Keywords() { XPath = "$", ForEach = "#"};

			string expected = "aBcdEfdGfhI";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ConvertTestLatexEscapeCharacters()
		{
			Converter target = new Converter();
			string source = "$1$ $2$ $3$ $4$ $5$ $6$ $7$ $8$ $9$ $10$";

			// http://tex.stackexchange.com/questions/34580/escape-character-in-latex
			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
				{ "1", new string[] {"&"} },
				{ "2", new string[] {"%"} },
				{ "3", new string[] {"$"} },
				{ "4", new string[] {"#"} },
				{ "5", new string[] {"_"} },
				{ "6", new string[] {"{"} },
				{ "7", new string[] {"}"} },
				{ "8", new string[] {"~"} },
				{ "9", new string[] {"^"} },
				{ "10", new string[] {"\\"} },
			});

			Keywords keys = new Keywords() { XPath = "$", ForEach = "#" };

			string expected = "\\& \\% \\$ \\# \\_ \\{ \\} \\textasciitilde \\textasciicircum \\textbackslash";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		// TODO [TestMethod()]
		public void ConvertTestLoopWithinLoop()
		{
			Converter target = new Converter();
			string source = "a #1 b $c$ e #2 f $g$ h #2 i #1 j";

			IQuerier data = new StubQuerier(new Dictionary<string, IEnumerable<string>>()
			{
				{ "c", new string[] {"c1", "c2"} },
				{ "g", new string[] {"g1", "g2", "g3"} },
				{ "gc1", new string[] {"x1", "x2", "x3"} },
				{ "gc2", new string[] {"y1", "y2"} },
			});

			Keywords keys = new Keywords() { XPath = "$", ForEach = "#" };

			string expected = "a  b c1 e  f x1 h f x2 h f x3 h i  b c2 e  f y1 h f y2 h i j";
			string actual;
			actual = target.Convert(source, data, keys);
			Assert.AreEqual(expected, actual);
		}

		[TestMethod()]
		public void ValidateSourceTest()
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

		[TestMethod()]
		public void ConvertTest_Integrated()
		{
			Converter target = new Converter();

			string source = @"C:\Git\TestBed\Template.tex";
			string dest = @"C:\Git\TestBed\Actual" + Guid.NewGuid().ToString() + ".tex";
			//string expected = @"C:\Git\TestBed\Expected.tex";
			string xmlData = @"C:\Git\TestBed\Data.xml";

			target.Convert(source, dest, xmlData);

			Process process = new Process()
			{
				StartInfo = new ProcessStartInfo(dest)
			};

			process.Start();

			//Assert.AreEqual(File.ReadAllText(expected), File.ReadAllText(dest));
			// test for $ at beginning of line. Multiline?
			// how will u handle repetetion foreach pattern?
			// Redirection/cross ref via xpath inside xml
		}

		// Loop count doesn't match # $key1$ $key2$ # where key1 returns 1, key2 returns 2 items should throw exception. Count should MATCH.
	}
}
