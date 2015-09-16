/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  
  public abstract partial class Widget
  {
    public Widget Container { get; set; }
    public IMui Parent { get; set; }
    
    virtual public string Text { get; set; }
    
    public List<MuiService> Services {
      get { return services; }
      set { services = value; }
    } List<MuiService> services = new List<MuiService>();
    
    protected internal IEnumerable<int> ServicesIndexed {
      get { for (int i = 0; i < Services.Count; i++) yield return i; }
    }
    #region Value
    
    public object DoubleValue { get; set; }

    virtual public object Value { get; set; }

    public string ValueFormat {
      get { return valueFormat; }
      set { valueFormat = value; }
    }
    internal string valueFormat = "{0}";

    virtual public Widget[] Widgets {
      get { return widgets; }
      set { widgets = value; }
    } Widget[] widgets = new Widget[0];

    #endregion
    
    public Widget(IMui parent) {
      //this.Parent = parent;
      //Initialize(parent,null);
    }
    public Widget() {
    }
    
    protected IEnumerable<int> WidgetsIndexed { get { for (int i = 0; i < this.Widgets.Length; i++) yield return i; } }
    protected IEnumerable<Widget> WidgetsEnumerated { get { for (int i = 0; i < this.Widgets.Length; i++) yield return Widgets[i]; } }
    protected IEnumerable<KeyValuePair<int,Widget>> WidgetsDictionary { get { for (int i = 0; i < this.Widgets.Length; i++) yield return new KeyValuePair<int,Widget>(i,Widgets[i]); } }
    
    virtual public bool HasFocus {
      get { return Parent.FocusedControl == this; }
    }
    
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
    
    protected internal bool IsInitialized { get; set; }
    
    /// <summary>
    /// In client coordinates.
    /// </summary>
    public FloatPoint ClientMouse { get { return Parent.ClientMouse; } }
    /// <summary>
    /// We're dealing with ClientCoordinates here (relative to the window)
    /// </summary>
    /// <param name="point"></param>
    /// <returns></returns>
    public bool HitTest(Point point) { return Bounds.Contains(Parent.ClientMouse); }
    public FloatPoint PointToClient(FloatPoint point) { FloatPoint p1 = Parent.PointToClient(point); p1 = Bounds.Location-p1; return p1; }
    
    
    virtual public void Increment() {}
    virtual public void Increment<T>(T data) {}
    virtual public void Design() {}
    virtual public void Initialize(IMui parent, Widget client)
    {
      Parent = parent;
      Container = client;
      
      if (IsInitialized)
        System.Diagnostics.Debug.Print("{0}=>{1}", Container==null ? Parent.ToString() : Container.ToString(),this);
      
      this.ParentClick += WidgetButton_ParentClick;
      this.ParentMouseDown += WidgetButton_ParentMouseDown;
      this.ParentMouseUp += WidgetButton_ParentMouseUp;
      this.ParentMouseMove += WidgetButton_ParentMouseMove;
      
      Design();
      
      foreach (var widget in WidgetsIndexed) {
        Widgets[widget].Initialize(parent,this);
      }
      foreach (var sindex in ServicesIndexed) {
        Services[sindex].Initialize(this);
        Services[sindex].Register();
      }
      
      IsInitialized = true;
    
    }
    virtual public void Uninitialize(IMui parent, Widget client)
    {
      this.ParentClick     -= WidgetButton_ParentClick;
      this.ParentMouseDown -= WidgetButton_ParentMouseDown;
      this.ParentMouseUp   -= WidgetButton_ParentMouseUp;
      this.ParentMouseMove -= WidgetButton_ParentMouseMove;
      
      foreach (var sindex in ServicesIndexed)
        Services[sindex].Unregister();
      
      foreach (var i in WidgetsIndexed)
      {
        Widgets[i].Uninitialize(parent,client);
        Widgets[i] = null;
      }
      
      Widgets = null;
      IsInitialized  = false;
    }
    
    public bool SetFocus()
    {
      Parent.FocusedControl = this;
      return HasFocus;
    }
    #region Not Implemented
    
    public FloatPoint Offset { get; set; }

    // Not implemented; Should derive from parent 'activecontrol'
    bool IsActive { get; set; }
    
    // not implemented
    bool NeedsPaint { get; set; }

    #endregion
    
  }
}




