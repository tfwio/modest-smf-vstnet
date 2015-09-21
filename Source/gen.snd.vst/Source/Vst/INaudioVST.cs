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

namespace gen.snd.Vst
{
	/// <remarks>
	/// We are missing a method of checking to see if any Midi Events are ready
	/// to be fired.
	/// </remarks>
	public interface INaudioVST: IBufferInfo
	{
	  /// <summary>
    /// TODO: Document
	  /// </summary>
		NoteTransport BarSegment { get; set; }
    /// <summary>
    /// TODO: Document
    /// </summary>
		void Play();
		/// <summary>
    /// TODO: Document
		/// </summary>
		void Stop();
		/// <summary>
    /// TODO: Document
		/// </summary>
		void Pause();
		/// <summary>
    /// TODO: Document
		/// </summary>
		bool IsPaused { get; }
		/// <summary>
    /// TODO: Document
		/// </summary>
		bool IsRunning { get; }
		/// <summary>
    /// TODO: Document
		/// </summary>
		Guid DriverId { get;set; }
		/// <summary>
		/// The HostCommandStub is our VstHost implementation.
		/// </summary>
		/// <remarks>
		/// In the future this will be a interface based IVstHostCommandStub
		/// in addition to something helpful to a VstEvent Queue.
		/// </remarks>
		HostCommandStub VstHostCommandStub { get; }
		/// <summary>
		/// Main timing and audio buffer configuration: Rate, Channels, Latency settings.
		/// </summary>
		/// <remarks>
		/// Currently our (only) app hosts a static copy of this particular Property.
		/// —Perhaps to underline its significance.
		/// </remarks>
		/// <seealso cref="TimeConfiguration"/>
		/// <seealso cref="TimeConfiguration.Instance"/>- TimeConfiguration.Instance
		ITimeConfiguration Settings { get; }
		/// <summary>
		/// This process is called each buffer-process to update a TIMER!
		/// </summary>
		event EventHandler<NAudioVSTCycleEventArgs> BufferCycle;
		/// <summary>
		/// Used to attach controls to the audio process such as volume, etc...
		/// </summary>
		event EventHandler PlaybackStarted;
		/// <summary>
    /// TODO: Document
		/// </summary>
		event EventHandler PlaybackStopped;
	}

}
