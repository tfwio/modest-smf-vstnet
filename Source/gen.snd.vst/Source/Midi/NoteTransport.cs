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
using System.Drawing;
using System.Linq;

using gen.snd.Midi;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Interop.Host;
using NAudio.Wave;

namespace gen.snd.Midi
{

	/// <summary>
	/// This is to be used sparingly, when ever bar-selection in piano-view
	/// is changed (such as panning horizongally or vertically within the bounds
	/// of a Piano-layout control
	/// </summary>
	public class NoteTransport
	{
		double wedge, multiply, division;
		
		public LoopPoint LoopRegion {
			get { return loopRegion; }
			set { loopRegion = value;/* Notify("BarPosition");*/ }
		} LoopPoint loopRegion;
		
		/// <summary>
		/// Contains a read only piano info 
		/// </summary>
		public PianoCalculator PianoInfo {
			get { return pianoInfo; }
		} readonly PianoCalculator pianoInfo;
		
		const string drawdotformat =
			"Q: {0:N0}, Pulse: {1:N0}, MBQ: {2} — {3,-2}{4} — {5,3} ({6:000}-{7:000})";
		
		/// <summary></summary>
		public FloatPoint ClientInput { get; set; }
		/// <summary></summary>
		public double Quarter { get;set; }
		//			public double Division { get;set; }
		/// <summary></summary>
		public double Pulse { get;set; }
		/// <summary></summary>
		public int    Key { get;set; }
		/// <summary></summary>
		public string KeySharp { get { return MidiReader.SmfStringFormatter.GetKeySharp(Key); } }
		/// <summary></summary>
		public int    KeyOctave { get { return MidiReader.SmfStringFormatter.GetOctave(Key); } }
		/// <summary>
		/// Key domain; provides the top key in piano-view.
		/// Upper &gt; Lower.
		/// </summary>
		public double Upper { get;set; }
		/// Key domain; total number of midi-keys are visible in piano-view.
		public double Count { get;set; }
		/// Key domain; The visible midi-key in piano-view; Lower &lt; Upper
		public double Lower { get { return Upper-Count; } }
		
		public double GetPulses(double division)
		{
			return Quarter  * division;
		}
		/// <summary>
		/// Approximates: PianoCalculator, Key, Count, Quarter and Pulse.
		/// Also acts as storage for LoopPoint (piano-view transport or 
		/// buffer-transport).
		/// </summary>
		/// <param name="clientMouse">(stored); ClientInput</param>
		/// <param name="loopRegion">(stored); LoopRegion</param>
		/// <param name="pxNode">(ref) ignored after usage (only referenced)</param>
		/// <param name="wedge">(stored); Typically (in local terms): pxNode.X * 4 * 4</param>
		/// <param name="multiply">(stored); Typically 4</param>
		/// <param name="division">(stored); From sequencer or midi program</param>
		/// <param name="upper">(stored); Top visible midi key. (Key and Count values are provided from this value in relation to clientMouse)</param>
		public NoteTransport(
			FloatPoint clientMouse,
			LoopPoint loopRegion,
			FloatPoint pxNode,
			double wedge,
			double multiply,
			double division,
			double upper)
		{
			this.ClientInput = clientMouse;
			this.loopRegion = loopRegion;
			this.pianoInfo = PianoCalculator.Create(clientMouse,pxNode.X*4);
			this.wedge = wedge; // nodewidth * 4 * 4
			this.multiply = multiply; // 4
			this.division = division;
			//                           quarter    bar
			//                                  beat    measure
			// wedge * multiply  ==  nodewidth * 4 * 4 * 4
			Quarter = ClientInput.X / wedge * multiply;
			Pulse = GetPulses(division);
			Upper = upper;
			Key = Upper.ToInt32() - Convert.ToDouble(ClientInput.Y/pxNode.Y).FloorMinimum(0).ToInt32();
			Count = Convert.ToDouble(pxNode.Y).FloorMinMax(0,127).ToInt32()-1;
		}
		
		public override string ToString() { return Human; }
		public string Human
		{
			get
			{
				return
					string.Format(drawdotformat,
					              Quarter.FloorMinimum(0),   // quarter
					              Pulse,     // pulse
					              pianoInfo.MBQ,   // mbq
					              KeySharp, // midi key
					              KeyOctave,   // octave
					              Key,				// key
					              Lower,  // key-lower
					              Upper   // key-upper
					             );
			}
		}
	}
}
