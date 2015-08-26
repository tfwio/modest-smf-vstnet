/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
  public class SliderWidget : ButtonWidget
  {
    public DoubleMinMax SliderValue { get; set; }
    // public DoubleMinMax ThumbValue { get; set; }
    
    public float ThumbWidth { get;set; }
    
    FloatRect mthumb;
    
    double size,left;
    
    public override FloatRect Bounds {
      get {
        return base.Bounds;
      }
      set {
        base.Bounds = value;
        UpdateBounds();
      }
    }
    protected override void ButtonWidget_MouseDown(object sender, MouseEventArgs e)
    {
      UpdateBounds();
      base.ButtonWidget_MouseDown(sender, e);
    }
    protected override void ButtonWidget_MouseMove(object sender, MouseEventArgs e)
    {
      if (HasMouseDown)
      {
        base.ButtonWidget_MouseMove(sender, e);
        UpdateBounds();
        Parent.Invalidate(this.Bounds);
      }
    }
    public void UpdateBounds()
    {
      size = Bounds.Width;
      left = Bounds.Left;
      
      FloatPoint mouse = Parent.PointToClient(MainForm.MousePosition);
      SliderValue.Value = (mouse.X-left)/size;
      Debug.Print( "Value: {0:n2}, {1}, {2}", SliderValue.Value, SliderValue.Minimum, SliderValue.Maximum );
      this.Text = string.Format( "{0:p2}", SliderValue.Value );
    }
    public SliderWidget(MainForm parent) : base(parent)
    {
      ThumbWidth = 10;
      SliderValue = new DoubleMinMax(){
        Minimum=0,
        Maximum=1,
      };
      ValueFormat = "{0}";
    }

    public override void Paint(Graphics g)
    {
      base.Paint(g);
      using (g.Clip = new Region(this.PaddedBounds)) {
        
        var msize = this.Bounds.Clone();
        msize.Width = Convert.ToSingle(Bounds.Width * this.SliderValue.Value);
        
        FloatPoint mouse = Parent.PointToClient(MainForm.MousePosition);
        
        g.FillRectangle(new SolidBrush(ColourFg),msize);

        Painter.DrawText(g, this);
      }
    }
  }
}






