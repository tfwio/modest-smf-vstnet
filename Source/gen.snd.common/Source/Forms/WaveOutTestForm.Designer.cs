#region User/License
// oio * 7/19/2012 * 1:00 PM

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
	partial class WaveOutTestForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WaveOutTestForm));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.panel1 = new System.Windows.Forms.Panel();
			this.label1 = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.knob1 = new gen.snd.Forms.Knob();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.volumeSlider1 = new NAudio.Gui.VolumeSlider();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button4 = new System.Windows.Forms.Button();
			this.label2 = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Tick += new System.EventHandler(this.Event_Timer_Tick);
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.White;
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(558, 57);
			this.panel1.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(12, 12);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(130, 24);
			this.label1.TabIndex = 18;
			this.label1.Text = "Wave Output";
			// 
			// panel2
			// 
			this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
									| System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.panel2.BackColor = System.Drawing.Color.White;
			this.panel2.Controls.Add(this.label2);
			this.panel2.Controls.Add(this.knob1);
			this.panel2.Controls.Add(this.textBox1);
			this.panel2.Controls.Add(this.volumeSlider1);
			this.panel2.Location = new System.Drawing.Point(0, 57);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(558, 215);
			this.panel2.TabIndex = 13;
			// 
			// knob1
			// 
			this.knob1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("knob1.BackgroundImage")));
			this.knob1.KnobImage = ((System.Drawing.Image)(resources.GetObject("knob1.Img")));
			this.knob1.Length = 54;
			this.knob1.Location = new System.Drawing.Point(12, 39);
			this.knob1.Maximum = 127;
			this.knob1.Minimum = 0;
			this.knob1.Mode = gen.snd.Forms.KnobType.Knob;
			this.knob1.Name = "knob1";
			this.knob1.Offset = 1;
			this.knob1.RowsCols = new System.Drawing.Rectangle(0, 0, 0, 0);
			this.knob1.Size = new System.Drawing.Size(30, 30);
			this.knob1.TabIndex = 16;
			// 
			// textBox1
			// 
			this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.textBox1.Location = new System.Drawing.Point(11, 79);
			this.textBox1.Name = "textBox1";
			this.textBox1.Size = new System.Drawing.Size(538, 20);
			this.textBox1.TabIndex = 15;
			// 
			// volumeSlider1
			// 
			this.volumeSlider1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
									| System.Windows.Forms.AnchorStyles.Right)));
			this.volumeSlider1.Font = new System.Drawing.Font("Ubuntu Mono", 11F, System.Drawing.FontStyle.Bold);
			this.volumeSlider1.Location = new System.Drawing.Point(48, 39);
			this.volumeSlider1.Name = "volumeSlider1";
			this.volumeSlider1.Size = new System.Drawing.Size(501, 30);
			this.volumeSlider1.TabIndex = 13;
			this.volumeSlider1.Volume = 0.2F;
			this.volumeSlider1.VolumeChanged += new System.EventHandler(this.Event_Snd_ButtonAmp);
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.AutoSize = true;
			this.button1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button1.Font = new System.Drawing.Font("Ubuntu Mono", 11F, System.Drawing.FontStyle.Bold);
			this.button1.Location = new System.Drawing.Point(499, 283);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(47, 25);
			this.button1.TabIndex = 14;
			this.button1.Text = "Play";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.Event_Snd_Play);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.AutoSize = true;
			this.button2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button2.Font = new System.Drawing.Font("Ubuntu Mono", 11F, System.Drawing.FontStyle.Bold);
			this.button2.Location = new System.Drawing.Point(404, 283);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(48, 25);
			this.button2.TabIndex = 15;
			this.button2.Text = "Stop";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.Event_Snd_Stop);
			// 
			// button3
			// 
			this.button3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button3.AutoSize = true;
			this.button3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button3.Font = new System.Drawing.Font("Ubuntu Mono", 11F, System.Drawing.FontStyle.Bold);
			this.button3.Location = new System.Drawing.Point(460, 283);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(31, 25);
			this.button3.TabIndex = 14;
			this.button3.Text = "||";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.Event_Snd_Pause);
			// 
			// button4
			// 
			this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.button4.AutoSize = true;
			this.button4.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.button4.Font = new System.Drawing.Font("Ubuntu Mono", 11F, System.Drawing.FontStyle.Bold);
			this.button4.Location = new System.Drawing.Point(372, 283);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(24, 25);
			this.button4.TabIndex = 15;
			this.button4.Text = "!";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.Event_Snd_Clear);
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(11, 10);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(35, 13);
			this.label2.TabIndex = 17;
			this.label2.Text = "label2";
			// 
			// WaveOutTestForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(558, 317);
			this.Controls.Add(this.button4);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.panel2);
			this.Controls.Add(this.button3);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.panel1);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "WaveOutTestForm";
			this.ShowIcon = false;
			this.ShowInTaskbar = false;
			this.Text = "SineWave32TestForm";
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();
		}
		private System.Windows.Forms.Label label2;
		private gen.snd.Forms.Knob knob1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.Timer timer1;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button1;
		private NAudio.Gui.VolumeSlider volumeSlider1;
	}
}
