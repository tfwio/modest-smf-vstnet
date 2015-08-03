#region User/License
// oio * 7/31/2012 * 11:12 PM

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

using gen.snd.IffForm;

namespace gen.snd.Wave
{
	static class RiffUtil
	{
		/// <summary>
		/// Returns the Index where reading begins for a wave-sample (<see cref="RiffForm" />) in memory.
		/// </summary>
		/// <param name="riff">Our ram/riff-wave.</param>
		/// <returns>Index where reading begins</returns>
		public static int FindSampleStart(RiffForm riff)
		{
			int start = -1;
	//			if (!MustReadFromDisk)
			{
				foreach (KeyValuePair<long,SUBCHUNK> ck in riff.Cks.SubChunks)
				{
					if (ck.Value.ckID=="data")
						start = Convert.ToInt32(ck.Key)+8;
				}
			}
			return start;
		}
		
		/// <summary>
		/// Convert our WaveFormat to NAudio compatible WaveFormat.
		/// </summary>
		/// <param name="riff">gen.snd.IffForm.WaveFormat</param>
		/// <returns>NAudio compatible WaveFormat</returns>
		public static NAudio.Wave.WaveFormat ToNAudio(gen.snd.IffForm.WaveFormat riff)
		{
			return new NAudio.Wave.WaveFormat(
				Convert.ToInt32(riff.fmtRate),
				Convert.ToInt32(riff.fmtBPSmp),
				Convert.ToInt32(riff.fmtChannels)
			);
		}
	
		/// <summary>
		/// A calculation that returnes the number of bytes to read from
		/// a wave-sample in memory.
		/// </summary>
		/// <param name="channel">NAudio IWaveChannel.</param>
		/// <param name="offset">The position or requested index in our NAudio channel.</param>
		/// <param name="sampleCount">the number of samples requested for our buffer.</param>
		/// <returns>Total number of bytes to read.</returns>
		static public int BytesToRead(IWaveChannel channel, int offset, int sampleCount)
		{
			channel.Status = BufferStatus.Running;
			if (offset > channel.SampleData_ChunkLength)
			{
				channel.Status = BufferStatus.Finished;
				return 0;
			}
			else if ((offset + sampleCount) > channel.SampleData_ChunkLength)
			{
				channel.Status = BufferStatus.LastPage;
				return channel.SampleData_ChunkLength - offset;
			}
			return sampleCount;
		}
	}
}
