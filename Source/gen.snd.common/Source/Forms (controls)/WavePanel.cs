#region User/License
// oio * 11/1/2012 * 1:15 AM

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

using gen.snd.Formats;
using gen.snd.Formats.ImpluseTracker;

namespace ititoo
{

	/// <summary>
	/// Description of WavePanel.
	/// </summary>
	public partial class WavePanel : UserControl
	{
		ITI mo { get;set; }
		bool midf;
		int newint = 0, oldint = 0, vh = 64, numnum = 0;
		int ppx=6;
		Graphics gx;
		public void eMmv(object sender, System.EventArgs e) { mmv(); }
		
		bool IsMouseDown { get;set; }
		
		protected override void OnMouseDown(MouseEventArgs e)
		{
			if (e.Button==MouseButtons.Left) IsMouseDown = true;
			base.OnMouseDown(e);
			Invalidate();
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			if (e.Button==MouseButtons.Left) IsMouseDown = false;
//			eMouse(e);
			base.OnMouseUp(e);
			Invalidate();
		}
		
		public void eMouse(MouseEventArgs e)
		{
			newint = e.X;
			switch (e.Button)
			{
				case MouseButtons.Left:
					break;
//				case MouseButtons.None:
//					Text = e.Button.ToString()+" "+(e.X/ppx).ToString()+" "+(((int)e.Y*(64f/pik.ClientSize.Height))).ToString("##,###,##0.00");
//					break;
				case MouseButtons.Middle:
					break;
//					Text = e.Button.ToString()+" "+(e.X/ppx).ToString()+" "+(((int)e.Y*(64f/pik.ClientSize.Height))).ToString("##,###,##0.00");
//					break;
				default:
					midf = false;
					newint = e.X;
					numnum = (int)(vh-(e.Y*((float)vh/(float)this.ClientSize.Height)));
					if (numnum <=vh & numnum >=0) Text = string.Format( "{0}{1}{2:##,###,##0.00}", e.Button, e.X/ppx, numnum );
					oldint = newint;
					break;
			}
			oldint = newint;
			mmv();
		}
		
		void mmv() {
			try
			{
				Bitmap bmp = new Bitmap(4,4,System.Drawing.Imaging.PixelFormat.Format24bppRgb);
				Graphics g = Graphics.FromImage(bmp);
				g.Clear(Color.White);
				
				if (mo==null) return;
				
				if (((int)mo.ITI_INST.impVolEnvelop.envFlag & ((int)ITI.evl.on))>=0)
				{
					gx = this.CreateGraphics();
					gx.Clear(Color.Black);
					Pen penn = new Pen(Color.BlanchedAlmond);
					ITI.enveloPoint pt = new ITI.enveloPoint();
					List<Point> jake = new List<Point>();
					gx.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.Bicubic;
					// volume envelope
					int c = 0; float tempx = this.ClientSize.Height/64; // 
					ppx = 6;  // six pixels per tick
					Bitmap crx = new Bitmap("cross.png");
					Text = crx.Width.ToString();
					foreach (ITI.enveloPoint ep in mo.ITI_INST.impVolEnvelop.envPoints)
					{
						if (c<mo.ITI_INST.impVolEnvelop.envNodeCount)
							jake.Add(new Point((int)(ep.epPos*ppx),(int)(ep.epVal*tempx)));
						c++;
					}
					gx.DrawCurve(new Pen(Color.White,2),jake.ToArray());
					//	comboBox1.Items.AddRange(jake.ToArray());
				}
			} catch {}
		}
		
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			ScrollPixels = 24;
			Invalidate();
		}
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Render(e.Graphics);
		}
		
		readonly Color LineColor = Color.FromArgb(0,127,255);
		FloatPoint HalfPoint { get { return new FloatPoint(Convert.ToInt32(ClientSize.Width / 2),Convert.ToInt32(ClientSize.Height / 2)); } }
		int LineHeight { get { return Convert.ToInt32(Font.GetHeight()); } }
		int HalfLineHeight { get { return Convert.ToInt32(LineHeight / 2); } }
		
		public int ScrollPixels { get;set; }
		/// apparently we're just storing the height of an object?
		public FloatPoint ScrollBottomPosition { get { return new FloatPoint(0,ClientSize.Height - ScrollPixels); } }
		
		void Render(Graphics g)
		{
			FloatPoint h = HalfPoint;
			string value = string.Format("{0:N0}x{1:N0}",ClientSize.Width,ClientSize.Height);
			FloatPoint posText = new FloatPoint(0,HalfPoint.Y);
			posText.Y -= HalfLineHeight;
			using (Pen line = new Pen(LineColor,1)) g.DrawLine(line,0,h.Y,ClientSize.Width,h.Y);
			using (Brush fg = new SolidBrush(BackColor)) g.DrawString(value,Font,fg,posText+-2);
			using (Brush fg = new SolidBrush(BackColor)) g.DrawString(value,Font,fg,posText+2);
			using (Brush fg = new SolidBrush(LineColor)) g.DrawString(value,Font,fg,posText);
		}
		public WavePanel()
		{
			InitializeComponent();
			DoubleBuffered = true;
		}
	}
}
