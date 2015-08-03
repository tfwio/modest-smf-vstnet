#region User/License
// oio * 8/28/2012 * 5:28 PM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.Collections.Generic;
using gen.snd.IffForm;
//ppqPos = (samplePos / sampleRate) * (tempo / 60.);
namespace gen.snd.Generators
{
	public class TimeController
	{
		public double Interval {
			get { return interval; }
		} double interval;
		
		public IList<IGenerator> Generators {
			get { return generators; }
		} List<IGenerator> generators = new List<IGenerator>();
		
		
	}
	/// <summary>
	/// Description of IGenerator.
	/// </summary>
	public interface IGenerator
	{
		WaveFormat Format { get; set; }
		int Process(byte[] input, byte[] output, int length);
	}
	abstract public class GeneratorBase :IGenerator
	{
		public WaveFormat Format {
			get { return format; }
			set { format = value; }
		} WaveFormat format;
		
		abstract public int Process(byte[] input, byte[] output, int length);
	}
	abstract public class Gen2ChInt32 : GeneratorBase
	{
		public override int Process(byte[] input, byte[] output, int length)
		{
			throw new NotImplementedException();
		}
	}
}
