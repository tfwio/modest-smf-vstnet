/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
  public class ButtonWidget : Widget
  {
    // Not used
    virtual protected void ButtonWidget_Click(object sender, EventArgs e)
    {
      using (Region rgn = new Region(this.Bounds))
        Parent.Invalidate(rgn);
    }
    
    virtual protected void ButtonWidget_MouseDown(object sender, MouseEventArgs e)
    {
      if (HasClientMouse)
      {
        this.SetFocus();
        this.HasMouseDown = true;
      }
      using (Region rgn = new Region(this.Bounds))
        Parent.Invalidate(rgn);
    }
    
    virtual protected void ButtonWidget_MouseUp(object sender, MouseEventArgs e)
    {
      this.HasMouseDown = false;
      using (Region rgn = new Region(this.Bounds))
        Parent.Invalidate(rgn);
    }

    virtual protected void ButtonWidget_MouseMove(object sender, MouseEventArgs e)
    {
//      if (HasFocus)
//        using (Region rgn = new Region(this.Bounds))
//          Parent.Invalidate(rgn);
    }
    public ButtonWidget(IMui parent) : base(parent)
    {
      this.ValueFormat = "{0}";
      //      this.Click += ButtonWidget_Click;
      this.MouseDown += ButtonWidget_MouseDown;
      this.MouseUp += ButtonWidget_MouseUp;
      this.MouseMove += ButtonWidget_MouseMove;;
    }

    public override void Paint(Graphics g)
    {
      base.Paint(g);
      using (var region = new Region(this.Bounds))
      {
        g.Clip = region;
        Painter.DrawText(g,this);
      }
    }
  }
}




