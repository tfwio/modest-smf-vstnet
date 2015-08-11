/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using ren_mbqt_layout.Widgets;

namespace ren_mbqt_layout
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MainForm : Form
  {
    Timer         appTimer = new Timer() { Interval = 100 };
    FloatRect     myRect = new FloatRect(200,100,100,100);
    IncrementUtil Incrementor = new IncrementUtil();
    Widget[]      Widgets { get; set; }
    
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
    public Stack<object> StateMachine;
    
    public FloatPoint ClientMouse { get { return new FloatPoint(PointToClient(MousePosition)) - new FloatPoint(Padding.Left,Padding.Top); } }
    
    #region Wheel Event
    
    public event EventHandler<WheelArgs> Wheel;
	
    protected virtual void OnWheel(int val)
    {
      var args = new WheelArgs(1,HasControlKey);
      var handler = Wheel;
      if (handler != null) handler(this, args);
    }
    
    #endregion
    
    #region Mouse Overrides
    
    protected void OnMouseWheel(object sender, MouseEventArgs e)
    {
      OnWheel(e.Delta > 0 ? 1 : -1);
      //      if (HasControlKey) {
      //        OffsetX = (e.Delta > 0) ? OffsetX + 1 : OffsetX - 1;
      //        if (OffsetX <= 0) OffsetX = 0;
      //      } else {
      //        this.OffsetY = (e.Delta > 0) ? OffsetY + 1 : OffsetY - 1;
      //        if (OffsetY > 127) this.OffsetY = 127;
      //        else if (this.OffsetY <= 0) this.OffsetY = 0;
      //      }
    }
    
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
    
    public MainForm()
    {
      InitializeComponent();
      
      DoubleBuffered = true;
      MouseWheel += OnMouseWheel;
      
      appTimer.Tick += AppTimer_Tick;
      appTimer.Start();
      
      var TopGridLoc = new FloatRect(10,10,100,28);
      var DPadding = new Padding(4);
      
      Widgets = new Widget[]
      {
        new MousePositionWidget(this){ Bounds=TopGridLoc, Padding=DPadding },
        new ButtonWidget(this){ Padding=DPadding, Bounds=TopGridLoc.Clone(), Text="BSOME" },
        new ButtonWidget(this){ Padding=DPadding, Bounds=TopGridLoc.Clone(), Text="3SOME" },
        new ClockWidget(this) { Bounds=new FloatRect(40, 150, 200, 24), Padding=DPadding },
      };
      
      Widgets[1].Bounds.X += 100;
      Widgets[2].Bounds.X += 200;
    }
    
    private void AppTimer_Tick(object sender, EventArgs e)
    {
      this.Incrementor.IncrementY();
      foreach (var widget in Widgets) widget.Increment();
      Invalidate();
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.Clear(Painter.DictColour[ColourClass.Dark50]);
      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
      
      
      Brush fontColour = Painter.DictBrush[ColourClass.White];
      if (MouseM==null) return;
//      Incrementor.PCoords.X = 30;
//      e.Graphics.DrawString(string.Format("X: {0,-7}\nY: {1,-7}", ClientMouse.X, ClientMouse.Y), this.Font, fontColour, Incrementor.PCoords);
//
//      Incrementor.PCoords.X += 100;
//      e.Graphics.DrawString(string.Format("X: {0,-7}\nY: {1,-7}", MouseM.X, MouseM.Y), this.Font, fontColour, Incrementor.PCoords);
//
//      Incrementor.PCoords.X += 100;
//      e.Graphics.DrawString("BSOME", this.Font, fontColour, Incrementor.PCoords);
//
//      Incrementor.PCoords.X += 100;
//      e.Graphics.DrawString("3SOME", this.Font, fontColour, Incrementor.PCoords);
//
//      Incrementor.PCoords.X += 100;
//      e.Graphics.DrawString("XSOME", this.Font, fontColour, Incrementor.PCoords);
//
//      Incrementor.PCoords.X += 100;
//      e.Graphics.DrawString("Some Text", this.Font, fontColour, Incrementor.PCoords);
//
//      Incrementor.PCoords.X += 100;
//      e.Graphics.DrawString("Some Text", this.Font, fontColour, Incrementor.PCoords);
//
//      Incrementor.PCoords.X += 100;
//      e.Graphics.DrawString("Some Text", this.Font, fontColour, Incrementor.PCoords);
      
      e.Graphics.DrawPie(Painter.DictPen[ColourClass.Default], myRect, 45, 180);
      
      for (int i = 0; i < Widgets.Length; i++) Widgets[i].Paint(e.Graphics);
      
    }
  }
}
