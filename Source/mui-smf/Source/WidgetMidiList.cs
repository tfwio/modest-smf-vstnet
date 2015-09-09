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

  public class WidgetMidiList : Widget
  {
    // 48-1 = C3
    string[] MKeys = gen.snd.MidiHelper.OctaveMacro();
    
    // we would like to center C3 on the following for the default view
    const float DefaultRatioOfInterest = 0.5f;
    int LineHeight = 14;
    int MinLinesVisible = 5;
    
    #region Grid Info
    const int ApplyPaddingBottom = 32;
    const int ApplyPaddingRight = 32;
    const int HeightFooter = 24;
    const int HeightHeader = 48;
    const int WidthGutter0 = 100;
    const int WidthGutterLeft1 = 40;
    const int WidthGutterLeft2 = 40;
    const int WidthGutter = WidthGutterLeft1 + WidthGutterLeft2;

    int GridHeight { get { return Convert.ToInt32(Container.Bounds.Height - HeightHeader - HeightFooter).Contain(Bounds.Top, Bounds.Bottom); } }
    int GridWidth { get { return Convert.ToInt32(Container.Bounds.Width - WidthGutter).Contain(Bounds.Left, Bounds.Right); } }

    PointF[] HeaderPoint { get { return new PointF[2] { new FloatPoint { X = Container.Bounds.Left, Y = Container.Bounds.Top + HeightHeader }, new FloatPoint { X = Container.Bounds.Right, Y = Container.Bounds.Top + HeightHeader }, }; } }
    PointF[] FooterPoint { get { return new PointF[2] { new PointF { X = Bounds.Left, Y = Bounds.Bottom - HeightFooter }, new PointF { X = Bounds.Right, Y = Container.Bounds.Bottom - HeightFooter }, }; } }
    
    FloatRect GridRect
    {
      get
      {
        return new FloatRect
        {
          X = Container.Bounds.Left + WidthGutterLeft1 + WidthGutterLeft2,
          Y = Container.Bounds.Top + HeightHeader,
          Width = GridWidth,
          Height = GridHeight
        };
      }
    }

    #endregion

    static int Rev(int index, int padding=127, int min=0, int max=127)
    {
      var num = padding-index;
      return !num.IsIn(min,max) ? -1 : num.Contain(0,127);
    }
    
    #region Grid Horizontal
    struct HRowInfo
    {
      public int I  { get; set; }
      public int X  { get; set; }
      public int XO { get; set; }
    }
    const int DefaultPixelsPerQuarterNote = 4;
    int PixelsPerQuarterNote = DefaultPixelsPerQuarterNote;
    
    const int DefaultNotesPerBar = 16;
    int NotesPerBar = DefaultNotesPerBar;
    
    int MaxQuarterNotesOnScreen { get { return Convert.ToInt32(Math.Floor(GridRect.Width / PixelsPerQuarterNote)); } }
    // not impmented
    /// Pen Color: 282828
    System.Collections.Generic.IEnumerable<HRowInfo> GetHLines(int increment=1, int offset = 0)
    {
      int numquarters = MaxQuarterNotesOnScreen;
      int xoffset = Convert.ToInt32(Math.Floor(GridRect.X));
      
      for (int i=0; i <= MaxQuarterNotesOnScreen; i+=increment)
      {
        var location = i*PixelsPerQuarterNote;
        yield return new HRowInfo{ I=i, X=location, XO=xoffset+location };
      }
    }
    
    #endregion

    #region Grid Vertical
    
    /// <summary>
    /// We want to start rendering from 12->127-0->12
    /// 
    /// What is offset --- if we want to center on C3?
    /// </summary>
    /// <param name="reverse"></param>
    /// <param name="offset"></param>
    /// <param name="padding"></param>
    /// <returns></returns>
    System.Collections.Generic.IEnumerable<RowInfo> GetVLines(int offset = 0)
    {
      int maxRows = MaxVisibleRows;
      
      for (int i=0; i < maxRows; i++)
      {
        var y0=i*LineHeight;
        float top=y0+GridRect.Top;
        
        var r  = Rev(i,127);
        var ro = Rev(i+offset,127);
        
        yield return new RowInfo{
          I=i,
          IO=i,
          RO=ro.Contain(0,127),
          R=r.Contain(0,127),
          IsIvory=gen.snd.MidiHelper.IsIvory[ro.Contain(0,127) % 12],
          Top=top,
          Line=new Point[]{
            new FloatPoint{ X=GridRect.Left,  Y=top },
            new FloatPoint{ X=GridRect.Right, Y=top }
          },
          CanDo=ro.IsIn(0,127)
        };
      }
    }
    
    #endregion

    void parent_Resize(object sender, EventArgs e)
    {
      if (Parent==null) return;
      System.Threading.Thread.SpinWait(300);
      Width=Container.Width;
      Height= Container.Height;
    }
    
    int LineOffset = 80;
    int MaxVisibleRows { get { return Convert.ToInt32(Math.Floor(GridRect.Height / LineHeight)); } }
    
    RowInfo VisibleRowFirst { get { return GetVLines(LineOffset).FirstOrDefault(a => a.CanDo == true); } }
    RowInfo VisibleRowLast { get {  return GetVLines(LineOffset) .LastOrDefault (a => a.CanDo == true); } }
    
    //    int MaxVisibleLines(Rectangle gridRect) { return gridRect.Height / LineHeight; }
    
    void WidgetMidiList_Wheel(object sender, WheelArgs e)
    {
      if (!HasClientMouse) return;
      int mv = MaxVisibleRows;
      
      if (e.Flag==0x02) LineHeight = (LineHeight+e.Amount).Contain(8,100);
      else if (e.Flag==0x01) LineOffset += e.Amount*12;
      else LineOffset += e.Amount;
      int x0=0,x1=0,x2=0;
      
      // (calculated) actual number of visible rows.
      x0 = LineOffset < 0 ? (mv + LineOffset).Contain(0,mv) : (128 - LineOffset).Contain(0,mv);
      x1 = (127-LineOffset);
      //      x1 = x1 > 127 ? -1 : x1.Contain(-1,127);
      int r1=0,r2=0,r3=0;
      if (VisibleRowFirst==null || VisibleRowLast==null) {
        r1=-1;
        r2=-1;
        r3 = 0;
      }
      else
      {
        r1=VisibleRowFirst.RO;
        r2=VisibleRowLast .RO;
        r3 = r1-r2+1;
      }
      System.Diagnostics.Debug.Print("max={0}, offset={1}, first={2}:{6}, last={3}:{7}, visi={4}:{5}", mv, LineOffset, r1, /*03*/r2, r3,/*05*/x0,x1,x2);
      Parent.Invalidate();
    }
    
    public WidgetMidiList(IMui parent) : base(parent)
    {
      Parent.SizeChanged += parent_Resize;
      ParentWheel += WidgetMidiList_Wheel;
    }

    public override void Paint(PaintEventArgs arg)
    {
      var grid = GridRect;
      using (var reg = new Region(/*rgrid*/Bounds)) {
        
        // simplicity
        var g = arg.Graphics;
        g.Clip = reg;
        
        // Maximum Visible Rows
        using (var linePen=new Pen(Color.Blue,1)) foreach (var row in GetVLines(LineOffset))
        {
          var r = new FloatRect(this.Container.X+12, row.Top, WidthGutter, LineHeight );
          if (row.CanDo)
          {
            var str = string.Format("{0:00#} {1,-3}", row.RO, MKeys[row.RO]);
            var rc = new FloatRect(grid.Left, row.Top, grid.Width, LineHeight);
            rc = rc.Shrink(1);
            using (var sb = new SolidBrush(Color.FromArgb(64,row.IsIvory ? Color.White : Color.Black)))
            {
              g.FillRectangle(sb,rc);
            }
            g.DrawText(str, Color.White, this.Font, r, StringAlignment.Near);
          }
        }
        // 
        using (var p0 = new Pen(Color.FromArgb(40,40,40)))
          using (var p1 = new Pen(Color.FromArgb(130,130,130)))
            using (var p2 = new Pen(Color.FromArgb(255,255,255)))
        {
          foreach (var i in GetHLines(4)) g.DrawLines(p0,new Point[]{ new FloatPoint(i.XO,grid.Top), new FloatPoint(i.XO,grid.Bottom) });
          foreach (var i in GetHLines(Convert.ToInt32(Math.Pow(4,2)))) g.DrawLines(p1,new Point[]{ new FloatPoint(i.XO,grid.Top), new FloatPoint(i.XO,grid.Bottom) });
          foreach (var i in GetHLines(Convert.ToInt32(Math.Pow(4,3)))) g.DrawLines(p2,new Point[]{ new FloatPoint(i.XO,grid.Top), new FloatPoint(i.XO,grid.Bottom) });
        }
        // Grid-Box
        using (var pen = new Pen(Color.Green, 1))
        {
          g.DrawRectangle(pen, Bounds);
          g.DrawLines(pen, HeaderPoint);
          g.DrawLines(pen, FooterPoint);
        }
        // 
        using (var pen = new Pen(Color.White, 1)) g.DrawRectangle(pen, GridRect);
        //
        g.DrawRectangle(Pens.Silver,Container.Bounds);
        arg.Graphics.ResetClip();
        
      }
      
    }
  }
}


