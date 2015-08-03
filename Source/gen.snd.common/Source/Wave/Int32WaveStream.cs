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
using NAudio.Wave;

namespace gen.snd.Wave
{
	abstract public class Int32WaveStream : WaveStream
	{
		public Int32WaveStream(int rate, int bps, int nch)
		{
			_fmt = new WaveFormat(rate,bps,nch);
		}
		public Int32WaveStream() : this(44100,16,2)
		{
		}
		
		public override WaveFormat WaveFormat {
			get {
				return _fmt;
			}
		} readonly WaveFormat _fmt;
		
//		public override int Read(byte[] buffer, int offset, int count)
//		{
//			throw new NotImplementedException();
//		}
		
//		public override long Position {
//			get {
////				throw new NotImplementedException();
//				return 0;
//			}
//			set {
////				throw new NotImplementedException();
//			}
//		}
		
//		public override long Length {
//			get {
//				return 0;
////				throw new NotImplementedException();
//			}
//		}
	}

}
