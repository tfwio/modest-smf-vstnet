/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
namespace ren_mbqt_layout.Widgets
{
  
  partial class Painter
  {
    static public void DrawBorder(Graphics graphics, Widget widget)
    {
      if (widget.Bounds != null)
      {
        using (Region rgn = new Region(widget.Bounds))
        using (var pen1=GetPen(widget.ColourFg,3))
        {
          graphics.Clip = rgn;
          graphics.FillRectangle(DictBrush[ColourClass.Dark90],widget.Bounds);
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
          LineAlignment=StringAlignment.Center,
        }
       );
    }
    
    static public readonly Dictionary<ColourClass, Color> DictColour = new Dictionary<ColourClass, Color>() {
      { ColourClass.Active, Color.FromArgb(255, 0, 127, 255) },
      { ColourClass.Dark50, Color.FromArgb(255, 50, 50, 50) },
      { ColourClass.Dark70, Color.FromArgb(255, 70, 70, 70) },
      { ColourClass.Dark90, Color.FromArgb(255, 90, 90, 90) },
      { ColourClass.Black, Color.Black },
      { ColourClass.White, Color.White },
      { ColourClass.Default, Color.Red },
    };
    static public readonly Dictionary<ColourClass, Brush> DictBrush = new Dictionary<ColourClass, Brush> {
      { ColourClass.Active, new SolidBrush(DictColour[ColourClass.Active]) },
      { ColourClass.Dark50, new SolidBrush(DictColour[ColourClass.Dark50]) },
      { ColourClass.Dark70, new SolidBrush(DictColour[ColourClass.Dark70]) },
      { ColourClass.Dark90, new SolidBrush(DictColour[ColourClass.Dark90]) },
      { ColourClass.Black, Brushes.Black },
      { ColourClass.White, Brushes.White },
      { ColourClass.Default, Brushes.Red },
    };
    static public Pen GetPen(ColourClass cc, float width)
    {
      return new Pen(DictColour[cc],width);
    }
    static public Brush GetBrush(ColourClass cc)
    {
      return DictBrush[cc];
    }
    static public Pen GetPen(Color cc, float width)
    {
      return new Pen(cc,width);
    }
    static public readonly Dictionary<ColourClass, Pen> DictPen = new Dictionary<ColourClass, Pen> {
      { ColourClass.Active, new Pen(DictBrush[ColourClass.Active], 2) { StartCap = LineCap.Round, EndCap = LineCap.Round } },
      { ColourClass.Default, new Pen(DictBrush[ColourClass.Default], 2) { StartCap = LineCap.Round, EndCap = LineCap.Round } },
      { ColourClass.Dark50, new Pen(DictBrush[ColourClass.Dark50], 2) { StartCap = LineCap.Round, EndCap = LineCap.Round } },
      { ColourClass.Dark70, new Pen(DictBrush[ColourClass.Dark70], 2) { StartCap = LineCap.Round, EndCap = LineCap.Round } },
      { ColourClass.Dark90, new Pen(DictBrush[ColourClass.Dark90], 2) { StartCap = LineCap.Round, EndCap = LineCap.Round } },
      { ColourClass.Black, new Pen(DictBrush[ColourClass.Black], 2) { StartCap = LineCap.Round, EndCap = LineCap.Round } },
      { ColourClass.White, new Pen(DictBrush[ColourClass.White], 2) { StartCap = LineCap.Round, EndCap = LineCap.Round } },
    };
  }
}


