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

namespace DspAudio
{
	public class Int32Region : Region<int>{
		public Int32Region(decimal a, decimal b) { Begin = Convert.ToInt32(a); Length = Convert.ToInt32(b); }
		public Int32Region(double a, double b) { Begin = Convert.ToInt32(a); Length = Convert.ToInt32(b); }
		public Int32Region(float a, float b) { Begin = Convert.ToInt32(a); Length = Convert.ToInt32(b); }
		public Int32Region(int a, int b) { Begin = a; Length = (b); }
		static public implicit operator Int32Region(DoubleRegion r) { return new Int32Region(r.Begin,r.Length); }
	}
}
