/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
	public interface IMui
	{
	  System.Collections.Generic.List<MuiAppService> Services { get; set; }
	  
	  string Text { get; set; }
	  
	  int Width { get; set; }
	  int Height { get; set; }

	  Size Size { get; set; }
    Point Location { get; set; }
    
    Rectangle Bounds { get; set; }
	  Rectangle ClientRectangle { get; }
	  
    System.Drawing.Text.FontIndex FontIndex { get; }
	  
	  event EventHandler Tick;
	  
	  void AppTimer_Tick(object sender, EventArgs e);
	  
	  IncrementUtil Incrementor { get; set; }
	  
	  Point PointToClient(Point point);
	  
	  void Invalidate();
	  
	  void Invalidate(Region region);
	  
	  void Invalidate(Rectangle region);
	  
		Widget FocusedControl { get; set; }
    
		FloatPoint MouseD { get; set; }
    
		FloatPoint MouseU { get; set; }
    
		FloatPoint MouseM { get; set; }
    
		bool HasControlKey { get; set; }
		bool HasAltKey { get; set; }
		bool HasShiftKey { get; set; }
    
		Font Font { get; set; }
    
		FloatPoint ClientMouse { get; }

		// we should have an undo-redo state-machine
		// even though were not using it yet.
		// Rather than using 'object' as our state, we should be using
		// a derived type on IStateObject or such which will provide a
		// name of the action, oldvalue and newvalue.
		// Stack<object> StateMachine { get; set; }
		
		Widget[] Widgets { get; set; }
		
		event EventHandler Resize;
		event EventHandler ResizeBegin;
		event EventHandler ResizeEnd;
		event EventHandler SizeChanged;
		event EventHandler ClientSizeChanged;
		
		event KeyEventHandler KeyDown;
		
		event KeyEventHandler KeyUp;
		
		// Added Mouse Events
		
		event EventHandler<WheelArgs> Wheel;
		
		//void OnWheel(int val);
		
		// Mouse Events
		
		event EventHandler DoubleClick;
		
		event EventHandler Click;

		event MouseEventHandler MouseUp;

		event MouseEventHandler MouseDown;

		event MouseEventHandler MouseMove;

	}
}


