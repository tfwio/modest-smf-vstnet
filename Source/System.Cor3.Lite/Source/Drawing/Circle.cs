using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows;
using System.Windows.Forms.VisualStyles;
namespace on.trig
{
  using GraphicsPath = System.Drawing.Drawing2D.GraphicsPath;
  
  /// <summary>all static</summary>
  public class SCircle {
    
    const double pi2 = 2*Math.PI;
    
    #region Hit Test
    static public bool HitTest(FloatPoint Center, float CircleRadius, FloatPoint TestPoint)
    {
      FloatPoint
        newPoint = Center-TestPoint,
      maxPoint = HitTestMaxPoint(Center,CircleRadius),
      minPoint = HitTestMinPoint(Center,CircleRadius);
      return newPoint.Slope <= new FloatPoint(CircleRadius).Slope;
      //double Slope = newPoint.Slope;
      //        return ( TestPoint.IsLEq(maxPoint) ) && ( TestPoint.IsGEq(minPoint) );
    }
    static public FloatPoint HitTestMinPoint(FloatPoint Center, float CircleRadius)
    {
      return Center - new FloatPoint(CircleRadius);
    }
    static public FloatPoint HitTestMaxPoint(FloatPoint Center, float CircleRadius)
    {
      return Center + new FloatPoint(CircleRadius);
    }
    #endregion
    
    #region Stylus Pressure (Velocity)
    static public FloatPoint CenteredX(FloatPoint xp, double max_radius, double velocity)
    {
      double velo = (velocity/1024) * max_radius;
      return xp - (velo*.5F);
    }
    static public FloatPoint Centered(FloatPoint xp, double max_radius, double velocity)
    {
      float velo = (float)((velocity/1024D) * max_radius);
      return xp - (velo*.5D);
    }
    static public FloatPoint GetXPressureRadius(double rad_max, double velo)
    {
      return new FloatPoint((double)((velo/1024D)*rad_max));
    }
    static public FloatPoint GetPressureRadius(double rad_max, double velo)
    {
      return new FloatPoint((float)((velo/1024f) * rad_max));
    }
    #endregion

    #region Elipse
    static public void ElipseToPath(System.Drawing.Drawing2D.GraphicsPath gp, FloatPoint xp, double radius)
    {
      FloatPoint rp = new FloatPoint(radius), hp = rp * 0.5f, dp = rp * 2.0f;
      FloatPoint p = xp-rp;
      gp.AddEllipse(p.X,p.Y,dp.X,dp.Y);
    }
    static public void ElipseToPath(System.Drawing.Drawing2D.GraphicsPath gp, FloatPoint xp, int rad_max, int velo)
    {
      ElipseToPath(gp,xp,(double)rad_max,(double)velo);
    }
    static public void ElipseToPath(System.Drawing.Drawing2D.GraphicsPath gp, FloatPoint xp, double rad_max, double velo)
    {
      FloatPoint pnew = Centered(xp,rad_max,velo);
      FloatPoint hp = GetPressureRadius(rad_max,velo);
      gp.AddEllipse(xp.X-hp.X,xp.Y-hp.Y,hp.X,hp.Y);
    }
    #endregion
    
    /// ? Not quite sure if this works
    /// <returns></returns>
    /// <param name="PhaseOffset">
    /// Zero-Point or the offset from N=0.
    /// Positive moves from north to east at circumference.
    /// </param>
    /// <param name="CircumferencePoints">Number of Points on the path</param>
    /// <param name="Radius">Radius</param>
    static public System.Drawing.Drawing2D.GraphicsPath CirclePath(FloatPoint PhaseOffset, int CircumferencePoints, int Radius)
    {
      var P = new PointF[Radius];
      var T = new byte[Radius];
      for (int i=0; i < CircumferencePoints; i++)
      {
        FloatPoint point = CirclePoint(i,CircumferencePoints)+PhaseOffset;
        /*double ox = -Math.Sin(cvt(M,i)), oy = Math.Cos(cvt(M,i)); P[i]=new FloatPoint((float)(ox*R)+O.X,(float)(oy*R)+O.Y); T[i] = (byte)PatFPointType.Line;*/
      }
      T[0] = (byte)System.Drawing.Drawing2D.PathPointType.Start;
      T[T.Length-1] = (byte)System.Drawing.Drawing2D.PathPointType.CloseSubpath;
      return new System.Drawing.Drawing2D.GraphicsPath(P,T);
    }
    
    // These graphics methods are probably moreso useful than above.
    
