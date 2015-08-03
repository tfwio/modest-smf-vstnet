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

using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Interop.Host;
using NAudio.Wave;

namespace DspAudio.Wave
{

	/// <summary>
	/// We've renamed the SampleWaveProvider to this so we can build on it.
	/// This currently is the class used in the DspAudio MainForm for playing wave samples.
	/// There are some obvious errors.
	/// </summary>
	public class Int32PluginContextChannel : WaveProvider32, IDisposable
	{
		internal VstPluginContext PluginContext {
			get { return pluginContext; }
			set { pluginContext = value; }
		} VstPluginContext pluginContext;
		
		public Int32PluginContextChannel(VstPluginContext pluginContext) : base(44100,2)
		{
			this.pluginContext = pluginContext;
			lastPosition = 0;
		}
		
		public void Dispose()
		{
		}
		bool IsProcessReplacing {
			get { return isProcessReplacing; }
		} bool isProcessReplacing = false;
		
		long lastPosition = 0;
		Jacobi.Vst.Core.Host.IVstPluginCommandStub plugin { get { return PluginContext.PluginCommandStub; } }
		VstPluginInfo info { get { return PluginContext.PluginInfo; } }
		
		private VstAudioBuffer[] vstBufIn = null;
		private VstAudioBuffer[] vstBufOut = null;
		
		public override int Read(float[] buffer, int offset, int samplesRequested)
		{
			int sampleCount = samplesRequested / 2;
			
			plugin.SetBlockSize(sampleCount);
			plugin.SetSampleRate(WaveFormat.SampleRate);
			plugin.SetProcessPrecision(VstProcessPrecision.Process32);
			
			using (VstAudioBufferManager inputMgr = new VstAudioBufferManager(info.AudioInputCount, samplesRequested))
				using (VstAudioBufferManager outputMgr = new VstAudioBufferManager(info.AudioOutputCount, samplesRequested))
			{
				{
					vstBufIn = inputMgr.ToArray();
					vstBufOut = outputMgr.ToArray();
					
					plugin.StartProcess();
					plugin.ProcessReplacing(vstBufIn,vstBufOut);
					plugin.StopProcess();
					
					int i = 0, j = 0;
					while (j < samplesRequested/2)
					{
						buffer[i++] = vstBufOut[0][j];
						buffer[i++] = vstBufOut[1][j++];
					}
				}
			}
			Debug.Print("Requested: {0}, Returned: {1}, Offset: {2}",samplesRequested,vstBufOut[0].SampleCount,offset);
			return samplesRequested;
		}
		
		public int Readx2x(float[] destBuffer, int position, int bytesRequested)
		{
			
			Debug.Print("VST Read Operation!!! {0}, {1}",position,bytesRequested);
			int inputCount = PluginContext.PluginInfo.AudioInputCount;
			int outputCount = PluginContext.PluginInfo.AudioOutputCount;
			
			PluginContext.PluginCommandStub.SetBlockSize(bytesRequested);
			PluginContext.PluginCommandStub.SetSampleRate(WaveFormat.SampleRate);
			PluginContext.PluginCommandStub.SetProcessPrecision(VstProcessPrecision.Process32);

			using (VstAudioBufferManager inputMgr = new VstAudioBufferManager(inputCount, bytesRequested))
				using (VstAudioBufferManager outputMgr = new VstAudioBufferManager(outputCount, bytesRequested))
			{
//				if (IsProcessReplacing)
				foreach (VstAudioBuffer buffer in inputMgr.ToArray())
					for (int i = 0; i < bytesRequested; i++)
						buffer[i] = destBuffer[i];
				
				PluginContext.PluginCommandStub.ProcessReplacing(inputMgr.ToArray(), outputMgr.ToArray());
				
				foreach (VstAudioBuffer buffer in outputMgr.ToArray())
					for (int i = 0; i < bytesRequested; i++)
				{
					destBuffer[i] = (byte)buffer[i];
					Debug.Print("index: {i}, data: {0}",i,buffer[i]);
				}
				lastPosition += bytesRequested;
				position += bytesRequested;
				return bytesRequested;
				// The Original
				// ---------------------------------------
//				foreach (VstAudioBuffer buffer in outputMgr.ToArray())
//					for (int i = 0; i < blockSize; i++)
//				{
//					destBuffer[i] = Convert.ToByte(((float)buffer[i] + 1.0f) * 128.0f);
//				}
				// Jakobi's Process
				// ---------------------------------------
//				for (int i = 0; i < inputBuffers.Length && i < outputBuffers.Length; i++)
//					for (int j = 0; j < blockSize; j++)
//						if (inputBuffers[i][j] != outputBuffers[i][j])
//							if (outputBuffers[i][j] != 0.0)
//				{
//					MessageBox.Show(this, "The plugin has processed the audio.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
//					return;
//				}
				// Continued Forum Post Code:
				// ---------------------------------------
//			inputMgr.ClearBuffer(inputMgr.ToArray()[0]);
//			inputMgr.ClearBuffer(inputMgr.ToArray()[1]);
//			inputMgr.Dispose();
//			outputMgr.ClearBuffer(outputMgr.ToArray()[0]);
//			outputMgr.ClearBuffer(outputMgr.ToArray()[1]);
//			outputMgr.Dispose();
			}
		}
		
	}
}
