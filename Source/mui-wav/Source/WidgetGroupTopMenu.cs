/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_wav
{
  
  public class WidgetGroupTopMenu : WidgetGroup
  {
    public override void DoLayout()
    {
      base.DoLayout();
      Width = Parent.Size.Width;
      LeftToRight();
    }
    public override void Design()
    {
      Bounds = new FloatRect(4,4,Parent.Size.Width,32);
      Widgets = new Widget[]
      {
        new WidgetMouse(Parent) { Bounds = new FloatRect(4,0,140,Height), },
        new WidgetClock(Parent) { Bounds = new FloatRect(0,0, 200, Height), },
//        new WidgetButton(Parent) { Bounds = new FloatRect(0,0,100,Height), Text = "ASOME" },
        new WidgetButton(Parent) { Bounds = new FloatRect(0,0,Height,Height), Font=Parent.FontIndex["awesome",16.0f], Text=FontAwesome.Adjust, Smoother=true },
//        new WidgetButton(Parent) { Bounds = new FloatRect(0,0,150,Height), Text = "CSOME" },
        new WidgetSlideH(Parent) { Bounds = new FloatRect(0,0,250,Height), Text = "SLIDE", SliderValue = new DoubleMinMax() { Minimum = 0, Maximum = 1, Value = .5 } },
      };
    }
    public override void Initialize(IMui app, Widget client)
    {
      base.Initialize(app,client);
      
      LeftToRight();
    }
    
    
  }
}


