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

    public WidgetButton(IMui parent) : base(parent)
    {
      this.ValueFormat = "{0}";
    }

    public override void Paint(PaintEventArgs arg)
    {
      base.Paint(arg);
      using (var region = new Region(this.Bounds))
      {
        arg.Graphics.Clip = region;
        
        Painter.DrawText(arg.Graphics,this);
        
        arg.Graphics.ResetClip();
      }
    }
  }
}




