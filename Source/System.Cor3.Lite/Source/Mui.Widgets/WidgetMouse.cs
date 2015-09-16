/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
	public class WidgetMouse : Widget
	{
    protected override void WidgetButton_ParentMouseMove(object sender, MouseEventArgs e)
    {
//      base.WidgetButton_ParentMouseMove(sender, e);
    }
	  
		public override string Text {
			get {
	      var pos = (FloatPoint)Parent.PointToClient(Form.MousePosition);
				return Parent.MouseM == null ? "" : string.Format("{0}, {1}", pos.X, pos.Y);
			}
		}

		public WidgetMouse(IMui parent) : base(parent)
		{
		}

		public override void Paint(PaintEventArgs arg)
		{
			using (var region = new Region(Bounds))
			{
				arg.Graphics.Clip = region;
				Painter.DrawText(arg.Graphics, this, false);
				
				arg.Graphics.ResetClip();
			}
		}
	}
}






