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
	public class Region<T> where T:struct 
	{
	//		public Region(T a, T b)
	//		{
	//			Begin = a;
	//			Length = b;
	//		}
	//		public Region()
	//		{
	//		}
		
	//		public static T Empty {
	//			get { return new Region<T>((T)0,(T)0); }
	//		}
		
		public T Begin { get;set; }
		public T Length { get;set; }
		
		static public Region<T> Create(T a, T b)
		{
			return Create<T>(a,b);
		}
		static public Region<T1> Create<T1>(T1 a, T1 b) where T1:struct
		{
			Region<T1> region = new Region<T1>();
			region.Begin = a;
			region.Length = b;
			return region;
		}
	//		/// <summary>
	//		/// int32, single, and double can be cloned.
	//		/// </summary>
	//		/// <param name="copy"></param>
	//		/// <returns></returns>
	//		static public Region<T> Convert<T1>(Region<T1> copy) where T1:struct
	//		{
	//			Region<T> r = new Region<T>();
	//			if (T is int) { r.Begin = Convert.ToInt32(copy.Begin); r.Length = (T)Convert.ToInt32(copy.Length); }
	//			if (T is double) { r.Begin = (T)Convert.ToDouble(copy.Begin); r.Length = (T)Convert.ToDouble(copy.Length); }
	//			if (T is float) { r.Begin = (T)Convert.ToSingle(copy.Begin); r.Length = (T)Convert.ToSingle(copy.Length); }
	//			return r;
	//		}
	//		static public implicit operator DoubleRegion(Int32Region region) { return (DoubleRegion)DoubleRegion.Clone<Double>(region); }
	//		static public implicit operator Int32Region(DoubleRegion region) { return (Int32Region)Int32Region.Clone<Double>(region); }
	}
}
