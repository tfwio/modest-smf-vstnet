/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Windows.Forms;
using Mui.Widgets;
namespace Mui
{
  abstract public class WidgetGroup : Widget
  {
    /// <summary>
    /// Overrides layout engine to 'DockLayout' if set or not 'None'.
    /// </summary>
    public DockStyle    Dock { get; set; }
    
    /// <summary>
    /// This allows for the default 'SimpleLayout' engine,
    /// which is responsible for resizing our widgets.
    /// </summary>
    public AnchorStyles Anchor { get; set; }
    
    virtual public Widget FocusedControl { get; set; }

    virtual public Widget[] Widgets { get; set; }
    
    override public void Paint(System.Drawing.Graphics graphics)
    {
      for (int i = 0; i < Widgets.Length; i++)
        Widgets[i].Paint(graphics);
    }

    virtual public void Initialize(){
      if (Widgets == null) return;
      foreach (var widget in Widgets) widget.Container = this;
    }

    virtual public void Parent_Resize(object sender, EventArgs e)
    {
      DoLayout();
    }
    
    virtual public void DoLayout()
    {
      
    }
    
    virtual public void Initialize(IMui parent)
    {
      Parent = parent;
      Parent.Resize += Parent_Resize;
      Initialize();
    }
		
  }
}






