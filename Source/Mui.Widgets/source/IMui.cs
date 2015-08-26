/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ren_mbqt_layout.Widgets;
namespace ren_mbqt_layout
{
	public interface IMui
	{
	  Point PointToClient(Point point);
	  void Invalidate();
	  void Invalidate(Region region);
	  void Invalidate(Rectangle region);
	  
		Widget FocusedControl { get; set; }

		FloatPoint MouseD { get; set; }

		FloatPoint MouseU { get; set; }

		FloatPoint MouseM { get; set; }

		bool HasControlKey { get; set; }

		Font Font { get; set; }

		FloatPoint ClientMouse { get; }

		// we should have an undo-redo state-machine
		// even though were not using it yet.
		// Rather than using 'object' as our state, we should be using
		// a derived type on IStateObject or such which will provide a
		// name of the action, oldvalue and newvalue.
		// Stack<object> StateMachine { get; set; }
		
		Widget[] Widgets { get; set; }
		
		event EventHandler<WheelArgs> Wheel;

		event EventHandler Click;

		event MouseEventHandler MouseUp;

		event MouseEventHandler MouseDown;

		event MouseEventHandler MouseMove;

	}
}


