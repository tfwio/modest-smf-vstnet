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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using gen.snd.Midi;
using gen.snd.Vst.Module;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Interop.Host;
using NAudio.Wave;

namespace gen.snd.Vst
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
	public class VSTStream32 : WaveStream, IDisposable
	{
		public void Dispose()
		{
			
		}
		#region PARENT
		public NAudioVST Parent {
			get { return parent; }
			set { parent = value; }
		} NAudioVST parent;
		
		IMidiParser MidiParser { get { return parent.Parent.Parent.MidiParser; } }
		
		VstPluginManager PluginManager { get { return parent.Parent.PluginManager; } }
		
		#endregion
		#region VST PLUGIN
		VstPlugin module_instrument { get { return parent.Parent.PluginManager.MasterPluginInstrument; } }
		VstPlugin module_effect { get { return parent.Parent.PluginManager.MasterPluginEffect; } }
		#endregion
		
		#region Volume
		
		public float Volume {
			get { return volume; }
			set { volume = value; }
		} float volume = 1;
		
		#endregion
		#region BUFFER OFFSET FLOOR
//		double nextoffset(int sampleCount) { return parent.SampleOffset+sampleCount; }
		
		int GetSamplesWithinLoop(int sampleCount, int nch)
		{
			double newsamplecount = sampleCount;
			int actualSamples = newsamplecount.FloorMinimum(0).ToInt32() / nch;
			
			Loop o = parent.One;
			
			if ((parent.SampleOffset+actualSamples) > o.End) {
				newsamplecount = (o.End - (parent.SampleOffset)).ToInt32() * nch;
				actualSamples = (newsamplecount.FloorMinimum(0).ToInt32() / nch);
			}
			// this might be incorrect
			if (actualSamples==0) {
				parent.SampleOffset = o.Begin;
				newsamplecount = sampleCount;
			}
			
			return newsamplecount.FloorMinimum(0).ToInt32();
			
		}
		#endregion
		#region BUFFER FLUSH
		
		float[] ProcessToMixer(VstPlugin plugin, VstAudioBuffer[] buffer)
		{
			int indexOutput = 0;
			int oc = plugin.PluginInfo.AudioOutputCount;
			
			for (int j = 0; j < BlockSize; j++)
			{
				if (oc <= 2)
				{
					actualOutput[indexOutput] = buffer[0][j]  * volume;
					actualOutput[indexOutput + 1] = buffer[(oc > 1)?1:0][j]  * volume;
				}
				else if (oc >= 4)
				{
					actualOutput[indexOutput]   = FloatMathExtension.Combine(volume, buffer[0][j], buffer[2][j] );
					actualOutput[indexOutput+1] = FloatMathExtension.Combine(volume, buffer[1][j], buffer[3][j] );
				}
				indexOutput += 2;
				parent.BufferIncrement++;
			}
			return actualOutput;
		}
		
		#endregion
		
		#region AUDIO PROCESS
		
		int BlockSize = 0;
		float[] actualOutput;
		VstAudioBuffer[] actualBuffer;
		AudioModule InputManager = null, OutputManager = null;
		IOModule mod;
		
		private float[] ProcessReplace(int blockSize)
		{
			lock (this)
			{
				if (blockSize != BlockSize)
				{
					BlockSize = blockSize;
					actualOutput = new float[WaveFormat.Channels * blockSize];
				}
				try
				{
					if (mod==null) mod = IOModule.Create(blockSize,module_instrument,module_effect);
					
					mod.Reset(blockSize,module_instrument,module_effect);
					mod.GeneralProcess(module_instrument,module_effect);
					
					actualBuffer = module_effect==null ? mod.Inputs.Outputs.ToArray() : mod.Outputs.Outputs.ToArray();
				}
				catch (Exception ex) {
					parent.Stop();
					System.Windows.Forms.MessageBox.Show(ex.ToString());
				}
				ProcessToMixer(module_effect??module_instrument,actualBuffer);
			}
			return actualOutput;
		}
		
		#endregion

		#region WAVE
		
		private WaveFormat waveFormat;
		
		public override WaveFormat WaveFormat { get { return waveFormat; } }
		/// <summary>
		/// Probably reflected in Read(float[] buffer, int offset, int sampleCount) as sampleCount.
		/// </summary>
		public override long Length { get { return long.MaxValue; } }
		/// <summary>
		/// Probably reflected in Read(float[] buffer, int offset, int sampleCount) as offset.
		/// </summary>
		public override long Position { get { return 0; } set { long x = value; } }
		
		public int NumSamplesToProcess {
			get { return numSamplesToProcess; }
			set { numSamplesToProcess = value; }
		} int numSamplesToProcess = 0;
		
		/// <summary>
		/// NumSamplesToProcess / Parent.Settings.Channels
		/// </summary>
		public int NumSamplesPerChannel { get { return NumSamplesToProcess / Parent.Settings.Channels; } }
		
		#endregion
		
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			numSamplesToProcess = GetSamplesWithinLoop(sampleCount,Parent.Settings.Channels);
			
			int numSamplesPerChannel = NumSamplesPerChannel;
			float[] tempBuffer = ProcessReplace( numSamplesPerChannel );
			for (int i = 0; i < numSamplesToProcess; i++) buffer[i + offset] = tempBuffer[i];
			
			Parent.OnBufferCycle( numSamplesPerChannel );
			return numSamplesToProcess;
		}
		
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
		}

		#region Read(byte[] buffer, int offset, int count)
		
		/// <summary>
		/// </summary>
		/// <param name="buffer"></param>
		/// <param name="offset">always Zero. (see Position)</param>
		/// <param name="count">always Length</param>
		/// <returns></returns>
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
