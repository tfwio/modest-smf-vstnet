/* oOo * 11/19/2007 : 8:00 AM */
using System;
namespace System
{
	static public class NumericExtensions
	{
	  static public int Contain(this int number, int? min, int? max)
	  {
	    if (min.HasValue && number < min) return min.Value;
	    return max.HasValue && number > max.Value ? max.Value : number;
	  }
	  static public uint Contain(this uint number, uint? min, uint? max)
	  {
	    if (min.HasValue && number < min) return min.Value;
	    return max.HasValue && number > max.Value ? max.Value : number;
	  }
	  static public float Contain(this float number, float? min, float? max)
	  {
	    if (min.HasValue && number < min) return min.Value;
	    return max.HasValue && number > max.Value ? max.Value : number;
	  }
	  static public double Contain(this double number, double? min, double? max)
	  {
	    if (min.HasValue && number < min) return min.Value;
	    return max.HasValue && number > max.Value ? max.Value : number;
	  }
	}
}


