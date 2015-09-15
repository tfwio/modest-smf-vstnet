/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
  
  public class MuiService_MbtCaret : MuiService<WidgetGroupMidiList>
  {
    public gen.snd.Midi.MBT CurrentOffset {
      get;
      set;
    }
    
    ContextMenu barMenu;
    
    public override void Register()
    {
      Client.Label_CaretInfo.MouseDown += Event_Context;
      Client.Button_MbtAdd.Click       += Event_OffsetMinus;
      Client.Button_MbtSubtract.Click  += Event_OffsetPlus;
    }
    public override void Unregister()
    {
      Client.Label_CaretInfo.MouseDown -= Event_Context;
      Client.Button_MbtAdd.Click       -= Event_OffsetMinus;
      Client.Button_MbtSubtract.Click  -= Event_OffsetPlus;
      Parent = null;
    }
    public override void Initialize(Widget widget)
    {
      base.Initialize(widget);
      barMenu = new ContextMenu(
        new MenuItem[]{
          new MenuItem(){ Text="Quarter-Note" },
          new MenuItem(){ Text="Whole-Note" },
          new MenuItem(){ Text="Bar" },
          new MenuItem(){ Text="Measure" }
        }
       );
    }
    void Event_Context(object sender, MouseEventArgs args)
    {
      if (args.Button==MouseButtons.Right) barMenu.Show(Program.AppForm,Program.AppForm.ClientMouse);
    }

    void Event_OffsetMinus(object sender, EventArgs args)
    {
      Client.Label_CaretInfo.Text = "MINUS";
    }

    void Event_OffsetPlus(object sender, EventArgs args)
    {
      Client.Label_CaretInfo.Text = "PLUS";
    }
  }
  
}






