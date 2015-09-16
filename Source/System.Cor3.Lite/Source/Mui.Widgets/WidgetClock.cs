/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class WidgetClock : Widget
  {
    FloatPoint PClock = new FloatPoint(40,150);
    protected override void WidgetButton_ParentMouseMove(object sender, MouseEventArgs e)
    {
//      base.WidgetButton_ParentMouseMove(sender, e);
    }
    
    readonly Brush BrushDefault = Painter.DictBrush[ColourClass.White];
    readonly Brush BrushHover = Painter.DictBrush[ColourClass.Default];
    readonly Brush BrushActive = Painter.DictBrush[ColourClass.Dark90];
    
    public override object Value {
      get {
        return DateTime.Now;
      }
    }
    
    public override void Increment()
    {
      base.Increment();
    }

    public override void Design()
    {
      this.ValueFormat = "hh:mm:ss.fff tt";
    }
    public WidgetClock(IMui parent) : base(parent)
    {
    }
    
    public override void Paint(PaintEventArgs arg)
    {
      //base.Paint(arg);
      //Painter.DrawBorder(arg.Graphics, this, Pens.Transparent, Brushes.Transparent);
      using (var region = new Region(this.Bounds))
      {
        var nloc = PaddedBounds.Clone();
        nloc.Location = nloc.Location + new FloatPoint(1,1);
        Text = DateTime.Now.ToString(ValueFormat);
        Painter.DrawText(arg.Graphics,this,false);
//        arg.Graphics.DrawString(
//          ,
//          this.Font,
//          Painter.GetBrush(ColourClassFg),
//          PaddedBounds
//         );
      }
    }
  }
  
  
}


