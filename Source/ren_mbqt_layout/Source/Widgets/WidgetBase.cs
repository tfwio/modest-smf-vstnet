/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
	public abstract class WidgetBase<TParent> where TParent : Control
	{
		
	  virtual public bool HasMouse {
	    get { return false; }
		}
	  
	  virtual public bool HasMouseDown {
	    get { return false; }
	  }
	  
	  public Color ColourFg { get { return Painter.DictColour[ColourClassFg]; } }
	  
	  public ColourClass ColourClassFg
	  {
	    get
	    {
	      if (HasMouseDown) return ColourClass.Active;
	      else if (HasMouse) return ColourClass.White;
	      else return ColourClass.Default;
	    }
	  }

		#region Not Useful
		
		public FloatPoint Offset {
			get;
			set;
		}

		#endregion
		
		public WidgetBase(TParent parent)
		{
			this.Parent = parent;
		}

		virtual public void Increment()
		{
		}

		virtual public void Increment<T>(T data)
		{
		}

		#region Position
		
		public FloatRect Bounds {
			get;
			set;
		}

		public Padding Padding {
			get;
			set;
		}

		public FloatRect PaddedBounds {
			get {
				if (Bounds == null)
					return null;
				var NewRect = FloatRect.ApplyPadding(Bounds, Padding);
				return NewRect;
			}
		}

		#endregion
		
		public TParent Parent {
			get;
			set;
		}

		virtual public string Text {
			get;
			set;
		}

		virtual public Font Font {
      get { return font == null ? Parent.Font : font; }
      set { font = value; }
    } Font font;

		// this should attach to its parent
		bool IsActive {
			get;
			set;
		}

		bool NeedsPaint {
			get;
			set;
		}

		#region Value
		
		public object DoubleValue {
			get;
			set;
		}

		virtual public object Value {
			get;
			set;
		}

		public string ValueFormat {
      get {
        return valueFormat;
      }
      set {
        valueFormat = value;
      }
    } internal string valueFormat = "{0}";

		#endregion
		
		abstract public void Paint(Graphics g);
		
	}
}






