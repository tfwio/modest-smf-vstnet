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
using on.smfio;

namespace gen.snd
{
	/// <summary>
	/// For implementation under NAudioVST, INaudioVST in the containing Assembly.
	/// </summary>
	public interface IBufferInfo
	{
	  /// <summary>
	  /// TODO: Document
	  /// </summary>
		SampleClock SampleTime { get; }
		/// <summary>
		/// Current number of samples processed divide by number of channels.
		/// <para>
		/// In NAudioVST, we actually set this via an event-handler triggered
		/// by way of an event named OnBufferCycle.
		/// </para>
		/// </summary>
		/// <remarks>
		/// This property is VERY IMPORTANT to our audio process!
		/// </remarks>
		double SampleOffset { get;set; }
		
		/// <summary>
		/// This represents the current location of the audio buffer process.
		/// It is reset to zero after each buffer cycle is processed and increments
		/// throughout the process-replacing mechanism.
		/// </summary>
		double BufferIncrement { get;set; }
		
		/// <summary>
		/// <para>
		/// Get the last sample length (provided per buffer cycle).
		/// </para>
		/// <para>
		/// The number is a result of the buffer calculation which divides by the number of
		/// channels.  Since we've already calculated as such, we're avoiding cpu cycles here.
		/// </para>
		/// </summary>
		double CurrentSampleLength { get; }
	}
}
