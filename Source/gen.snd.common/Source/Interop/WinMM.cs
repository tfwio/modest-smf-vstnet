#region User/License
// oio * 8/28/2012 * 9:02 PM

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
using System.Runtime.InteropServices;

namespace gen.snd.Interop
{
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Auto )]
	public struct midi_caps
	{
		public  short	w_mid;
		public  short	w_pid;
		public  int		driver_version;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)]
		public  string	szPname;
	}
	class MIDI_DevCaps : IEnumerable<MIDIOUTCAPS>
	{
		/// <summary>
		/// needs values from windows-api-header (mmsystem.h)
		/// </summary>
		enum ApiResult
		{
			/// <summary>
			/// The specified device identifier is out of range.
			/// </summary>
			MMSYSERR_BADDEVICEID,
			/// <summary>
			/// The specified pointer or structure is invalid.
			/// </summary>
			MMSYSERR_INVALPARAM,
			/// <summary>
			/// The driver is not installed.
			/// </summary>
			MMSYSERR_NODRIVER,
			/// <summary>
			/// The system is unable to load mapper string description.
			/// </summary>
			MMSYSERR_NOMEM
		}
		static readonly int SizeOfOutCaps = Marshal.SizeOf(typeof(MIDIOUTCAPS));
		uint NumDevs { get { return WinMM.midiOutGetNumDevs(); } }
		public IEnumerator<MIDIOUTCAPS> GetEnumerator()
		{
			uint api_result;
			MIDIOUTCAPS MC;
			for (int i=0; i < NumDevs; i++)
			{
				api_result = WinMM.midiOutGetDevCaps(i, out MC, SizeOfOutCaps);
				yield return MC;
			}
		}
		
		System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
		{
			return GetEnumerator() as System.Collections.IEnumerator;
		}
	}
	/// <summary>
	/// Description of WinMM_Midi.
	/// </summary>
	class WinMM
	{
		static public List<MIDIOUTCAPS> EnumerateMidiDev()
		{
			MIDIOUTCAPS MC;
			uint boy = midiOutGetNumDevs();
			int sz = Marshal.SizeOf(typeof(MIDIOUTCAPS));
			List<MIDIOUTCAPS> list = new List<MIDIOUTCAPS>(new MIDI_DevCaps());
			return list;
		}

		const string winmm = "winmm.dll";
		[DllImport(winmm)] extern static public int midiOutOpen(ref int handle, int deviceID, int outproc, int instance, int flags);
		[DllImport(winmm)] extern static public int midiOutOpen(ref IntPtr handle, int deviceID, int outproc, int instance, int flags);
		[DllImport(winmm)] extern static public int midiOutClose(int handle);
		[DllImport(winmm)] extern static public int midiOutReset(int handle);
		[DllImport(winmm)] extern static public int midiOutShortMsg(int handle, int message);
		[DllImport(winmm)] extern static public uint midiOutGetNumDevs();
//		[ DllImport(winmm)] extern static public short midiOutGetNumDevs();
		
		[ DllImport(winmm, CharSet=CharSet.Ansi) ]
		extern static public uint midiOutGetDevCapsA(int uDeviceID, out MIDIOUTCAPS lpMidiOutCaps, int cbMidiOutCaps);
		[ DllImport(winmm, CharSet=CharSet.Unicode) ]
		extern static public uint midiOutGetDevCapsW(int uDeviceID, out MIDIOUTCAPS lpMidiOutCaps, int cbMidiOutCaps);
		[ DllImport(winmm, CharSet=CharSet.Auto) ]
		extern static public uint midiOutGetDevCaps(int uDeviceID, out MIDIOUTCAPS lpMidiOutCaps, int cbMidiOutCaps);
	}

}
