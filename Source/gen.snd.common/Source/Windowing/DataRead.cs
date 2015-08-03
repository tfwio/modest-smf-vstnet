/* oio * 6/18/2014 * Time: 4:18 AM
 */
using System;
using System.IO;
using gen.snd.IffForm;
using Complex = System.Drawing.FloatPoint;

namespace gen.snd.Windowing
{
	public interface IRead<TBlock>
		where TBlock: struct
	{
		int Channels { get; set; }
		void Read(Stream stream, long posi, int length);
		void Read(TBlock[] stream, int length);
	}
	abstract public class DataRead<TStruct> : IRead<TStruct>
		where TStruct: struct
	{
		public int Channels { get; set; }
		public abstract void Read(Stream stream, long posi, int length);
		public abstract void Read(TStruct[] stream, int length);
	}
}






