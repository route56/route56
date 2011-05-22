using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GCJSolver
{
	public class Program
	{
		int N;
		string[] s = new string[110];
		double[] WP = new double[110];
		double[,] WP2 = new double[110,110];
		double[] OWP= new double[110];
		double[] OOWP=new double[110];
		double[] RPI=new double[110];

		public void DoIt()
		{
			N = Int32.Parse(Console.ReadLine());

			for (int ii = 0; ii < N; ii++)
			{
				s[ii] = Console.ReadLine();
			}

			for (int i = 0; i < N; i++)
			{
				for (int j = 0; j < N+1; j++)
				{
					double sum = 0.0, cnt = 0.0;

					for (int k = 0; k < N; k++)
					{
						if(k != j && s[i][k] != '.')
						{
							cnt += 1.0;
							if(s[i][k] == '1') sum += 1.0;
						}
					}
					if(j < N) WP2[i,j] = sum / cnt; else WP[i] = sum / cnt;
				}
			}
	
	for (int i = 0; i < N; i++)
	{
	    double sum = 0.0, cnt = 0.0;
	    for (int j = 0; j < N; j++) if(s[i][j] != '.')
		{
	        cnt += 1.0;
	        sum += WP2[j,i];
	    }
	    OWP[i] = sum / cnt;
	}
	
	for (int i = 0; i < N; i++){
	    double sum = 0.0, cnt = 0.0;
	    for (int j = 0; j < N; j++) if(s[i][j] != '.'){
	        cnt += 1.0;
	        sum += OWP[j];
	    }
	    OOWP[i] = sum / cnt;
	}
	
	for (int i = 0; i < N; i++) RPI[i] = 0.25 * WP[i] + 0.5 * OWP[i] + 0.25 * OOWP[i];
	for (int i = 0; i < N; i++) Console.WriteLine(RPI[i]);
	    }

		public static void Main(string[] args)
		{
			int caser = Int32.Parse(Console.ReadLine());
			for (int i = 0; i < caser; i++)
			{
				Console.WriteLine("Case #{0}:",i);
				new Program().DoIt();
			}
		}
		}
}
