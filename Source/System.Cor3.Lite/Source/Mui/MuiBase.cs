/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
  public class MuiBase : Form, IMui
  {
    public List<MuiAppService> Services {
      get { return services; }
      set { services = value; }
    } List<MuiAppService> services = new List<MuiAppService>();

    public System.Drawing.Text.FontIndex FontIndex { get; protected set; }

    public MuiBase() : base() {}

    protected internal void PreInitialize(int timerInterval = 90)
    {
      //
      // base settings
      // ---------------------------------------
      AppTimer.Interval = timerInterval;

      MouseD = FloatPoint.Empty;
      MouseU = FloatPoint.Empty;
      MouseM = FloatPoint.Empty;

      FontIndex = new System.Drawing.Text.FontIndex();

      AddLocalFont("adfx",    "asset/adfx3.ttf");
      AddLocalFont("awesome", "asset/fontawesome-webfont.ttf");
    }
    void PostInitialize(params MuiAppService[] serviso)
    {
      foreach (var windex in WidgetsIndexed)
        Widgets[windex].Initialize(this, null);
      
      Services.AddRange(serviso);
      MuiAppService.RegisterAll(this);
      MouseWheel += OnMouseWheel;
      AppTimer.Tick += AppTimer_Tick;
      AppTimer.Start();
    }
    protected internal void InitializeWidgets(params MuiAppService[] serviso)
    {
      PreInitialize();
      Design();
      PostInitialize(serviso);
    }

    virtual protected void Design()
    {
    }

    protected override void OnGotFocus(EventArgs e)
    {
      base.OnGotFocus(e);
      AppTimer.Enabled = true;
    }
    protected override void OnLostFocus(EventArgs e)
    {
      base.OnLostFocus(e);
      AppTimer.Enabled = false;
      Invalidate();
    }

    protected IEnumerable<int> WidgetsIndexed {
      get {
        for (int i = 0; i < this.Widgets.Length; i++)
          yield return i;
      }
    }
    protected IEnumerable<Widget> WidgetsEnumerated {
      get {
        for (int i = 0; i < this.Widgets.Length; i++)
          yield return Widgets[i];
      }
    }
    protected IEnumerable<KeyValuePair<int,Widget>> WidgetsDictionay {
      get {
        for (int i = 0; i < this.Widgets.Length; i++)
          yield return new KeyValuePair<int,Widget>(i, Widgets[i]);
      }
    }

    protected internal bool AddFont(string alias, string filePath, bool localToApp)
    {
      System.IO.FileInfo file;
      file = localToApp ? System.Reflection.Assembly.GetExecutingAssembly().GetAppFile(filePath) : new System.IO.FileInfo(filePath);
      
      if (!file.Exists)
        return false;
      
      FontIndex.AddFamily(file, alias);
      
      return true;
    }
    protected internal bool AddLocalFont(string alias, string relativePath)
    {
      return AddFont(alias, relativePath, true);
    }
    
    #region Timer
    
    virtual public IncrementUtil Incrementor {
      get { return incrementor; }
      set { incrementor = value; }
    }
    IncrementUtil incrementor = new IncrementUtil();

    public Timer AppTimer {
      get { return appTimer; }
      set { appTimer = value; }
    }
    Timer appTimer = new Timer() { Interval = 30 };
    
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
    }
    protected bool hasControlKey = false;

    public bool HasShiftKey {
      get { return hasShiftKey; }
      set { hasShiftKey = value; }
    }
    protected bool hasShiftKey = false;

    public bool HasAltKey {
      get { return hasAltKey; }
      set { hasAltKey = value; }
    }
    protected bool hasAltKey = false;

    // we should have an undo-redo state-machine
    // even though were not using it yet.
    // Rather than using 'object' as our state, we should be using
    // a derived type on IStateObject or such which will provide a
    // name of the action, oldvalue and newvalue.
    public Stack<object> StateMachine { get; set; }

    public FloatPoint ClientMouse {
      get { return new FloatPoint(PointToClient(MousePosition)) - new FloatPoint(Padding.Left, Padding.Top); }
    }

    protected override void OnClientSizeChanged(EventArgs e)
    {
      base.OnClientSizeChanged(e);
      Invalidate();
    }

    #region Wheel Event

    public event EventHandler<WheelArgs> Wheel;

    protected virtual void OnWheel(int val)
    {
      var args = new WheelArgs(val, HasControlKey, HasShiftKey, HasAltKey);
      var handler = Wheel;
      if (handler != null)
        handler(this, args);
    }

    protected void OnMouseWheel(object sender, MouseEventArgs e)
    {
      int result = e.Delta > 0 ? 1 : -1;
      OnWheel(result);
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
      hasShiftKey = e.Shift;
      hasControlKey = e.Control;
      hasAltKey = e.Alt;
    }

    protected override void OnKeyUp(KeyEventArgs e)
    {
      base.OnKeyUp(e);
      hasShiftKey = e.Shift;
      hasControlKey = e.Control;
      hasAltKey = e.Alt;
    }
    
    #endregion
  }
}




