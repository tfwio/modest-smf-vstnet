/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
	public class WidgetLabel : Widget
	{
		protected override void WidgetButton_ParentMouseMove(object sender, MouseEventArgs e)
		{
//			base.WidgetButton_ParentMouseMove(sender, e);
		}

		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			this.SetFocus();
		}

		public WidgetLabel(IMui parent) : base(parent)
		{
		}

		internal static StringFormat PathStringFormat = new StringFormat() {
			Alignment = StringAlignment.Center,
			LineAlignment = StringAlignment.Center,
			FormatFlags = StringFormatFlags.DisplayFormatControl | StringFormatFlags.FitBlackBox,
			Trimming = StringTrimming.None
		};

		public override void Paint(PaintEventArgs arg)
		{
			//base.Paint(arg);
//			Painter.DrawBorder(arg.Graphics,this,4, new Pen(Color.FromArgb(0,127,255),2));
			
			using (var region = new Region(this.Bounds)) {
				arg.Graphics.Clip = region;
				Painter.DrawText(arg.Graphics, this, Smoother);
				arg.Graphics.ResetClip();
			}
		}
	}
}






