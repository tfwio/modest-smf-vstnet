/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ren_mbqt_layout.Widgets;
namespace ren_mbqt_layout
{
	abstract public class ControlFactory
	{
		public IMui Parent {
			get;
			set;
		}

		virtual public Widget FocusedControl {
			get;
			set;
		}

		virtual public Widget[] Widgets {
			get;
			set;
		}

		// we should have an undo-redo state-machine
		// even though were not using it yet.
		// Rather than using 'object' as our state, we should be using
		// a derived type on IStateObject or such which will provide a
		// name of the action, oldvalue and newvalue.
		virtual public Stack<object> StateMachine {
			get;
			set;
		}

		abstract public void Initialize();

		abstract public void Paint(PaintEventArgs args);
	}
}




