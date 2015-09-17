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
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;

using gen.snd.Midi;

namespace gen.snd.Forms
{
	public class KnobImageTool : Form
	{
		public KnobImageTool()
		{
			InitializeComponent();
		}
		
//		layout
		

		
		#region Designer
		
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
		
		void InitializeComponent()
		{
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KnobImageTool));
            this.btnLoadFirstImage = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.ofd = new System.Windows.Forms.OpenFileDialog();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tSpace = new System.Windows.Forms.NumericUpDown();
            this.tHeight = new System.Windows.Forms.NumericUpDown();
            this.tWidth = new System.Windows.Forms.NumericUpDown();
            this.tCount = new System.Windows.Forms.NumericUpDown();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.knob1 = new gen.snd.Forms.Knob();
            this.button1 = new System.Windows.Forms.Button();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tSpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tHeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tWidth)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCount)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnLoadFirstImage
            // 
            this.btnLoadFirstImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLoadFirstImage.Location = new System.Drawing.Point(451, 17);
            this.btnLoadFirstImage.Name = "btnLoadFirstImage";
            this.btnLoadFirstImage.Size = new System.Drawing.Size(75, 23);
            this.btnLoadFirstImage.TabIndex = 0;
            this.btnLoadFirstImage.Text = "Load";
            this.btnLoadFirstImage.UseVisualStyleBackColor = true;
            this.btnLoadFirstImage.Click += new System.EventHandler(this.BtnLoadFirstImageClick);
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(12, 19);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(433, 20);
            this.textBox1.TabIndex = 1;
            // 
            // ofd
            // 
            this.ofd.Filter = "All Supported Image Formats|*.bmp;*.png;*.jpg";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tSpace);
            this.groupBox1.Controls.Add(this.tHeight);
            this.groupBox1.Controls.Add(this.tWidth);
            this.groupBox1.Controls.Add(this.tCount);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.button2);
            this.groupBox1.Controls.Add(this.btnLoadFirstImage);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(6, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(532, 98);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Input Settings";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Space";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(150, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(38, 13);
            this.label3.TabIndex = 3;
            this.label3.Text = "Height";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(150, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(35, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Width";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Count";
            // 
            // tSpace
            // 
            this.tSpace.Location = new System.Drawing.Point(91, 71);
            this.tSpace.Name = "tSpace";
            this.tSpace.Size = new System.Drawing.Size(49, 20);
            this.tSpace.TabIndex = 2;
            this.tSpace.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.tSpace.ValueChanged += new System.EventHandler(this.Event_SpinButton);
            // 
            // tHeight
            // 
            this.tHeight.Location = new System.Drawing.Point(194, 71);
            this.tHeight.Name = "tHeight";
            this.tHeight.Size = new System.Drawing.Size(67, 20);
            this.tHeight.TabIndex = 2;
            this.tHeight.Value = new decimal(new int[] {
            13,
            0,
            0,
            0});
            this.tHeight.ValueChanged += new System.EventHandler(this.Event_SpinButton);
            // 
            // tWidth
            // 
            this.tWidth.Location = new System.Drawing.Point(194, 45);
            this.tWidth.Maximum = new decimal(new int[] {
            90000000,
            0,
            0,
            0});
            this.tWidth.Name = "tWidth";
            this.tWidth.Size = new System.Drawing.Size(67, 20);
            this.tWidth.TabIndex = 2;
            this.tWidth.Value = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.tWidth.ValueChanged += new System.EventHandler(this.Event_SpinButton);
            // 
            // tCount
            // 
            this.tCount.Location = new System.Drawing.Point(91, 45);
            this.tCount.Maximum = new decimal(new int[] {
            410065408,
            2,
            0,
            0});
            this.tCount.Name = "tCount";
            this.tCount.Size = new System.Drawing.Size(49, 20);
            this.tCount.TabIndex = 2;
            this.tCount.Value = new decimal(new int[] {
            36,
            0,
            0,
            0});
            this.tCount.ValueChanged += new System.EventHandler(this.Event_SpinButton);
            // 
            // button2
            // 
            this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button2.Location = new System.Drawing.Point(451, 45);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "Reload";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.Event_ButtonReload);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.knob1);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(6, 101);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(532, 135);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Preview";
            // 
            // knob1
            // 
            this.knob1.ActualNumberMax = 1.7976931348623157E+308D;
            this.knob1.ActualNumberMin = 0D;
            this.knob1.ActualNumberValue = 0D;
            this.knob1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.knob1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("knob1.BackgroundImage")));
            this.knob1.KnobFrameIndex = 0;
            this.knob1.KnobImage = ((System.Drawing.Image)(resources.GetObject("knob1.KnobImage")));
            this.knob1.Length = 36;
            this.knob1.Location = new System.Drawing.Point(15, 19);
            this.knob1.Maximum = 0;
            this.knob1.Minimum = 0;
            this.knob1.Mode = gen.snd.Forms.KnobType.Knob;
            this.knob1.Name = "knob1";
            this.knob1.Offset = 1;
            this.knob1.RowsCols = new System.Drawing.Rectangle(0, 0, 0, 0);
            this.knob1.Size = new System.Drawing.Size(13, 43);
            this.knob1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.button1.Location = new System.Drawing.Point(451, 19);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Export";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.BtnSaveImageClick);
            // 
            // sfd
            // 
            this.sfd.Filter = "PNG Image|*.png";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.textBox2);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(6, 236);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(532, 169);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Help";
            // 
            // textBox2
            // 
            this.textBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox2.Location = new System.Drawing.Point(3, 16);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBox2.Size = new System.Drawing.Size(526, 150);
            this.textBox2.TabIndex = 4;
            this.textBox2.Text = resources.GetString("textBox2.Text");
            // 
            // KnobImageTool
            // 
            this.ClientSize = new System.Drawing.Size(544, 411);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox3);
            this.MinimumSize = new System.Drawing.Size(300, 350);
            this.Name = "KnobImageTool";
            this.Padding = new System.Windows.Forms.Padding(6, 3, 6, 6);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Knob Image Tool";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tSpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tHeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tWidth)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tCount)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

		}
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.SaveFileDialog sfd;
		private System.Windows.Forms.NumericUpDown tSpace;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.NumericUpDown tCount;
		private System.Windows.Forms.NumericUpDown tWidth;
		private System.Windows.Forms.NumericUpDown tHeight;
		private System.Windows.Forms.Label label1;
		private gen.snd.Forms.Knob knob1;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.OpenFileDialog ofd;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button btnLoadFirstImage;
		
		#endregion
		
		Bitmap SelectedImage;
		KnobImageUtility ImageUtil;
		string ImageReferencePath;
		
		
		void BtnSaveImageClick(object sender, EventArgs e)
		{
			using (Bitmap img = new Bitmap(knob1.KnobImage))
			{
				if (sfd.ShowDialog()== DialogResult.OK)
					img.Save(sfd.FileName,ImageFormat.Png);
			}
			
		}
		void BtnLoadFirstImageClick(object sender, EventArgs e)
		{
			if (ofd.ShowDialog() == DialogResult.OK)
			{
				RefreshKnob(ofd.FileName);
				RefreshKnob();
				textBox1.Text = ImageReferencePath;
			}
		}
		void RefreshKnob(string imagePath)
		{
			ImageReferencePath = imagePath;
			ImageUtil = null;
		}
		void RefreshKnob()
		{
			ImageUtil = KnobImageUtility.Create(ImageReferencePath,(int)tCount.Value,(int)tSpace.Value);
			if (ImageUtil==null) return;
//			this.knob1.SuspendLayout();
			this.knob1.Length = ImageUtil.ImageCount;
			this.knob1.Offset = ImageUtil.ImageSpace;
			this.knob1.Width = ImageUtil.ImageWidth;
			this.knob1.Height = ImageUtil.ImageHeight;
			this.knob1.KnobImage = ImageUtil.SelectedImage;
//			this.knob1.ResumeLayout(true);
			this.knob1.Invalidate();
		}
		
		void Event_ButtonReload(object sender, EventArgs e)
		{
			RefreshKnob();
		}
		
		class KnobImageUtility
		{
			public FlowDirection ImageAnchor {
				get { return Direction; }
				set { Direction = value; }
			} FlowDirection Direction = FlowDirection.TopDown;

			public int ImageCount { get;set; }
			public int ImageSpace { get;set; }
			
			public int ImageWidth { get;set; }
			public int ImageHeight { get;set; }
			
			public Bitmap SelectedImage { get;set; }

			FileInfo InitialFileInfo { get;set; }
			
			public KnobImageUtility(string file, int count, int space)
			{
				InitialFileInfo = new FileInfo(file);
				ImageCount = count;
				ImageSpace = space;
				Draw(this);
			}
			
			/// <summary>
			/// A one time merger of images to a single image.
			/// </summary>
			static public void Draw(KnobImageUtility k)
			{
				if (k.SelectedImage!=null)
				{
					k.SelectedImage.Dispose();
					k.SelectedImage = null;
				}
				using (Image img = Bitmap.FromFile(k.InitialFileInfo.FullName))
				{
					k.ImageWidth = img.Width;
					k.ImageHeight = img.Height;
				}
				int newWidth = k.ImageWidth * k.ImageCount + (k.ImageSpace * (k.ImageCount-1));
				k.SelectedImage = new Bitmap( newWidth, k.ImageHeight, PixelFormat.Format32bppArgb );
				
				int counter = 0;
				
				using (Graphics g = Graphics.FromImage(k.SelectedImage))
				{
					g.Clear(Color.Transparent);
					foreach (Image image in EnumerateExternalNumberedImages(k))
					{
						using (image)
						{
							if (k.Direction == FlowDirection.TopDown) g.DrawImage( image , new Point( counter, 0 ) );
							if (k.Direction == FlowDirection.LeftToRight) g.DrawImage( image , new Point( 0, counter ) );
							counter += k.ImageWidth+k.ImageSpace;
						}
					}
				}
			}
			
			static IEnumerable<Image> EnumerateExternalNumberedImages(KnobImageUtility k)
			{
				for (int i = 1; i <= k.ImageCount; i++)
				{
					string path = string.Format( @"{0}\{1:000#}{2}", k.InitialFileInfo.DirectoryName, 	i, k.InitialFileInfo.Extension );
					Image newImage = Bitmap.FromFile(path);
					Debug.Print(path);
					yield return newImage;
				}
			}
			
			static public KnobImageUtility Create(string file, int count, int space)
			{
				KnobImageUtility kiu = null;
				try { kiu = new KnobImageTool.KnobImageUtility(file,count,space);
				} catch { }
				return kiu;
			}
			
		}
		
		void Event_SpinButton(object sender, EventArgs e)
		{
			RefreshKnob();
		}
	}
}
