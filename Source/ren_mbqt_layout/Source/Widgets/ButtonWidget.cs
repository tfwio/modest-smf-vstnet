/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
	public class ButtonWidget : Widget
	{
	  public ButtonWidget(MainForm parent) : base(parent)
		{
			this.ValueFormat = "{0}";
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
	public class MousePositionWidget : Widget
	{
    public override string Text {
      get {
	      return Parent.MouseM == null ? "" : string.Format("{0}, {1}", Parent.MouseM.X, Parent.MouseM.Y);
      }
    }
	  
	  public MousePositionWidget(MainForm parent) : base(parent)
		{
			this.ValueFormat = "{0}";
		}

		public override void Paint(Graphics g)
		{
			 base.Paint(g);
			 using (var region = new Region(Bounds))
			 {
			   g.Clip = region;
			   Painter.DrawText(g,this);
			   g.ResetClip();
			 }
		}
	}
	
}




