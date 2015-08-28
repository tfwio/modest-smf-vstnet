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
		public override void Initialize()
		{
		  base.Initialize();
		  
		  if (Bounds==null) Bounds = FloatRect.Zero;
		  
		  float height = 48;
			float top = Bounds.Top;
			
			var TopGridLoc = new FloatRect(10, top, 100, height);
			
			var DPadding = new Padding(4);
			
			float i = TopGridLoc.X + TopGridLoc.Width;
			
			var sliderrect = new FloatRect(460, top, 200, height);
			Widgets = new Widget[] {
				new MousePositionWidget(Parent) {
					Bounds = TopGridLoc,
					Padding = DPadding
				},
				new ButtonWidget(Parent) {
					Padding = DPadding,
					Bounds = new FloatRect(i, top, 100, height),
					Text = "ASOME"
				},
				new ButtonWidget(Parent) {
					Padding = DPadding,
					Bounds = new FloatRect(i = i + 100, top, 50, height),
					Font=Parent.FontIndex["awesome",24f],
//					Font=new Font("FontAwesome",24),
//					Text=Convert.ToChar(uint.Parse("f00d",System.Globalization.NumberStyles.HexNumber)).ToString(),
					Text="g"
				},
				new ButtonWidget(Parent) {
					Padding = DPadding,
					Bounds = new FloatRect(i = i + 50, top, 100, height),
					Text = "CSOME"
				},
				new SliderWidget(Parent) {
					Padding = DPadding,
					Bounds = sliderrect,
					Text = "SLIDE",
					SliderValue = new DoubleMinMax() {
						Minimum = 0,
						Maximum = 1,
						Value = .5
					}
				},
			};
			Widgets[4].Bounds.X = (i = i + 100);
		}
	
	}
}


