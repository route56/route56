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
			for (int i = 0; i < Int32.Parse(input); i++)
			{
				string ip = Console.ReadLine();
				string[] arr = ip.Split();
				Console.WriteLine("Case #{0}: {1}", i+1, IsFreeCellStatCorrect(Int32.Parse(arr[0]), Int32.Parse(arr[1]), Int32.Parse(arr[2])) ? "Possible" : "Broken");
			}
		}

		public static bool IsFreeCellStatCorrect(long n, long pd, long pg)
		{
			// pg*oldg - 100*oldgwin = (pd - pg)d
			long a = pg;
			long b = -100;
			long c1 = pd - pg;

			long gcdab = GCD(a, b);
			long aa = b / gcdab;
			long bb = - a / gcdab;

			//if (c1 == 0)
			//{
			//    return true; // really?
			//}

			//// c1*c2 % gcdab should be 0
			//if (c1 % gcdab != 0)
			//{
			//    //???
			//}

			//ax+by=gcdab solve
			// x = (gcdab-by) % a == 0

			long x1 = 0, y1 = 0;

			for (long y = 0; y < long.MaxValue; y++)
			{
				long ax = (gcdab - b * y);
				if (ax % a == 0)
				{
					x1 = ax / a;
					y1 = y;
					break;
				}
			}

			for (long k = 0; k < long.MaxValue; k++)
			{
				long cplus = a * (x1 + k * aa) + b * (y1 + k * bb);
				long cminus = a * (x1 - k * aa) + b * (y1 - k * bb);

				if ( IsThisTheOne(n, c1, cplus) || IsThisTheOne(n, c1, cminus))
				{
					return true;
				}
				else if (HasCrossedPointOfNoReturn(n, c1, cplus) && HasCrossedPointOfNoReturn(n, c1, cminus))
				{
					return false;
				}
			}

			return false;
		}

		private static bool HasCrossedPointOfNoReturn(long n, long c1, long cplus)
		{
			return Math.Abs(c1 * n) < Math.Abs(cplus);
		}

		private static bool IsThisTheOne(long n, long c1, long cplus)
		{
			return (cplus % c1 == 0 && cplus / c1 > 0 && cplus / c1 <= n);
		}

		//9,223,372,036,854,775,807
		public static long GCD(long a, long b) // zero case not handled
		{
			if (b == 0)
			{
				return Math.Abs(a);
			}
			else
			{
				return (GCD(b, a % b));
			}
		}
	}
	
}

