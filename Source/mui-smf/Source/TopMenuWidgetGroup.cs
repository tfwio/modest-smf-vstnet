/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
  
	public class TopMenuWidgetGroup : WidgetGroup
	{
    public override void DoLayout()
    {
      base.DoLayout();
      Width = Parent.Size.Width;
      LeftToRight();
    }
		public override void Initialize()
		{
		  base.Initialize();
		  
		  if (Bounds==null) Bounds = new FloatRect(4,4,Parent.Size.Width,48);
			var DPadding = new Padding(4);
			
			Widgets = new Widget[]
			{
				new WidgetMouse(Parent) {
			    Bounds = new FloatRect(4,0,140,Height),
					Padding = DPadding
				},
        new WidgetClock(Parent) {
          Bounds = new FloatRect(0,0, 200, Height),
          Padding = DPadding,
        },
				new WidgetButton(Parent) {
					Padding = DPadding,
			    Bounds = new FloatRect(0,0,100,Height),
					Text = "ASOME"
				},
				new WidgetButton(Parent) {
					Padding = DPadding,
			    Bounds = new FloatRect(0,0,48,Height),
					Font=Parent.FontIndex["awesome",18f],
					Text=FontAwesome.Adjust
				},
				new WidgetButton(Parent) {
					Padding = DPadding,
			    Bounds = new FloatRect(0,0,150,Height),
					Text = "CSOME"
				},
				new WidgetSlideH(Parent) {
					Padding = DPadding,
			    Bounds = new FloatRect(0,0,250,Height),
					Text = "SLIDE",
					SliderValue = new DoubleMinMax() { Minimum = 0, Maximum = 1, Value = .5 }
				},
			};
			LeftToRight();
		}
		
		
	}
}


