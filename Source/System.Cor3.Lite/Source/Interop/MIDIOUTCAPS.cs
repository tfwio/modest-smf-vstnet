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
	/// <summary>
	/// Note usage of CharSet.Auto in GetDevCaps.
	/// </summary>
	[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Auto )]
	public struct MIDIOUTCAPS
	{
		public  short	wMid;
		public  short	wPid;
		public  int		vDriverVersion;
		[MarshalAs(UnmanagedType.ByValTStr, SizeConst=32)] public  string	szPname;
		public  short	wTechnology;
		private short	wVoices;
		public  short	wNotes;
		public  short	wChannelMask;
		public  int		dwSupport;
	}
}
