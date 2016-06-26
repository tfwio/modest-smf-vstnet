using System;
using System.Linq;
namespace on.trig
{
  using Point = System.Drawing.DoublePoint;
  
  class Draw
  {
    int numSegments = 4;
    const int MAX_RECURSION = 10;
    
    int DrawCubicBézier(IApiDraw api, Vertex pt)
    {
      Tuple<Point,LineObj> curt = null; // Tangent Object (LineObj)?
      Tuple<Point,LineObj> next; // Tangent Object (LineObj)?
      int    total    = 0; // int or long
      int    nSegment = numSegments < 2 ? 4 : this.numSegments;
      double tstep    = 1 / nSegment;
      
      curt = new Tuple<Point,LineObj>(pt.p0,LineObj.GetLine(pt.p0,pt.p1));
      if (api!=null) api.moveTo(pt.p0);
      for (int i=1; i < nSegment; i++)
      {
        next = pt.CubicTangent(Convert.ToInt32(i * tstep)); // next tangent
        total += SliceCubicBézierSegments( api, pt, (i - 1) * tstep, i * tstep, curt, next, 0); // current segment
        if (total > numSegments) {} curt = next;
      }
      return total;
    }
    
    void DrawCubicBézier2(IApiDraw api, Vertex pt)
    {
      // base-points
      Point PA = pt.p0.GetPointOnSegment(pt.p1, 3/4);
      Point PB = pt.p3.GetPointOnSegment(pt.p2, 3/4);
      
      Point d = (pt.p3 - pt.p0) / 16;
      Point Pc_1 = pt.p0.GetPointOnSegment(pt.p1, 3/8);
      Point Pc_2 = PA.GetPointOnSegment(PB, 3/8) - d;
      Point Pc_3 = PB.GetPointOnSegment(PA, 3/8) + d;
      Point Pc_4 = pt.p3.GetPointOnSegment(pt.p2, 3/8) + d;
      
      // three anchor points
      Point Pa_1 = Pc_1.GetMiddle(Pc_2);
      Point Pa_2 = PA.GetMiddle(PB);
      Point Pa_3 = Pc_3.GetMiddle(Pc_4);
      
      // draw four quadratic segments
      if (api!=null) api.curveTo(Pc_1, Pa_1);
      if (api!=null) api.curveTo(Pc_2, Pa_2);
      if (api!=null) api.curveTo(Pc_3, Pa_3);
      if (api!=null) api.curveTo(Pc_4, pt.p3);
    }
    
    void DrawCubicBezierSpline(IApiDraw api, Vertex pt)
    {
      Point midp = (pt.p1+pt.p2) / 2;
      // GDI equiv being curveto()
      // draw fake cubic bezier curve lines (in two parts)
      if (api!=null) api.curveTo(pt.p1,midp);
      if (api!=null) api.curveTo(pt.p2,pt.p3);
    }
    
    static int SliceCubicBézierSegments( IApiDraw api, Vertex PT, double n1, double n2, Tuple<Point,LineObj> Tu1, Tuple<Point,LineObj> Tu2, int recursion )
    {
      // Tu1 may be null and cause Exception
      // Tu2 may be null and cause Exception
      
      // infinity recursion ?
      if (recursion > MAX_RECURSION)
      {
        Point P = Tu2.Item1;
        if (api!=null) api.lineTo(Tu2.Item1);
        return 1;
      }
      
      // process segment
      Point  CP = Tu1.Item2.GetLineCross(Tu2.Item2);
      
      // a controlpoint is considered misplaced if its distance
      // from one of the anchor is greater than the distance
      // between the two anchors.
      
      Point  PO = Tu1.Item1-Tu2.Item1;     // point
      double DN = PO.Pow();                // double
      double D0 = Tu1.Item1.To(CP);        // double
      double D1 = Tu1.Item1.To(Tu2.Item1); // double
      double D2 = Tu2.Item1.To(CP);        // double
      
      if ( CP == null || D1 > D0 || D2 > D0)
      {
        
        int    total = 0;                  // total for this subsegment starts at 0
        double umid  = (n1 + n2) / 2D;     // if misplaced control-point, slice segment more
        Vertex PM    = PT.CloneAt(Convert.ToInt64(umid));
        Tuple<Point,LineObj> tumid = PM.CubicTangent();
        // sub segment count
        total += SliceCubicBézierSegments(api, PT, n1, umid, Tu1, tumid, recursion+1);
        total += SliceCubicBézierSegments(api, PT, umid, n2, tumid, Tu2, recursion+1);
        return total;
      }
      else
      {
        // draw curve
        if (api!=null) api.curveTo(CP.X, CP.Y, Tu2.Item1.X, Tu2.Item1.Y);
        return 1;
      }
    }
  }
  
  
}





