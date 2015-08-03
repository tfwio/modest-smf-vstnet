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
using System.Diagnostics;
using System.IO;

using gen.snd.IffForm;

namespace gen.snd.Wave
{
	/// <summary>
	/// We've renamed the SampleWaveProvider to this so we can build on it.
	/// This currently is the class used in the gen.snd MainForm for playing wave samples.
	/// There are some obvious errors.
	/// </summary>
	public class Int32WaveChannel : Int32WaveChannelBase
	{
		// riff-info
		RiffForm WaveForm;
		// file inupt
		Stream waveFileInputStream;
		
		readonly string FilePath;
		
		// data chunk start position
		internal int sampleData_DataStart;
		
		// datalength / num-channels
		internal int SampleData_SampleCount;
		
		byte[] RawWaveData = null;
		
		public float ResampleFrequency {
			get { return resampleFrequency; }
			set { resampleFrequency = value; }
		} float resampleFrequency;
		
		/// <summary>
		/// .ctor
		/// </summary>
		/// <param name="path"></param>
		public Int32WaveChannel(string path)
		{
			FilePath = path;
			InitializeMemory(path);
		}
		
		void InitializeMemory(string path)
		{
			this.WaveForm = RiffForm.Load(path);
			this.wformat = RiffUtil.ToNAudio(this.WaveForm.Cks.ckFmt);
			
			sampleData_ChunkLength	= this.WaveForm["data"].ckLength;
			SampleData_SampleCount	= SampleData_ChunkLength / WaveFormat.Channels;
			sampleData_DataStart	= RiffUtil.FindSampleStart(this.WaveForm);
			
			RawWaveData = new byte[sampleData_ChunkLength];
			using (waveFileInputStream = new FileStream(
				this.FilePath,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
			{
				waveFileInputStream.Seek(sampleData_DataStart, SeekOrigin.Begin);
				waveFileInputStream.Read(RawWaveData,/*sampleData_DataStart*/0,SampleData_ChunkLength);
			}
		}
		
		public int LastReadOffset {
			get { return lastReadOffset; }
			set { lastReadOffset = value; }
		} int lastReadOffset = 0;
		
		override public int Read(byte[] b, int position, int length)
		{
			int ToRead = RiffUtil.BytesToRead(this,lastReadOffset,length), BytesReturned = 0;
			
			if (Status==BufferStatus.Finished || ToRead==0)
			{
				this.OnEndOfTrack();
				lastReadOffset = 0;
				return lastReadOffset;
			}
			
			Debug.Print(
				"status: {4}, offset: {0}, length: {1}, toread: {2}, of: {3}",
				lastReadOffset,length,ToRead,SampleData_ChunkLength,Status
			);
			
			if (ToRead==0) {
				Status = BufferStatus.Finished;
				return BytesReturned;
			}
			else
			{
				using (MemoryStream memory = new MemoryStream(RawWaveData))
				{
					memory.Seek(lastReadOffset,SeekOrigin.Begin);
					lastReadOffset += BytesReturned = memory.Read(b,0,ToRead);
					memory.Close();
				}
			}
			if (Status==BufferStatus.LastPage) Status = BufferStatus.Finished;
			return  BytesReturned;
		}
		
		void DisposeStream()
		{
			if (waveFileInputStream == null) return;
			waveFileInputStream.Close();
			waveFileInputStream.Dispose();
			waveFileInputStream = null;
		}

		public override void Dispose()
		{
			DisposeStream();
			if (RawWaveData!=null)
			{
				Array.Clear(RawWaveData, 0, RawWaveData.Length);
				RawWaveData = null;
			}
			this.wformat = null;
			this.WaveForm = null;
		}

	}
	

}
