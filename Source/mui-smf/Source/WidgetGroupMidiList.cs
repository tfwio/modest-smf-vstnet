/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
	public class WidgetGroupMidiList : WidgetGroup
	{
		const int ApplyPaddingBottom = 32;

		const int ApplyPaddingRight = 32;

		WidgetMidiList midilist = null;

		int FormRight, FormBottom;

		public WidgetGroupMidiList(IMui parent)
		{
			this.Parent = parent;
		}

		public override void Initialize()
		{
			Widgets = new Widget[] {
				midilist = new WidgetMidiList(Parent) {
					Bounds = new FloatRect {
						X = Bounds.X,
						Y = Bounds.Y,
						Width = 1,
						Height = 1
					},
					Font = new Font(Font.FontFamily, 10.0f, FontStyle.Regular)
				}
			};
			base.Initialize();
		}

		public override void Parent_Resize(object sender, EventArgs e)
		{
			FormRight = Parent.Size.Width;
			FormBottom = Parent.Size.Height;
			Bounds.Width = FormRight - Bounds.Left - ApplyPaddingRight;
			Bounds.Height = FormBottom - Bounds.Top - ApplyPaddingBottom - ApplyPaddingBottom;
			base.Parent_Resize(sender, e);
		}
	}
}




