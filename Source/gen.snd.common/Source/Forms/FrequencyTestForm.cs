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
using System.Drawing;
using System.Windows.Forms;

namespace gen.snd.Forms
{
	/// <summary>
	/// Description of FrequencyTestForm.
	/// </summary>
	public partial class FrequencyTestForm : Form
	{
		public FrequencyTestForm()
		{
			InitializeComponent();
			Text = string.Format("{0}: dec: {1}, {1:X}",Text, (12 * 16) << 2);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			this.frequencyTestControl1.Calculate(
				double.Parse(tStart.Text),
				double.Parse(tStop.Text),
				double.Parse(tIncr.Text),
				double.Parse(tOffset.Text),
				double.Parse(tFreq.Text),
				double.Parse(tPow.Text),
				double.Parse(tPowD.Text)
			);
		}
	}
}
