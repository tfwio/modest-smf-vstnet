/*
 * Created by SharpDevelop.
 * User: tfwxo
 * Date: 9/14/2015
 * Time: 1:08 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Drawing;
using Mui;

namespace mui_smf
{
  /// <summary>
  /// Description of MidiListRenderer.
  /// </summary>
  static class MidiListPainter
  {
    
    // 48-1 = C3
    static readonly string[] MKeys = gen.snd.Midi.MidiHelper.OctaveMacro();
    static readonly Color Gray20  = Color.FromArgb(20, 20, 20);
    static readonly Color Gray130 = Color.FromArgb(130, 130, 130);
    static readonly Color White = Color.FromArgb(255, 255, 255);
    
    static public void DoNoteIds(this WidgetMidiList widget, FloatRect grid, Graphics g)
    {
      var gs = g.Save();
      using (g.Clip = new Region(grid))
      {
        foreach (var i in widget.GetHLines(Convert.ToInt32(Math.Pow(4, 2))))
        {
          var r2=new FloatRect(i.XO-16,grid.Top,32,24);
          g.FillEllipse(Brushes.Black,r2);
          g.DrawText((i.Index / 4).ToString(),Color.White,widget.Font,r2);
        }
        foreach (var i in widget.GetHLines(Convert.ToInt32(Math.Pow(4, 3))))
        {
          var r2=new FloatRect(i.XO-16,grid.Top+32,32,24);
          g.FillEllipse(Brushes.Black,r2);
          g.DrawText((i.Index / 64).ToString(),Color.White,widget.Font,r2);
        }
        g.ResetClip();
      }
      g.Restore(gs);
      
    }
    
    static public void DoGrid(this WidgetMidiList widget, FloatRect grid, Graphics g)
    {
      var gs = g.Save();
      using (g.Clip = new Region(grid))
      {
        using (var p0 = new Pen(Color.Black)) foreach (var i in widget.GetHLines(4))
          g.DrawLines(p0, new Point[] { new FloatPoint(i.XO, grid.Top), new FloatPoint(i.XO, grid.Bottom) });
        
        using (var p1 = new Pen(Gray130)) foreach (var i in widget.GetHLines(Convert.ToInt32(Math.Pow(4, 2))))
          g.DrawLines(p1, new Point[] { new FloatPoint(i.XO, grid.Top), new FloatPoint(i.XO, grid.Bottom) });
        
        using (var p2 = new Pen(White)) foreach (var i in widget.GetHLines(Convert.ToInt32(Math.Pow(4, 3))))
          g.DrawLines(p2, new Point[] { new FloatPoint(i.XO, grid.Top), new FloatPoint(i.XO, grid.Bottom) });
        
        g.ResetClip();
      }
      g.Restore(gs);
      
    }
    
    static public void DoVKeys(this WidgetMidiList widget, FloatRect grid, Graphics g)
    {
      var gs  = g.Save();
      // Vertical
      using (var linePen = new Pen(Color.Blue, 1)) foreach (var row in widget.GetVLines(widget.LineOffset))
      {
        var r = new FloatRect(widget.Container.X + 12, row.Top, widget.WidthGutter, widget.LineHeight);
        
        if (row.CanDo)
        {
          // Note-Number + Piano-KeyName
          var str = string.Format("{0:00#} {1,-3}", row.RowIndex, MKeys[row.RowIndex]);
          
          // create rect, then move x/y and shrinks w/h by one pixel.
          var rc = new FloatRect(grid.Left, row.Top, grid.Width, widget.LineHeight).Shrink(1);
          
          using (var sb = new SolidBrush(Color.FromArgb(24, row.IsIvory ? Color.White : Color.Black)))
            if (row.IsIvory) g.FillRectangle(sb, rc);
          
          var r2 = r.Clone();
          r2.Location = r2.Location.NegX(10);
          g.DrawText(str,Color.White,widget.Font,r2,StringAlignment.Near);

        }
      }
      g.Restore(gs);
    }
  }
}
