/* oio * 6/18/2014 * Time: 4:18 AM
 */
using System;
using Complex = System.Drawing.FloatPoint;
using gen.snd.IffForm;
namespace gen.snd.Windowing
{
	public class MinMaxEvent : EventArgs
	{
		public float Max { get; set; }
		public float Min { get; set; }
		
		public MinMaxEvent(float min, float max)
		{
			this.Max = max;
			this.Min = min;
		}
	}
}






