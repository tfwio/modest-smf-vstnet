/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public abstract partial class Widget
  {
    virtual public FloatRect Bounds {
      get { return bounds; }
      set { bounds = value; }
    } FloatRect bounds = FloatRect.Zero;

    public Padding Padding {
      get { return padding; }
      set { padding = value; }
    } Padding padding = Padding.Empty;

    public FloatRect PaddedBounds {
      get {
        if (Bounds == null)
          return null;
        var NewRect = FloatRect.ApplyPadding(Bounds, Padding);
        return NewRect;
      }
    }

    virtual public Font Font {
      get { return font ?? Parent.Font; }
      set { font = value; }
    } Font font;

    
    public int? WidthMin { get; set; }
    public int? WidthMax { get; set; }
    
    public int? HeightMin { get; set; }
    public int? HeightMax { get; set; }
    
    public int X { get { return Convert.ToInt32(Bounds.X); } set { Bounds.X = value; } }
    public int Y { get { return Convert.ToInt32(Bounds.Y); } set { Bounds.Y = value; } }
    
    public int Width  { get { return Convert.ToInt32(Bounds.Width); }  set { Bounds.Width = value.Contain(WidthMin,WidthMax); } }
    public int Height { get { return Convert.ToInt32(Bounds.Height); } set { Bounds.Height = value.Contain(HeightMin,HeightMax); } }
    
    /// <summary>
    /// Render text for the button using GrphicsPath.
    /// This is particularly used in ButtonWidget renderer.
    /// </summary>
    public bool Smoother { get; set; }
  }
}










