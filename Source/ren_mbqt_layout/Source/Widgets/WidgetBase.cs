/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout.Widgets
{
  public abstract class WidgetBase <TParent> where TParent : MainForm
  {
    virtual public bool HasMouseDown {
      get; set;
    }
    
    virtual public bool HasClientMouse {
      get { return false; }
    }
    
    virtual public bool HasClientMouseDown {
      get { return false; }
    }
    
    virtual public Color ColourFg { get { return Painter.DictColour[ColourClassFg]; } }
    
    abstract public bool HasFocus { get; }
    
    public ColourClass ColourClassFg
    {
      get
      {
        if (HasMouseDown/* || HasClientMouseDown*/) return ColourClass.Active;
        else if (HasFocus) return ColourClass.Focus;
        else if (HasClientMouse) return ColourClass.White;
        else return ColourClass.Default;
      }
    }

    #region event-Mouse
    public event EventHandler Click {
      add    { Parent.Click += value; }
      remove { Parent.Click -= value; }
    }
    public event MouseEventHandler MouseDown {
      add    { Parent.MouseDown += value; }
      remove { Parent.MouseDown -= value; }
    }
    public event MouseEventHandler MouseUp {
      add    { Parent.MouseUp += value; }
      remove { Parent.MouseUp -= value; }
    }
    public event MouseEventHandler MouseMove {
      add    { Parent.MouseMove += value; }
      remove { Parent.MouseMove -= value; }
    }
    #endregion
    
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
    
    virtual public FloatRect Bounds {
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






