#region User/License
// oio * 8/1/2012 * 11:17 PM

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
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace gen.snd.Forms
{
	/// <summary>
	/// Description of FrequencyTestControl.
	/// </summary>
	public partial class FrequencyTestControl : UserControl
	{
		public double IterationStart {
			get { return iterationStart; }
			set { iterationStart = value; }
		} double iterationStart;
		public double IterationStop {
			get { return iterationStop; }
			set { iterationStop = value; }
		} double iterationStop;
		public double IterationOffset {
			get { return iterationOffset; }
			set { iterationOffset = value; }
		} double iterationOffset;
		public double IterationIncrament;
		public double Freq {
			get { return freq; }
			set { freq = value; }
		} double freq;
		public double Power {
			get { return power; }
			set { power = value; }
		} double power;
		public double PowerDivisor {
			get { return powerDivisor; }
			set { powerDivisor = value; }
		} double powerDivisor;
		/// <summary>
		/// There are 6 values:
		/// </summary>
		/// <param name="values">i1, i2, iIncrament, iOffset, freq, square, squareDivisor</param>
		public void Calculate(params double[] values)
		{
			dataGridView1.Rows.Clear();
			double i = double.NaN;
			{
				iterationStart = values[0];
				iterationStop = values[1];
				IterationIncrament = values[2];
				iterationOffset = values[3];
				freq= values[4];
				power = values[5];
				powerDivisor = values[6];
			}
			for (
				i = iterationStart;
				i <= iterationStop;
				i += IterationIncrament )
			{
				dataGridView1.Rows.Add(
					string.Format("{0:0000#}",i),
					Frequency.note(freq,i,power,iterationOffset,powerDivisor)
				);
			}
		}
		
		public FrequencyTestControl()
		{
			InitializeComponent();
		}
	}
}
