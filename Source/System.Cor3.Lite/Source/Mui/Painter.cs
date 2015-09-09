using System;
using System.Drawing;
using Mui.Widgets;
namespace Mui
{
  static class Ex
  {
    static public float Smallest(this float input, params float[] inputs) { return input.Conditional<float>( (a, b) => a < b ? a : b, inputs); }
    static public float Largest(this float input, params float[] inputs) { return input.Conditional<float>( (a, b) => a > b ? a : b, inputs); }
    public static T Conditional<T>(this T input, Func<T, T, T> condition, params T[] inputs)
    {
      var result = input;
      foreach (var i in inputs) result = condition(result, i);
      return result;
    }
    static public FloatPoint Smallest(this FloatPoint input, params FloatPoint[] inputs)
    {
      return input.Conditional<FloatPoint>( (a, b) => a.Slope < b.Slope ? a : b, inputs);
    }
    static public FloatPoint Largest(this FloatPoint input, params FloatPoint[] inputs)
    {
      return input.Conditional<FloatPoint>( (a, b) => a.Slope > b.Slope ? a : b, inputs);
    }
    static public FloatPoint Limit(this FloatPoint input, FloatPoint min, FloatPoint max)
    {
      var result = input.Clone();
      result.X = result.X.MinMax(min.X,max.X).ToSingle();
      result.Y = result.Y.MinMax(min.Y, max.Y).ToSingle();
      return result;
    }
  }
  static public class PainterHelper
  {

    static public void DrawText(this Graphics graphics, string text, Color color, Font font, FloatRect rect, StringAlignment hAlign=StringAlignment.Center)
    {
      graphics.DrawString(
        text,
        font,
        new SolidBrush(color),
        rect,
        new StringFormat()
        {
          Alignment=hAlign,
          LineAlignment=StringAlignment.Center,
        }
       );
    }
    readonly static Color DefaultSelectBoxColour = Color.FromArgb(0, 127, 255);
    // TODO: Needs 'DrawSelectBox(…)`
    // We also need implementation strategy for drawing a select-box that can be applied to widgets universally.
    static public void DrawSelectBox(this Graphics graphics, Widget widget, FloatRect lim=null, Color? colour=null)
    {
      if (widget.Parent.MouseM == null || widget.Parent.MouseD == null) return;
      Func<Point, Point> p2c = widget.Parent.PointToClient;

      var state = graphics.Save();
      graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
      graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighSpeed;

      var c1 = colour ?? DefaultSelectBoxColour;
      var c2 = Color.FromArgb(64, c1); // with alpha

      // find the closest x
      FloatPoint p0 = p2c(widget.Parent.MouseD.Nearest(widget.Parent.MouseM));
      FloatPoint p1 = p2c(widget.Parent.MouseD.Furthest(widget.Parent.MouseM));

      FloatPoint pA = p0.Smallest(p1);
      FloatPoint pB = p0.Largest(p1);

      if (lim!=null)
      {
        pA = pA.Limit(lim.Location,lim.BottomRight);
        pB = pB.Limit(lim.Location,lim.BottomRight);
        pB = pB-pA;
      }
      FloatRect rect = new FloatRect(pA, pB);

      using (var p = new Pen(c1, 1))
      using (var b = new SolidBrush(c2))
      {
        graphics.DrawRectangle(p, rect);
        graphics.FillRectangle(b, rect);
      }

      graphics.Restore(state);
    }
  }
  public partial class Painter
  {
    static public void DrawBorder(Graphics graphics, Widget widget, Pen pBorder=null, Brush bFill=null)
    {
      if (widget.Bounds != null)
      {
        using (var rgn = new Region(widget.Bounds))
          using (var pen1=GetPen(widget.ColourFg,3))
        {
          graphics.Clip = rgn;
          
          if (widget.Parent.FocusedControl==widget)
            graphics.FillRectangle(bFill??DictBrush[ColourClass.Dark40],widget.Bounds);
          graphics.DrawRectangle(pen1,widget.Bounds);
          
          graphics.ResetClip();
        }
      }
    }
    static public void DrawTextValue(Graphics graphics, Widget widget)
    {
      var str = string.IsNullOrEmpty(widget.Text) ? "..." : widget.Text;
      
      graphics.DrawString(
        str,
        widget.Font,
        DictBrush[ColourClass.White],
        widget.PaddedBounds,
        new StringFormat()
        {
          LineAlignment=StringAlignment.Center,
        }
       );
    }
    static public void DrawText(Graphics graphics, Widget widget)
    {
      var str = string.IsNullOrEmpty(widget.Text) ? "..." : widget.Text;
      graphics.DrawString(
        str,
        widget.Font,
        DictBrush[ColourClass.White],
        widget.PaddedBounds,
        new StringFormat()
        {
          Alignment=StringAlignment.Center,
          LineAlignment=StringAlignment.Center,
        }
       );
    }
    static public void RenderSlider(Graphics g, Widget widget, Color back, Decible decible, System.Windows.Forms.Control control)
    {
      using (var format  = new StringFormat())
      {
        format.LineAlignment = StringAlignment.Center;
        format.Alignment     = StringAlignment.Center;
        using (Pen p = new Pen(control.ForeColor,1)) g.DrawRectangle(p, 0, 0, control.Width - 1, control.Height - 1);
        using (Brush b = new SolidBrush(back)) g.FillRectangle(b, 1, 1, (int)((control.Width - 2) * decible.Percent), control.Height - 2);
        using (Brush b = new SolidBrush(control.ForeColor)) g.DrawString( decible.ToString(), control.Font, b, control.ClientRectangle, format );
      }
    }
  }
}


