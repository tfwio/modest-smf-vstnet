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
using on.smfio.util;

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
	
	// At the time this file was implemented, it had been cloned from:
	// 
	// - microDRUM project
	//   https://github.com/microDRUM by massimo.bernava@gmail.com
	// - Modified by perivar@nerseth.com to support processing audio files
	// - Then customized or perhaps re-written again, here.
	//   https://github.com/tfwio
	//
	// It appears this file is most-likely irrecognisable at this point.
	
	public class VSTStream32 : WaveStream
	{
		/// <summary>
		/// Globally 
		/// </summary>
		public NAudioVST Parent { get; set; }
		
		// ============================
		// Public Fields and Properties
		// ============================
		
		/// <summary>
		/// NumSamplesToProcess / Parent.Settings.Channels.
		/// Depends on NumSamplesToProcess: Eg: <pre><code>NumSamplesToProcess / Parent.Settings.Channels</code></pre>
		/// </summary>
		/// <seealso cref="NumSamplesToProcess" />
		public int NumSamplesPerChannel { get { return NumSamplesToProcess / Parent.Settings.Channels; } }
		
		public override WaveFormat WaveFormat { get { return waveFormat; } }
		
		/// <summary>Probably reflected in Read(float[] buffer, int offset, int sampleCount) as sampleCount.</summary>
		public override long Length { get { return long.MaxValue; } }
		
		/// <summary>Probably reflected in Read(float[] buffer, int offset, int sampleCount) as offset.</summary>
		public override long Position { get { return 0; } set { long x = value; } }
		
		/// <summary>
		/// </summary>
		/// <seealso cref="Read(float[],int,int)"/>
		/// <seealso cref="NumSamplesPerChannel"/>
		public int NumSamplesToProcess {
			get { return numSamplesToProcess; }
			set { numSamplesToProcess = value; }
		} int numSamplesToProcess = 0;
		
		public float Volume {
			get { return volume; }
			set { volume = value; }
		} float volume = 1;
		
		// =============================
		// Private Fields and Properties
		// =============================
		
		//IMidiParser MidiParser { get { return Parent.Parent.Parent.MidiParser; } }
		
		//VstPluginManager PluginManager { get { return Parent.Parent.PluginManager; } }
		
		VstPlugin module_instrument { get { return Parent.Parent.PluginManager.MasterPluginInstrument; } }
		VstPlugin module_effect { get { return Parent.Parent.PluginManager.MasterPluginEffect; } }
		
		int BlockSize = 0;
		
		float[] actualOutput;
		
		VstAudioBuffer[] actualBuffer;
		
		AudioModule InputManager = null, OutputManager = null;
		
		IOModule mod;
		
		WaveFormat waveFormat;
		
		// ==============
		// Helper Methods
		// ==============
		
		//double nextoffset(int sampleCount) { return parent.SampleOffset+sampleCount; }
		/// <summary>
		/// Step 3 / N or ( 2.1 / N )
		/// </summary>
		/// <param name="sampleCount"></param>
		/// <param name="nch"></param>
		int GetNumSamplesWithinLoop(int sampleCount, int nch)
		{
			double newsamplecount = sampleCount;
			int actualSamples = newsamplecount.FloorMinimum(0).ToInt32() / nch;
			
			Loop o = Parent.One;
			
			if ((Parent.SampleOffset+actualSamples) > o.End)
			{
				newsamplecount = (o.End - (Parent.SampleOffset)).ToInt32() * nch;
				actualSamples = newsamplecount.FloorMinimum(0).ToInt32() / nch;
			}
			// this might be incorrect
			if (actualSamples==0)
			{
				Parent.SampleOffset = o.Begin;
				newsamplecount = sampleCount;
			}
			
			return newsamplecount.FloorMinimum(0).ToInt32();
		}
		
		// ==============
		// Methods
		// ==============
		/// <summary>
		/// Step 5 / N
		/// </summary>
		/// <param name="plugin"></param>
		/// <param name="buffer"></param>
		/// <returns></returns>
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
				
				Parent.BufferIncrement++;
			}
			
			return actualOutput;
		}
		/// Step 4 (2.2) of N
		/// <seealso cref="ProcessToMixer(VstPlugin,VstAudioBuffer[])"/>
		float[] ProcessReplace(int blockSize)
		{
			//lock (this)
			{
				if (blockSize != BlockSize)
				{
					BlockSize = blockSize;// phase doesn't match?
					actualOutput = new float[WaveFormat.Channels * blockSize];
				}
				try
				{
					if (mod == null) mod = IOModule.Create(blockSize, module_instrument, module_effect);

					mod.Reset(blockSize, module_instrument, module_effect);
					mod.GeneralProcess(module_instrument, module_effect);

					actualBuffer = module_effect == null ?
						mod.Inputs.Outputs.ToArray() :
						mod.Outputs.Outputs.ToArray();

				}
				catch (Exception ex)
				{
					Parent.Stop();
					System.Windows.Forms.MessageBox.Show(ex.ToString());
				}
				ProcessToMixer(module_effect ?? module_instrument, actualBuffer);
			}
			return actualOutput;
		}
		
		public void SetWaveFormat(int sampleRate, int channels)
		{
			this.waveFormat = WaveFormat.CreateIeeeFloatWaveFormat(sampleRate, channels);
		}
		
		/// <summary>
		/// Starting point (2 / N).
		/// </summary>
		/// <param name="buffer">You know</param>
		/// <param name="offset">always Zero. (see Position)</param>
		/// <param name="sampleCount">always Length</param>
		/// <returns></returns>
		/// <seealso cref="ProcessReplace(int)"/>
		/// <seealso cref="NAudioVST.OnBufferCycle(int)"/>
		public int Read(float[] buffer, int offset, int sampleCount)
		{
			NumSamplesToProcess = GetNumSamplesWithinLoop(sampleCount, Parent.Settings.Channels);

			// used in ProcessReplace; re-used in OnBufferCycle.
			int nSmpPCh = NumSamplesPerChannel;

			float[] tempBuffer = ProcessReplace(nSmpPCh);

			for (int i = 0; i < NumSamplesToProcess; i++)

				buffer[i + offset] = tempBuffer[i];

			Parent.OnBufferCycle(nSmpPCh);

			return NumSamplesToProcess;
		}

		/// <summary>
		/// Starting point (1 / N).
		/// </summary>
		/// <param name="buffer">You know</param>
		/// <param name="offset">always Zero. (see Position)</param>
		/// <param name="count">always Length</param>
		/// <returns>samples processed.</returns>
		/// <seealso cref="Read(float[],int,int)" title="float[],int,int" />
		public override int Read(byte[] buffer, int offset, int count)
		{
			WaveBuffer waveBuffer = new WaveBuffer(buffer);

			int samplesRequired = count / 4;

			int samplesRead = Read(waveBuffer.FloatBuffer, offset / 4, samplesRequired);

			return samplesRead * 4;
		}
		
	}

}
