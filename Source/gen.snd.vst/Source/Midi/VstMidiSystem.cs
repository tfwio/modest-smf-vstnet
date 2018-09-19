﻿#region User/License
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
using System.Linq;

using on.smfio;
using on.smfio.util;

namespace gen.snd.Midi
{
	static public class VstMidiSystem
	{
		static public Jacobi.Vst.Core.VstMidiEvent ToVstMidiEvent(this MIDIMessage item, int offset, ITimeConfiguration config, SampleClock c)
		{
	   // byte b0 = (config.IsSingleZeroChannel) ? (byte)item.MessageBit : item.Data[0];
			int samples = c.SolveSamples(item.Pulse).Samples32Floor - offset;
			return new Jacobi.Vst.Core.VstMidiEvent(samples, 0, 0, new byte[4]{ item.Data[0], item.Data[1], item.Data[2], 0 }, 0 , 0 );
		}
		static public Jacobi.Vst.Core.VstMidiSysExEvent ToVstMidiSysex(this MIDIMessage item, int offset, ITimeConfiguration config, SampleClock c)
		{
			int samples = c.SolveSamples(item.Pulse).Samples32Floor-offset;
			return new Jacobi.Vst.Core.VstMidiSysExEvent(samples,(item as on.smfio.SysExMessage).SystemData);
		}
	}
}
