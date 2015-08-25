/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
  public class SliderWidget : ButtonWidget
  {
    public DoubleMinMax SliderValue { get; set; }
    public DoubleMinMax ThumbValue { get; set; }
    public float ThumbWidth { get;set; }
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
    
    protected override void ButtonWidget_MouseMove(object sender, MouseEventArgs e)
    {
      if (HasMouseDown)
      {
        base.ButtonWidget_MouseMove(sender, e);
        UpdateBounds();
        Parent.Invalidate(this.Bounds);
      }
    }
    FloatRect mthumb;
    public void UpdateBounds()
    {
      size = Bounds.Width;
      left = Bounds.Left;
      ThumbValue.Minimum=ThumbWidth / 2;
      ThumbValue.Maximum=size-ThumbValue.Minimum;
      FloatPoint mouse = Parent.PointToClient(MainForm.MousePosition);
      SliderValue.Value = (mouse.X-left)/200;
      ThumbValue.Value = ThumbValue.Contain(SliderValue.Value);
//      mthumb = new FloatRect(){
//          X=Convert.ToSingle((mouse.X-ThumbValue.Minimum)),
//          Y=PaddedBounds.Top,
//          Width=ThumbWidth,
//          Height=PaddedBounds.Height
//        };
      this.Text = string.Format( "{0:p2}", SliderValue.Contain(SliderValue.Value) );
    }
    public SliderWidget(MainForm parent) : base(parent)
    {
      ThumbWidth = 10;
      ThumbValue = new DoubleMinMax();
      SliderValue = new DoubleMinMax();
      SliderValue.Minimum = 0;
      SliderValue.Maximum = 1;
      this.ValueFormat = "{0}";
      //      this.Click += ButtonWidget_Click;
//			this.MouseDown += ButtonWidget_MouseDown;
//			this.MouseUp += ButtonWidget_MouseUp;
//			this.MouseMove += ButtonWidget_MouseMove;
      ;
    }

    public override void Paint(Graphics g)
    {
      base.Paint(g);
      using (var region = new Region(this.PaddedBounds)) {
        
        g.Clip = region;
        
        var msize = this.Bounds.Clone();
        msize.Width = Convert.ToSingle(Bounds.Width * this.SliderValue.Value);
        
        FloatPoint mouse = Parent.PointToClient(MainForm.MousePosition);
        
        g.FillRectangle(new SolidBrush(ColourFg),msize);
//        g.FillRectangle(new SolidBrush(Color.Black),mthumb);
        
        Painter.DrawText(g, this);
      }
    }
  }
}






