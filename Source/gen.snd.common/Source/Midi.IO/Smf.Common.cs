#region User/License
// oio * 2005-11-12 * 04:19 PM
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
#region Using
using System;
#endregion

namespace DspAudio.Midi.Extra
{
	static class Common
	{
		public const double timeDividend		= 60000000;
		public const int msg0xA0				= 0xA0;
	
		public const string resSequenceNumber	= "Sequence Number ???";
		public const string resRSENote			= "n#{0} on {1}";
		public const string resTimeMsPQn		= "{0:N3} ms-p/♪ — {1}";
		public const string msg_chanel			= "{0}";
		public const string msg_time_format		= "{0}";
		public const string resEndTrack			= "End of track";
		public const string resSysexNImpl		= "sysex: string not implemented";
		public const string res0xA0				= "0xFFAx: Message not supported";
		public const string resFF54				= "0xFF54: ¿ SMPTE ?";
		public const string resUndetermined		= "Undetermined Message";
	}
}
