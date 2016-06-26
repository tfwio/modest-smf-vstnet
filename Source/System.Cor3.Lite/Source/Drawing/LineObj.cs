using System;
using System.Linq;
namespace on.trig
{
  using Point = System.Drawing.DoublePoint;
  
  public class LineObj {
    
    internal protected double a, b, c;
    
    static public implicit operator Tuple<double,double,double>(LineObj o) {
      return new Tuple<double,double,double>(o.a,o.b,o.c);
    }
    static public implicit operator LineObj(Tuple<double,double,double> o) {
      return new LineObj{ a = o.Item1, b = o.Item2, c = o.Item3 };
    }

    /// <summary>
    /// May very well be our delta-point of interest.
    /// </summary>
    /// <remarks>
    /// This method is called by <see cref="Cubic.CubicTangent(Vertex,int)"/>
    /// </remarks>
    /// <param name="Cubed"></param>
    /// <param name="Derivitive"></param>
    /// <returns>
    /// NULL if Cubed and Derivitive Points are equal.
    /// </returns>
    static public LineObj GetLine(Point Cubed, Point Derivitive)
    {
      if (Cubed.X.Equals(Derivitive.X) && Cubed.Y.Equals(Derivitive.Y))
        return null;
      
      if (Cubed.X.Equals(Derivitive.X)) return new LineObj { c = Cubed.X };
      else
      {
        var p = Cubed - Derivitive;
        var d = p.Y / p.X;
        return new LineObj { a = d, b = Cubed.Y - (d * Cubed.X) };
      }
    }
  }
}

