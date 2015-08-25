/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
	public class MousePositionWidget : Widget
	{
		public override string Text {
			get {
	      var pos = (FloatPoint)Parent.PointToClient(Form.MousePosition);
				return Parent.MouseM == null ? "" : string.Format("{0}, {1}", pos.X, pos.Y);
			}
		}

		public MousePositionWidget(MainForm parent) : base(parent)
		{
			this.ValueFormat = "{0}";
		}

		public override void Paint(Graphics g)
		{
			base.Paint(g);
			using (var region = new Region(Bounds)) {
				g.Clip = region;
				Painter.DrawText(g, this);
				g.ResetClip();
			}
		}
	}
}






