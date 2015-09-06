/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
  abstract public class WidgetGroup : Widget
  {
    protected void TopToBottom(params Action<Widget>[] widgetAction) {
      foreach (var i in EnumerateWidgetIndex()) {
        Widgets[i].Y = i == 0 ? Convert.ToInt32(Y) : Convert.ToInt32(Gap + Widgets[i - 1].Bounds.Bottom);
        if (widgetAction ==null) foreach (var act in widgetAction) act(Widgets[i]);
      }
      
    }
	  protected void LeftToRight(params Action<Widget>[] widgetAction) {
      foreach (var i in EnumerateWidgetIndex()) {
        Widgets[i].X = i == 0 ? Convert.ToInt32(X) : Convert.ToInt32(Gap + Widgets[i - 1].Bounds.Right);
        if (widgetAction ==null) foreach (var act in widgetAction) act(Widgets[i]);
      }
    }
	  
    virtual public float Gap { get; set; }
    
    protected System.Collections.Generic.IEnumerable<int> EnumerateWidgetIndex() { for (int i = 0; i < this.Widgets.Length; i++) yield return i; }
    protected System.Collections.Generic.IEnumerable<Widget> EnumerateWidgets() { for (int i = 0; i < this.Widgets.Length; i++) yield return Widgets[i]; }
    protected System.Collections.Generic.IEnumerable<System.Collections.Generic.KeyValuePair<int,Widget>> EnumerateWidgetsWithIndex() { for (int i = 0; i < this.Widgets.Length; i++) yield return new System.Collections.Generic.KeyValuePair<int,Widget>(i,Widgets[i]); }
    
    #region Layout Constraints
    
    /// <summary>
    /// <para>
    /// Overrides layout engine to 'DockLayout' if set or not 'None'.
    /// </para>
    /// (when implemented)
    /// </summary>
    public DockStyle    Dock { get; set; }
    
    /// <summary>
    /// <para>
    /// This allows for the default 'SimpleLayout' engine,
    /// which is responsible for resizing our widgets.
    /// </para>
    /// <para>
    /// Generally, this should always be set.
    /// If Dock is set, then it will override settings here.
    /// </para>
    /// (when implemented)
    /// </summary>
    public AnchorStyles Anchor { get; set; }
    
    /// <summary>
    /// <para>Affects layout.</para>
    /// (when implemented)
    /// </summary>
    public AlignHorizontal AlignH { get; set; }
    
    /// <summary>
    /// <para>Affects layout.</para>
    /// (when implemented)
    /// </summary>
    public AlignVertical AlignV { get; set; }
    
    #endregion
    
    /// <summary>
    /// We would like to retain the focused control if for example, we switch
    /// Widget-groups in a tab-like control.
    /// </summary>
    virtual public Widget FocusedControl { get; set; }

    virtual public Widget[] Widgets { get; set; }
    
    override public void Paint(PaintEventArgs arg)
    {
      foreach (var i in EnumerateWidgetIndex())
        Widgets[i].Paint(arg);
    }

    virtual public void Parent_Resize(object sender, EventArgs e) { DoLayout(); }
    
    virtual public void DoLayout() { }

    override public void Initialize(){
      Parent.Resize += Parent_Resize;
      if (Widgets == null) return;
      foreach (var widget in EnumerateWidgets()) widget.Container = this;
      DoLayout();
    }
    
    virtual public void Initialize(IMui parent)
    {
      Parent = parent;
      Initialize();
    }
		
  }
}






