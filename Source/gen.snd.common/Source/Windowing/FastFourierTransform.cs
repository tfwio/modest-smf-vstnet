/* oio * 6/18/2014 * Time: 4:18 AM
 */
using System;
using Complex = System.Drawing.FloatPoint;
namespace gen.snd.Windowing
{
	
	
	static class FFTExt
	{
		static public Complex Factor(this Complex me, Complex c)
		{
			return new Complex(
				(me.X * c.X) - (me.Y * c.Y),
				(me.Y * c.X) + (me.X * c.Y)
			);
		}
		static public Complex Root(this Complex me, bool forward)
		{
			float inv = (float)Math.Sqrt((1.0f + me.X) / 2.0f);
			return  new Complex( forward ? inv : -inv, Math.Sqrt((1.0f - me.X) / 2.0f) );
		}
	}
	
	/// <summary>
	/// Summary description for FastFourierTransform.
	/// </summary>
	public class FastFourierTransform
	{
		/// <summary>
		/// Calculate the number of points
		/// </summary>
		/// <param name="m"></param>
		/// <returns></returns>
		static public int CountPoints(int m)
		{
			int count = 1;
			for (int i = 0; i < m; i++) count *= 2;
			return count;
		}
		static public void BitReversal(Complex[] data, int n)
		{
			Complex t = Complex.Empty;
			int i2 = n >> 1;
			int j = 0;
			for (int i = 0; i < n - 1; i++)
			{
				if (i < j) {
					t = data[i];
					data[i] = data[j];
					data[j] = t;
				}
				int k = i2;
				while (k <= j) { j -= k; k >>= 1; }
				j += k;
			}
		}
		/// <summary>
		// This computes an in-place complex-to-complex FFT
		// x and y are the real and imaginary arrays of 2^m points.
		/// </summary>
		/// <param name="forward"></param>
		/// <param name="m">is this the mean, or the maximum?</param>
		/// <param name="data"></param>
		public static void FFT(bool forward, int m, Complex[] data)
		{
			int
				i, i2, // counter
			j = 0,     // counter
			k,         // counter
			l, l1, l2, // counter
			n = CountPoints(m);
			Complex c = Complex.Empty; // cycle? coefficient?
			Complex t = Complex.Empty; // time?
			Complex u = Complex.Empty; //

			// Do the bit reversal
			// ============================================================================
			i2 = n >> 1;
			for (i = 0; i < n - 1; i++)
			{
				if (i < j)
				{
					t = data[i];
					data[i] = data[j];
					data[j] = t;
				}
				k = i2;
				while (k <= j) { j -= k; k >>= 1; }
				j += k;
			}

			// Compute the FFT
			// ============================================================================
			c = Complex.RootComplex;
			l2 = 1;
			for (l = 0; l < m; l++)
			{
				l1 = l2;
				l2 <<= 1;
				u = new Complex(1.0f, 0.0f);
				
				for (j = 0; j < l1; j++)
				{
					for (i = j; i < n; i += l2)
					{
						int i1 = i + l1;
						t = data[i1].Factor(u);
						data[i1] = data[i] - t;
						data[i] += t;
					}
					u = u.Factor(c);
				}
				c = c.Root(forward);
			}

			// Scaling for forward transform
			// ============================================================================
			if (forward) for (i = 0; i < n; i++) data[i] /= n;
		}

		/// <summary>
		/// Applies a Hamming Window
		/// </summary>
		/// <param name="n">Index into frame</param>
		/// <param name="frameSize">Frame size (e.g. 1024)</param>
		/// <returns>Multiplier for Hamming window</returns>
		public static double HammingWindow(int n, int frameSize)
		{
			return 0.54 - 0.46 * Math.Cos((2 * Math.PI * n) / (frameSize - 1));
		}

		/// <summary>
		/// Applies a Hann Window
		/// </summary>
		/// <param name="n">Index into frame</param>
		/// <param name="frameSize">Frame size (e.g. 1024)</param>
		/// <returns>Multiplier for Hann window</returns>
		public static double HannWindow(int n, int frameSize)
		{
			return 0.5 * (1 - Math.Cos((2 * Math.PI * n) / (frameSize - 1)));
		}

		/// <summary>
		/// Applies a Blackman-Harris Window
		/// </summary>
		/// <param name="n">Index into frame</param>
		/// <param name="frameSize">Frame size (e.g. 1024)</param>
		/// <returns>Multiplier for Blackmann-Harris window</returns>
		public static double BlackmannHarrisWindow(int n, int frameSize)
		{
			return 0.35875 - (0.48829 * Math.Cos((2 * Math.PI * n) / (frameSize - 1))) + (0.14128 * Math.Cos((4 * Math.PI * n) / (frameSize - 1))) - (0.01168 * Math.Cos((6 * Math.PI * n) / (frameSize - 1)));
		}
	}
}


