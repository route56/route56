using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RegexProblems.SRM538
{
	public class EvenRoute
	{
		public string isItPossible(int[] x, int[] y, int wantedParity)
		{
			if (isItPossibleMain(x,y,wantedParity))
			{
				return "CAN";
			}
			else
			{
				return "CANNOT";
			}
		}

		bool isItPossibleMain(int[] x, int[] y, int wantedParity)
		{
			int xmin = FindMin(x);
			int xmax = FindMax(x);
			int ymin = FindMin(y);
			int ymax = FindMax(y);

			int? xsteps = GetXSteps(xmin, xmax);
			int? ysteps = GetXSteps(ymin, ymax);

			if (xsteps.HasValue)
			{
				if (ysteps.HasValue)
				{
					return ((xsteps + ysteps) % 2 == wantedParity);
				}
				else
				{
					return Logic(wantedParity, ymin, ymax, xsteps);
				}
			}
			else
			{
				if (ysteps.HasValue)
				{
					return Logic(wantedParity, xmin, xmax, ysteps);
				}
				else
				{
					xsteps = xmax - xmin;
					ysteps = ymax - ymin;

					if (xsteps % 2 == 0)
					{
						if (ysteps % 2 == 0)
						{
							return 0 == wantedParity;
						}
						else
						{
							return 1 == wantedParity;
						}
					}
					else
					{
						if (ysteps % 2 == 0)
						{
							return 1 == wantedParity;
						}
						else
						{
							return 0 == wantedParity;
						}
					}
				}
			}
		}

		private bool Logic(int wantedParity, int ymin, int ymax, int? xsteps)
		{
			if (Math.Abs(ymin) % 2 == 0)
			{
				if (Math.Abs(ymax) % 2 == 0)
				{
					return (xsteps % 2 == wantedParity);
				}
				else
				{
					return true;
				}
			}
			else
			{
				if (Math.Abs(ymax) % 2 == 0)
				{
					return true;
				}
				else
				{
					return ((xsteps + 1) % 2 == wantedParity);
				}
			}
		}

		private int FindMax(int[] x)
		{
			int max = x[0];

			for (int i = 1; i < x.Length; i++)
			{
				if (max < x[i])
				{
					max = x[i];
				}
			}

			return max;
		}

		private int FindMin(int[] x)
		{
			int min = x[0];

			for (int i = 1; i < x.Length; i++)
			{
				if (min > x[i])
				{
					min = x[i];
				}
			}

			return min;
		}

		private int? GetXSteps(int xmin, int xmax)
		{
			int? xsteps = null;

			if (xmin >= 0)
			{
				xsteps = xmax;
			}
			else if (xmax <= 0)
			{
				xsteps = -xmin;
			}

			return xsteps;
		}
	}
}
