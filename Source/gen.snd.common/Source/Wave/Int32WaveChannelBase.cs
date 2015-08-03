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
using NAudioWaveFormat = NAudio.Wave.WaveFormat;

namespace gen.snd.Wave
{
	/// <summary>
	/// Provides wave samples to a given wave device.
	/// The samples are expected to match that of the output device, so good luck with that one.
	/// </summary>
	public abstract class Int32WaveChannelBase : IWaveChannel
	{
		public NAudioWaveFormat WaveFormat {
			get { return wformat; }
		} internal protected NAudioWaveFormat wformat;
		
		public BufferStatus Status {
			get { return status; }
			set { status = value; }
		} BufferStatus status = BufferStatus.Finished;
		
		// data chunk total length
		public int SampleData_ChunkLength {
			get { return sampleData_ChunkLength; }
		} internal int sampleData_ChunkLength;
		
		internal bool MustReadFromDisk { get { return SampleData_ChunkLength >= int.MaxValue; } }
		
		public float Amplitude {
			get { return amplitude; }
			set { amplitude = value; }
		} internal protected float amplitude;
		
		public event EventHandler EndOfTrack
		{
			add { endOfTrack += value; }
			remove { endOfTrack -= value; }
		} internal protected event EventHandler endOfTrack;
		
		public virtual void OnEndOfTrack()
		{
			if (endOfTrack != null) {
				endOfTrack(this, EventArgs.Empty);
			}
		}
		
		abstract public int Read(byte[] buffer, int start, int length);
		
		virtual public void Dispose()
		{
		}
	}
}
