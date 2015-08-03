#region User/License
// oio * 7/19/2012 * 1:00 PM

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
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using gen.snd.Wave;
using NAudio.Wave;

namespace gen.snd.Forms
{
	/// <summary>
	/// Provide a wave-file to a WaveOut Device.
	/// </summary>
	public partial class WaveOutTestForm : Form
	{
		WaveOut waveOut;
		Int32WaveChannel waveProvider;
		
		float Amplitude
		{
			set { if (waveOut!=null) waveOut.Volume = value; }
			get { return volumeSlider1.Volume; }
		}

		bool IsWaveProviderReady { get { return (waveProvider!=null); } }
		bool IsWaveOutReady { get { return waveOut != null; } }

		void PrepareWaveOut() { if (waveOut==null) { waveOut = new WaveOut(); } }

		/// <summary>
		/// If the wave device is capable of being prepared for playback, it will be,
		/// otherwise it returns false to notify that nothing can be done (as far as
		/// playback).
		/// </summary>
		/// <returns>True on success, false on error.</returns>
		bool EnsureWaveOut()
		{
			if (!IsWaveOutReady) PrepareWaveOut();
			if (!IsWaveReady) return false;
			if (!IsWaveProviderReady)
			{
				PrepareWaveProvider();
				waveOut.Volume = this.Amplitude;
				waveOut.DesiredLatency = 200;
				waveOut.Init(waveProvider);
				System.Diagnostics.Debug.Print("Waveout Was Set");
			}
			return true;
		}
		
		#region WaveSample
		
		public string WaveFile {
			get { return this.textBox1.Text; }
			set { this.textBox1.Text = value; }
		}

		bool IsWaveReady {
			get { return !string.IsNullOrEmpty(WaveFile) || File.Exists(WaveFile); }
		}

		#endregion

		void PrepareWaveProvider()
		{
			waveProvider = null;
			waveProvider = new Int32WaveChannel(WaveFile);
			waveProvider.EndOfTrack += delegate {
				Playback_Stop();
			};
		}

		/// <summary>
		/// Destroy the waveProvider assigned to the
		/// waveOut Device.
		/// </summary>
		/// <remarks>
		/// The waveProvider can be a sine-provider, or
		/// a wave sample provider.
		/// </remarks>
		void DestroyWaveProvider()
		{
			if (waveOut!=null) waveProvider.Dispose();
			waveProvider = null;
		}
		
		#region Playback Controls
		string Playback_Status
		{
			get {
				return (waveOut==null) ? "Inactive" : string.Format( "State: {0}, Position: {1}", waveOut.PlaybackState, waveOut.GetPosition() );
			}
		}
		void UpdateAmplitude()
		{
			Amplitude = Amplitude;
			System.Diagnostics.Debug.Print("Amplitude: {0}", Amplitude);
		}

		void Playback_Play()
		{
			if (!EnsureWaveOut())
			{
				System.Diagnostics.Debug.Print("EnsureWaveOut Failed");
				return;
			}
			if (waveOut.PlaybackState == PlaybackState.Playing)
			{
				Playback_Stop();
				
//				System.Threading.Thread.CurrentThread.Interrupt();
//				waveProvider.LastReadOffset = 0;
				Playback_Play();
				return;
			}
			UpdateAmplitude();
			waveOut.Play();
			System.Diagnostics.Debug.Print("Play");
		}
		
		void Playback_Stop()
		{
			if (waveOut!=null)
			{
				waveProvider.LastReadOffset = 0;
				waveOut.Stop();
				Debug.Print("EOF; Resetting Position to {0}", waveProvider.LastReadOffset);
			}
		}
		void Playback_Pause() { if (waveOut!=null) waveOut.Pause(); }
		void Playback_Clear()
		{
			if (waveOut!=null)
			{
				waveOut.Dispose();
				waveOut = null;
			}
			if (waveProvider!=null)
			{
				waveProvider.Dispose();
				waveProvider = null;
			}
		}
		#endregion

		public WaveOutTestForm()
		{
			InitializeComponent();
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			timer1.Start();
		}
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			timer1.Dispose();
			timer1 = null;
			Playback_Clear();
		}

		#region UI Button Events

		void Event_Timer_Tick(object sender, EventArgs e) { Text = Playback_Status; }
		void Event_Snd_ButtonAmp(object sender, EventArgs e) { UpdateAmplitude(); }
		void Event_Snd_Play(object sender, EventArgs e) { Playback_Play(); }
		void Event_Snd_Stop(object sender, EventArgs e) { Playback_Stop(); }
		void Event_Snd_Clear(object sender, EventArgs e) { Playback_Clear(); }
		void Event_Snd_Pause(object sender, EventArgs e) { Playback_Pause(); }

		#endregion
		
	}
}
