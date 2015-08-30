/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class SliderWidget : ButtonWidget
  {
    
    public DoubleMinMax SliderValue { get; set; }
    // public DoubleMinMax ThumbValue { get; set; }
    
    public float ThumbWidth { get;set; }
    
    public override FloatRect Bounds {
      get { return base.Bounds; }
      set { base.Bounds = value; UpdateBounds(); }
    }
    protected override void ButtonWidget_ParentMouseDown(object sender, MouseEventArgs e)
    {
      if (HasClientMouse) UpdateBounds();
      base.ButtonWidget_ParentMouseDown(sender, e);
    }
    protected override void ButtonWidget_ParentMouseMove(object sender, MouseEventArgs e)
    {
      if (HasMouseDown)
      {
        base.ButtonWidget_ParentMouseMove(sender, e);
        UpdateBounds();
        Parent.Invalidate(this.Bounds);
      }
    }
    public void UpdateBounds()
    {
      SliderValue.Value = (Mouse.X-Bounds.Left)/Bounds.Width;
      Debug.Print( "Value: {0:n2}, {1}, {2}", SliderValue.Value, SliderValue.Minimum, SliderValue.Maximum );
      this.Text = string.Format( "{0:p2}", SliderValue.Value );
    }
    public SliderWidget(IMui parent) : base(parent)
    {
      ThumbWidth = 10;
      SliderValue = new DoubleMinMax(){ Minimum=0, Maximum=1, };
      ValueFormat = "{0}";
    }

    public override void Paint(PaintEventArgs arg)
    {
      base.Paint(arg);
      using (var rgn = new Region(this.PaddedBounds))
      {
        arg.Graphics.Clip = rgn;
        
        var msize = this.Bounds.Clone();
        msize.Width = Convert.ToSingle(Bounds.Width * this.SliderValue.Value);
        
        arg.Graphics.FillRectangle(new SolidBrush(ColourFg),msize);
        Painter.DrawText(arg.Graphics, this);
        
        arg.Graphics.ResetClip();
      }
    }
  }
}






