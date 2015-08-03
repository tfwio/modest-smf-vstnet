/* oio * 6/18/2014 * Time: 4:18 AM
 */
using System;
using Complex = System.Drawing.FloatPoint;
using gen.snd.IffForm;
namespace gen.snd.Windowing
{
	public interface IWaveData<TProvider>
	{
		TProvider Module { get;set; }
		long? Samples { get; set; }
		int?  BitsPerSample { get; set; }
		int?  Rate { get; set; }
		int?  NumChannels { get; set; }

		void Load(string path);
	}
}






