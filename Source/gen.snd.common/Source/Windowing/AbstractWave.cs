/* oio * 6/18/2014 * Time: 4:18 AM
 */
using System;
using Complex = System.Drawing.FloatPoint;
namespace gen.snd.Windowing
{
	public abstract class AbstractWave<TBlock, TProvider> : IWaveData<TProvider>
		where TBlock:struct
		where TProvider: class
	{
		IRead<TBlock> Reader { get;set; }
		public TProvider Module { get;set; }
		
		static public readonly Complex DefaultWindow = new Complex(1920,1080);
		public readonly Complex DefaultMaxim = DefaultWindow.Multiply(0.5f);
		
		public EventHandler<MinMaxEvent> MinMaxEventHandler;
		
		public long? Samples { get; set; }
		public int? BitsPerSample { get; set; }
		public int? NumChannels { get; set; }
		public int? Rate { get; set; }

		protected void Reset()
		{
//			Module = null;
			Samples = null;
			BitsPerSample = null;
			Rate = null;
			NumChannels = null;
		}

		public abstract void Load(string path);
	}
}