    #region Graphics 1
    static public void DoCircle(Graphics gfx, FloatPoint O, int M, int R){ DoCircle(gfx,SystemPens.ControlText,O,M,R); }
    static public void DoCircle(Graphics gfx, Pen pen, FloatPoint O, int M, int R){ DoCircle(gfx,pen,0,O,M,R); }
    static public void DoCircle(Graphics gfx, Pen pen, float phase, FloatPoint O, int M, int R)
    {
      for (float i=-M;i<M;i++)
      {
        double ox = -Math.Sin(cvt(M,i+phase));
        double oy = Math.Cos(cvt(M,i+phase));
        double nx = -Math.Sin(cvt(M,i+1+phase));
        double ny = Math.Cos(cvt(M,i+1+phase));
        
        gfx.DrawLine( pen, (float)(ox*R)+O.X,(float)(oy*R)+O.Y, (float)(nx*R)+O.X,(float)(ny*R)+O.Y );
      }
    }
    
    static public void DrawCirclePoints( Graphics gfx, Brush brush, float phase, FloatPoint box_dimension, FloatPoint size, int _count, float scale_by )
    {
      FloatPoint Offset = box_dimension * 0.5f;
      FloatPoint scale = FloatPoint.FlattenPoint(box_dimension,false);
      
      FloatRect render_rect = new FloatRect(Offset,size);
      
      for (float i=-_count;i<_count;i++)
      {
        FloatPoint Position = new FloatPoint(
          (float)(-Math.Sin(cvt(_count,i+phase))*((scale.X*scale_by)*0.5f)) ,
          (float)(Math.Cos(cvt(_count,i+phase))*((scale.Y*scale_by)*0.5f))
         );
        
        render_rect.Location = Offset+Position-(size*0.5f);
        gfx.FillEllipse(brush,render_rect);
      }
    }
    #endregion
    #region Graphics 2
    static public void DrawCircles(
      Graphics gfx, Brush brush,
      float phase,
      FloatPoint box_dimension,
      FloatPoint size,
      int _count,
      float scale_by)
    {
      FloatPoint Offset = box_dimension * 0.5f;
      FloatPoint scale = FloatPoint.FlattenPoint(box_dimension,false);
      FloatRect render_rect = new FloatRect(Offset,size);
      FloatPoint[] points = CirclePoints(phase,box_dimension,size,_count);
      FloatPoint scaler = scale * (scale_by*0.5f);
      for (int i=0;i<points.Length;i++)
      {
        FloatPoint Position = CirclePoint(i,_count,phase);
        render_rect.Location = Offset+(Position*scaler)-(size*0.5f);
        gfx.FillEllipse(brush,render_rect);
      }
    }
    static public void DrawCircleOutlines(Graphics gfx, Pen pen, float phase, FloatPoint box_dimension, FloatPoint size, int _count, float scale_by){
      FloatPoint Offset = box_dimension * 0.5f;
      FloatPoint scale = FloatPoint.FlattenPoint(box_dimension,false);
      FloatRect render_rect = new FloatRect(Offset,size);
      FloatPoint[] points = CirclePoints(phase,box_dimension,size,_count);
      FloatPoint scaler = scale * (scale_by*0.5f);
      for (int i=0;i<points.Length;i++)
      {
        FloatPoint Position = new FloatPoint(
          (float)(-Math.Sin(cvtf(_count,i+phase))) ,
          (float)(Math.Cos(cvtf(_count,i+phase)))
         );
        render_rect.Location = Offset+(Position*scaler)-(size*0.5f);
        gfx.DrawEllipse(pen,render_rect);
      }
    }

    #endregion
    
    static public FloatPoint CirclePoint(float index, float ration, float phase=0.0f)
    {
      return FloatPoint.Angus(index,ration,phase);
    }
    static public FloatPoint[] CirclePoints(float phase, FloatPoint box_dimension, FloatPoint size, int _count)
    {
      var points = new List<FloatPoint>();
      for (float i=0;i<_count;i++) points.Add(CirclePoint(i,_count,phase));
      return points.ToArray();
    }
    
    // WTF?
    static public FloatPoint[] ScalePoints(FloatPoint box, FloatPoint size, int _count, float scale_by)
    {
      var points = new List<FloatPoint>();
      FloatPoint scale = FloatPoint.FlattenPoint(box,false);
      
      for (float i=-_count;i<_count;i++)
      {
        points.Add(scale * scale_by*0.5f);
      }
      return points.ToArray();
    }

    /// <summary>
    /// (double){ 2pi * ( I / S ) }
    /// </summary>
    static double cvt(double TotalPointsAround, double Iter){ return (pi2*(Iter/TotalPointsAround)); }
    /// <summary>
    /// (float){ 2pi * ( I / S ) }
    /// </summary>
    static float cvtf(float TotalPointsAround, float Iter){ return (float)(pi2*(Iter/TotalPointsAround)); }
  }
}



