using System;
using System.Drawing;
using Mui.Widgets;
namespace Mui
{
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


