/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace ren_mbqt_layout
{
  
	public class DefaultWidgetGroup : WidgetGroup
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
		  
		  if (Bounds==null) Bounds = new FloatRect(0,0,Parent.Size.Width,48);
			var DPadding = new Padding(4);
			
			Widgets = new Widget[]
			{
				new MousePositionWidget(Parent) {
			    Bounds = new FloatRect(0,0,140,Height),
					Padding = DPadding
				},
        new ClockWidget(Parent) {
          Bounds = new FloatRect(0,0, 200, Height),
          Padding = DPadding,
        },
				new ButtonWidget(Parent) {
					Padding = DPadding,
			    Bounds = new FloatRect(0,0,100,Height),
					Text = "ASOME"
				},
				new ButtonWidget(Parent) {
					Padding = DPadding,
			    Bounds = new FloatRect(0,0,48,Height),
					Font=Parent.FontIndex["awesome",18f],
					Text=Convert.ToChar(uint.Parse("f00b",System.Globalization.NumberStyles.HexNumber)).ToString()
				},
				new ButtonWidget(Parent) {
					Padding = DPadding,
			    Bounds = new FloatRect(0,0,150,Height),
					Text = "CSOME"
				},
				new SliderWidget(Parent) {
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


