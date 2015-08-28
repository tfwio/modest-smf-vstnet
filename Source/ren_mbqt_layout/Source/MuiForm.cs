/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;

namespace ren_mbqt_layout
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MuiForm : Form, IMui
  {
    public FontIndex FontIndex { get; protected set; }
    
    Timer appTimer = new Timer() { Interval = 10 };
    FloatRect myRect = new FloatRect(200, 100, 100, 100);
    IncrementUtil Incrementor = new IncrementUtil();
    
    public Widget[] Widgets { get; set; }
    
    public Widget FocusedControl { get; set; }
    
    public FloatPoint MouseD { get; set; }
    public FloatPoint MouseU { get; set; }
    public FloatPoint MouseM { get; set; }
    
    public bool HasControlKey {
      get { return hasControlKey; }
      set { hasControlKey = value; }
    }
    protected bool hasControlKey = false;
    
    // we should have an undo-redo state-machine
    // even though were not using it yet.
    // Rather than using 'object' as our state, we should be using
    // a derived type on IStateObject or such which will provide a
    // name of the action, oldvalue and newvalue.
    public Stack<object> StateMachine { get; set; }
    
    public FloatPoint ClientMouse { get { return new FloatPoint(PointToClient(MousePosition)) - new FloatPoint(Padding.Left, Padding.Top); } }
    
    #region Wheel Event
    
    public event EventHandler<WheelArgs> Wheel;
    
    protected virtual void OnWheel(int val)
    {
      var args = new WheelArgs(1, HasControlKey);
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
      hasControlKey = e.Control;
      base.OnKeyDown(e);
    }
    protected override void OnKeyUp(KeyEventArgs e)
    {
      hasControlKey = e.Control;
      base.OnKeyUp(e);
    }

    #endregion
    
    WidgetGroup Facto = null;
    
    public MuiForm()
    {
      FontIndex = new FontIndex();
      
      System.IO.FileInfo file;
      
      file = System.Reflection.Assembly.GetExecutingAssembly().GetAppFile("asset/adfx3.ttf");
      FontIndex.AddFamily(file, "adfx");
      
      file = System.Reflection.Assembly.GetExecutingAssembly().GetAppFile("asset/FontAwesome.ttf");
      FontIndex.AddFamily(file, "awesome");
      
      //      Font fontAwesome = FontIndex["awesome",12.0f];
      //      Font fontAdfx = FontIndex["adfx",9.0f];
      //      System.Diagnostics.Debug.Assert(fontAdfx!=null,"Font was NULL!");
      
      DoubleBuffered = true;
      
      MouseD = FloatPoint.Empty;
      MouseU = FloatPoint.Empty;
      MouseM = FloatPoint.Empty;
      
      Facto = new DefaultWidgetGroup();
      
      InitializeComponent();
      
      MouseWheel += OnMouseWheel;
      
      appTimer.Tick += AppTimer_Tick;
      appTimer.Start();
      
      var TopGridLoc = new FloatRect(10, 10, 100, 28);
      var DPadding = new Padding(4);
      
      float i = TopGridLoc.X + TopGridLoc.Width;
      var sliderrect = new FloatRect(460, 10, 200, 24);
      
      Widgets = new Widget[] {
        new ClockWidget(this) {
          Bounds = new FloatRect(40, 150, 200, 24),
          Padding = DPadding,
          //          ForegroundColor=Color.Green, // BackgroundColor (or brush) was intended but not currently possible
        },
      };
      
      Facto.Parent = this;
      Facto.Initialize();
    }
    
    public event EventHandler Tick {
      add { appTimer.Tick += value; }
      remove { appTimer.Tick -= value; }
    }
    
    private void AppTimer_Tick(object sender, EventArgs e)
    {
      this.Incrementor.IncrementY();
      // none of the widgets implement this guy.
      foreach (var widget in Widgets) widget.Increment();
      // draw
      Invalidate();
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      if (MouseM == null) return;
      e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      
      e.Graphics.Clear(Painter.DictColour[ColourClass.Dark50]);
      
      for (int i = 0; i < Widgets.Length; i++) Widgets[i].Paint(e.Graphics);
      Facto.Paint(e.Graphics);
    }
  }
  
}
