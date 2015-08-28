/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
namespace System
{
	public class DoubleMinMax : MinMax<double>
	{
	  public double Depth          { get { return Maximum-Minimum; } }
	  public double DepthAbs       { get { return Math.Abs(Depth); } }
	  public DoubleMinMax Absolute { get { return new DoubleMinMax{ Minimum=0, Maximum=Depth }; } }
//	  public double ScaleValue     { get { return 0; } }
	  
    public override double Value {
      get { return base.Value; }
      set { base.Value = Contain(value); }
    }
    
    //static public bool operator >(double a, DoubleMinMax b){ return  a >  b.Maximum; }
    //static public bool operator >=(double a, DoubleMinMax b){ return a >= b.Maximum; }
    //static public bool operator <(double a, DoubleMinMax b){ return  a >  b.Minimum; }
    //static public bool operator <=(double a, DoubleMinMax b){ return a >= b.Minimum; }
	  /// <summary>
	  /// returns a value: <code>Minimum &lt;= Value &lt;= Maximum</code>
	  /// </summary>
	  /// <param name="value"></param>
	  /// <returns></returns>
	  public double Contain(double value)
	  {
	    return (value < Minimum) ? Minimum : value > Maximum ? Maximum : value ;
	  }
	  public bool IsGreater(double value) { return value > Maximum; }
	  public bool IsLesser(double value) { return value < Minimum; }
		public bool IsIn(double value) { return !( IsLesser(value) || IsGreater(value) ); }
		
	}
}








