/* oio * 6/18/2014 * Time: 4:18 AM
 */
using System;
using System.IO;
using gen.snd.IffForm;
using Complex = System.Drawing.FloatPoint;

namespace gen.snd.Windowing
{
	public class IntWave : DataRead<int>
	{
		public override void Read(Stream stream, long posi, int length)
		{
			
		}
		public override void Read(int[] stream, int length)
		{
		}
	}
	public class LongWave : DataRead<long>
	{
		public override void Read(long[] stream, int length)
		{
		}
		
		public override void Read(Stream stream, long posi, int length)
		{
			throw new NotImplementedException();
		}
	}
	public class DoubleWave : DataRead<double>
	{
		public override void Read(double[] stream, int length)
		{
		}
		
		public override void Read(Stream stream, long posi, int length)
		{
			throw new NotImplementedException();
		}
	}
	
	public class FloatWave : DataRead<float>
	{
		
		public override void Read(float[] stream, int length)
		{
			throw new NotImplementedException();
		}
		
		public override void Read(Stream stream, long posi, int length)
		{
			throw new NotImplementedException();
		}
	}

}






