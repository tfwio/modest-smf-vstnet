/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
  
  abstract public partial class WidgetGroup : Widget
  {
    protected internal void TopToBottom(params Action<Widget>[] widgetAction) {
      foreach (var i in WidgetsIndexed) {
        Widgets[i].Y = i == 0 ? Convert.ToInt32(Y) : Convert.ToInt32(Gap + Widgets[i - 1].Bounds.Bottom);
        if (widgetAction ==null) foreach (var act in widgetAction) act(Widgets[i]);
      }
    }
    protected internal void LeftToRight(Func<int,bool> filterAction, params Action<Widget>[] widgetAction)
    {
      foreach (var i in WidgetsIndexed)
      {
        if (!filterAction(i)) continue;
        Widgets[i].X = i == 0 ? Convert.ToInt32(X) : Convert.ToInt32(Gap + Widgets[i - 1].Bounds.Right);
        if (widgetAction ==null) foreach (var act in widgetAction) act(Widgets[i]);
        
      }
    }
    protected internal void LeftToRight(params Action<Widget>[] widgetAction) {
      foreach (var i in WidgetsIndexed)
      {
        Widgets[i].X = i == 0 ? Convert.ToInt32(X) : Convert.ToInt32(Gap + Widgets[i - 1].Bounds.Right);
        if (widgetAction ==null) foreach (var act in widgetAction) act(Widgets[i]);
        
      }
    }
    
    virtual public float Gap {
      get { return gap; }
      set { gap = value; }
    } float gap = 0F;
    
    /// <summary>
    /// We would like to retain the focused control if for example, we switch
    /// Widget-groups in a tab-like control.
    /// </summary>
    virtual public Widget FocusedControl { get; set; }
    
    override public void Paint(PaintEventArgs arg)
    {
      foreach (var i in WidgetsIndexed) Widgets[i].Paint(arg);
    }

    virtual public void Parent_Resize(object sender, EventArgs e) { DoLayout(); }
    
    virtual public void DoLayout() { }

    override public void Initialize(IMui parent, Widget client)
    {
      base.Initialize(parent,client);
      
      Parent.Resize    += Parent_Resize;
      Parent.ResizeEnd += Parent_Resize;
      DoLayout();
    }

  }
}






