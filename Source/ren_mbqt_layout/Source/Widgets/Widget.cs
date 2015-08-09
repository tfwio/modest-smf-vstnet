/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
  public class Widget : WidgetBase<MainForm>
  {
    public Widget(MainForm parent) : base(parent)
    {
    }
    
    public override bool HasMouseDown {
      get {
        return HasMouse && Parent.MouseD != null;
      }
    }
    
    public override bool HasMouse {
      get {
        return Bounds.Contains(Parent.PointToClient(Parent.MouseM));
      }
    }
    public override void Paint(Graphics g)
    {
      Painter.DrawBorder(g, this);
    }
  }
  
}




