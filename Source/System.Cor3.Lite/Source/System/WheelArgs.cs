/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
namespace System.Windows.Forms
{
	public class WheelArgs : EventArgs
	{
	  int FlagControl { get { return ControlKey?0x01 : 0x00; } }
	  int FlagShift   { get { return ShiftKey ? 0x02 : 0x00; } }
	  int FlagAlt     { get { return AltKey?0x04 : 0x00; } }
	  
	  public int Flag { get { return FlagControl | FlagAlt | FlagShift; } }
	  
		public int Amount { get; set; }
		public bool ControlKey { get; set; }
		public bool ShiftKey { get; set; }
		public bool AltKey { get; set; }

		public WheelArgs(int amount, bool hasControl, bool hasShift, bool hasAlt)
		{
			Amount = amount;
			ControlKey = hasControl;
			ShiftKey = hasShift;
			AltKey = hasAlt;
		}
	}
}


