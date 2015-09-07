/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class Widget : WidgetBase
  {
    
    public Widget(IMui parent) : base(parent) {
    }
    public Widget() {
    }
    
    #region Logical Mouse
    
    // disable once AccessToStaticMemberViaDerivedType
    protected internal FloatPoint Mouse { get { return Parent.PointToClient(Form.MousePosition); } }
    
    public FloatPoint PointToClient(FloatPoint point)
    {
      FloatPoint p1 = Parent.PointToClient(point);
      p1 = Bounds.Location-p1;
      return p1;
    }
    
    protected void GetHasMouse(){
      Bounds.Contains(Parent.PointToClient(Parent.MouseM));
    }
    
    public override bool HasClientMouseDown {
      get { return HasClientMouse && Parent.MouseD != null; }
    }
    
    override public bool HasFocus {
      get { return Parent.FocusedControl == this; }
    }
    
    public bool SetFocus()
    {
      Parent.FocusedControl = this;
      return HasFocus;
    }
    
    /// <summary>
    /// Mouse is contained within the current control.
    /// </summary>
    public override bool HasClientMouse {
      get { return HitTest(ClientMouse); }
    }
    
    #endregion
    
    public override void Paint(PaintEventArgs arg)
    {
      Painter.DrawBorder(arg.Graphics, this);
    }
  }
}




