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
			int d = 1; 

			while (pd * d % 100 != 0) 
				++d;

			return d > n || pg == 0 && pd > 0 || pg == 100 && pd < 100 ? false : true;
		}

		public static bool IsFreeCellStatCorrectIncomplete(long n, long pd, long pg)
		{
			// pg*oldg - 100*oldgwin = (pd - pg)d
			long a = pg;
			long b = -100;
			long c1 = pd - pg;

			long gcdab = GCD(a, b);

			long window = Math.Abs(b / gcdab);

			// window = 10, every 10 pts it comes.
			// n = 100. sure its coming
			// n = 1, maybe maybe not.
			if (n <= window)
			{
				// huh tricky?
				// k*gcd = c1*(1-n)
				//1-n = k*gcd/c1.

				// 1- 10 = 2*12/3 | 8 true
				// 1-10 = 4*12/3
				// 1-3 = 12/3 = 4
				if (n <= gcdab / c1)
				{
					return false;
				}
				else
				{
					return true;
				}


				// gcd/c1??? n*c1/gcd = kmax.
				// c1/gcd = kmin

				//gcdab % c1 == 0 && gcdab / c1 > 0 && gcdab / c1 <= n
			}
			else
			{
				return true;
			}

			//c1*c2 % gcdab == 0
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

//#include <algorithm>  
//#include <iostream>  
//#include <sstream>  
//#include <string>  
//#include <vector>  
//#include <queue>  
//#include <set>  
//#include <map>  
//#include <cstdio>  
//#include <cstdlib>  
//#include <cctype>  
//#include <cmath>  
//#include <list>  
//using namespace std;  

//#define PB push_back  
//#define MP make_pair  
//#define SZ(v) ((int)(v).size())  
//#define FOR(i,a,b) for(int i=(a);i<(b);++i)  
//#define REP(i,n) FOR(i,0,n)  
//#define FORE(i,a,b) for(int i=(a);i<=(b);++i)  
//#define REPE(i,n) FORE(i,0,n)  
//#define FORSZ(i,a,v) FOR(i,a,SZ(v))  
//#define REPSZ(i,v) REP(i,SZ(v))  
//typedef long long ll;  

//void run(int casenr) {
//    ll n; int pd,pg; scanf("%lld%d%d",&n,&pd,&pg);
//    int d=1; while(pd*d%100!=0) ++d;
//    printf("Case #%d: %s\n",casenr,d>n||pg==0&&pd>0||pg==100&&pd<100?"Broken":"Possible");
//}

//int main() {
//    int n; scanf("%d",&n); FORE(i,1,n) run(i);
//    return 0;
//}

 
