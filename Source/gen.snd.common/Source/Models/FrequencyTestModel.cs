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
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace gen.snd.Models
{
	public class FreqTableInfo : gen.snd.Core.PropertyChange
	{
		public double IterationIncrement {
			get { return iterationIncrement; }
			set { iterationIncrement = value; OnPropertyChanged("IterationIncrament"); }
		} double iterationIncrement;
		
		public double IterationStart {
			get { return iterationStart; }
			set { iterationStart = value; OnPropertyChanged("IterationStart"); }
		} double iterationStart;
		
		public double IterationStop {
			get { return iterationStop; }
			set { iterationStop = value; OnPropertyChanged("IterationStop"); }
		} double iterationStop;
		
		public double IterationOffset {
			get { return iterationOffset; }
			set { iterationOffset = value; OnPropertyChanged("IterationOffset"); }
		} double iterationOffset;
		
		public double HzFrequency {
			get { return hzFreq; }
			set { hzFreq = value; OnPropertyChanged("HzFrequency"); }
		} double hzFreq;
		
		public double Power {
			get { return power; }
			set { power = value; OnPropertyChanged("Power"); }
		} double power;
		
		public double PowerDivisor {
			get { return powerDivisor; }
			set { powerDivisor = value; OnPropertyChanged("PowerDivisor"); }
		} double powerDivisor;
		
		protected override void OnPropertyChanged(string property)
		{
			base.OnPropertyChanged(property);
		}
		
		public IEnumerator<object[]> Calculate()
		{
			for (
				double i = IterationStart;
				i <= IterationStop;
				i += IterationIncrement
			) yield return new object[]
			{
				string.Format("{0:0000#}",i), Frequency.note(HzFrequency,i,Power,IterationOffset,PowerDivisor)
			};
		}

	}
}


