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
			if (pd == 0 && pg == 0)
			{
				return true;
			}

			long div = gcd(pg, 100);
			long dcoeff = pd - pg;

			if (dcoeff == 0)
			{
				return true;
			}

			long d2 = gcd(div, dcoeff);

			if (div % dcoeff != 0)
			{
				if (div/d2 > n)
				{
					return false;
				}
			}

			return true;
			//long d = div / d2;

			// pg*oldg - 100*oldgwin = (pd - pg)d
			// oldg >= oldgiwn
			// solve for pg*oldg - 100*oldgwin = gcd

			//long? G = null;
			//long? g = null;

			//for (long oldG = 1; oldG < long.MaxValue && !G.HasValue; oldG++) // bad idea!
			//{
			//    for (long oldgwin = 0; oldgwin <= oldG; oldgwin++)
			//    {
			//        long isOne = (pg / div) * oldG - (100 / div) * oldgwin;

			//        if (isOne == 1)
			//        {
			//            G = oldG;
			//            g = oldgwin;

			//            break;
			//        }
			//    }
			//}

			//if (!G.HasValue) //unreachable late code... if true
			//{
			//    return false;
			//}

			//for (long k = 0; k < long.MaxValue; k++)
			//{
			//    long x = G.Value - k * 100 / div;
			//    long y = g.Value - k * pg / div;

			//    long xx = G.Value + k * 100 / div;
			//    long yy = g.Value + k * pg / div;

			//    long c = pg * x - 100 * y;  //// pg*oldg - 100*oldgwin = (pd - pg)d
			//    long cc = pg * xx - 100 * yy;  //// pg*oldg - 100*oldgwin = (pd - pg)d

			//    if (c == d*dcoeff || cc == d*dcoeff)
			//    {
			//        return true;
			//    }
			//    else if (Math.Abs(c) > Math.Abs(d * dcoeff) && Math.Abs(cc) > Math.Abs(d * dcoeff))
			//    {
			//        return false;
			//    }
			//}

			return false;
		}

		//9,223,372,036,854,775,807
		public static long gcd(long a, long b) // zero case not handled
		{
			if (b == 0)
			{
				return Math.Abs(a);
			}
			else
			{
				return (gcd(b, a % b));
			}
		}
	}
	
}

