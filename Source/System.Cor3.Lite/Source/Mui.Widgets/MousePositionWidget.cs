/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
	public class MousePositionWidget : Widget
	{
		public override string Text {
			get {
	      var pos = (FloatPoint)Parent.PointToClient(Form.MousePosition);
				return Parent.MouseM == null ? "" : string.Format("{0}, {1}", pos.X, pos.Y);
			}
		}

		public MousePositionWidget(IMui parent) : base(parent)
		{
			this.ValueFormat = "{0}";
		}

		public override void Paint(PaintEventArgs arg)
		{
//			base.Paint(arg);
			using (var region = new Region(Bounds))
			{
				arg.Graphics.Clip = region;
				
				Painter.DrawText(arg.Graphics, this);
				
				arg.Graphics.ResetClip();
			}
		}
	}
}






