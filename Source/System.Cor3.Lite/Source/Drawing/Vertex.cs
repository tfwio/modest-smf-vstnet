using System;
using System.Linq;
using System.Windows;
namespace on.trig
{
  using Point = System.Drawing.DoublePoint;

  public partial class Vertex
  {
    // : Tuple<Point,Point,Point,long>
    internal protected Point p0, p1, p2, p3;

    internal protected double t;
    // increment precision?
    public Vertex CloneAt(long value)
    {
      return new Vertex{p0=p0, p1=p1, p2=p2, p3=p3,t=value};
    }
  }

  public partial class Vertex
  {
    internal const int MIN_SUBDIV = 3;
    
    // disable once InvokeAsExtensionMethod
    public Point GetCubic(int n = MIN_SUBDIV) { return GetCubic(this,n); }
    // disable once InvokeAsExtensionMethod
    /// <seealso cref="CubicTangent"/>
    /// <summary>
    /// </summary>
    /// <param name="n"></param>
    /// <returns></returns>
    public Point Derivitive(int n = MIN_SUBDIV) { return Derivitive(this,n); }
    // disable once InvokeAsExtensionMethod
    public Tuple<Point, LineObj> CubicTangent(int n = MIN_SUBDIV) { return CubicTangent(this,n); }
    
    /// <summary>
    /// </summary>
    /// <returns></returns>
    static public Point GetCubic(Vertex V, int n = MIN_SUBDIV)
    {
      Point X = n.n() * ( V.p1 - V.p0    );
      Point Y = n.n() * ( V.p2 - V.p1 - X);
      Point A = V.p3 - V.p0 - Y - X;
      double T1 = V.t;
      double T2 = T1 * T1;
      double T3 = T2 * T1;
      return
        ( A * T3.n() ) +
        ( Y * T2.n() ) +
        ( X * T1.n() ) +
        V.p0;
    }
    /// <summary>
    /// 
    /// </summary>
    /// <param name="V"></param>
    /// <param name="n"></param>
    /// <returns></returns>
    static public Point Derivitive(Vertex V, int n = MIN_SUBDIV)
    {
      Point X =   n.n() * ( V.p1 - V.p0 );
      Point Y = ( n.n() * ( V.p2 - V.p1 ) ) - X;
      Point A = V.p3 - V.p0 - Y - X;
      double T1 = V.t;
      double T2 = T1 * T1;
      return
        ( n.n() * ( A * T2  .n() ) ) +
        ( ( 2 * T1 ).n() * Y ) + X;
    }

    static public Tuple<Point, LineObj> CubicTangent(Vertex V, int n = MIN_SUBDIV)
    {
      var C = V.GetCubic(n);
      var D = V.Derivitive(n);
      var L = LineObj.GetLine(C, D);
      return new Tuple<Point, LineObj>(C, L);
    }
  }
}



