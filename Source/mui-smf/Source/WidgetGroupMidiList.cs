/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Collections.Generic;
using System.Drawing;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
  
  public class WidgetGroupMidiList : WidgetGroup
  {
    const int ApplyPaddingBottom = 32, ApplyPaddingRight = 32;

    public WidgetMidiList MidiList { get; set; }
    
    public WidgetButton Button_MbtAdd { get; set; }
    public WidgetButton Button_MbtSubtract { get; set; }
    
    public WidgetLabel Label_MouseInfo { get; set; }
    public WidgetLabel Label_CaretInfo { get; set; }
    
    MuiService_MbtCaret CaretManager { get; set; }

    public WidgetGroupMidiList(IMui parent)
    {
      this.Parent = parent;
    }

    public override void DoLayout()
    {
      base.DoLayout();
      LeftToRight((i) => Widgets[i] is WidgetButton || Widgets[i] is WidgetLabel );
    }
    public override void Design()
    {
      Widgets = new Widget[] {
        Label_MouseInfo = new WidgetLabel(Parent) { Bounds = new FloatRect(Bounds.Left,Bounds.Top,200,32), Text="X = ?, Y = ?", Container=this },
        
        Button_MbtAdd = new WidgetButton(Parent) { Bounds = new FloatRect(Bounds.Left,Bounds.Top,60,32), Text="-", Container=this },
        Button_MbtSubtract = new WidgetButton(Parent) { Bounds = new FloatRect(Bounds.Left,Bounds.Top,60,32), Text="+", Container=this },
        Label_CaretInfo = new WidgetLabel(Parent) { Bounds = new FloatRect(Bounds.Left,Bounds.Top,160,32), Text="1 / 8 Meas", Container=this },
        
        MidiList = new WidgetMidiList(Parent) {
          Bounds = new FloatRect { X = Bounds.X, Y = Bounds.Y, Width = 1, Height = 1 },
          Font = new Font("FreeMono", 10.0f, FontStyle.Regular)
        }
      };
      
      Services = new List<MuiService>(){
        new MuiService_MidiGridMouse(),
        new MuiService_MbtCaret(),
        new MuiService_Wheeler(),
      };
    }
    public override void Initialize(IMui app, Widget client)
    {
      base.Initialize(app,client);
    }

    public override void Parent_Resize(object sender, EventArgs e)
    {
      Bounds.Width  = Parent.ClientRectangle.Width -  Bounds.Left - ApplyPaddingRight;
      Bounds.Height = Parent.ClientRectangle.Height - Bounds.Top  - ApplyPaddingBottom - ApplyPaddingBottom;
      base.Parent_Resize(sender, e);
    }
  }
}




