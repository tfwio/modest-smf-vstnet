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
using System.IO;

namespace gen.snd.Pcm
{
	public struct Int32BitStereo: IBinaryIO<Int32BitStereo>
	{
		public int Left, Right;
	
		public Int32BitStereo Write(BinaryWriter w) { w.Write(Left); w.Write(Right); return this; }
		public Int32BitStereo Read(BinaryReader r) { Left = r.ReadInt32(); Right = r.ReadInt32(); return this; }
		
		static public Int32BitStereo[] Provide(Stream s, long offset, int count)
		{
			return IOHelper.ReadChunk<Int32BitStereo>(offset,count,s);
		}
		
		#region Op
		static public Int32BitStereo operator +(Int32BitStereo a, Int32BitStereo b) { a.Left += b.Left; a.Right += b.Right; return a; }
		static public Int32BitStereo operator -(Int32BitStereo a, Int32BitStereo b) { a.Left -= b.Left; a.Right -= b.Right; return a; }
		static public Int32BitStereo operator *(Int32BitStereo a, Int32BitStereo b) { a.Left *= b.Left; a.Right *= b.Right; return a; }
		static public Int32BitStereo operator /(Int32BitStereo a, Int32BitStereo b) { a.Left /= b.Left; a.Right /= b.Right; return a; }
		static public Int32BitStereo operator %(Int32BitStereo a, Int32BitStereo b) { a.Left %= b.Left; a.Right %= b.Right; return a; }
		#endregion
	}
}
