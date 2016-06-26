using System;
using System.Drawing;
using System.Linq;
using System.Xml.Serialization;
namespace on.trig
{
  using Point = System.Drawing.DoublePoint;
  using Padding = System.Windows.Forms.Padding;
  using Control = System.Windows.Forms.Control;

  
  public class DoubleRect
  {
    /// <summary>
    /// returns a floored copy (EG: Math.Floor(...))
    /// </summary>
    public DoubleRect Floored {
      get {
        return Floor(this);
      }
    }

    static public DoubleRect Floor(DoubleRect source)
    {
      return new DoubleRect(source.Location.Floored, source.Size.Floored);
    }

    static public DoubleRect Round(DoubleRect source, int ith)
    {
      return new DoubleRect(source.Location.Rounded, source.Size.Rounded);
    }

    static public DoubleRect Zero {
      get {
        return new DoubleRect(0, 0, 0, 0);
      }
    }

    ////////////////////////////////////////////////////////////////////////
    [XmlIgnore]
    public DoublePoint Location = DoublePoint.Empty;

    [XmlIgnore]
    public DoublePoint Size = DoublePoint.Empty;

    ////////////////////////////////////////////////////////////////////////
    //
    [XmlIgnore]
    public DoublePoint Center {
      get {
        return Size * 0.5f;
      }
    }

    [XmlIgnore]
    public DoublePoint BottomRight {
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
    static public DoubleRect operator +(DoubleRect a, DoubleRect b) {
      return new DoubleRect(a.X + b.X, a.Y + b.Y, a.Width + b.Width, a.Height + b.Height);
    }

    static public DoubleRect operator -(DoubleRect a, DoubleRect b) {
      return new DoubleRect(a.X - b.X, a.Y - b.Y, a.Width - b.Width, a.Height - b.Height);
    }

    static public DoubleRect operator /(DoubleRect a, DoubleRect b) {
      return new DoubleRect(a.X / b.X, a.Y / b.Y, a.Width / b.Width, a.Height / b.Height);
    }

    static public DoubleRect operator *(DoubleRect a, DoubleRect b) {
      return new DoubleRect(a.X * b.X, a.Y * b.Y, a.Width * b.Width, a.Height * b.Height);
    }

    static public DoubleRect operator %(DoubleRect a, DoubleRect b) {
      return new DoubleRect(a.X % b.X, a.Y % b.Y, a.Width % b.Width, a.Height % b.Height);
    }

    static public DoubleRect operator ++(DoubleRect a) {
      return new DoubleRect(a.X++, a.Y++, a.Width++, a.Height++);
    }

    static public DoubleRect operator --(DoubleRect a) {
      return new DoubleRect(a.X--, a.Y--, a.Width--, a.Height--);
    }

    #endregion
    #region implicit operator Point,PointF
    /// <summary>
    /// We use rounding before int conversion
    /// </summary>
    /// <param name="a"></param>
    /// <returns></returns>
    static public implicit operator Rectangle(DoubleRect a) {
      return new Rectangle((int)Math.Round(a.X, 0), (int)Math.Round(a.Y, 0), (int)Math.Round(a.Width, 0), (int)Math.Round(a.Height, 0));
    }

    static public implicit operator RectangleF(DoubleRect a) {
      return new RectangleF(a.X, a.Y, a.Width, a.Height);
    }

    static public implicit operator Padding(DoubleRect a) {
      return new Padding((int)a.X, (int)a.Y, (int)a.Width, (int)a.Height);
    }

    static public implicit operator DoubleRect(Rectangle a) {
      return new DoubleRect(a.X, a.Y, a.Right, a.Bottom);
    }

    static public implicit operator DoubleRect(RectangleF a) {
      return new DoubleRect(a.X, a.Y, a.Right, a.Bottom);
    }

    #endregion
    public DoubleRect Clone()
    {
      return new DoubleRect();
    }

    ///  static FromControl Methods (relative to the control)
    static public DoubleRect FromClientInfo(DoublePoint ClientSize, Padding pad)
    {
      return new DoubleRect(DoublePoint.GetPaddingTopLeft(pad), ClientSize - DoublePoint.GetPaddingOffset(pad));
    }

    ///  static FromControl Methods (relative to the control)
    static public DoubleRect FromControl(Control ctl, bool usepadding)
    {
      return FromControl(ctl, (usepadding) ? ctl.Padding : Padding.Empty);
    }

    ///  static FromControl Methods (relative to the control)
    static public DoubleRect FromControl(Control ctl, Padding pad)
    {
      return new DoubleRect(DoublePoint.GetPaddingTopLeft(pad), DoublePoint.GetClientSize(ctl) - DoublePoint.GetPaddingOffset(pad));
    }

    /// <para>• p.Top,p.Right,p.Bottom,p.Left</para>
    static public DoubleRect FromPadding(Padding p)
    {
      return new DoubleRect(p.Left, p.Top, p.Right, p.Bottom);
    }

    //
    public DoubleRect()
    {
    }

    public DoubleRect(float x, float y, float width, float height)
    {
      Location = new DoublePoint(x, y);
      Size = new DoublePoint(width, height);
    }

    public DoubleRect(int x, int y, int width, int height) : this((float)x, (float)y, (float)width, (float)height)
    {
    }

    public DoubleRect(DoublePoint L, DoublePoint S) : this(L.X, L.Y, S.X, S.Y)
    {
    }

    public DoubleRect(Rectangle R) : this(R.X, R.Y, R.Width, R.Height)
    {
    }

    public DoubleRect(float num) : this(num, num, num, num)
    {
    }

    public DoubleRect(PointF Loc, SizeF Siz) : this(Loc.X, Loc.Y, Siz.Width, Siz.Height)
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
      return string.Format("DoubleRect: X:{0},Y:{1},Width:{2},Height:{3}", X, Y, Width, Height);
    }
  }
}







