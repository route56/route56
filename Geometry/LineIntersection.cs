// Modified http://tog.acm.org/resources/GraphicsGems/gemsii/xlines.c
using System;
using System.Collections.Generic;
using System.Text;

namespace Geometry
{
	public enum IntersectionType
	{
		DONT_INTERSECT,
		DO_INTERSECT,
		COLLINEAR,
	}

	public class LineIntersection
	{
/* Original lines_intersect:  AUTHOR: Mukesh Prasad
 * http://tog.acm.org/resources/GraphicsGems/gemsii/xlines.c
 *
 *   This function computes whether two line segments,
 *   respectively joining the input points (x1,y1) -- (x2,y2)
 *   and the input points (x3,y3) -- (x4,y4) intersect.
 *   If the lines intersect, the output variables x, y are
 *   set to coordinates of the point of intersection.
 *
 *   All values are in integers.  The returned value is rounded
 *   to the nearest integer point.
 *
 *   If non-integral grid points are relevant, the function
 *   can easily be transformed by substituting floating point
 *   calculations instead of integer calculations.
 *
 *   Entry
 *        x1, y1,  x2, y2   Coordinates of endpoints of one segment.
 *        x3, y3,  x4, y4   Coordinates of endpoints of other segment.
 *
 *   Exit
 *        x, y              Coordinates of intersection point.
 *
 *   The value returned by the function is one of:
 *
 *        DONT_INTERSECT    0
 *        DO_INTERSECT      1
 *        COLLINEAR         2
 *
 * Error conditions:
 *
 *     Depending upon the possible ranges, and particularly on 16-bit
 *     computers, care should be taken to protect from overflow.
 *
 *     In the following code, 'double' values have been used for this
 *     purpose, instead of 'int'.
 *
 */
		/// <summary>
		/// Gets the point of intersection between two lines, if possible
		/// </summary>
		/// <param name="x1">line 1</param>
		/// <param name="y1">line 1</param>
		/// <param name="x2">line 1</param>
		/// <param name="y2">line 1</param>
		/// <param name="x3">line 2</param>
		/// <param name="y3">line 2</param>
		/// <param name="x4">line 2</param>
		/// <param name="y4">line 2</param>
		/// <returns>
		/// Caller needs to interpret IntersectionType before using the point x, y
		/// </returns>
		public Tuple<IntersectionType, double, double> FindIntersectionPoint(
			//Tuple<double, double, double, double> line1,
			//Tuple<double, double, double, double> line2)
			double x1, double y1, double x2, double y2,
			double x3, double y3, double x4, double y4)
		{
			double x, y;

			double a1, a2, b1, b2, c1, c2; /* Coefficients of line eqns. */
			double r1, r2, r3, r4;         /* 'Sign' values */
			double denom, offset, num;     /* Intermediate values */

			/* Compute a1, b1, c1, where line joining points 1 and 2
			 * is "a1 x  +  b1 y  +  c1  =  0".
			 */

			a1 = y2 - y1;
			b1 = x1 - x2;
			c1 = x2 * y1 - x1 * y2;

			/* Compute r3 and r4.
			 */

			r3 = a1 * x3 + b1 * y3 + c1;
			r4 = a1 * x4 + b1 * y4 + c1;

			/* Check signs of r3 and r4.  If both point 3 and point 4 lie on
			 * same side of line 1, the line segments do not intersect.
			 */

			if (r3 != 0 &&
				 r4 != 0 &&
				 SAME_SIGNS(r3, r4))
				return new Tuple<IntersectionType, double, double>(IntersectionType.DONT_INTERSECT, Double.NaN, Double.NaN);

			/* Compute a2, b2, c2 */

			a2 = y4 - y3;
			b2 = x3 - x4;
			c2 = x4 * y3 - x3 * y4;

			/* Compute r1 and r2 */

			r1 = a2 * x1 + b2 * y1 + c2;
			r2 = a2 * x2 + b2 * y2 + c2;

			/* Check signs of r1 and r2.  If both point 1 and point 2 lie
			 * on same side of second line segment, the line segments do
			 * not intersect.
			 */

			if (r1 != 0 &&
				 r2 != 0 &&
				 SAME_SIGNS(r1, r2))
				return new Tuple<IntersectionType, double, double>(IntersectionType.DONT_INTERSECT, Double.NaN, Double.NaN);

			/* Line segments intersect: compute intersection point. 
			 */

			denom = a1 * b2 - a2 * b1;
			if (denom == 0)
				return new Tuple<IntersectionType, double, double>(IntersectionType.COLLINEAR, Double.NaN, Double.NaN);
			offset = denom < 0 ? -denom / 2 : denom / 2;

			/* The denom/2 is to get rounding instead of truncating.  It
			 * is added or subtracted to the numerator, depending upon the
			 * sign of the numerator.
			 */

			num = b1 * c2 - b2 * c1;
			x = (num < 0 ? num - offset : num + offset) / denom;

			num = a2 * c1 - a1 * c2;
			y = (num < 0 ? num - offset : num + offset) / denom;

			return new Tuple<IntersectionType, double, double>(IntersectionType.DO_INTERSECT, x, y);
		}

		private bool SAME_SIGNS(double a, double b)
		{
			return (a < 0 && b < 0) || (a >= 0 && b >= 0);
		}

	}
}
