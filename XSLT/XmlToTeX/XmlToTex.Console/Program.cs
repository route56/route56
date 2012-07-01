using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using XmlToTeX;
using System.IO;

namespace XmlToTex.Console
{
	class Program
	{
		static void Main(string[] args)
		{
			if (args.Length != 3)
			{
				PrintUsage();
				return;
			}

			string source = args[0];
			string xmlData = args[1];
			string dest = args[2];

			if (!File.Exists(source))
			{
				System.Console.WriteLine("Template file not found " + source);
				PrintUsage();
				return;
			}

			// TODO validate xml against Final.xsd
			if (!File.Exists(xmlData))
			{
				System.Console.WriteLine("Xml data file not found " + xmlData);
				PrintUsage();
				return;
			}

			try
			{
				Converter target = new Converter();
				target.Convert(source, dest, xmlData);
			}
			catch (Exception ex)
			{
				System.Console.WriteLine(ex.ToString());
			}
		}

		private static void PrintUsage()
		{
			System.Console.WriteLine("Usage: XmlToTex.exe Template.tex Data.xml Target.tex");
		}
	}
}
