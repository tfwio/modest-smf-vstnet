using System;
using System.Linq;
using System.Windows;
namespace on.trig
{
	using Point = System.Drawing.DoublePoint;

	static class PointExtension
	{
		public static Point n(this long input)
		{
			return new Point(input, input);
		}
    
		public static Point n(this double input)
		{
			return new Point(input, input);
		}
    
		public static Point n(this int input)
		{
			return new Point(input, input);
		}
		
		public static Point GetMiddle(this Point P0, Point P1)
		{
		  return (P0+P1) / 2;
		}
    
		public static double To(this Point a, Point b)
    {
      Point diff = a - b;
      return Math.Pow(a.X-b.X, 2)+Math.Pow(a.Y-b.Y, 2);
    }

		public static Point GetPointOnSegment(this Point P0, Point P1, double ratio)
		{
		  return P0 + (P1-P0 * ratio.n());
			//turn P0.add(pExt.mult(P1.subtract(P0),pExt.n(ratio)));
		}
		
		public static double Pow(this Point point)
		{
		  return Math.Pow(point.X,2) + Math.Pow(point.Y,2);
		}
    
		public static Point GetLineCross(this LineObj l0, LineObj l1)
    {
      double u;
      // do both lines exhist?
      if (l0==null && l1==null) return null;
      // are both lines vertical?
      if (double.IsNaN(l0.c) && double.IsNaN(l1.c))
      {
        // no intersection; is parallel, not vertical
        if (l0.a.Equals(l1.a)) return null;
        // common X value
        u = (l1.b - l0.b) / (l0.a - l1.a);
        return new Point(u, l0.a * u + l0.b);
      }
      
      // l0.c != undefined
      // wasn't fun figuring this out
      // should write some notes on this Object usage.
      if (!double.IsNaN(l0.c)) {
        if (!double.IsNaN(l1.c)) {
          // both lines vertical, intersection does not exhist
          return null;
        }
        else
        {
          // return the point on l1 with x = l0.c
          return new Point(l0.c, (l1.a * l0.c + l1.b));
        }
      }
      else if (!double.IsNaN(l1.c)) {
        // l0.c as it was tested above
        // return the point on l0 with x = l1.c
        return new Point(l1.c, (l0.a * l1.c + l0.b));
      }
      return null;
    }
	  
	}
}



