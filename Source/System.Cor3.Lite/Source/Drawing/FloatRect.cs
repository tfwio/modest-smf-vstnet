﻿using System;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
namespace on.trig
{
	using Point = System.Drawing.DoublePoint;
	using Padding = System.Windows.Forms.Padding;
	using Control = System.Windows.Forms.Control;

	public class FloatRect
	{
		/// <summary>
		/// returns a floored copy (EG: Math.Floor(...))
		/// </summary>
		public FloatRect Floored {
			get {
				return Floor(this);
			}
		}

		static public FloatRect Floor(FloatRect source)
		{
			return new FloatRect(source.Location.Floored, source.Size.Floored);
		}

		static public FloatRect Round(FloatRect source, int ith)
		{
			return new FloatRect(source.Location.Rounded, source.Size.Rounded);
		}

		static public FloatRect Zero {
			get {
				return new FloatRect(0, 0, 0, 0);
			}
		}

		////////////////////////////////////////////////////////////////////////
		[XmlIgnore]
		public FloatPoint Location = FloatPoint.Empty;

		[XmlIgnore]
		public FloatPoint Size = FloatPoint.Empty;

		////////////////////////////////////////////////////////////////////////
		//
		[XmlIgnore]
		public FloatPoint Center {
			get {
				return Size * 0.5f;
			}
		}

		[XmlIgnore]
		public FloatPoint BottomRight {
			get {
				return Location + Size;
			}
		}

		////////////////////////////////////////////////////////////////////////
		//
		[XmlAttribute]
		public float X {
			get {
				return Location.X;
			}
			set {
				Location.X = value;
			}
		}

		[XmlAttribute]
		public float Y {
			get {
				return Location.Y;
			}
			set {
				Location.Y = value;
			}
		}

		/// <summary> (read only) </summary>
		[XmlIgnore]
		public float Top {
			get {
				return Location.Y;
			}
		/*set { Location.Y = value; }*/}

		/// <summary> (read only) </summary>
		[XmlIgnore]
		public float Bottom {
			get {
				return Location.Y + Size.Y;
			}
		/*set { Size.Y = value-Location.Y; }*/}

		[XmlIgnore]
		public float Left {
			get {
				return Location.X;
			}
			set {
				Location.X = value;
			}
		}

		/// <summary> (read only) </summary>
		[XmlIgnore]
		public float Right {
			get {
				return (Location.AddX(Size)).X;
			}
		/*set { Size.X = value-Location.X; }*/}

		////////////////////////////////////////////////////////////////////////
		/// <summary> (read only) </summary>
		[XmlAttribute]
		public float Width {
			get {
				return Size.X;
			}
			set {
				Size.X = value;
			}
		}

		[XmlAttribute]
		public float Height {
			get {
				return Size.Y;
			}
			set {
				Size.Y = value;
			}
		}

		////////////////////////////////////////////////////////////////////////
		//  operator
		#region Standard +,-,*,/,++,--
		static public FloatRect operator +(FloatRect a, FloatRect b) {
			return new FloatRect(a.X + b.X, a.Y + b.Y, a.Width + b.Width, a.Height + b.Height);
		}

		static public FloatRect operator -(FloatRect a, FloatRect b) {
			return new FloatRect(a.X - b.X, a.Y - b.Y, a.Width - b.Width, a.Height - b.Height);
		}

		static public FloatRect operator /(FloatRect a, FloatRect b) {
			return new FloatRect(a.X / b.X, a.Y / b.Y, a.Width / b.Width, a.Height / b.Height);
		}

		static public FloatRect operator *(FloatRect a, FloatRect b) {
			return new FloatRect(a.X * b.X, a.Y * b.Y, a.Width * b.Width, a.Height * b.Height);
		}

		static public FloatRect operator %(FloatRect a, FloatRect b) {
			return new FloatRect(a.X % b.X, a.Y % b.Y, a.Width % b.Width, a.Height % b.Height);
		}

		static public FloatRect operator ++(FloatRect a) {
			return new FloatRect(a.X++, a.Y++, a.Width++, a.Height++);
		}

		static public FloatRect operator --(FloatRect a) {
			return new FloatRect(a.X--, a.Y--, a.Width--, a.Height--);
		}

		#endregion
		#region implicit operator Point,PointF
		/// <summary>
		/// We use rounding before int conversion
		/// </summary>
		/// <param name="a"></param>
		/// <returns></returns>
		static public implicit operator Rectangle(FloatRect a) {
			return new Rectangle((int)Math.Round(a.X, 0), (int)Math.Round(a.Y, 0), (int)Math.Round(a.Width, 0), (int)Math.Round(a.Height, 0));
		}

		static public implicit operator RectangleF(FloatRect a) {
			return new RectangleF(a.X, a.Y, a.Width, a.Height);
		}

		static public implicit operator Padding(FloatRect a) {
			return new Padding((int)a.X, (int)a.Y, (int)a.Width, (int)a.Height);
		}

		static public implicit operator FloatRect(Rectangle a) {
			return new FloatRect(a.X, a.Y, a.Right, a.Bottom);
		}

		static public implicit operator FloatRect(RectangleF a) {
			return new FloatRect(a.X, a.Y, a.Right, a.Bottom);
		}

		#endregion
		public FloatRect Clone()
		{
			return new FloatRect();
		}

		///  static FromControl Methods (relative to the control)
		static public FloatRect FromClientInfo(FloatPoint ClientSize, Padding pad)
		{
			return new FloatRect(FloatPoint.GetPaddingTopLeft(pad), ClientSize - FloatPoint.GetPaddingOffset(pad));
		}

		///  static FromControl Methods (relative to the control)
		static public FloatRect FromControl(Control ctl, bool usepadding)
		{
			return FromControl(ctl, (usepadding) ? ctl.Padding : Padding.Empty);
		}

		///  static FromControl Methods (relative to the control)
		static public FloatRect FromControl(Control ctl, Padding pad)
		{
			return new FloatRect(FloatPoint.GetPaddingTopLeft(pad), FloatPoint.GetClientSize(ctl) - FloatPoint.GetPaddingOffset(pad));
		}

		/// <para>• p.Top,p.Right,p.Bottom,p.Left</para>
		static public FloatRect FromPadding(Padding p)
		{
			return new FloatRect(p.Left, p.Top, p.Right, p.Bottom);
		}

		//
		public FloatRect()
		{
		}

		public FloatRect(float x, float y, float width, float height)
		{
			Location = new FloatPoint(x, y);
			Size = new FloatPoint(width, height);
		}

		public FloatRect(int x, int y, int width, int height) : this((float)x, (float)y, (float)width, (float)height)
		{
		}

		public FloatRect(FloatPoint L, FloatPoint S) : this(L.X, L.Y, S.X, S.Y)
		{
		}

		public FloatRect(Rectangle R) : this(R.X, R.Y, R.Width, R.Height)
		{
		}

		public FloatRect(float num) : this(num, num, num, num)
		{
		}

		public FloatRect(PointF Loc, SizeF Siz) : this(Loc.X, Loc.Y, Siz.Width, Siz.Height)
		{
		}

		public override bool Equals(object obj)
		{
			return obj.ToString() == ToString();
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		public override string ToString()
		{
			return string.Format("FloatRect: X:{0},Y:{1},Width:{2},Height:{3}", X, Y, Width, Height);
		}
	}
}









