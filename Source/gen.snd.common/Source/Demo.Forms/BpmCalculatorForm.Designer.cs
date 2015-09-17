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
namespace gen.snd.Forms
{
	partial class BpmCalculatorForm
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
			this.components = new System.ComponentModel.Container();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.udBpm = new System.Windows.Forms.NumericUpDown();
			this.udOutput = new System.Windows.Forms.NumericUpDown();
			this.label3 = new System.Windows.Forms.Label();
			this.udDiv1 = new System.Windows.Forms.NumericUpDown();
			this.udDiv2 = new System.Windows.Forms.NumericUpDown();
			this.label4 = new System.Windows.Forms.Label();
			this.label5 = new System.Windows.Forms.Label();
			this.nMulti = new System.Windows.Forms.NumericUpDown();
			this.label6 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.s2m_ClocksAtSample = new System.Windows.Forms.TextBox();
			this.s2m_TicksPerClock = new System.Windows.Forms.TextBox();
			this.s2m_Out_MBQT = new System.Windows.Forms.TextBox();
			this.s2m_Out_Time = new System.Windows.Forms.TextBox();
			this.label12 = new System.Windows.Forms.Label();
			this.s2m_SamplesPerClock = new System.Windows.Forms.TextBox();
			this.label15 = new System.Windows.Forms.Label();
			this.s2m_MSPQN = new System.Windows.Forms.TextBox();
			this.lMSPQN = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.s2m_Pulses = new System.Windows.Forms.NumericUpDown();
			this.s2m_Samples = new System.Windows.Forms.NumericUpDown();
			this.s2m_BufferSize = new System.Windows.Forms.NumericUpDown();
			this.s2m_Rate = new System.Windows.Forms.NumericUpDown();
			this.s2m_BPM = new System.Windows.Forms.NumericUpDown();
			this.s2m_Division = new System.Windows.Forms.NumericUpDown();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.rPulse = new System.Windows.Forms.RadioButton();
			this.rQuarters = new System.Windows.Forms.RadioButton();
			this.rClock = new System.Windows.Forms.RadioButton();
			((System.ComponentModel.ISupportInitialize)(this.udBpm)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udOutput)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udDiv1)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.udDiv2)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.nMulti)).BeginInit();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Pulses)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Samples)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_BufferSize)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Rate)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_BPM)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Division)).BeginInit();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(22, 22);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(54, 17);
			this.label1.TabIndex = 2;
			this.label1.Text = "BPM";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(22, 107);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(54, 17);
			this.label2.TabIndex = 2;
			this.label2.Text = "Hz";
			// 
			// udBpm
			// 
			this.udBpm.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.udBpm.DecimalPlaces = 3;
			this.udBpm.Location = new System.Drawing.Point(94, 20);
			this.udBpm.Maximum = new decimal(new int[] {
									999999,
									0,
									0,
									0});
			this.udBpm.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.udBpm.Name = "udBpm";
			this.udBpm.Size = new System.Drawing.Size(120, 16);
			this.udBpm.TabIndex = 3;
			this.udBpm.Value = new decimal(new int[] {
									120,
									0,
									0,
									0});
			this.udBpm.ValueChanged += new System.EventHandler(this.Button1Click);
			// 
			// udOutput
			// 
			this.udOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.udOutput.DecimalPlaces = 13;
			this.udOutput.Location = new System.Drawing.Point(94, 105);
			this.udOutput.Maximum = new decimal(new int[] {
									90000000,
									0,
									0,
									0});
			this.udOutput.Name = "udOutput";
			this.udOutput.Size = new System.Drawing.Size(120, 16);
			this.udOutput.TabIndex = 4;
			this.udOutput.ThousandsSeparator = true;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(22, 49);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(54, 17);
			this.label3.TabIndex = 2;
			this.label3.Text = "Dividend";
			// 
			// udDiv1
			// 
			this.udDiv1.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.udDiv1.Location = new System.Drawing.Point(94, 45);
			this.udDiv1.Maximum = new decimal(new int[] {
									190,
									0,
									0,
									0});
			this.udDiv1.Name = "udDiv1";
			this.udDiv1.Size = new System.Drawing.Size(45, 16);
			this.udDiv1.TabIndex = 4;
			this.udDiv1.Value = new decimal(new int[] {
									4,
									0,
									0,
									0});
			this.udDiv1.ValueChanged += new System.EventHandler(this.Button1Click);
			// 
			// udDiv2
			// 
			this.udDiv2.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.udDiv2.Location = new System.Drawing.Point(169, 45);
			this.udDiv2.Maximum = new decimal(new int[] {
									90,
									0,
									0,
									0});
			this.udDiv2.Name = "udDiv2";
			this.udDiv2.Size = new System.Drawing.Size(45, 16);
			this.udDiv2.TabIndex = 4;
			this.udDiv2.Value = new decimal(new int[] {
									4,
									0,
									0,
									0});
			this.udDiv2.ValueChanged += new System.EventHandler(this.Button1Click);
			// 
			// label4
			// 
			this.label4.Location = new System.Drawing.Point(150, 48);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(24, 17);
			this.label4.TabIndex = 2;
			this.label4.Text = "/";
			// 
			// label5
			// 
			this.label5.Location = new System.Drawing.Point(22, 75);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(54, 17);
			this.label5.TabIndex = 2;
			this.label5.Text = "Multiplier";
			// 
			// nMulti
			// 
			this.nMulti.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.nMulti.DecimalPlaces = 3;
			this.nMulti.Location = new System.Drawing.Point(94, 73);
			this.nMulti.Maximum = new decimal(new int[] {
									999999,
									0,
									0,
									0});
			this.nMulti.Minimum = new decimal(new int[] {
									999999,
									0,
									0,
									-2147483648});
			this.nMulti.Name = "nMulti";
			this.nMulti.Size = new System.Drawing.Size(120, 16);
			this.nMulti.TabIndex = 3;
			this.nMulti.Value = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.nMulti.ValueChanged += new System.EventHandler(this.Button1Click);
			// 
			// label6
			// 
			this.label6.BackColor = System.Drawing.SystemColors.ControlDark;
			this.label6.Location = new System.Drawing.Point(22, 98);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(189, 1);
			this.label6.TabIndex = 5;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.udDiv2);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.udDiv1);
			this.groupBox1.Controls.Add(this.udBpm);
			this.groupBox1.Controls.Add(this.udOutput);
			this.groupBox1.Controls.Add(this.nMulti);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Font = new System.Drawing.Font("Consolas", 8F);
			this.groupBox1.Location = new System.Drawing.Point(12, 12);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(231, 139);
			this.groupBox1.TabIndex = 6;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "BPM to Hz";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.s2m_ClocksAtSample);
			this.groupBox2.Controls.Add(this.s2m_TicksPerClock);
			this.groupBox2.Controls.Add(this.s2m_Out_MBQT);
			this.groupBox2.Controls.Add(this.s2m_Out_Time);
			this.groupBox2.Controls.Add(this.label12);
			this.groupBox2.Controls.Add(this.s2m_SamplesPerClock);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.s2m_MSPQN);
			this.groupBox2.Controls.Add(this.lMSPQN);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.label8);
			this.groupBox2.Controls.Add(this.label10);
			this.groupBox2.Controls.Add(this.label14);
			this.groupBox2.Controls.Add(this.label7);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.s2m_Pulses);
			this.groupBox2.Controls.Add(this.s2m_Samples);
			this.groupBox2.Controls.Add(this.s2m_BufferSize);
			this.groupBox2.Controls.Add(this.s2m_Rate);
			this.groupBox2.Controls.Add(this.s2m_BPM);
			this.groupBox2.Controls.Add(this.s2m_Division);
			this.groupBox2.Font = new System.Drawing.Font("Consolas", 8F);
			this.groupBox2.Location = new System.Drawing.Point(251, 12);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(231, 254);
			this.groupBox2.TabIndex = 6;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Samples To MPQT";
			// 
			// s2m_ClocksAtSample
			// 
			this.s2m_ClocksAtSample.BackColor = System.Drawing.SystemColors.ControlDark;
			this.s2m_ClocksAtSample.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_ClocksAtSample.Location = new System.Drawing.Point(96, 160);
			this.s2m_ClocksAtSample.Name = "s2m_ClocksAtSample";
			this.s2m_ClocksAtSample.ReadOnly = true;
			this.s2m_ClocksAtSample.Size = new System.Drawing.Size(120, 13);
			this.s2m_ClocksAtSample.TabIndex = 6;
			// 
			// s2m_TicksPerClock
			// 
			this.s2m_TicksPerClock.BackColor = System.Drawing.SystemColors.ControlDark;
			this.s2m_TicksPerClock.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_TicksPerClock.Location = new System.Drawing.Point(96, 141);
			this.s2m_TicksPerClock.Name = "s2m_TicksPerClock";
			this.s2m_TicksPerClock.ReadOnly = true;
			this.s2m_TicksPerClock.Size = new System.Drawing.Size(120, 13);
			this.s2m_TicksPerClock.TabIndex = 6;
			// 
			// s2m_Out_MBQT
			// 
			this.s2m_Out_MBQT.BackColor = System.Drawing.SystemColors.ControlDark;
			this.s2m_Out_MBQT.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_Out_MBQT.Location = new System.Drawing.Point(96, 227);
			this.s2m_Out_MBQT.Name = "s2m_Out_MBQT";
			this.s2m_Out_MBQT.ReadOnly = true;
			this.s2m_Out_MBQT.Size = new System.Drawing.Size(120, 13);
			this.s2m_Out_MBQT.TabIndex = 6;
			this.toolTip1.SetToolTip(this.s2m_Out_MBQT, "Measure, Bar, Quarter, Tick");
			// 
			// s2m_Out_Time
			// 
			this.s2m_Out_Time.BackColor = System.Drawing.SystemColors.ControlDark;
			this.s2m_Out_Time.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_Out_Time.Location = new System.Drawing.Point(96, 208);
			this.s2m_Out_Time.Name = "s2m_Out_Time";
			this.s2m_Out_Time.ReadOnly = true;
			this.s2m_Out_Time.Size = new System.Drawing.Size(120, 13);
			this.s2m_Out_Time.TabIndex = 6;
			this.toolTip1.SetToolTip(this.s2m_Out_Time, "Time Offset—From the provided Sample Offset");
			// 
			// label12
			// 
			this.label12.AutoSize = true;
			this.label12.Font = new System.Drawing.Font("Consolas", 7F);
			this.label12.Location = new System.Drawing.Point(35, 20);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(55, 12);
			this.label12.TabIndex = 2;
			this.label12.Text = "Sample Pos";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.label12, "Sample Offset Position:\r\n\tThe number of elapsed samples.");
			// 
			// s2m_SamplesPerClock
			// 
			this.s2m_SamplesPerClock.BackColor = System.Drawing.SystemColors.ControlDark;
			this.s2m_SamplesPerClock.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_SamplesPerClock.Location = new System.Drawing.Point(96, 122);
			this.s2m_SamplesPerClock.Name = "s2m_SamplesPerClock";
			this.s2m_SamplesPerClock.ReadOnly = true;
			this.s2m_SamplesPerClock.Size = new System.Drawing.Size(120, 13);
			this.s2m_SamplesPerClock.TabIndex = 6;
			// 
			// label15
			// 
			this.label15.AutoSize = true;
			this.label15.Font = new System.Drawing.Font("Consolas", 7F);
			this.label15.Location = new System.Drawing.Point(15, 123);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(75, 12);
			this.label15.TabIndex = 2;
			this.label15.Text = "Smp  per Clock";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// s2m_MSPQN
			// 
			this.s2m_MSPQN.BackColor = System.Drawing.SystemColors.ControlDark;
			this.s2m_MSPQN.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_MSPQN.Location = new System.Drawing.Point(96, 189);
			this.s2m_MSPQN.Name = "s2m_MSPQN";
			this.s2m_MSPQN.ReadOnly = true;
			this.s2m_MSPQN.Size = new System.Drawing.Size(120, 13);
			this.s2m_MSPQN.TabIndex = 6;
			this.toolTip1.SetToolTip(this.s2m_MSPQN, "Milliseconds Per Quarter Note (Calculated from Tempo)");
			// 
			// lMSPQN
			// 
			this.lMSPQN.AutoSize = true;
			this.lMSPQN.Font = new System.Drawing.Font("Consolas", 7F);
			this.lMSPQN.Location = new System.Drawing.Point(60, 189);
			this.lMSPQN.Name = "lMSPQN";
			this.lMSPQN.Size = new System.Drawing.Size(30, 12);
			this.lMSPQN.TabIndex = 2;
			this.lMSPQN.Text = "MSPQN";
			this.lMSPQN.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.lMSPQN, "Milliseconds Per Quarter Note (Calculated from Tempo)");
			// 
			// label13
			// 
			this.label13.AutoSize = true;
			this.label13.Font = new System.Drawing.Font("Consolas", 7F);
			this.label13.Location = new System.Drawing.Point(25, 64);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(65, 12);
			this.label13.TabIndex = 2;
			this.label13.Text = "Rate, Buffer";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.label13, "Sample Rate and Buffer Size (Sample Increments)");
			// 
			// label8
			// 
			this.label8.AutoSize = true;
			this.label8.Font = new System.Drawing.Font("Consolas", 7F);
			this.label8.Location = new System.Drawing.Point(30, 161);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(60, 12);
			this.label8.TabIndex = 2;
			this.label8.Text = "Clocks @Smp";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Consolas", 7F);
			this.label10.Location = new System.Drawing.Point(25, 42);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(65, 12);
			this.label10.TabIndex = 2;
			this.label10.Text = "Pulses/Ticks";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label14
			// 
			this.label14.AutoSize = true;
			this.label14.Font = new System.Drawing.Font("Consolas", 7F);
			this.label14.Location = new System.Drawing.Point(10, 142);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(80, 12);
			this.label14.TabIndex = 2;
			this.label14.Text = "Ticks Per Clock";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label7
			// 
			this.label7.AutoSize = true;
			this.label7.Font = new System.Drawing.Font("Consolas", 7F);
			this.label7.Location = new System.Drawing.Point(20, 89);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(70, 12);
			this.label7.TabIndex = 2;
			this.label7.Text = "BPM, Division";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.label7, "Beats Per Minute\r\n\t4 Quarter Notes in a “Beat”");
			// 
			// label9
			// 
			this.label9.AutoSize = true;
			this.label9.Font = new System.Drawing.Font("Consolas", 7F);
			this.label9.Location = new System.Drawing.Point(65, 227);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(25, 12);
			this.label9.TabIndex = 2;
			this.label9.Text = "MBQT";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.label9, "Measure, Bar, Quarter, Tick");
			// 
			// label11
			// 
			this.label11.AutoSize = true;
			this.label11.Font = new System.Drawing.Font("Consolas", 7F);
			this.label11.Location = new System.Drawing.Point(65, 208);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(25, 12);
			this.label11.TabIndex = 2;
			this.label11.Text = "Time";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.toolTip1.SetToolTip(this.label11, "Time Offset—From the provided Sample Offset");
			// 
			// s2m_Pulses
			// 
			this.s2m_Pulses.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_Pulses.Location = new System.Drawing.Point(96, 42);
			this.s2m_Pulses.Maximum = new decimal(new int[] {
									1316134912,
									2328,
									0,
									0});
			this.s2m_Pulses.Name = "s2m_Pulses";
			this.s2m_Pulses.Size = new System.Drawing.Size(120, 16);
			this.s2m_Pulses.TabIndex = 3;
			this.s2m_Pulses.ThousandsSeparator = true;
			this.s2m_Pulses.ValueChanged += new System.EventHandler(this.Event_Go);
			this.s2m_Pulses.Enter += new System.EventHandler(this.S2m_PulsesEnter);
			// 
			// s2m_Samples
			// 
			this.s2m_Samples.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_Samples.Increment = new decimal(new int[] {
									4800,
									0,
									0,
									0});
			this.s2m_Samples.Location = new System.Drawing.Point(96, 20);
			this.s2m_Samples.Maximum = new decimal(new int[] {
									1316134912,
									2328,
									0,
									0});
			this.s2m_Samples.Name = "s2m_Samples";
			this.s2m_Samples.Size = new System.Drawing.Size(120, 16);
			this.s2m_Samples.TabIndex = 3;
			this.s2m_Samples.ThousandsSeparator = true;
			this.toolTip1.SetToolTip(this.s2m_Samples, "Sample Offset Position:\r\n\tThe number of elapsed samples.");
			this.s2m_Samples.ValueChanged += new System.EventHandler(this.Event_Go);
			this.s2m_Samples.Enter += new System.EventHandler(this.S2m_SamplesEnter);
			// 
			// s2m_BufferSize
			// 
			this.s2m_BufferSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_BufferSize.Location = new System.Drawing.Point(158, 64);
			this.s2m_BufferSize.Maximum = new decimal(new int[] {
									100000000,
									0,
									0,
									0});
			this.s2m_BufferSize.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.s2m_BufferSize.Name = "s2m_BufferSize";
			this.s2m_BufferSize.Size = new System.Drawing.Size(58, 16);
			this.s2m_BufferSize.TabIndex = 3;
			this.s2m_BufferSize.ThousandsSeparator = true;
			this.toolTip1.SetToolTip(this.s2m_BufferSize, "Sample Rate and Buffer Size (Sample Increments)");
			this.s2m_BufferSize.Value = new decimal(new int[] {
									4800,
									0,
									0,
									0});
			this.s2m_BufferSize.ValueChanged += new System.EventHandler(this.Event_S2MBufferSize);
			// 
			// s2m_Rate
			// 
			this.s2m_Rate.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_Rate.Location = new System.Drawing.Point(96, 64);
			this.s2m_Rate.Maximum = new decimal(new int[] {
									100000000,
									0,
									0,
									0});
			this.s2m_Rate.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.s2m_Rate.Name = "s2m_Rate";
			this.s2m_Rate.Size = new System.Drawing.Size(58, 16);
			this.s2m_Rate.TabIndex = 3;
			this.s2m_Rate.ThousandsSeparator = true;
			this.toolTip1.SetToolTip(this.s2m_Rate, "Sample Rate and Buffer Size (Sample Increments)");
			this.s2m_Rate.Value = new decimal(new int[] {
									48000,
									0,
									0,
									0});
			this.s2m_Rate.ValueChanged += new System.EventHandler(this.Event_Go);
			// 
			// s2m_BPM
			// 
			this.s2m_BPM.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_BPM.Location = new System.Drawing.Point(96, 89);
			this.s2m_BPM.Maximum = new decimal(new int[] {
									999999,
									0,
									0,
									0});
			this.s2m_BPM.Minimum = new decimal(new int[] {
									1,
									0,
									0,
									0});
			this.s2m_BPM.Name = "s2m_BPM";
			this.s2m_BPM.Size = new System.Drawing.Size(58, 16);
			this.s2m_BPM.TabIndex = 3;
			this.toolTip1.SetToolTip(this.s2m_BPM, "Beats Per Minute");
			this.s2m_BPM.Value = new decimal(new int[] {
									120,
									0,
									0,
									0});
			this.s2m_BPM.ValueChanged += new System.EventHandler(this.Event_Go);
			// 
			// s2m_Division
			// 
			this.s2m_Division.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.s2m_Division.Increment = new decimal(new int[] {
									24,
									0,
									0,
									0});
			this.s2m_Division.Location = new System.Drawing.Point(158, 89);
			this.s2m_Division.Maximum = new decimal(new int[] {
									2000,
									0,
									0,
									0});
			this.s2m_Division.Minimum = new decimal(new int[] {
									24,
									0,
									0,
									0});
			this.s2m_Division.Name = "s2m_Division";
			this.s2m_Division.Size = new System.Drawing.Size(58, 16);
			this.s2m_Division.TabIndex = 3;
			this.toolTip1.SetToolTip(this.s2m_Division, "MIDI “TPQN” (Ticks Per Quarter Note)\r\nAKA: “PPQ” (Parts Per Quarter)");
			this.s2m_Division.Value = new decimal(new int[] {
									480,
									0,
									0,
									0});
			this.s2m_Division.ValueChanged += new System.EventHandler(this.Event_Go);
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.rPulse);
			this.groupBox3.Controls.Add(this.rQuarters);
			this.groupBox3.Controls.Add(this.rClock);
			this.groupBox3.Font = new System.Drawing.Font("Consolas", 8F);
			this.groupBox3.Location = new System.Drawing.Point(12, 157);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(230, 109);
			this.groupBox3.TabIndex = 7;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Pulse Increment Mode";
			// 
			// rPulse
			// 
			this.rPulse.AutoSize = true;
			this.rPulse.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rPulse.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rPulse.Font = new System.Drawing.Font("Consolas", 8F);
			this.rPulse.Location = new System.Drawing.Point(137, 47);
			this.rPulse.Name = "rPulse";
			this.rPulse.Size = new System.Drawing.Size(73, 18);
			this.rPulse.TabIndex = 8;
			this.rPulse.Text = "1 Pulse";
			this.rPulse.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rPulse.UseVisualStyleBackColor = true;
			this.rPulse.CheckedChanged += new System.EventHandler(this.Event_ResetPulseIntervals);
			// 
			// rQuarters
			// 
			this.rQuarters.AutoSize = true;
			this.rQuarters.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rQuarters.Checked = true;
			this.rQuarters.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rQuarters.Font = new System.Drawing.Font("Consolas", 8F);
			this.rQuarters.Location = new System.Drawing.Point(131, 28);
			this.rQuarters.Name = "rQuarters";
			this.rQuarters.Size = new System.Drawing.Size(79, 18);
			this.rQuarters.TabIndex = 9;
			this.rQuarters.TabStop = true;
			this.rQuarters.Text = "Quarters";
			this.rQuarters.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rQuarters.UseVisualStyleBackColor = true;
			this.rQuarters.CheckedChanged += new System.EventHandler(this.Event_ResetPulseIntervals);
			// 
			// rClock
			// 
			this.rClock.AutoSize = true;
			this.rClock.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rClock.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rClock.Font = new System.Drawing.Font("Consolas", 8F);
			this.rClock.Location = new System.Drawing.Point(119, 66);
			this.rClock.Name = "rClock";
			this.rClock.Size = new System.Drawing.Size(91, 18);
			this.rClock.TabIndex = 10;
			this.rClock.Text = "MIDI Clock";
			this.rClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			this.rClock.UseVisualStyleBackColor = true;
			this.rClock.CheckedChanged += new System.EventHandler(this.Event_ResetPulseIntervals);
			// 
			// BpmCalculatorForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(495, 284);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "BpmCalculatorForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "BPM/PPQ Calculator";
			((System.ComponentModel.ISupportInitialize)(this.udBpm)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udOutput)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udDiv1)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.udDiv2)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.nMulti)).EndInit();
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Pulses)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Samples)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_BufferSize)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Rate)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_BPM)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.s2m_Division)).EndInit();
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.RadioButton rPulse;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.TextBox s2m_ClocksAtSample;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.TextBox s2m_SamplesPerClock;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.TextBox s2m_TicksPerClock;
		private System.Windows.Forms.RadioButton rQuarters;
		private System.Windows.Forms.RadioButton rClock;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.NumericUpDown s2m_Pulses;
		private System.Windows.Forms.NumericUpDown s2m_BufferSize;
		private System.Windows.Forms.TextBox s2m_MSPQN;
		private System.Windows.Forms.NumericUpDown s2m_Division;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.NumericUpDown s2m_Rate;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.NumericUpDown s2m_Samples;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.TextBox s2m_Out_MBQT;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.TextBox s2m_Out_Time;
		private System.Windows.Forms.NumericUpDown s2m_BPM;
		private System.Windows.Forms.Label lMSPQN;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.NumericUpDown nMulti;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.NumericUpDown udDiv2;
		private System.Windows.Forms.NumericUpDown udDiv1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown udOutput;
		private System.Windows.Forms.NumericUpDown udBpm;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}
