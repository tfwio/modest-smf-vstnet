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
using System.Linq;

using gen.snd.Midi;
using gen.snd.Vst;
using gen.snd.Vst.Module;
using Jacobi.Vst.Core;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace gen.snd.Vst
{

	/// <summary>
	/// This is the old VST Host Core.  The core (which contains this class)
	/// has moved over to NAudioVstContainer.  In this class are the various
	/// Sound-Driver related configuration settings (See <see cref="NAudioVST.Config"/>)
	/// in addition to the various time-settings implemented in the HostCommandStub.
	/// </summary>
	public class NAudioVST : INaudioVST
	{
		
		#region Timings

		SampleClock clock = new SampleClock();
		
		public Loop One {
			get
			{
			  var multiplier=Settings.BarStart*Settings.Division;
				return new Loop {
					Begin = clock.SolveSamples(multiplier * Settings.BarStartPulses, Settings).Samples32,
					Length= clock.SolveSamples(multiplier * Settings.BarLengthPulses, Settings).Samples32
				};
			}
		} Loop o;
		
		/// Check sample position against loop region position in samples
		public double FilterOffset(double value)
		{
			o = One;
			if (value <= o.Begin) sampleOffset = o.Begin;
			else if (value > o.End) sampleOffset = o.Begin;
			else sampleOffset = value;
			return sampleOffset ;
		}
		/// <summary></summary>
		public NoteTransport BarSegment {
			get { return barSegment; }
			set { barSegment = value; /*Notify("BarPosition");*/ }
		} NoteTransport barSegment;

		/// <summary>
		/// This is a reference point for timing configuration used in VstHostCommandStub.GetTimeInfo(…).
		/// </summary>
		public SampleClock SampleTime {
			get { return sampleTime.SolvePPQ(SampleOffset,Settings); }
			private set { sampleTime = value; }
		} SampleClock sampleTime = new SampleClock();
		#endregion
		
		/// <summary></summary>
		public event EventHandler PlaybackStarted;
		
		/// <summary></summary>
		public event EventHandler PlaybackStopped;
		
		#region Main Properties
		/// <summary></summary>
		public Guid DriverId { get { return driverId; } set { driverId = value; } } Guid driverId = Guid.Empty;
		
		/// <summary></summary>
		public float Volume {
			get { return volume; }
			set { volume = value; if (CurrentChannel!=null) CurrentChannel.Volume = volume; }
		} float volume = 1;
		
		/// <summary>
		/// (private) Audio Interface
		/// </summary>
		VSTStream32 CurrentChannel = null;
		
		/// <summary></summary>
		public INaudioVstContainer Parent { get { return parent; } } INaudioVstContainer parent;
		
		/// <summary>
		/// Main timing and audio buffer configuration.
		/// </summary>
		public ITimeConfiguration Settings { get { return TimeConfiguration.Instance; } }
		
		/// <summary>
		/// Host's main control point—interfaced by VST plugins and instruments.
		/// </summary>
		public HostCommandStub VstHostCommandStub {
			get { return vstHostCommandStub; }
		} HostCommandStub vstHostCommandStub;
		

		#endregion
		
		/// <summary>
		/// GET;  When referenced, <see cref="SampleTime" /> is re-calculated using settings from
		/// <see cref="Settings" /> stored in <see cref="Config" />.
		/// </summary>
		public string MeasureString {
			get { return SampleTime.SolvePPQ( sampleOffset,Settings.Rate,Settings.Tempo,Settings.Division,true ).MeasureString; }
		}
		
		#region Time Conversion
		
		/// <summary>
		/// The number is a result of the buffer calculation which divides by the number of
		/// channels.  Since we've already calculated as such, we're avoiding cpu cycles here.
		/// </summary>
		public double SampleOffset {
			get { return sampleOffset; } set { sampleOffset = value; }
		} double sampleOffset = 0;
		
		/// <summary>
		/// This represents the current location of the audio buffer process.
		/// It is reset to zero after each buffer cycle is processed and increments
		/// throughout the process-replacing process.
		/// </summary>
		public double BufferIncrement {
			get { return bufferIncrement; } set { bufferIncrement = value; }
		} double bufferIncrement = 0;

		/// <summary>
		/// The number is a result of the buffer calculation which divides by the number of
		/// channels.  Since we've already calculated as such, we're avoiding cpu cycles here.
		/// </summary>
		public double CurrentSampleLength {
			get { return currentSampleLength; } set { currentSampleLength = value; }
		} double currentSampleLength = 0;
		
		#endregion
		
		#region NAudio
		
		/// <summary>
		/// The wave-format requested by NAudio drivers.
		/// </summary>
		public NAudio.Wave.WaveFormat Format {
			get { return format; } set { format = value; }
		} NAudio.Wave.WaveFormat format;
		
		#region NAudio Device Enum
		/// <summary>
		/// NAudio WaveOut device types.
		/// <para>Notably, a minimal amount of attention has been given to NAudio implementation
		/// and this list of drivers.</para>
		/// <para>Basically with a minimal amount of configuration, DirectSound
		/// seems to work the best and I don't really intend to focus too much on NAudio in the future.</para>
		/// </summary>
		public enum Driver {
			/// <summary>
			/// While this device (working on Windows >= Vista) seems the fastest and most
			/// responsive, it seems to randomly create a buffer with a sample size of 1,
			/// which would be difficult to use to calculate and send VstEvent clusters.
			/// </summary>
			Wasapi,
			/// <summary>
			/// I've not tested ASIO with NAudio.
			/// </summary>
			ASIO,
			/// <summary>
			/// DirectSound seems to work the best due to it's accurate latency setting.
			/// <para>When you set the latency to 100 thousananths of a second at 48000 samples
			/// per second correctly creates a buffer size of 4800 (which is actually
			/// 100 thousandths of a second).</para>
			/// <para>Other drivers don't seem to take their settings as well.</para>
			/// </summary>
			DirectSound,
			/// <summary>
			/// Not implemented.  This is Microsoft WaveOut device (or MCI audio).
			/// </summary>
			Wave
		}
		#endregion
		
        /// <summary>
		/// The default driver used by the VST Host.
		/// </summary>
		public NAudioVST.Driver AudioDriver {
			get { return audioDriver; } set { audioDriver = value; }
		} Driver audioDriver = Driver.DirectSound;
		
		/// <summary>
		/// The audio device.
		/// </summary>
		public IWavePlayer XAudio {
			get { return xAudio; }
		} IWavePlayer xAudio;
		
		/// <summary>
		/// This method is called by the main Audio Device Initialization method: Prepare(…).
		/// </summary>
		void DriverInit()
		{
      switch (audioDriver) {
        case Driver.Wasapi:
          xAudio = new WasapiOut(AudioClientShareMode.Shared, TimeConfiguration.Instance.Latency);
          break;
        case Driver.Wave:
          xAudio = new WaveOut();
          break;
        case Driver.DirectSound:
          xAudio = DriverId != Guid.Empty ?
            new DirectSoundOut(DriverId, TimeConfiguration.Instance.Latency) :
            new DirectSoundOut(TimeConfiguration.Instance.Latency);
          break;
        case Driver.ASIO:
          xAudio = new AsioOut(1);
          break;
      }

      if (CurrentChannel!=null) { CurrentChannel.Dispose(); CurrentChannel = null; }


			xAudio.PlaybackStopped -= Event_PlaybackStopped;
			xAudio.PlaybackStopped += Event_PlaybackStopped;
		}
		
		/// <summary>
		/// Main NAudio WaveOut initialization call.
		/// </summary>
		/// <param name="context"></param>
		public void Prepare(/*params IVstPluginContext[] context*/)
		{
			DriverInit();
			CurrentChannel = new VSTStream32();
			CurrentChannel.SetWaveFormat( Settings.Rate, Settings.Channels );
			CurrentChannel.Parent = this;
			xAudio.Init(CurrentChannel);
			CurrentChannel.Volume = volume;
			if (PlaybackStarted != null) PlaybackStarted(this,EventArgs.Empty);
		}
		
		#endregion
		
		#region EventHandler BufferCycle
		/// <summary>
		/// This event is triggered each iteration through the audio buffer
		/// so that the host application can take advantage of timing calculations
		/// and the host can use timing calculations to align midi events.
		/// </summary>
		/// <remarks>Note that this event is triggered AFTER the buffer has processed and transmitted audio.</remarks>
		public event EventHandler<NAudioVSTCycleEventArgs> BufferCycle;
		/// <summary>
		/// triggers the <see cref="BufferCycle" /> event.
		/// </summary>
		/// <param name="cycle">The length of the current audio buffer that has been processed.</param>
		/// <remarks>Note that this event is triggered AFTER the buffer has processed and transmitted audio.</remarks>
		public void OnBufferCycle(int cycle)
		{
			if (BufferCycle != null) {
				Loop o = this.One;
				CurrentSampleLength = cycle;
				sampleOffset += CurrentSampleLength;
				Console.WriteLine("L: {0,7:N0}, End: {1,11:N0}, len:{2,11:N0}",sampleOffset, o.End, cycle);
				BufferIncrement = 0;
				BufferCycle(this, new NAudioVSTCycleEventArgs(cycle));
			}
		}
		
		#endregion
		
		/// <summary>
		/// Initiailzes a new HostCommandStub, SampleTime info and assigns a default handler to
		/// PluginCalledEventArgs (which isn't used any more).
		/// </summary>
		public NAudioVST(INaudioVstContainer parent)
		{
			this.parent = parent;
			this.vstHostCommandStub = new HostCommandStub(this);
		}
		
		#region Play Stop Etc
		
		public void ResetBufferToZero()
		{
			SampleOffset = 0;
			CurrentSampleLength = 0;
			System.Windows.Forms.Application.DoEvents();
			OnBufferCycle(0);
			if (PlaybackStopped != null) PlaybackStopped(this,EventArgs.Empty);
		}
		
		VstPlugin instrument { get {  return parent.PluginManager.MasterPluginInstrument; } }
		VstPlugin effect { get {  return parent.PluginManager.MasterPluginEffect; } }
		
		public bool IsRunning { get { return isRunning; } } bool isRunning;
		public bool IsPaused { get { return isPaused; } } bool isPaused;

		public void Play() { Play(0); }
		public void Play(double SampleStartPosition)
		{
			this.SampleOffset = SampleStartPosition;
			if (instrument !=null)
			{
				instrument.On();
			}
			if (effect !=null) {
				effect.On();
			}
			this.CurrentChannel.Volume = Volume;
			XAudio.Play();
			isRunning = true;
		}
		
		public void Stop()
		{
			// turn off effects/instruments
			if (effect !=null) effect.Off();
			if (instrument !=null) instrument.Off();
			// event handler respondes and shuts down memory.
			XAudio.Stop();
		}
		public void Pause()
		{
			XAudio.Pause();
			if (isPaused) Resume();
			else isPaused = true;
		}
		
		void Resume()
		{
			XAudio.Play();
			isPaused = false;
		}
		
		/// <summary>
		/// Used to notify when audio processing stops (so that the timing calculator can be reset).
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void Event_PlaybackStopped(object sender, StoppedEventArgs e)
		{
			isRunning = isPaused = false;
			if (xAudio==null) { ResetBufferToZero(); return; }
			
			XAudio.Dispose();
			xAudio = null;
			ResetBufferToZero();
		}
		
		#endregion
		
		#region MIDI VstEvent, MidiMessage
		
		readonly object locker = new object();
		
		IMidiParser Parser { get { return Parent.Parent.MidiParser; } }
		
		bool IsContained(MidiMessage msg, SampleClock c, double min, double max)
		{
			Loop b = One;
			double samplePos = c.SolveSamples(msg.DeltaTime).Samples32;
			return samplePos >= min && samplePos < max && samplePos < b.End;
		}
		
		public List<VstEvent> MidiBuffer {
			get { return midiBuffer; }
			internal set { midiBuffer = value; }
		} List<VstEvent> midiBuffer = new List<VstEvent>();
		
		#endregion
		
	}

}
