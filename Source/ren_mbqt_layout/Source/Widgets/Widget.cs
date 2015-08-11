/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
  public class Widget : WidgetBase<MainForm>
  {
    override public bool HasFocus {
      get { return Parent.FocusedControl == this; }
    }
    
    public bool SetFocus()
    {
      Parent.FocusedControl = this;
      return HasFocus;
    }
    
    public Widget(MainForm parent) : base(parent)
    {
    }
    
    public event EventHandler<WheelArgs> Wheel {
      add    { Parent.Wheel += value; }
      remove { Parent.Wheel -= value; }
    }
    
    public override bool HasClientMouseDown {
      get {
        return HasClientMouse && Parent.MouseD != null;
      }
    }
    protected void GetHasMouse(){
      Bounds.Contains(Parent.PointToClient(Parent.MouseM));
    }
    public override bool HasClientMouse {
      get { return Bounds.Contains(Parent.PointToClient(Parent.MouseM)); }
    }
    public override void Paint(Graphics g)
    {
      Painter.DrawBorder(g, this);
    }
  }
}




