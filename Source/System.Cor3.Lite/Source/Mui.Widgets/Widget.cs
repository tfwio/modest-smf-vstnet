/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class Widget : WidgetBase
  {
    
    #region Logical Size and Position (for use within layout)
    
    public int? WidthMin { get; set; }
    public int? WidthMax { get; set; }
    
    public int? HeightMin { get; set; }
    public int? HeightMax { get; set; }
    
    public int X { get { return Convert.ToInt32(Bounds.X); } set { Bounds.X = value; } }
    public int Y { get { return Convert.ToInt32(Bounds.Y); } set { Bounds.Y = value; } }
    
    public int Width  { get { return Convert.ToInt32(Bounds.Width); }  set { Bounds.Width = value.Contain(WidthMin,WidthMax); } }
    public int Height { get { return Convert.ToInt32(Bounds.Height); } set { Bounds.Height = value.Contain(HeightMin,HeightMax); } }
    
    #endregion
    
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
      get {
        return HasClientMouse && Parent.MouseD != null;
      }
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
      get { return Bounds.Contains(Parent.PointToClient(Parent.MouseM)); }
    }
    
    #endregion
    
    public override void Paint(PaintEventArgs arg)
    {
      Painter.DrawBorder(arg.Graphics, this);
    }
  }
}




