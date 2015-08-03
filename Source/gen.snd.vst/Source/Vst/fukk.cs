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
using System.Linq;

using DspAudio.Midi;
using DspAudio.Vst.Module;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Interop.Host;
using NAudio.Wave;

namespace DspAudio.Vst
{

	/*
	 * samples:'768,050', bpm: '120', MBQT: '03:01:01:000', Ticks: '15,360'
	 * samples:'768,051', bpm: '120', MBQT: '03:01:01:000', Ticks: '15,360'
	 * samples:'      0',       120                 0   0          '     0'
	 * samples:'     23', ,,														0 				 '     0'
	 * samples:'     24', ,,                        0   0          '     1'
	 * --------------------------------------------------------------
	 * sample resolution needs to conform to tick resolution.
	 * so a sample-to-tick formula is looking for the end @tick-resolution
	 * or tempo mapped to a given number of samples.
	 * ---
	 * Currently, we're using a loop-region which calculates in samples the
	 * end of our buffer loop so we can trim our buffer
	 */
	// Copied from the microDRUM project
	// https://github.com/microDRUM
	// I think it is created by massimo.bernava@gmail.com
	// Modified by perivar@nerseth.com to support processing audio files
	// Then modified again by tfwroble@gmail.com
	public class VSTStream32 : WaveStream
	{
		public float Volume {
			get { return volume; }
			set { volume = value; }
		} float volume = 1;
		
		#region Fields
		
		private int BlockSize = 0;
		VstAudioBuffer[] insI, insO, effI, effO;
		float[] input, output;
		
		#endregion
		
		#region Properties
		
		public NAudioVST Parent {
			get { return parent; }
			set { parent = value; }
		} NAudioVST parent;
		
		IMidiParser MidiParser { get { return parent.Parent.Parent.MidiParser; } }
		
		VstPluginManager PluginManager { get { return parent.Parent.PluginManager; } }
		
		VstPlugin instrument { get { return parent.Parent.PluginManager.MasterPluginInstrument; } }
		VstPlugin effect { get { return parent.Parent.PluginManager.MasterPluginEffect; } }
		
//		public IVstPluginContext PluginContext {
//			get { return pluginContext; }
//			set { pluginContext = value; }
//		} IVstPluginContext pluginContext = null;
		
		private WaveFormat waveFormat;
		public override WaveFormat WaveFormat { get { return waveFormat; } }
		public override long Length { get { return long.MaxValue; } }
		public override long Position { get { return 0; } set { long x = value; } }
		#endregion
		
		#region Block
		void UpdateBlock(VstPlugin plugin, int blockSize)
		{
			plugin.PluginCommandStub.SetBlockSize(blockSize);
			plugin.PluginCommandStub.SetSampleRate(Parent.Settings.Rate);
			plugin.PluginCommandStub.SetProcessPrecision(VstProcessPrecision.Process32);
		}
		
		/// <summary>
		/// we need to reflect the end of time in bars, or what-ever loop points are set.
		/// </summary>
		/// <param name="blockSize"></param>
		private void UpdateBlockSize(int blockSize)
		{
			VstAudioBufferManager ii = null, io = null, ei = null, eo = null;
			BlockSize = blockSize;
			ii  = new VstAudioBufferManager(instrument.PluginInfo.AudioInputCount, blockSize);
			io = new VstAudioBufferManager(instrument.PluginInfo.AudioOutputCount, blockSize);
			
			if (effect!=null)
			{
				ei  = new VstAudioBufferManager(effect.PluginInfo.AudioInputCount, blockSize);
				eo = new VstAudioBufferManager(effect.PluginInfo.AudioOutputCount, blockSize);
			}
			
			UpdateBlock(instrument,blockSize);
			if (effect!=null) UpdateBlock(effect,blockSize);
			
			insI = ii.ToArray();
			insO = io.ToArray();
			effI = ei.ToArray();
			effO = eo.ToArray();
			
			input  = new float[WaveFormat.Channels * blockSize];
			output = new float[WaveFormat.Channels * blockSize];
		}

		#endregion
		
		#region Audio Process
		private float[] ProcessReplace(int blockSize)
		{
			lock (this)
			{
				if (blockSize != BlockSize) UpdateBlockSize(blockSize);
				try
				{
					NAudioVST.SendMidi2Plugin( instrument, parent.Parent.Parent, blockSize );
					
					instrument.PluginCommandStub.StartProcess();
					instrument.PluginCommandStub.ProcessReplacing(insI, insO);
					instrument.PluginCommandStub.StopProcess();
					
					if (effect!=null)
					{
						effect.PluginCommandStub.StartProcess();
						effect.PluginCommandStub.ProcessReplacing(insO,effO);
						effect.PluginCommandStub.StopProcess();
					}
				}
				catch (Exception ex) { System.Windows.Forms.MessageBox.Show(ex.ToString()); }
				
				int indexOutput = 0;
				int oc = effect.PluginInfo.AudioOutputCount;
				
				if (oc <= 2)
				{
					for (int j = 0; j < BlockSize; j++)
					{
						if (effect==null)
						{
							output[indexOutput] = insO[0][j]  * volume;
							output[indexOutput + 1] = insO[(oc > 1)?1:0][j]  * volume;
						}
						else
						{
							output[indexOutput] = effO[0][j]  * volume;
							output[indexOutput + 1] = effO[(oc > 1)?1:0][j]  * volume;
						}
						indexOutput += 2;
						parent.BufferIncrement++;
					}
				}
				else if (oc >= 4)
				{
					for (int j = 0; j < BlockSize; j++)
					{
						if (effect==null)
						{
							output[indexOutput]   = FloatMathExtension.Combine(volume, insO[0][j], insO[2][j] );
							output[indexOutput+1] = FloatMathExtension.Combine(volume, insO[1][j], insO[3][j] );
						}
						else
						{
							output[indexOutput]   = FloatMathExtension.Combine(volume, effO[0][j], effO[2][j] );
							output[indexOutput+1] = FloatMathExtension.Combine(volume, effO[1][j], effO[3][j] );
						}
						indexOutput += 2;
						parent.BufferIncrement++;
					}
				}
			}
			return output;
		}
		#endregion
		
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			
			int newsamplecount = sampleCount;
			int actualSamples = newsamplecount / Parent.Settings.Channels;
			double nextoffset = parent.SampleOffset + actualSamples;
			
			// were attempting to bind to Loop region
			Loop o = parent.One;
			
			if (nextoffset > o.End) {
				newsamplecount = (o.End - (parent.SampleOffset)).FloorMinimum(0).ToInt32();
				actualSamples = newsamplecount;
				newsamplecount *= 2;
			}
			
			if (actualSamples==0) {
				parent.SampleOffset = o.Begin;
				newsamplecount = sampleCount;
				actualSamples = newsamplecount / Parent.Settings.Channels;
			}
			
			float[] tempBuffer = ProcessReplace( actualSamples );
			
			for (int i = 0; i < newsamplecount; i++) buffer[i + offset] = tempBuffer[i];
			
			Parent.OnBufferCycle( actualSamples );
			
			return newsamplecount;
		}
		
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
		}

		#region Impl:WaveStream.Read(byte[] buffer, int offset, int count)
		public override int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);
			int samplesRequired = count / 4;
			int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);
			return samplesRead * 4;
		}
		#endregion
		
	}
}
