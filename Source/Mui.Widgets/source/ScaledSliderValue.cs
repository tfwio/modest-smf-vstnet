/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
using ren_mbqt_layout.Widgets;
namespace ren_mbqt_layout
{
	public class ScaledSliderValue
	{
		internal Widget Parent {
			get;
			set;
		}

		public DoubleMinMax SliderValue {
			get;
			set;
		}

		public void BindToWidget(Widget widget)
		{
			Parent = widget;
			BindToWidget();
		}

		public void BindToWidget()
		{
			SliderValue.Minimum = 0;
			try {
				SliderValue.Maximum = Parent.Bounds.Width;
			}
			catch {
			}
			finally {
			}
		}
	}
}








