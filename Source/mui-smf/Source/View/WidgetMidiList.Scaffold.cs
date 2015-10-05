/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
using System.Linq;
namespace mui_smf
{
  public partial class WidgetMidiList : Widget
  {
    // we would like to center C3 on the following for the default view
    internal int LineHeight = 14;
    
    // ===========================================
    // Constants
    // ===========================================
    
    // Defaults
    // ===========================================
    const float DefaultRatioOfInterest = 0.5f;
    const int DefaultNotesPerBar = 16;
    const int DefaultPixelsPerQuarterNote = 4;
    
//    const int ApplyPaddingBottom = 32;
//    const int ApplyPaddingRight = 32;
//    const int WidthGutter0 = 100;

    // Geometry
    // ===========================================
    const int HeightHeader = 32;
    const int HeightFooter = 48;
    const int WidthGutterLeft1 = 40;
    const int WidthGutterLeft2 = 40;
    
    protected internal int WidthGutter { get { return WidthGutterLeft1 + WidthGutterLeft2; } }

    // ============================================
    // Primary Properties
    // ============================================
    
    int GridHeight { get { return Convert.ToInt32(Container.Bounds.Height - HeightHeader - HeightFooter).Contain(Bounds.Top, Bounds.Bottom); } }
    int GridWidth { get { return Convert.ToInt32(Container.Bounds.Width - WidthGutter).Contain(Bounds.Left, Bounds.Right); } }

    protected internal int LineOffset {
      get { return lineOffset; }
      set { lineOffset = value; }
    } int lineOffset = 80;
    
    protected internal int MaxVisibleRows { get { return Convert.ToInt32(Math.Floor(GridRect.Height / LineHeight)); } }
    
    protected internal int PixelsPerQuarterNote {
      get { return pixelsPerQuarterNote; }
      set { pixelsPerQuarterNote = value; }
    } int pixelsPerQuarterNote = DefaultPixelsPerQuarterNote;

    int MaxQuarterNotesOnScreen { get { return Convert.ToInt32(Math.Floor(GridRect.Width / pixelsPerQuarterNote)); } }
    
    // ===========================================
    // Secondary Properties
    // ===========================================
    
    protected internal int PixelsPerNote { get { return PixelsPerQuarterNote * 4; } }
    protected internal int PixelsPerQuarter { get { return PixelsPerNote * 4; } }
    protected internal int PixelsPerBar { get { return PixelsPerQuarter * 4; } }

    /// Pen Color: 282828
    
    // ===========================================
    // Methods
    // ===========================================
    
    /// <summary>
    /// Utility method to reverse midi note numbers.<br />
    /// Numbers are rendered from 127 (up) to 0 (down) transformed from index 0-127.
    /// </summary>
    /// <param name="index"></param>
    /// <param name="padding"></param>
    /// <param name="min"></param>
    /// <param name="max"></param>
    /// <returns></returns>
    static int Rev(int index, int padding=127, int min=0, int max=127)
    {
      var num = padding-index;
      return !num.IsIn(min,max) ? -1 : num.Contain(0,127);
    }
    
    RowInfoV VisibleRowFirst { get { return GetVLines(lineOffset).FirstOrDefault(a => a.CanDo == true); } }
    RowInfoV VisibleRowLast { get {  return GetVLines(lineOffset) .LastOrDefault (a => a.CanDo == true); } }

    /// <summary>
    /// We want to start rendering from 12->127-0->12
    /// 
    /// What is offset --- if we want to center on C3?
    /// </summary>
    /// <param name="offset"></param>
    /// <returns></returns>
    protected internal System.Collections.Generic.IEnumerable<RowInfoV> GetVLines(int offset = 0)
    {
      int maxRows = MaxVisibleRows;
      
      for (int i=0; i < maxRows; i++)
      {
        var y0=i*LineHeight;
        float top=y0+GridRect.Top;
        
        var r  = Rev(i,127);
        var ro = Rev(i+offset,127);
        
        yield return new RowInfoV{
          RowIndex=ro.Contain(0,127),
          IsIvory=gen.snd.Midi.MidiHelper.IsIvory[ro.Contain(0,127) % 12],
          Top=top,
          CanDo=ro.IsIn(0,127)
        };
      }
    }
    
    protected internal System.Collections.Generic.IEnumerable<RowInfoH> GetHLines(int increment=1, int offset = 0)
    {
      int numquarters = MaxQuarterNotesOnScreen;
      int xoffset = Convert.ToInt32(Math.Floor(GridRect.X));
      
      for (int i=0; i <= MaxQuarterNotesOnScreen; i+=increment)
      {
        var location = i*pixelsPerQuarterNote;
        yield return new RowInfoH{ Index=i, X=location, XO=xoffset+location };
      }
    }

  }
}




