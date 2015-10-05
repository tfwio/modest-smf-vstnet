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

namespace gen.snd
{
	public interface IAudioConfig
	{
		int Rate { get;set; }
		float RateF { get; }
		int Channels { get;set; }
		int Latency { get;set; }
	}
	public interface IMidiConfig
	{
		int Division { get;set; }
		
		double Tempo    { get;set; }
		
		MidiTimeSignature TimeSignature { get;set; }
		
		MidiKeySignature KeySignature { get;set; }
		
//		bool IsSingleZeroChannel { get;set; }
		
		/// <summary>
		/// <para>Why isn't this documented?</para>
		/// (Could it be that) This is the start point of our audio-process when we
		/// begin and thruought playback, in bars.
		/// When playback ends (<see cref="BarLength"/>), then we begin here again.
		/// </summary>
		/// <remarks>
		/// <para>
		/// Once upon a time, I imiplemented a NumericUpDown control which changed this value.
		/// It was later removed since it was never properly implemented.
		/// </para>
		/// <para>
		/// It appears that we're missing something equivelant to 'NextPhase'.<br />
		/// NextPhase would be relevant when the end position falls out of alignment with
		/// our buffer's size, where we need to start at 0 (or BarStart) inside the
		/// specific buffer-interval.
		/// </para>
		/// </remarks>
		double BarStart { get;set; }
		
		/// <summary>
		/// This appears to be un-set (or 0), as it is not referenced. 
		/// </summary>
		/// <remarks>
		/// It appears that this exposes an error.
		/// </remarks>
		double BarStartPulses { get;set; }
		
		/// <summary>
		/// This is used in <see cref="Loop"/>.<see cref="Loop.Length"/>.
		/// ...As well as a particular instance within NAudioVST.One(Loop).
		/// </summary>
		double BarLength { get;set; }
		
		double BarLengthPulses { get;set; }
	}
	
	public interface ITimeConfiguration : IMidiConfig, IAudioConfig {
		void FromMidi(IMidiParser parser);
	}
}
