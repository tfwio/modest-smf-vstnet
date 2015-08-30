/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class ButtonWidget : Widget
  {
    // Not used
    virtual protected void ButtonWidget_ParentClick(object sender, EventArgs e)
    {
      using (Region rgn = new Region(this.Bounds))
        Parent.Invalidate(rgn);
    }
    
    virtual protected void ButtonWidget_ParentMouseDown(object sender, MouseEventArgs e)
    {
      if (HasClientMouse)
      {
        this.SetFocus();
        this.HasMouseDown = true;
        OnMouseDown(e);
      }
      using (Region rgn = new Region(this.Bounds)) Parent.Invalidate(rgn);
    }
    
    virtual protected void ButtonWidget_ParentMouseUp(object sender, MouseEventArgs e)
    {
      this.HasMouseDown = false;
      using (Region rgn = new Region(this.Bounds)) Parent.Invalidate(rgn);
      OnMouseUp(e);
    }

    virtual protected void ButtonWidget_ParentMouseMove(object sender, MouseEventArgs e)
    {
      using (Region rgn = new Region(this.Bounds))
        Parent.Invalidate(rgn);
    }
    public ButtonWidget(IMui parent) : base(parent)
    {
      this.ValueFormat = "{0}";
      //      this.Click += ButtonWidget_Click;
      this.ParentMouseDown += ButtonWidget_ParentMouseDown;
      this.ParentMouseUp += ButtonWidget_ParentMouseUp;
      this.ParentMouseMove += ButtonWidget_ParentMouseMove;;
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




