/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 12/26/2005
 * Time: 5:44 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace gen.snd.Forms
{
	/// <summary>
	/// VolumeSlider control
	/// </summary>
	public class VolumeSlider : System.Windows.Forms.UserControl
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private float volume = 1.0f;
		private float MinDb = -48;
		/// <summary>
		/// Volume changed event
		/// </summary>
		public event EventHandler VolumeChanged;
	
		/// <summary>
		/// Creates a new VolumeSlider control
		/// </summary>
		public VolumeSlider()
		{
			this.DoubleBuffered = true;
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();
	
			// TODO: Add any initialization after the InitComponent call
		}
	
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (components != null)
					components.Dispose();
			}
			base.Dispose(disposing);
		}
	
		#region Component Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// VolumeSlider
			// 
			this.Name = "VolumeSlider";
			this.Size = new System.Drawing.Size(96, 16);
	
		}
		#endregion
	
		/// <summary>
		/// <see cref="Control.OnPaint"/>
		/// </summary>
		protected override void OnPaint(PaintEventArgs pe)
		{
			// Calling the base class OnPaint
			Render(pe.Graphics);
			//base.OnPaint(pe);
		}
		
		[CategoryAttribute("Appearance")]
		public Color BackValueColor {
			get { return backValueColor; }
			set { backValueColor = value; }
		} Color backValueColor = Color.Green;
		
		void Render(Graphics g)
		{
			StringFormat format = new StringFormat();
			format.LineAlignment = StringAlignment.Center;
			format.Alignment = StringAlignment.Center;
	
			using (Pen p = new Pen(this.BackColor,1)) g.DrawRectangle(p, 0, 0, this.Width - 1, this.Height - 1);
			
			float db = 20 * (float)Math.Log10(Volume);
			float percent = 1 - (db / MinDb);
	
			using (Brush b = new SolidBrush(this.BackValueColor))
				g.FillRectangle(b, 1, 1, (int)((this.Width - 2) * percent), this.Height - 2);
			
			string dbValue = String.Format("{0:F2} dB", db);
			/*if(Double.IsNegativeInfinity(db)) dbValue = "-\x221e db"; // -8 dB */
	
			using (Brush b = new SolidBrush(this.ForeColor))
				g.DrawString(dbValue, this.Font, b, this.ClientRectangle, format);
		}
		
		/// <summary>
		/// <see cref="Control.OnMouseMove"/>
		/// </summary>
		protected override void OnMouseMove(MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				SetVolumeFromMouse(e.X);
			}
			base.OnMouseMove(e);
		}
	
		/// <summary>
		/// <see cref="Control.OnMouseDown"/>
		/// </summary>
		protected override void OnMouseDown(MouseEventArgs e)
		{
			SetVolumeFromMouse(e.X);
			base.OnMouseDown(e);
		}
	
		/// <summary>
		/// <see cref="Control.OnResize"/>
		/// </summary>
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			Invalidate();
			
		}
		private void SetVolumeFromMouse(int x)
		{
	
			// linear Volume = (float) x / this.Width;
			float dbVolume = (1 - (float)x / this.Width) * MinDb;
			if (x <= 0) Volume = 0;
			else Volume = (float)Math.Pow(10, dbVolume / 20);
		}
	
		/// <summary>
		/// The volume for this control
		/// </summary>
		[DefaultValue(1.0f)]
		public float Volume
		{
			get
			{
				return volume;
			}
			set
			{
				if (value < 0.0f) value = 0.0f;
				if (value > 1.0f) value = 1.0f;
				if (volume != value)
				{
					volume = value;
					if (VolumeChanged != null)
						VolumeChanged(this, EventArgs.Empty);
					Invalidate();
				}
			}
		}
	}
}
