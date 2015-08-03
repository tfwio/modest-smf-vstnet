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
using gen.snd.Midi;
using gen.snd.Midi.Common;

namespace gen.snd
{
	public class TimeConfiguration : ITimeConfiguration
	{
		/// <summary>
		/// to be marked as internal
		/// </summary>
		public static TimeConfiguration Instance;
		
		static TimeConfiguration()
		{
			Instance = new TimeConfiguration();
			// Playback Device
			Instance.Rate			     = 48000;//
			Instance.Channels      = 2;
			Instance.Latency	     = 100;
			// Midi Configuration
			Instance.Division		   = 480;
			Instance.Tempo		     = 120;
			Instance.TimeSignature = new MidiTimeSignature(4,4,24,4);
			Instance.KeySignature  = new MidiKeySignature(KeySignatureType.C,true);
			// Midi Parser
			Instance.IsSingleZeroChannel = false;
		}
		
		public void FromMidi(IMidiParser parser)
		{
			this.TimeSignature = parser.TimeSignature;
			this.KeySignature = parser.KeySignature;
			this.Division = parser.SmfFileHandle.Division;
			this.Tempo = parser.MidiTimeInfo.Tempo;
		}
		
		public int Channels { get;set; }
		public int Rate { get;set; }
		public float RateF { get { return Convert.ToSingle(Rate); } }
		public int Latency { get;set; }
		/// <summary>
		/// Each MIDI file has within it a Division setting which stores
		/// the number of ticks per quarter note (AKA: TPQN, TPQ, pulses per quarter or PPQ).
		/// </summary>
		public int Division  { get;set; }
		public double Tempo { get;set; }
		public bool IsSingleZeroChannel { get;set; }
		
		public MidiKeySignature KeySignature { get;set; }
		public MidiTimeSignature TimeSignature { get;set; }
		
		double barStart;
		
		public double BarStart {
			get { return barStart; }
			set { barStart = value; }
		}
		
		double barLength=4;
		
		public double BarLength {
			get { return barLength; }
			set { barLength = value; }
		}
		
		public double BarStartPulses {
			get { return barStartPulsesPerQuarter; }
			set { barStartPulsesPerQuarter = value; }
		} double barStartPulsesPerQuarter=4;
		
		public double BarLengthPulses {
			get { return barLengthPulsesPerQuarter; }
			set { barLengthPulsesPerQuarter = value; }
		} double barLengthPulsesPerQuarter=4;
	}
}
