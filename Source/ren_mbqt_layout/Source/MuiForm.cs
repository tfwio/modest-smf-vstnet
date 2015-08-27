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
    Timer         appTimer = new Timer() { Interval = 90 };
    FloatRect     myRect = new FloatRect(200,100,100,100);
    IncrementUtil Incrementor = new IncrementUtil();
    
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
    public Stack<object> StateMachine { get; set; }
    
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
    
    SimpleWidgetGroup Facto = new DefaultWidgetGroup();
    PrivateFontCollection LocalFonts = new System.Drawing.Text.PrivateFontCollection();
    
    public MuiForm()
    {
      var asm = System.Reflection.Assembly.GetExecutingAssembly();
      var finf = new System.IO.FileInfo(asm.Location);
      var finf2 = new System.IO.FileInfo(System.IO.Path.Combine(finf.Directory.FullName,"asset/adfx3.ttf"));
      System.Diagnostics.Debug.Print(finf2.FullName);
      DoubleBuffered = true;
      MouseD = FloatPoint.Empty;
      MouseU = FloatPoint.Empty;
      MouseM = FloatPoint.Empty;
      
      InitializeComponent();
      this.Font = LocalFonts.GetFontResource(finf2,Font.Size);
      
      MouseWheel += OnMouseWheel;
      
      appTimer.Tick += AppTimer_Tick;
      appTimer.Start();
      
      var TopGridLoc = new FloatRect(10,10,100,28);
      var DPadding = new Padding(4);
      
      float i=TopGridLoc.X + TopGridLoc.Width;
      var sliderrect=new FloatRect( 460, 10, 200, 24);
      
      Widgets = new Widget[]
      {
        new ClockWidget(this) { Bounds=new FloatRect(40, 150, 200, 24), Padding=DPadding },
      };
      
      
      Facto.Parent = this;
      Facto.Initialize();
    }
    
    private void AppTimer_Tick(object sender, EventArgs e)
    {
      this.Incrementor.IncrementY();
      foreach (var widget in Widgets) widget.Increment();
      Invalidate();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
      if (MouseM==null) return;
      
      e.Graphics.Clear(Painter.DictColour[ColourClass.Dark50]);
      
//      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
//      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
//      e.Graphics.DrawPie(Painter.DictPen[ColourClass.Default], myRect, 45, 180);
      
      for (int i = 0; i < Widgets.Length; i++) Widgets[i].Paint(e.Graphics);
      Facto.Paint(e);
    }
  }
  
}
