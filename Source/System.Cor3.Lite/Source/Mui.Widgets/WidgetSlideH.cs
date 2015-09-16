/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class WidgetSlideH : WidgetButton
  {
    public DoubleMinMax SliderValue { get; set; }
    // public DoubleMinMax ThumbValue { get; set; }
    
    public float ThumbWidth { get;set; }
    
    public override FloatRect Bounds {
      get { return base.Bounds; }
      set { base.Bounds = value; UpdateBounds(); }
    }
    
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      if (HasClientMouse) UpdateBounds();
    }
    
    protected override void WidgetButton_ParentMouseMove(object sender, MouseEventArgs e)
    {
      base.WidgetButton_ParentMouseMove(sender, e);
      if (HasMouseDown) UpdateBounds();
    }
    
    public void UpdateBounds()
    {
      if (!IsInitialized) return;
      SliderValue.Value = (Mouse.X-Bounds.Left)/Bounds.Width;
      Debug.Print( "Value: {0:n2}, {1}, {2}", SliderValue.Value, SliderValue.Minimum, SliderValue.Maximum );
      this.Text = string.Format( "{0:p2}", SliderValue.Value );
    }
    
    public override void Design()
    {
      ThumbWidth = 10;
      SliderValue = new DoubleMinMax(){ Minimum=0, Maximum=1, };
      ValueFormat = "{0}";
    }
    
    public WidgetSlideH(IMui parent) : base(parent)
    {
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
        Painter.DrawText(arg.Graphics, this, false);
        
        arg.Graphics.ResetClip();
      }
    }
  }
  
}






