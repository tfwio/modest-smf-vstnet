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
namespace gen.snd.Forms
{
	partial class FrequencyTestForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.frequencyTestControl1 = new gen.snd.Forms.FrequencyTestControl();
			this.panel1 = new System.Windows.Forms.Panel();
			this.button1 = new System.Windows.Forms.Button();
			this.label3 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.tOffset = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.tStop = new System.Windows.Forms.TextBox();
			this.tIncr = new System.Windows.Forms.TextBox();
			this.tPowD = new System.Windows.Forms.TextBox();
			this.tStart = new System.Windows.Forms.TextBox();
			this.tPow = new System.Windows.Forms.TextBox();
			this.tFreq = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// frequencyTestControl1
			// 
			this.frequencyTestControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.frequencyTestControl1.Freq = 0D;
			this.frequencyTestControl1.IterationOffset = 0D;
			this.frequencyTestControl1.IterationStart = 0D;
			this.frequencyTestControl1.IterationStop = 0D;
			this.frequencyTestControl1.Location = new System.Drawing.Point(0, 68);
			this.frequencyTestControl1.Name = "frequencyTestControl1";
			this.frequencyTestControl1.Power = 0D;
			this.frequencyTestControl1.PowerDivisor = 0D;
			this.frequencyTestControl1.Size = new System.Drawing.Size(698, 255);
			this.frequencyTestControl1.TabIndex = 0;
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.button1);
			this.panel1.Controls.Add(this.label3);
			this.panel1.Controls.Add(this.label7);
			this.panel1.Controls.Add(this.label5);
			this.panel1.Controls.Add(this.label6);
			this.panel1.Controls.Add(this.label4);
			this.panel1.Controls.Add(this.label2);
			this.panel1.Controls.Add(this.tOffset);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.tStop);
			this.panel1.Controls.Add(this.tIncr);
			this.panel1.Controls.Add(this.tPowD);
			this.panel1.Controls.Add(this.tStart);
			this.panel1.Controls.Add(this.tPow);
			this.panel1.Controls.Add(this.tFreq);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(698, 68);
			this.panel1.TabIndex = 1;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(599, 12);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(74, 46);
			this.button1.TabIndex = 7;
			this.button1.Text = "Go";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(136, 41);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(47, 13);
			this.label3.TabIndex = 1;
			this.label3.Text = "Pow Div";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Location = new System.Drawing.Point(454, 41);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(35, 13);
			this.label7.TabIndex = 1;
			this.label7.Text = "Offset";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(309, 41);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(29, 13);
			this.label5.TabIndex = 1;
			this.label5.Text = "Stop";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(463, 15);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(19, 13);
			this.label6.TabIndex = 1;
			this.label6.Text = "+=";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(309, 15);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(29, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Start";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(155, 15);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(28, 13);
			this.label2.TabIndex = 1;
			this.label2.Text = "Pow";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tOffset
			// 
			this.tOffset.Location = new System.Drawing.Point(488, 38);
			this.tOffset.Name = "tOffset";
			this.tOffset.Size = new System.Drawing.Size(105, 20);
			this.tOffset.TabIndex = 6;
			this.tOffset.Text = "9";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(10, 15);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(28, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Freq";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// tStop
			// 
			this.tStop.Location = new System.Drawing.Point(343, 38);
			this.tStop.Name = "tStop";
			this.tStop.Size = new System.Drawing.Size(105, 20);
			this.tStop.TabIndex = 4;
			this.tStop.Text = "120";
			// 
			// tIncr
			// 
			this.tIncr.Location = new System.Drawing.Point(488, 12);
			this.tIncr.Name = "tIncr";
			this.tIncr.Size = new System.Drawing.Size(105, 20);
			this.tIncr.TabIndex = 5;
			this.tIncr.Text = "1";
			// 
			// tPowD
			// 
			this.tPowD.Location = new System.Drawing.Point(189, 38);
			this.tPowD.Name = "tPowD";
			this.tPowD.Size = new System.Drawing.Size(105, 20);
			this.tPowD.TabIndex = 2;
			this.tPowD.Text = "12";
			// 
			// tStart
			// 
			this.tStart.Location = new System.Drawing.Point(343, 12);
			this.tStart.Name = "tStart";
			this.tStart.Size = new System.Drawing.Size(105, 20);
			this.tStart.TabIndex = 3;
			this.tStart.Text = "40";
			// 
			// tPow
			// 
			this.tPow.Location = new System.Drawing.Point(189, 12);
			this.tPow.Name = "tPow";
			this.tPow.Size = new System.Drawing.Size(105, 20);
			this.tPow.TabIndex = 1;
			this.tPow.Text = "5";
			// 
			// tFreq
			// 
			this.tFreq.Location = new System.Drawing.Point(44, 12);
			this.tFreq.Name = "tFreq";
			this.tFreq.Size = new System.Drawing.Size(105, 20);
			this.tFreq.TabIndex = 0;
			this.tFreq.Text = "440";
			// 
			// FrequencyTestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(698, 323);
			this.Controls.Add(this.frequencyTestControl1);
			this.Controls.Add(this.panel1);
			this.Name = "FrequencyTestForm";
			this.Text = "FrequencyTestForm";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.TextBox tIncr;
		private System.Windows.Forms.TextBox tOffset;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.TextBox tStop;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox tStart;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.TextBox tPowD;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox tPow;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox tFreq;
		private System.Windows.Forms.Panel panel1;
		private gen.snd.Forms.FrequencyTestControl frequencyTestControl1;
	}
}
