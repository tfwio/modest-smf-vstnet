/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
	public class MinMax<T>
	{
		virtual public T Minimum {
			get;
			set;
		}

		virtual public T Maximum {
			get;
			set;
		}

		virtual public T Value {
			get;
			set;
		}
	}
}








