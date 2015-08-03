#region User/License
// oio * 7/31/2012 * 7:56 PM

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
using System.Drawing;

namespace System.Sound
{
	static class InterpolationExtension
	{
	}
	/// <summary>
	/// A interpolation process which has no bearing  (non functional)
	/// </summary>
	/// <remarks>
	/// We should derive this class on a wave-processor such as that
	/// we have a audio-track or channel that we would be working from
	/// and a standard interface to use to process input.
	/// <para>
	/// WaveFormat structure could very well prove of interest in this case.
	/// </para>
	/// </remarks>
	class Interpolate
	{
		/// <summary>
		/// lerp (solve for y @x)
		/// </summary>
		/// <param name="x">Solving for X</param>
		/// <param name="x1">Point1.X</param>
		/// <param name="y1">Point1.Y</param>
		/// <param name="x2">Point2.X</param>
		/// <param name="y2">Point2.Y</param>
		/// <returns></returns>
		public static int Process(int x, int x1, int y1, int x2, int y2)
		{
			return (x1 == x2) ? y1 :
				Convert.ToInt32( (x-x1) * (y2-y1) / ( x2-x1 ) + y1 );
		}
		public static int Process(int x, Point a, Point b)
		{
			return Process(x,a.X,a.Y,b.X,b.Y);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="x">Solving for y at x</param>
		/// <param name="a"></param>
		/// <param name="b"></param>
		/// <returns></returns>
		public static float Process(int x, FloatPoint a, FloatPoint b)
		{
			return Process(x,a.X,a.Y,b.X,b.Y);
		}
		/// <summary>
		/// lerp (solve for y @x)
		/// </summary>
		/// <param name="x">Solving for X</param>
		/// <param name="x1">Point1.X</param>
		/// <param name="y1">Point1.Y</param>
		/// <param name="x2">Point2.X</param>
		/// <param name="y2">Point2.Y</param>
		public static float Process(float x, float x1, float y1, float x2, float y2)
		{
			return (x1 == x2) ? y1 : Convert.ToSingle( (x-x1) * (y2-y1) / ( x2-x1 ) + y1 );
		}
	}
}
