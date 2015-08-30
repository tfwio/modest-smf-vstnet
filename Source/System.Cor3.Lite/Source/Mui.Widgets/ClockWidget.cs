/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
  public class ClockWidget : Widget
  {
    FloatPoint PClock = new FloatPoint(40,150);
    
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

    public ClockWidget(IMui parent) : base(parent)
    {
      this.ValueFormat = "hh:mm:ss.fff tt";
    }
    
    public override void Paint(PaintEventArgs arg)
    {
      base.Paint(arg);
      using (var region = new Region(this.Bounds))
      {
        var nloc = PaddedBounds.Clone();
        nloc.Location = nloc.Location + new FloatPoint(1,1);
        
        arg.Graphics.DrawString(
          DateTime.Now.ToString(ValueFormat),
          this.Font,
          Painter.GetBrush(ColourClassFg),
          PaddedBounds
         );
      }
    }
  }
  
  
}


