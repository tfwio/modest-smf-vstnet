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
	/// Description of cKnob.
	/// </summary>
	public class Knob : System.Windows.Forms.UserControl, IKnobSetting
	{

		#region Value Changed

		public event EventHandler ValueChanged;
		void OnValueChanged()
		{
			if (ValueChanged != null)
				ValueChanged(this, EventArgs.Empty);
		}

		#endregion

		#region Mouse Status (designer/internal)
		public enum MouseStatus : int
		{
			Default = 0,
			Down = 1
		}
		private MouseStatus MouseFlag = MouseStatus.Default;

		#endregion

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
		}
		/// <summary>
		/// Used to store the last mouse position
		/// </summary>
		private Point LastMousePoint = new Point();

		public Knob()
		{
			InitializeComponent();
		}
		
		void drawBg()
		{
			Bitmap imgMain = new Bitmap(this.Width, this.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
			Graphics gx = Graphics.FromImage(imgMain);
			gx.Clear(Color.Transparent);

			switch (this.mode) {
				case KnobType.Knob:
					gx.DrawImage(img, (-(ClientSize.Width + offset) * knobFrameIndex), 0, img.Width, img.Height);
					break;
				case KnobType.MatrixA:
					gx.DrawImage(img, 0, -(ClientSize.Height + offset) * knobFrameIndex, img.Width, img.Height);
					break;
				case KnobType.MatrixB:
					gx.DrawImage(img, 0, -(ClientSize.Height + offset) * knobFrameIndex, img.Width, img.Height);
					break;
			}

			this.BackgroundImage = imgMain;
		}
		
		#region Control Designer Properties

		[Category("Actual Value"), Description("A minimum value translated to frames.")]
		public int Maximum {
			get { return maximum; }
			set { maximum = value; }
		}
		int maximum;
		[Category("Actual Value"), Description("The minimum value translated to frames.")]
		public int Minimum {
			get { return minimum; }
			set { minimum = value; }
		}
		int minimum;
		[Category("Control"), Description("The number of frames in the image.")]
		public int Length {
			get { return length; }
			set {
				length = value;
				Invalidate();
			}
		}
		int length;
		[Category("Control"), Description("Indicates how the control interacts.")]
		public KnobType Mode {
			get { return mode; }
			set {
				mode = value;
				Invalidate();
			}
		}
		KnobType mode;
		[Category("Control"), Description("Width between frames if any.")]
		public int Offset {
			get { return offset; }
			set {
				offset = value;
				Invalidate();
			}
		}
		int offset = 0;
		[Category("Control"), Description("The number of frames in the image.")]
		public int KnobFrameIndex {
			get { return knobFrameIndex; }
			/*private */set {
				knobFrameIndex = value;
				OnValueChanged();
				Invalidate();
			}
		}
		int knobFrameIndex;
		[Category("Control"), Description("FOR MATRIX MODE: Rows and Colum Dimensions (relative to control dimentions)")]
		public Rectangle RowsCols {
			get { return rCol; }
			set {
				rCol = value;
				Invalidate();
			}
		}
		Rectangle rCol;
		[Category("Control"), Description("The ending color of the bar.")]
		public Image KnobImage {
			get { return img; }
			set {
				img = value;
				Invalidate();
			}
		}
		Image img;
		#endregion
		// 
		// we need a way to convert actual value to a image frame
		// given min and max values.
		// 
		#region Actual Value
		
		[Category("Value"), Description("Actual Value.")]
		public double ActualNumberValue {
			get { return actualNumberValue; }
			set { actualNumberValue = value; }
		} double actualNumberValue = 0;
		
		[Category("Value"), Description("Actual Value.")]
		public double ActualNumberMin {
			get { return actualNumberMin; }
			set { actualNumberMin = value; }
		} double actualNumberMin = 0;
		
		[Category("Value"), Description("Actual Value.")]
		public double ActualNumberMax {
			get { return actualNumberMax; }
			set { actualNumberMax = value; }
		} double actualNumberMax = double.MaxValue;
		
		#endregion
		
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			MouseFlag = MouseStatus.Default;
			if (this.img != null) drawBg();
		}

		#region Overrides: Mouse Down, Move, Up
		protected override void OnMouseDown(MouseEventArgs e)
		{
			base.OnMouseDown(e);
			MouseFlag = MouseStatus.Down;
			switch (this.mode) {
				case KnobType.Knob:
					LastMousePoint = new Point(e.X, e.Y);
					break;
				case KnobType.MatrixA:
					if ((e.X > rCol.X) && (e.Y > rCol.Y)) {
						OnMouseMove(e);
					}
					break;
			}
		}
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			switch (this.mode) {
				case KnobType.Knob:
					KnobMoveFunction(new Point(e.X, e.Y));
					break;
				case KnobType.MatrixA:
					MatrixAMoveFunction(new Point(e.X, e.Y));
					break;
				case KnobType.MatrixB:
					MatrixBMoveFunction(new Point(e.X, e.Y));
					break;
			}
			if (this.img != null) drawBg();
		}
		protected override void OnMouseUp(MouseEventArgs e)
		{
			base.OnMouseUp(e);
			MouseFlag = 0;
			int xx, yy;
			switch (this.mode) {
				case KnobType.MatrixA:
					LastMousePoint.X = e.X;
					LastMousePoint.Y = e.Y;
					yy = ((e.Y - rCol.Y) / 15) + 1;
					xx = ((e.X - rCol.X) / 15) + 1;
					if (yy == 2)
						xx += 4;
					knobFrameIndex = xx;
					break;
				case KnobType.MatrixB:
					LastMousePoint.X = e.X;
					LastMousePoint.Y = e.Y;
					xx = ((e.X - rCol.X) / 15);
					if ((xx > 0) && (xx < length))
						knobFrameIndex = xx;
					break;
			}
		}
		#endregion

		void KnobMoveFunction(Point MouseEventPoint)
		{
			if (MouseFlag == MouseStatus.Down) {
				// increment our value;
				if (MouseEventPoint.Y < LastMousePoint.Y)
					knobFrameIndex++;
				if (MouseEventPoint.Y > LastMousePoint.Y)
					knobFrameIndex--;

				// Limit our value
				if (knobFrameIndex >= length)
					knobFrameIndex = length - 1;
				if (knobFrameIndex < 0)
					knobFrameIndex = 0;

				LastMousePoint.Y = MouseEventPoint.Y;
			}
		}
		void MatrixAMoveFunction(Point MouseEventPoint)
		{
			int xx, yy;
			if ((MouseEventPoint.X > rCol.X) && (MouseEventPoint.Y > rCol.Y) && (MouseFlag == MouseStatus.Down)) {
				LastMousePoint.X = MouseEventPoint.X;
				LastMousePoint.Y = MouseEventPoint.Y;
				yy = ((MouseEventPoint.Y - rCol.Y) / 15) + 1;
				xx = ((MouseEventPoint.X - rCol.X) / 15) + 1;
				if (yy == 2) xx += 4;
				if ((xx > 0) && (xx < length)) knobFrameIndex = xx;
			}
		}
		void MatrixBMoveFunction(Point MouseEventPoint)
		{
			int xx, yy;
			if ((MouseEventPoint.X > rCol.X) && (MouseEventPoint.Y > rCol.Y) && (MouseFlag == MouseStatus.Down)) {
				LastMousePoint.X = MouseEventPoint.X;
				LastMousePoint.Y = MouseEventPoint.Y;
				xx = ((MouseEventPoint.X - rCol.X) / 15);
				if ((xx > 0) && (xx < length))
					knobFrameIndex = xx;
			}
		}
		void OldKnobFunction(Point MouseEventPoint)
		{
			if (MouseEventPoint.Y < LastMousePoint.Y)
				knobFrameIndex++;
			if (MouseEventPoint.Y > LastMousePoint.Y)
				knobFrameIndex--;
			if (knobFrameIndex >= length)
				knobFrameIndex = length - 1;
			if (knobFrameIndex < 0)
				knobFrameIndex = 0;
			LastMousePoint.Y = MouseEventPoint.Y;
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// Knob
			// 
			this.Name = "Knob";
			this.Size = new System.Drawing.Size(30, 30);
		}
		#endregion

	}


}
