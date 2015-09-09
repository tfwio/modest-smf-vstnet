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

		WidgetMidiList midilist { get; set; }
		Widget Label1 { get; set; }

    int FormRight, FormBottom;

		public WidgetGroupMidiList(IMui parent)
		{
			this.Parent = parent;
		}

		public override void Initialize()
		{
			Widgets = new Widget[] {
        Label1 = new WidgetButton(Parent) { Bounds = new FloatRect(Bounds.Left,Bounds.Top,200,32), Text="X = ?, Y = ?", Container=this },
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
      Parent.MouseMove += Event_MouseMove;
			base.Initialize();
		}
    int PointToOffset(FloatPoint input)
    {
      var pcl1 = (FloatPoint)Parent.PointToClient(input);
      var pcl2 = pcl1 - midilist.GridRect.Location;
      return Math.Floor(pcl2.Y / midilist.LineHeight).ToInt32();
    }
    double PointToRow(FloatPoint input, int offset)
    {
      return 127 - midilist.LineOffset - offset;
    }
    protected void Event_MouseMove(object sender, MouseEventArgs e)
    {
      var pcl1 = (FloatPoint)Parent.PointToClient(Parent.MouseM);

      int offset = PointToOffset(Parent.MouseM);

      var pcl2 = pcl1 - midilist.GridRect.Location;
      var r0 = PointToRow(Parent.MouseM, offset);
      var r1 = r0.MinMax(-1,128);
      var r2 = r1.MinMax(0,127);

      var str = string.Format("R={0}, Q={1}, N={2}, Q={3}, B={4}",
        r1==r2 ? r1.ToString() : "?",
        Math.Floor(pcl2.X / midilist.PixelsPerQuarterNote) + 1,
        Math.Floor(pcl2.X / midilist.PixelsPerNote) + 1,
        Math.Floor(pcl2.X / midilist.PixelsPerQuarter) + 1,
        Math.Floor(pcl2.X / midilist.PixelsPerBar) + 1
        );
      str = midilist.GridRect.Contains(pcl1) ? str : "?";
      Label1.Text = str;
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




