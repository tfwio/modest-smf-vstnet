/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
namespace ren_mbqt_layout
{
	public class WheelArgs : EventArgs
	{
		public int Amount {
			get;
			set;
		}
		public bool ControlKey {
			get;
			set;
		}

		public WheelArgs(int amount, bool hasControl)
		{
			Amount = amount;
			ControlKey = hasControl;
		}
	}
}


