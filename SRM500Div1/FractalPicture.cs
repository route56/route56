﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace SRM500Div1
{
	public class FractalPicture
	{
		public class Line
		{
			public double RootX { get; set; }
			public double RootY { get; set; }
			public double EndX { get; set; }
			public double EndY { get; set; }
			public double? Length { get; set; }
			public bool IsFinal { get; set; }

			public double ComputeLength()
			{
				return Math.Sqrt(Math.Pow(RootX - EndX, 2) + Math.Pow(RootY - EndY, 2));
			}
		}

		class Rect
		{
			public double X1 { get; set; }
			public double Y1 { get; set; }
			public double X2 { get; set; }
			public double Y2 { get; set; }

			public double FindLineLengthIntersectedWith(Line line)
			{
				double x = 0;
				double y = 0;

				Debug.Assert(X1 <= X2 && Y1 <= Y2);

				double x1line = Math.Min(line.EndX, line.RootX);
				double x2Line = Math.Max(line.EndX, line.RootX);
				double y1Line = Math.Min(line.EndY, line.RootY);
				double y2Line = Math.Max(line.EndY, line.RootY);

				x = GetSideLengthInRect(x1line, x2Line, X1, X2);
				y = GetSideLengthInRect(y1Line, y2Line, Y1, Y2);

				if (x == 0F || y == 0F)
					return 0; // TODO Need to come up a better way.
				else
				{
					return Math.Sqrt(x * x + y * y); // TODO One of them will be zero for this problem hence this call can be avoided
				}
			}

			private static double GetSideLengthInRect(double x1line, double x2Line, double X1, double X2)
			{
				double x = 0;

				// Lines are parallel to rect => 6 CASES POSSIBLE
				if (x1line < X1)
				{
					if (x2Line < X1)
					{
						x = 0; // CASE 1
					}
					else
					{
						if (x2Line < X2)
						{
							x = x2Line - X1; // CASE 2
						}
						else
						{
							x = X2 - X1; // CASE 3
						}
					}
				}
				else
				{
					if (x1line < X2)
					{
						if (x2Line < X2)
						{
							x = x2Line - x1line; // Case 4
						}
						else
						{
							x = X2 - x1line; // Case 5
						}
					}
					else
					{
						x = 0; // CASE 6
					}
				}

				return x;
			}
		}

		public List<Line> LineList { get; set; }

		public double getLength(int x1, int y1, int x2, int y2)
		{
			// TODO Move to static so this can be cached for 1st run and subsequent runs can just compute length. No 
			LineList = new List<Line>();
			LineList.Add(new Line() { RootX = 0, RootY = 0, EndX = 0, EndY = 81, IsFinal = false });

			// TODO Will this be really bad idead? Slow to compute 500th gen?? Optimize??
			GenerateFractal(500);

			return ComputeTotalLength(x1, y1, x2, y2);
		}

		private double ComputeTotalLength(int x1, int y1, int x2, int y2)
		{
			double totalLength = 0;
			Rect rect = new Rect() { X1 = x1, Y1 = y1, X2 = x2, Y2 = y2 };

			foreach (Line line in LineList)
			{
				totalLength += rect.FindLineLengthIntersectedWith(line);
			}

			return totalLength;
		}

		private void GenerateFractal(int generationNumber)
		{
			if (generationNumber > 1)
			{
				GenerateFractal(generationNumber - 1);
			}

			// TODO Recursion may have implication on member list LineList
			List<Line> newLines = new List<Line>();

			foreach (Line line in LineList)
			{
				if (line.IsFinal == false)
				{
					newLines.AddRange(ProcessLine(line));
				}
			}

			LineList.AddRange(newLines);
		}

		private Line[] ProcessLine(Line line)
		{
			Debug.Assert(line.IsFinal == false);
			Line[] newlines = { new Line(), new Line(), new Line() };
			
			double delX = line.EndX - line.RootX;
			double delY = line.EndY - line.RootY;

			Debug.Assert(delX == 0.0 || delY == 0.0, "Lines should be parallel or perpendicular to coordinate system");

			// find 2:1 point on line
			double newRootX = line.RootX + 2 * delX / 3;
			double newRootY = line.RootY + 2 * delY / 3;

			double oneThirdLength = Math.Abs(line.ComputeLength() / 3);

			foreach (Line ln in newlines)
			{
				ln.IsFinal = false;
				ln.RootX = newRootX;
				ln.RootY = newRootY;
			}

			// Sub line of original one
			newlines[0].EndX = line.EndX;
			newlines[0].EndY = line.EndY;

			// 90 Clockwise
			if (delY == 0.0)
			{
				newlines[1].EndX = newRootX;
				newlines[2].EndX = newRootX;

				newlines[1].EndY = newRootY + oneThirdLength;
				newlines[2].EndY = newRootY - oneThirdLength;
			}
			else
			{
				Debug.Assert(delX == 0.0, "Lines should be parallel or perpendicular to coordinate system");
				newlines[1].EndY = newRootY;
				newlines[2].EndY = newRootY;

				newlines[1].EndX = newRootX + oneThirdLength;
				newlines[2].EndX = newRootX - oneThirdLength;
			}

			line.EndX = newRootX;
			line.EndY = newRootY;
			line.IsFinal = true;

			return newlines;
		}
	}
}
