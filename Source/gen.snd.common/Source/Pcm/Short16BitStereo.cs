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
	public struct Short16BitStereo : IBinaryIO<Short16BitStereo>
	{
		public short Left;
		public short Right;
		
		public Short16BitStereo Write(BinaryWriter w) { w.Write(Left); w.Write(Right); return this; }
		public Short16BitStereo Read(BinaryReader r) { Left = r.ReadInt16(); Right = r.ReadInt16(); return this; }
	
		static public Short16BitStereo[] Provide(Stream s, long offset, int count)
		{
			return IOHelper.ReadChunk<Short16BitStereo>(offset,count,s);
		}
		
		#region Equals and GetHashCode implementation
		public override bool Equals(object obj)
		{
			return (obj is Short16BitStereo) && Equals((Short16BitStereo)obj);
		}
		
		public static bool operator ==(Short16BitStereo lhs, Short16BitStereo rhs)
		{
			return lhs.Equals(rhs);
		}
		
		public static bool operator !=(Short16BitStereo lhs, Short16BitStereo rhs)
		{
			return !(lhs == rhs);
		}
		#endregion

		#region Op
		static public Short16BitStereo operator +(Short16BitStereo a, Short16BitStereo b) { a.Left += b.Left; a.Right += b.Right; return a; }
		static public Short16BitStereo operator -(Short16BitStereo a, Short16BitStereo b) { a.Left -= b.Left; a.Right -= b.Right; return a; }
		static public Short16BitStereo operator *(Short16BitStereo a, Short16BitStereo b) { a.Left *= b.Left; a.Right *= b.Right; return a; }
		static public Short16BitStereo operator /(Short16BitStereo a, Short16BitStereo b) { a.Left /= b.Left; a.Right /= b.Right; return a; }
		static public Short16BitStereo operator %(Short16BitStereo a, Short16BitStereo b) { a.Left %= b.Left; a.Right %= b.Right; return a; }
		
		static public Short16BitStereo operator +(Short16BitStereo a, short b) { a.Left += b; a.Right += b; return a; }
		static public Short16BitStereo operator -(Short16BitStereo a, short b) { a.Left -= b; a.Right -= b; return a; }
		static public Short16BitStereo operator *(Short16BitStereo a, short b) { a.Left *= b; a.Right *= b; return a; }
		static public Short16BitStereo operator /(Short16BitStereo a, short b) { a.Left /= b; a.Right /= b; return a; }
		static public Short16BitStereo operator %(Short16BitStereo a, short b) { a.Left %= b; a.Right %= b; return a; }
		
		static public Short16BitStereo operator *(Short16BitStereo a, float b) { a.Left = Convert.ToInt16(a.Left*b); a.Right = Convert.ToInt16(a.Left*b); return a; }
		
		#endregion
		
		static public implicit operator Short16BitStereo(short b) { return new Short16BitStereo(b,b); }
		static public implicit operator Short16BitStereo(float b) { return new Short16BitStereo((short)b,(short)b); }
		
		public Short16BitStereo(short l, short r) { this.Left = l; this.Right = r; }
	}
}
