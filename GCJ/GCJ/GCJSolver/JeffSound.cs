using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace GCJSolver
{
	public class JeffSound
	{
		public static void Main(string[] args)
		{
//2
//3 2 100
//3 5 7
//4 8 16
//1 20 5 2
				int caser = Int32.Parse(Console.ReadLine());
				TextWriter tw = new StreamWriter(args[0]);
				for (int i = 0; i < caser; i++)
				{
					long res = new JeffSound().DoIt();

					if (res == -1)
					{
						Console.WriteLine("Case #{0}: NO", i + 1);
						tw.WriteLine("Case #{0}: NO", i + 1);
					}
					else
					{
						Console.WriteLine("Case #{0}: {1}", i + 1, res);
						tw.WriteLine("Case #{0}: {1}", i + 1, res);
					}
				}
				tw.Close();
		}

		private long DoIt1()
		{
			string[] num = Console.ReadLine().Split();
			long N = long.Parse(num[0]);
			long L = long.Parse(num[1]);
			long H = long.Parse(num[2]);

			string[] strfreq = Console.ReadLine().Split();
			long[] freq = new long[N];

			for (int i = 0; i < N; i++)
			{
				freq[i] = long.Parse(strfreq[i]);
			}

			long gcd = freq[0];
			long prod = freq[0];
			for (int i = 1; i < N; i++)
			{
				gcd = GCD(gcd, freq[i]);
				prod *= freq[i];
			}

			long lcm = prod / gcd;

			if (gcd >= L && gcd <= H)
			{
				return gcd;
			}
			else if (lcm >= L && lcm <= H)
			{
				return lcm;
			}
			else if(lcm < L)
			{
				long lcmLow = L / lcm;
				long lcmHigh = H / lcm;

				long ans = -1;
				for (long move = lcmLow; move <= lcmHigh; move++)
				{
					if (move*lcm >= L && move*lcm <= H)
					{
						ans = move * lcm;
					}
				}

				return ans;
			}
			else
			{
				return -1;
			}
		}

		private long DoIt()
		{
			string[] num = Console.ReadLine().Split();
			long N = long.Parse(num[0]);
			long L = long.Parse(num[1]);
			long H = long.Parse(num[2]);

			string[] strfreq = Console.ReadLine().Split();
			long[] freq = new long[N];

			for (int i = 0; i < N; i++)
			{
				freq[i] = long.Parse(strfreq[i]);
			}

			Array.Sort(freq);

			long gcdLow = freq[0];
			long lcmLow = freq[0];

			long gcdHigh = freq[N-1];
			long lcmHigh = freq[N-1];
			int? indexL = null;

			for (int i = 1; i < N-1; i++)
			{
				if (freq[i] <= L)
				{
					gcdLow = GCD(gcdLow, freq[i]);
					lcmLow = (freq[i] / gcdLow) * lcmLow;
				}
				else
				{
					if (!indexL.HasValue)
					{
						indexL = i;
					}

					gcdHigh = GCD(gcdHigh, freq[i]);
					lcmHigh = (freq[i] / gcdHigh) * lcmHigh;
				}
			}

			if (IsDivisible(lcmLow, gcdHigh))
			{
				long a = GetInRange(lcmLow, gcdHigh, L, H);
				if (a != -1)
				{
					return a;
				}
			}

			if (indexL.HasValue)
			{
				for (int i = indexL.Value; i < N; i++)
				{
					// a.b = g.l
					gcdLow = GCD(gcdLow, freq[i]);
					lcmLow = (freq[i] / gcdLow) * lcmLow;

					// can't think of better way to get this
					if (i + 1 < N)
					{
						gcdHigh = freq[i + 1];
						lcmHigh = freq[i + 1];

						for (int j = i + 1; j < N; j++)
						{
							gcdHigh = GCD(gcdHigh, freq[i]);
							lcmHigh = (freq[i] / gcdHigh) * lcmHigh;
						}

						if (IsDivisible(lcmLow, gcdHigh))
						{
							long a = GetInRange(lcmLow, gcdHigh, L, H);
							if (a != -1)
							{
								return a;
							}
						}
					}
					else
					{
						if (gcdLow >= L && gcdLow <= H)
						{
							return gcdLow;
						}
						else if (lcmLow >= L && lcmLow <= H)
						{
							return lcmLow;
						}
						else if (lcmLow < L)
						{
							long lcm_l = L / lcmLow;
							long lcm_h = H / lcm_l;

							long ans = -1;
							for (long move = lcm_l; move <= lcm_h; move++)
							{
								if (move * lcm_l >= L && move * lcm_l <= H)
								{
									ans = move * lcm_l;
								}
							}

							return ans;
						}
						else
						{
							return -1;
						}
					}
				}
			}

			return -1;
		}

		private long GetInRange(long lcmLow, long gcdHigh, long L, long H)
		{
			if (lcmLow >= L && lcmLow <= H)
			{
				return lcmLow;
			}
			else if (gcdHigh >= L && gcdHigh <= H)
			{
				return gcdHigh;
			}
			else
			{
				return -1;
			}
		}

		private static bool IsDivisible(long lcmLow, long gcdHigh)
		{
			if (gcdHigh % lcmLow == 0 || lcmLow % gcdHigh == 0)
			{
				return true;
			}
			else
			{
				return false;
			}
		}

		private static long EndCase(long N, long L, long H, long[] freq)
		{
			long gcd = freq[0];
			long prod = freq[0];
			for (int i = 1; i < N; i++)
			{
				gcd = GCD(gcd, freq[i]);
				prod *= freq[i];
			}

			long lcm = prod / gcd;

			if (gcd >= L && gcd <= H)
			{
				return gcd;
			}
			else if (lcm >= L && lcm <= H)
			{
				return lcm;
			}
			else if (lcm < L)
			{
				long lcmLow = L / lcm;
				long lcmHigh = H / lcm;

				long ans = -1;
				for (long move = lcmLow; move <= lcmHigh; move++)
				{
					if (move * lcm >= L && move * lcm <= H)
					{
						ans = move * lcm;
					}
				}

				return ans;
			}
			else
			{
				return -1;
			}
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
