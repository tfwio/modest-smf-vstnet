/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  /// <summary>
  /// We use this in place of interface
  /// </summary>
  public abstract class WidgetBase
  {
    static protected readonly Color DefaultBackgroundColor = Painter.DictColour[ColourClass.Dark40];
    
    bool IsInitialized { get; set; }
    
    public Color BackgroundColor { get; set; }
    
    /// <summary>
    /// Visible to rendering process.
    /// - Is within the boundary of the window.
    /// - Is marked as visible.
    /// </summary>
    public bool IsVisible { get; set; }
    
    /// <summary>
    /// Enable/Disable Enabled state.
    /// </summary>
    public bool Enabled { get; set; }
    
    public FloatPoint ClientMouse {
      get { return Parent.ClientMouse; }
    }
    /// <summary>
    /// We're dealing with ClientCoordinates here (relative to the window)
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool HitTest(Point point)
    {
      return Bounds.Contains(Parent.ClientMouse);
    }
    
    public WidgetGroup Container { get; set; }
    
    #region Color
    
    /// <summary>If set, overrides colourclass</summary>
    virtual public Color? ForegroundColor { get; set; }
    
    // <summary>If set, overrides colourclass</summary>
    //Color BackgroundColor { get; set; }
    
    virtual public bool HasMouseDown { get; set; }
    
    virtual public bool HasClientMouse { get { return false; } }
    
    virtual public bool HasClientMouseDown { get { return false; } }
    
    virtual public Color ColourFg { get { return ForegroundColor.HasValue ? ForegroundColor.Value : Painter.DictColour[ColourClassFg]; } }
    
    abstract public bool HasFocus { get; }
    
    public ColourClass ColourClassFg {
      get {
        if (HasMouseDown /* || HasClientMouseDown*/ ) return ColourClass.Active;
        if (HasFocus) return ColourClass.Focus;
        return HasClientMouse ? ColourClass.White : ColourClass.Default;
      }
    }
    
    #endregion
    
    #region event-Mouse
    
    protected event EventHandler DoubleClick;

    protected virtual void OnDoubleClick(EventArgs e)
    {
      var handler = DoubleClick;
      if (handler != null)
        handler(this, e);
    }
    protected event EventHandler Click;

    protected virtual void OnClick(EventArgs e)
    {
      var handler = Click;
      if (handler != null)
        handler(this, e);
    }

    protected event MouseEventHandler MouseDown;

    protected virtual void OnMouseDown(MouseEventArgs e)
    {
      var handler = MouseDown;
      if (handler != null)
        handler(this, e);
    }

    protected event MouseEventHandler MouseUp;

    protected virtual void OnMouseUp(MouseEventArgs e)
    {
      var handler = MouseUp;
      if (handler != null)
        handler(this, e);
    }

    protected event MouseEventHandler MouseMove;

    protected virtual void OnMouseMove(MouseEventArgs e)
    {
      var handler = MouseMove;
      if (handler != null)
        handler(this, e);
    }
    protected event EventHandler<WheelArgs> Wheel;

    protected virtual void OnWheel(WheelArgs e)
    {
      var handler = Wheel;
      if (handler != null)
        handler(this, e);
    }
    
    #endregion
    
    #region ParentEvents

    public event EventHandler ParentDoubleClick {
      add    { Parent.DoubleClick += value; }
      remove { Parent.DoubleClick -= value; }
    }
    
    public event EventHandler ParentClick {
      add    { Parent.Click += value; }
      remove { Parent.Click -= value; }
    }
    public event MouseEventHandler ParentMouseDown {
      add    { Parent.MouseDown += value; }
      remove { Parent.MouseDown -= value; }
    }
    public event MouseEventHandler ParentMouseUp {
      add    { Parent.MouseUp += value; }
      remove { Parent.MouseUp -= value; }
    }
    public event MouseEventHandler ParentMouseMove {
      add    { Parent.MouseMove += value; }
      remove { Parent.MouseMove -= value; }
    }
    public event EventHandler<WheelArgs> ParentWheel {
      add    { Parent.Wheel += value; }
      remove { Parent.Wheel -= value; }
    }
    virtual protected void WidgetButton_ParentClick(object sender, EventArgs e)
    {
      if (HasClientMouse) {
        OnClick(e);
        using (var rgn = new Region(this.Bounds))
          Parent.Invalidate(rgn);
      }
    }
    
    virtual protected void WidgetButton_ParentMouseDown(object sender, MouseEventArgs e)
    {
      if (HasClientMouse)
      {
        if (e.Button == MouseButtons.Left) OnMouseDown(e);
        this.HasMouseDown = true;
        OnMouseDown(e);
      }
      using (var rgn = new Region(this.Bounds)) Parent.Invalidate(rgn);
    }
    
    virtual protected void WidgetButton_ParentMouseUp(object sender, MouseEventArgs e)
    {
      this.HasMouseDown = false;
      if (HasClientMouse) using (var rgn = new Region(this.Bounds)) Parent.Invalidate(rgn);
      if (HasMouseDown || HasClientMouse) { OnMouseUp(e); }
    }

    virtual protected void WidgetButton_ParentMouseMove(object sender, MouseEventArgs e)
    {
      if (HasClientMouse || HasClientMouseDown)
      {
        OnMouseMove(e);
      }
      //      if (HasClientMouse)
      //      {
      //        using (var rgn = new Region(this.Bounds))
      //          Parent.Invalidate(rgn);
      //      }
      //        using (var rgn = new Region(this.Bounds))
      //          Parent.Invalidate(rgn);
    }
    
    #endregion
    
    #region Not Implemented
    
    public FloatPoint Offset { get; set; }

    // Not implemented; Should derive from parent 'activecontrol'
    bool IsActive { get; set; }
    
    // not implemented
    bool NeedsPaint { get; set; }

    #endregion
    
    protected WidgetBase()
    {
    }
    protected WidgetBase(IMui parent) : this()
    {
      this.Parent = parent;
      this.Initialize();
    }
    virtual public void Initialize()
    {
      this.ParentClick += WidgetButton_ParentClick;
      this.ParentMouseDown += WidgetButton_ParentMouseDown;
      this.ParentMouseUp += WidgetButton_ParentMouseUp;
      this.ParentMouseMove += WidgetButton_ParentMouseMove;;
    }

    #region Position
    
    virtual public FloatRect Bounds { get; set; }

    public Padding Padding { get; set; }

    public FloatRect PaddedBounds {
      get {
        if (Bounds == null)
          return null;
        var NewRect = FloatRect.ApplyPadding(Bounds, Padding);
        return NewRect;
      }
    }

    #endregion

    virtual public void Increment()
    {
    }

    virtual public void Increment<T>(T data)
    {
    }
    
    public IMui Parent { get; set; }

    virtual public string Text { get; set; }

    virtual public Font Font {
      get { return font ?? Parent.Font; }
      set { font = value; }
    }
    Font font;
    
    abstract public void Paint(PaintEventArgs arg);

    #region Value
    
    public object DoubleValue { get; set; }

    virtual public object Value { get; set; }

    public string ValueFormat {
      get { return valueFormat; }
      set { valueFormat = value; }
    }
    internal string valueFormat = "{0}";

    #endregion
    
  }
}






