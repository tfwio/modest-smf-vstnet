/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using Mui.Widgets;
namespace Mui
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
				SliderValue.Maximum = Parent.Bounds.Height;
			}
			catch {
			}
			finally {
			}
		}
	}
}








