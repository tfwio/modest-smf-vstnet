/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class WidgetButton : Widget
  {
    
    protected override void WidgetButton_ParentMouseMove(object sender, MouseEventArgs e)
    {
      base.WidgetButton_ParentMouseMove(sender, e);
    }

    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      this.SetFocus();
    }

    public override void Design()
    {
      this.ValueFormat = "{0}";
    }
    
    public WidgetButton(IMui parent) : base(parent)
    {
    }

    internal static StringFormat PathStringFormat = new StringFormat()
    {
      Alignment = StringAlignment.Center,
      LineAlignment = StringAlignment.Center,
      FormatFlags = StringFormatFlags.DisplayFormatControl | StringFormatFlags.FitBlackBox,
      Trimming = StringTrimming.None
    };
    
    public override void Paint(PaintEventArgs arg)
    {
      base.Paint(arg);

      using (var region = new Region(this.Bounds))
      {
        var state = arg.Graphics.Save();
        arg.Graphics.Clip = region;
        arg.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        Painter.DrawText(arg.Graphics,this,Smoother);
        arg.Graphics.ResetClip();
        arg.Graphics.Restore(state);
      }
    }
  }
  
}




