/* oio * 8/3/2015 * Time: 6:39 AM */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
namespace Mui.Widgets
{
	public abstract partial class Widget
	{
    virtual public void Paint(PaintEventArgs arg)
    {
      Painter.DrawBorder(arg.Graphics, this);
    }
    
    static protected readonly Color DefaultBackgroundColor = Painter.DictColour[ColourClass.Dark40];
    
    public Color BackgroundColor { get; set; }
    
    
    /// <summary>If set, overrides colourclass</summary>
    virtual public Color? ForegroundColor { get; set; }
    
    // <summary>If set, overrides colourclass</summary>
    //Color BackgroundColor { get; set; }
    
    virtual public Color ColourFg { get { return ForegroundColor.HasValue ? ForegroundColor.Value : Painter.DictColour[ColourClassFg]; } }
    
    
    public ColourClass ColourClassFg {
      get {
        if (HasMouseDown /* || HasClientMouseDown*/ ) return ColourClass.Active;
        if (HasFocus) return ColourClass.Focus;
        return HasClientMouse ? ColourClass.White : ColourClass.Default;
      }
    }
    
	}
}






