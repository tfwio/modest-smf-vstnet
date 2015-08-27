/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
	abstract public class SimpleWidgetGroup
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
		// virtual public Stack<object> StateMachine { get; set; }
		virtual public void Paint(PaintEventArgs args)
		{
			for (int i = 0; i < Widgets.Length; i++)
				Widgets[i].Paint(args.Graphics);
		}

		abstract public void Initialize();
	}
}






