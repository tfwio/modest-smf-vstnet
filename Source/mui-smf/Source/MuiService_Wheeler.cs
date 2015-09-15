/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
	public class MuiService_Wheeler : MuiService<WidgetGroupMidiList>
	{
		public WidgetMidiList MidiList { get { return Client.MidiList; } }
		
		public gen.snd.Midi.MBT CurrentOffset {
			get;
			set;
		}

//		ContextMenu barMenu;

		public override void Register()
		{
			Client.ParentWheel += WidgetMidiList_Wheel;
		}

		public override void Unregister()
		{
			Client.ParentWheel -= WidgetMidiList_Wheel;
			Parent = null;
		}
		
		void WidgetMidiList_Wheel(object sender, WheelArgs e)
		{
			if (!Client.HasClientMouse)
				return;
			int mv = MidiList.MaxVisibleRows;
			switch (e.Flag) {
				case 0x03:
					MidiList.PixelsPerQuarterNote = (MidiList.PixelsPerQuarterNote + e.Amount).Contain(1, 64);
					break;
				case 0x02:
					MidiList.LineHeight = (MidiList.LineHeight + e.Amount).Contain(8, 100);
					break;
				case 0x01:
					MidiList.LineOffset += e.Amount * 12;
					break;
				default:
					MidiList.LineOffset += e.Amount;
					break;
			}
			MidiList.LineOffset = MidiList.LineOffset.Contain(5 - MidiList.MaxVisibleRows, 127);
			Client.Parent.Invalidate();
		}
	}
}








