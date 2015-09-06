/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
	public class MuiBase : Form, IMui
	{
    public System.Drawing.Text.FontIndex FontIndex { get; protected set; }
    
    #region Timer
    
    virtual public IncrementUtil Incrementor {
      get { return incrementor; }
      set { incrementor = value; }
    } IncrementUtil incrementor = new IncrementUtil();

    public Timer AppTimer {
      get { return appTimer; }
      set { appTimer = value; }
    } Timer appTimer = new Timer() { Interval = 30 };
    
    public event EventHandler Tick {
      add { appTimer.Tick += value; }
      remove { appTimer.Tick -= value; }
    }
    
    virtual public void AppTimer_Tick(object sender, EventArgs e)
    {
      Incrementor.IncrementY();
      // none of the widgets implement this guy.
      foreach (var widget in Widgets)
        widget.Increment();
      // draw
      Invalidate();
    }
    
    #endregion
    
		public Widget[] Widgets { get; set; }

		public Widget FocusedControl { get; set; }

		public FloatPoint MouseD { get; set; }

		public FloatPoint MouseU { get; set; }

		public FloatPoint MouseM { get; set; }

		public bool HasControlKey {
			get { return hasControlKey; }
			set { hasControlKey = value; }
		} protected bool hasControlKey = false;

		// we should have an undo-redo state-machine
		// even though were not using it yet.
		// Rather than using 'object' as our state, we should be using
		// a derived type on IStateObject or such which will provide a
		// name of the action, oldvalue and newvalue.
		public System.Collections.Generic.Stack<object> StateMachine { get; set; }

		public FloatPoint ClientMouse {
			get { return new FloatPoint(PointToClient(MousePosition)) - new FloatPoint(Padding.Left, Padding.Top); }
		}

		#region Wheel Event
		public event EventHandler<WheelArgs> Wheel;

		protected virtual void OnWheel(int val)
		{
			var args = new WheelArgs(1, HasControlKey);
			var handler = Wheel;
			if (handler != null)
				handler(this, args);
		}

		protected void OnMouseWheel(object sender, MouseEventArgs e)
		{
			OnWheel(e.Delta > 0 ? 1 : -1);
		}

		#endregion
		#region Mouse Overrides
		protected override void OnMouseDown(MouseEventArgs e)
		{
			MouseD = MousePosition;
			base.OnMouseDown(e);
		}

		protected override void OnMouseUp(MouseEventArgs e)
		{
			MouseU = MousePosition;
			MouseD = null;
			base.OnMouseUp(e);
		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			MouseM = MousePosition;
			base.OnMouseMove(e);
		}

		#endregion
		#region Key Overrides
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			hasControlKey = e.Control;
		}

		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			hasControlKey = e.Control;
		}
	#endregion
	}
}




