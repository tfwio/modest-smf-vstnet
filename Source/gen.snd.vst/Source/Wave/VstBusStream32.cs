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
using System.Diagnostics;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Interop.Host;
using NAudio.Wave;

namespace DspAudio.Wave
{
	// Copied from the microDRUM project
	// https://github.com/microDRUM
	// I think it is created by massimo.bernava@gmail.com
	// Modified by perivar@nerseth.com to support processing audio files
	public class VstBusStream32 : WaveStream
	{
		#region Properties: Plugins
		public IVstPluginContext[] Plugins {
			get { return plugins; }
			set { plugins = value; }
		} internal IVstPluginContext[] plugins = null;
		#endregion
		
		#region Fields
		bool IsStopped = false;
		
		private int BlockSize = 0;
		
		VstAudioBuffer[] inputBuffers;
		VstAudioBuffer[] outputBuffers;
		
		float[] input;
		float[] output;
		
		private WaveChannel32 wavStream;
		private WaveFileReader wavFileReader;
		
		private int foundSilenceCounter = 0;
		
		public bool DoProcess = true;
	
		private WaveFormat waveFormat;
		
		#endregion
		
		#region Events
		
		// event handlers
		public event EventHandler<VSTStreamEventArgs> ProcessCalled;
		private void RaiseProcessCalled(float maxL, float maxR)
		{
			EventHandler<VSTStreamEventArgs> handler = ProcessCalled;
			Debug.Print("RaiseProcessCalled MaxL,MaxR");
			if (handler != null)
			{
				handler(this, new VSTStreamEventArgs(maxL, maxR));
			}
		}
		
		public event EventHandler PlayingStarted;
		private void RaisePlayingStarted()
		{
			if (PlayingStarted != null)
			{
				PlayingStarted(this, EventArgs.Empty);
			}
		}
		
		public event EventHandler PlayingStopped;
		private void RaisePlayingStopped()
		{
			Debug.Print("RaisePlayingStopped");
			if (PlayingStopped != null)
			{
				IsStopped = true;
				PlayingStopped(this, EventArgs.Empty);
			}
		}
		#endregion
		
		private void UpdateBlockSize(int blockSize, IVstPluginContext plugin)
		{
			BlockSize = blockSize;
			
			int inputCount = plugin.PluginInfo.AudioInputCount;
			int outputCount = plugin.PluginInfo.AudioOutputCount;
			
			VstAudioBufferManager inputMgr = new VstAudioBufferManager(inputCount, blockSize);
			VstAudioBufferManager outputMgr = new VstAudioBufferManager(outputCount, blockSize);
			
			plugin.PluginCommandStub.SetBlockSize(blockSize);
			plugin.PluginCommandStub.SetSampleRate(WaveFormat.SampleRate);
			plugin.PluginCommandStub.SetProcessPrecision(VstProcessPrecision.Process32);
			
			inputBuffers = inputMgr.ToArray();
			outputBuffers = outputMgr.ToArray();
			
			input = new float[WaveFormat.Channels * blockSize];
			output = new float[WaveFormat.Channels * blockSize];
			
		}
		
		private float[] ProcessReplace(int blockSize, IVstPluginContext plugin)
		{
			lock (this)
			{
				if (blockSize != BlockSize) UpdateBlockSize(blockSize, plugin);
				try
				{
					plugin.PluginCommandStub.StartProcess();
					plugin.PluginCommandStub.ProcessReplacing(inputBuffers, outputBuffers);
					plugin.PluginCommandStub.StopProcess();
				}
				catch (Exception ex)
				{
					Debug.Print("IO Write Error");
					Console.Out.WriteLine(ex.Message);
				}
				
				int indexOutput = 0;
				int oc = plugin.PluginInfo.AudioOutputCount;
				
				float maxL = float.MinValue;
				float maxR = float.MinValue;
				
				for (int j = 0; j < BlockSize; j++)
				{
					output[indexOutput] = outputBuffers[0][j];
					output[indexOutput + 1] = outputBuffers[(oc > 1)?1:0][j];
					indexOutput += 2;
				}
				
	//				// try to find when processing input file has reached
	//				// zero volume level
	//				float almostZero = 0.0000001f;
	//				if (maxL < almostZero && maxR < almostZero) {
	//					//Console.Out.Write("-");
	//
	//					// don't stop until we have x consequetive silence calls after each other
	//					if (foundSilenceCounter >= 5) {
	//						if (wavStream != null && wavStream.CurrentTime >= wavStream.TotalTime) {
	//							RaisePlayingStopped();
	//						}
	//					} else {
	//						foundSilenceCounter++;
	//					}
	//				} else {
	//					foundSilenceCounter = 0;
	//					//Console.Out.Write(".");
	//				}
				RaiseProcessCalled(maxL, maxR);
			}
			return output;
		}
		
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			if (!DoProcess) { return 0; }
			if (IsStopped) Debug.Print("STOPPED!!!!!!");
			int pr = sampleCount / 2;
			// CALL VST PROCESS HERE WITH BLOCK SIZE OF sampleCount
			foreach (IVstPluginContext plugin in this.Plugins)
			{
				float[] tempBuffer = ProcessReplace(pr,plugin);
				// Copying Vst buffer inside Audio buffer, no conversion needed for WaveProvider32
				try { for (int i = 0; i < sampleCount; i++) buffer[i + offset] = tempBuffer[i]; }
				catch { Debug.Print("ProcessReplace Error~!"); }
			}
	//			Debug.Print("Position: {0}, Next: {1}, ProcessReplace: {2}",offset,offset+sampleCount,tempBuffer.Length);
	//			pluginContext.HostCommandStub.ProcessIdle();
			return sampleCount;
		}
		
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
		}
		
		#region Overrides
		public override int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int samplesRequired = count / 4;
			int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
			return samplesRead * 4;
		}
		
		public override WaveFormat WaveFormat
		{
			get { return waveFormat; }
		}
		
		public override long Length
		{
			get { return long.MaxValue; }
		}
		
		public override long Position
		{
			get
			{
				return 0;
			}
			set
			{
				long x = value;
			}
		}
	
		#endregion
		
		public new void Dispose() {
			base.Dispose();
		}
	}
}
