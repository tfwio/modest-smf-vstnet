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
    bool HasGridMouse { get { return GridRect.Contains(ClientMouse); } }
    
    PointF[] HeaderPoint { get { return new PointF[2] { new FloatPoint { X = Container.Bounds.Left, Y = Container.Bounds.Top + HeightHeader }, new FloatPoint { X = Container.Bounds.Right, Y = Container.Bounds.Top + HeightHeader }, }; } }
    PointF[] FooterPoint { get { return new PointF[2] { new PointF { X = Bounds.Left, Y = Bounds.Bottom - HeightFooter }, new PointF { X = Bounds.Right, Y = Container.Bounds.Bottom - HeightFooter }, }; } }
    
    public FloatRect GridRect
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

    void parent_Resize(object sender, EventArgs e)
    {
      if (Parent==null) return;
      Width=Container.Width;
      Height= Container.Height;
      lineOffset = lineOffset.Contain(5 - MaxVisibleRows, 127);
    }

    public override void Initialize(IMui parent, Widget client)
    {
      base.Initialize(parent, client);
      Parent.SizeChanged += parent_Resize;
    }
    public override void Uninitialize(IMui parent, Widget client)
    {
      Parent.SizeChanged -= parent_Resize;
      base.Uninitialize(parent, client);
    }
    //    int MaxVisibleLines(Rectangle gridRect) { return gridRect.Height / LineHeight; }
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      //      this.SetFocus();
    }
    
    public WidgetMidiList(IMui parent) : base(parent)
    {
      //
      //      ParentWheel += WidgetMidiList_Wheel;
    }

    readonly StringFormat sf = new StringFormat()
    {
      Alignment = StringAlignment.Near,
      LineAlignment = StringAlignment.Center,
      FormatFlags = StringFormatFlags.DisplayFormatControl | StringFormatFlags.FitBlackBox,
      Trimming = StringTrimming.None
    };
    public override void Paint(PaintEventArgs arg)
    {
      var grid = GridRect;
      var localBounds = Bounds.Shrink(1);
      
      using (var reg = new Region(/*rgrid*/Bounds))
      {
        // simplicity
        var g = arg.Graphics;
        var state = g.Save();
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        g.Clip = reg;
        
        // Perhaps we can draw an octave to bitmap, and replicate.
        this.DoGrid(grid,arg.Graphics);
        this.DoNoteIds(grid,arg.Graphics);
        this.DoVKeys(grid,arg.Graphics);
        
        // Gutter Separators
        using (var pen = new Pen(Color.Green, 1))
        {
          g.DrawLines(pen, HeaderPoint);
          g.DrawLines(pen, FooterPoint);
        }
        
        // Bounding Box
        using (var pen = new Pen(Color.White, 1))
          g.DrawRectangle(pen, localBounds);

        bool flag = false;
        
        // Sometimes throws a null exception either for Parent or MouseD; unsure.
        try { flag = /*HasFocus && */grid.Contains(Parent.PointToClient(Parent.MouseD)); } catch { }
        
        if (flag) {
          var gridLim = grid.Clone();
          gridLim.Size = gridLim.Size - 1;
          gridLim.Width -=1;
          g.DrawSelectBox(this,gridLim);
        }
        
        g.ResetClip();
        g.Restore(state);
      }
      
    }
  }
}


