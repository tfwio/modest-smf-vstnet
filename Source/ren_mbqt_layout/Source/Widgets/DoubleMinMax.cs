/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
	public class DoubleMinMax : MinMax<double>
	{
    public override double Value {
      get {
        return base.Value;
      }
      set {
	      base.Value = Contain(value);
      }
    }
    
//	  public double GetValue(
	  // double size = Math.Abs(Bounds.Width - Bounds.Left);
    // double left = Bounds.Left;
    // double value = Parent.PointToClient(MainForm.MousePosition).X - Bounds.Left;
    // this.Text = string.Format( "{0}, {1}, {2}, {3}", size, left, value, value/size );
	  public double Contain(double value)
	  {
	    if (value > Maximum) return Maximum;
	    if (value < Minimum) return Minimum;
	    return value;
	  }
		public bool IsIn(double value)
		{
			return (value >= Minimum) && (value <= Maximum);
		}
	}
}








