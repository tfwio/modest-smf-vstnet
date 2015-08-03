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
		NoteTransport BarSegment { get; set; }

		void Play();
		void Stop();
		void Pause();
		
		bool IsPaused { get; }
		bool IsRunning { get; }
		
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
		/// Rate, Channels, Latency settings.
		/// </summary>
		ITimeConfiguration Settings { get; }
		/// <summary>
		/// This process is called each buffer-process to update a TIMER!
		/// </summary>
		event EventHandler<NAudioVSTCycleEventArgs> BufferCycle;
		/// <summary>
		/// Used to attach controls to the audio process such as volume, etc...
		/// </summary>
		event EventHandler PlaybackStarted;
		event EventHandler PlaybackStopped;
	}

}
