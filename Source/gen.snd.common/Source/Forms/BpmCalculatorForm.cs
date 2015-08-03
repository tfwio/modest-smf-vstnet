#region User/License
/*
 oio * 7/16/2012 * 12:46 PM
 */
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

using gen.snd.Midi;

namespace gen.snd.Forms
{

	/// <summary>
	/// Description of Form1.
	/// </summary>
	public partial class BpmCalculatorForm : Form
	{
		bool IsPulseMode = false;
		SampleClock s2mbqt = null;
		public BpmCalculatorForm()
		{
			InitializeComponent();
			Perform();
		}
		
		void Perform()
		{
			double bpm = (double)this.udBpm.Value;
			double div1 = (double)udDiv1.Value, div2 = (double)udDiv2.Value, m = (double)nMulti.Value;
			double result  = BpmCalculator.GetHz(bpm,div1,div2,m);
			
			try {
				this.udOutput.Value = (decimal) result;
				this.udOutput.BackColor = NumericUpDown.DefaultBackColor;
			} catch {
				this.udOutput.Value = 0;
				this.udOutput.BackColor = Color.Red;
			}
			s2mbqt = new SampleClock(
				(double)s2m_Samples.Value,
				(int)s2m_Rate.Value,
				(double)s2m_BPM.Value,
				(int)s2m_Division.Value,
				true);
		}
		
		void Button1Click(object sender, EventArgs e)
		{
			Perform();
		}
		
		void Event_Go(object sender, EventArgs e)
		{
			ResetIntervals();
			if (!IsPulseMode)
			{
				s2mbqt.SolvePPQ((double)s2m_Samples.Value,(int)s2m_Rate.Value,(double)s2m_BPM.Value,(int)s2m_Division.Value,true);
				s2m_MSPQN.Text = s2mbqt.MSPQN.ToString("N");
				s2m_Out_Time.Text = s2mbqt.TimeString;
				s2m_Out_MBQT.Text = s2mbqt.MeasureString;
				s2m_TicksPerClock.Text = s2mbqt.PulsesPerPPQDivision.ToString("N0");
				s2m_SamplesPerClock.Text = s2mbqt.SamplesPerClock.ToString("N5");
				s2m_ClocksAtSample.Text = s2mbqt.ClocksAtPosition.ToString("N5");
				s2m_Pulses.Value = (decimal)s2mbqt.Pulses;
			}
			else
			{
				s2mbqt.SolveSamples((double)s2m_Pulses.Value,(int)s2m_Rate.Value,(double)s2m_BPM.Value,(int)s2m_Division.Value,true);
				s2m_MSPQN.Text = s2mbqt.MSPQN.ToString("N");
				s2m_Out_Time.Text = s2mbqt.TimeString;
				s2m_Out_MBQT.Text = s2mbqt.MeasureString;
				s2m_TicksPerClock.Text = s2mbqt.PulsesPerPPQDivision.ToString("N0");
				s2m_SamplesPerClock.Text = s2mbqt.SamplesPerClock.ToString("N5");
				s2m_ClocksAtSample.Text = s2mbqt.ClocksAtPosition.ToString("N5");
				s2m_Samples.Value = (decimal)s2mbqt.Samples;
			}
		}
		
		void Event_S2MBufferSize(object sender, EventArgs e)
		{
			s2m_Samples.Increment = s2m_BufferSize.Value;
			((Action<object,EventArgs>)Event_Go).Invoke(null,null);
		}
		
		void S2m_PulsesEnter(object sender, EventArgs e) { IsPulseMode = true; }
		void S2m_SamplesEnter(object sender, EventArgs e) { IsPulseMode = false; }
		
		void Event_ResetPulseIntervals(object sender, EventArgs e)
		{
			ResetIntervals();
		}
		
		void ResetIntervals()
		{
			       if (rPulse.Checked) {
				s2m_Pulses.Increment = 1;
			} else if (rQuarters.Checked) {
				s2m_Pulses.Increment = s2m_Division.Value;
			} else if (rClock.Checked) {
				s2m_Pulses.Increment = s2m_Division.Value / 24;
			}
		}
	}
}
