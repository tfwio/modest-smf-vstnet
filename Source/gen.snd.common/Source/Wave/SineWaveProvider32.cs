#region User/License
// oio * 7/19/2012 * 11:33 AM

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
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;

using gen.snd.IffForm;
using NAudio.Wave;
using WFormat = NAudio.Wave.WaveFormat;
namespace gen.snd.Wave
{
	// http://mark-dot-net.blogspot.com/2009/10/playback-of-sine-wave-in-naudio.html
	
	/// <summary>
	/// Though implemented, our SampleFinished handler is never
	/// called.
	/// </summary>
	public class SineWaveProvider32 : WaveProvider32, IWaveChannel
	{
		int sample;
		const double pi2 = 2 * Math.PI;
		
		internal protected event EventHandler sampleFinished;
		public event EventHandler SampleFinished
		{
			add { sampleFinished += value; }
			remove { sampleFinished -= value; }
		}
		
		public float Frequency { get; set; }
		public float Amplitude { get; set; }
		
		public SineWaveProvider32()
		{
			Frequency = 1000;
			Amplitude = 0.25f; // let's not hurt our ears
		}
		
		public override int Read(float[] buffer, int offset, int sampleCount)
		{
			int sampleRate = WaveFormat.SampleRate;
			for (int n = 0; n < sampleCount; n++)
			{
				buffer[n+offset] = (float)(
					Amplitude *
					Math.Sin((pi2 * sample * Frequency) / sampleRate)
				);
				sample++;
				if (sample >= sampleRate) sample = 0;
			}
			return sampleCount;
		}
		
		public void Dispose()
		{
		}
	
		
		public event EventHandler EndOfTrack;
		
		
		int IWaveChannel.SampleData_ChunkLength {
			get {
				throw new NotImplementedException();
			}
		}
		
		BufferStatus IWaveChannel.Status {
			get {
				throw new NotImplementedException();
			}
			set {
				throw new NotImplementedException();
			}
		}
	}
	
}
