/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Windows.Forms;
using Mui;
namespace mui_smf
{
  public class MuiService_MidiGridMouse : MuiService<WidgetGroupMidiList>
  {
    public override void Register()
    {
      Client.Parent.MouseMove += Event_MouseMove;
    }

    public override void Unregister()
    {
      Client.Parent.MouseMove -= Event_MouseMove;
      Parent = null;
    }

    int PointToOffset(FloatPoint input)
    {
      var pcl1 = (FloatPoint)Client.PointToClient(input);
      var pcl2 = pcl1 - Client.MidiList.GridRect.Location;
      return Math.Floor(pcl2.Y / Client.MidiList.LineHeight).ToInt32();
    }

    double PointToRow(FloatPoint input, int offset)
    {
      return 127 - Client.MidiList.LineOffset - offset;
    }
    
    void Event_MouseMove(object sender, MouseEventArgs e)
    {
      var pcl1 = (FloatPoint)Client.Parent.PointToClient(Client.Parent.MouseM);
      int offset = PointToOffset( Client.Parent.MouseM );
      
      var pcl2 = pcl1 - Client.MidiList.GridRect.Location;
      var r0 = PointToRow(Client.Parent.MouseM, offset);
      var r1 = r0.MinMax(-1,128);
      var r2 = r1.MinMax(0,127);

      var str = string.Format(
        "R={0}, Q={1}, N={2}, Q={3}, B={4}",
        r1.Equals(r2) ? r1.ToString() : "?",
        Math.Floor(pcl2.X / Client.MidiList.PixelsPerQuarterNote) + 1,
        Math.Floor(pcl2.X / Client.MidiList.PixelsPerNote) + 1,
        Math.Floor(pcl2.X / Client.MidiList.PixelsPerQuarter) + 1,
        Math.Floor(pcl2.X / Client.MidiList.PixelsPerBar) + 1
       );
      str = Client.MidiList.GridRect.Contains(pcl1) ? str : "?";
      Client.Label_MouseInfo.Text = str;
    }
  }
}








