/* oio * 6/18/2014 * Time: 4:18 AM
 */
using System;
using Complex = System.Drawing.FloatPoint;
using gen.snd.IffForm;
namespace gen.snd.Windowing
{
	public class BasicWindow
	{
		public Complex WindowSize {
			get { return windowSize; }
			set { windowSize = value; }
		}
		
		Complex windowSize = new Complex(1920, 1080);

		int? WaveTotalSamples { get; set; }
		int? WaveBitsPerSample { get; set; }
		int? WaveNumChannels { get; set; }
		
		bool HasWave {
			get { return WaveBitsPerSample.HasValue && WaveNumChannels.HasValue; }
		}

		public int WaveSampleInterval {
			get {
				if (!HasWave) return 0;
				return WaveBitsPerSample.Value * WaveNumChannels.Value;
			}
		}
		
		public void LoadWaveform(string path)
		{
		}
		
		void Reset()
		{
			WaveBitsPerSample = null;
			WaveNumChannels = null;
		}
	}
	
}




