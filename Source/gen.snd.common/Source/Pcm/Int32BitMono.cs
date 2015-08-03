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
	public struct Int32BitMono: IBinaryIO<Int32BitMono>
	{
		public int Sample;
	
		static public Int32BitMono[] Provide(Stream s, long offset, int count)
		{
			return IOHelper.ReadChunk<Int32BitMono>(offset,count,s);
		}
		public Int32BitMono Write(BinaryWriter w) { w.Write(Sample); return this; }
		public Int32BitMono Read(BinaryReader r) { Sample = r.ReadInt32(); return this; }
		
		#region Op
		static public Int32BitMono operator +(Int32BitMono a, Int32BitMono b) { a.Sample += b.Sample; return a; }
		static public Int32BitMono operator -(Int32BitMono a, Int32BitMono b) { a.Sample -= b.Sample; return a; }
		static public Int32BitMono operator *(Int32BitMono a, Int32BitMono b) { a.Sample *= b.Sample; return a; }
		static public Int32BitMono operator /(Int32BitMono a, Int32BitMono b) { a.Sample /= b.Sample; return a; }
		static public Int32BitMono operator %(Int32BitMono a, Int32BitMono b) { a.Sample %= b.Sample; return a; }
		#endregion
	}

	public struct Short16BitMono: IBinaryIO<Short16BitMono>
	{
		public short Sample;
		
		public Short16BitMono Write(BinaryWriter w) { w.Write(Sample); return this; }
		public Short16BitMono Read(BinaryReader r) { Sample = r.ReadInt16(); return this; }
	
		static public Short16BitMono[] Provide(Stream s, long offset, int count)
		{
			return IOHelper.ReadChunk<Short16BitMono>(offset,count,s);
		}
//		static public Short16BitMono[] Provide(ref byte[] data)
//		{
//			return IOHelper.ConvertBytes<Short16BitMono>(ref data);
//		}
		
		#region Op
		static public Short16BitMono operator +(Short16BitMono a, Short16BitMono b) { a.Sample += b.Sample; return a; }
		static public Short16BitMono operator -(Short16BitMono a, Short16BitMono b) { a.Sample -= b.Sample; return a; }
		static public Short16BitMono operator *(Short16BitMono a, Short16BitMono b) { a.Sample *= b.Sample; return a; }
		static public Short16BitMono operator /(Short16BitMono a, Short16BitMono b) { a.Sample /= b.Sample; return a; }
		static public Short16BitMono operator %(Short16BitMono a, Short16BitMono b) { a.Sample %= b.Sample; return a; }
		#endregion
		
		static public implicit operator Short16BitMono(short value) { return new Short16BitMono(value); }
		static public implicit operator Short16BitMono(float value) { return new Short16BitMono(Convert.ToInt16(value)); }
		
		public Short16BitMono(short value) { this.Sample = value; }
	}

}
