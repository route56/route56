using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCJSolver
{
	public class Program
	{
		public static void Main(string[] args)
		{
			string input = Console.ReadLine();
			Console.WriteLine(ProblemSpecificUnit(input));
			//switch (input)
			//{
			//    case "Hi":
			//        Console.WriteLine("Hello world");
			//        break;
			//    default:
			//        Console.WriteLine("Bye world");
			//        break;
			//}
		}

		public static string ProblemSpecificUnit(string input)
		{
			switch (input)
			{
				case "Hi":
					return "Hello world";
				default:
					return "Bye world";
			}
		}
	}
}
