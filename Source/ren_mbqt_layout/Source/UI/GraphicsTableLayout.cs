/* oio * 8/3/2015 * Time: 6:40 AM
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout
{
  static class GraphicsTableHelper
  {
  }

  public class GraphicsTableCell
  {
    public int Index { get; set; }
    public Point CellBorderSize { get; set; } = new Point(0, 0);

    /// <summary>This is to be thought of as 'reserved' for future use.</summary>
    public ContentAlignment Alignment { get; set; } = ContentAlignment.TopLeft;

    public GraphicsClient Content { get; set; } = null;

    public GraphicsTableRow Row { get; internal protected set; }
    public GraphicsTableLayout Table { get { return Row.Table; } }

    public float Width { get; set; }
    public int IntWidth { get { return Convert.ToInt32(Width); } set { Width = value; } }

    public int ColSpan { get; set; } = 1;
    public GraphicsTableCell(GraphicsTableRow row)
    {
      this.Row = row;
    }
  }

  public class GraphicsTableRow
  {
    public int Index { get; set; }

    public GraphicsTableLayout Table { get; set; }
    public int RowSpan { get; set; } = 1;

    public float Height { get; set; } = -1;
    public int IntHeight { get { return Convert.ToInt32(Height); } set { Height = value; } }
    bool IsPercentHeight { get; set; } = false;


    void Initialize(int nCols)
    {
      if (nCols == this.Columns.Count) return;
      this.Columns.Clear();
      for (int i = 0; i < nCols; i++) Columns.Add(new GraphicsTableCell(this) { Index = i });
    }

    public GraphicsTableRow(GraphicsTableLayout table, int nCols)
    {
      this.Table = table;
      Initialize(nCols);
    }

    public List<GraphicsTableCell> Columns { get; set; } = new List<GraphicsTableCell>();
  }

  public class GraphicsTableLayout : IScrollableLayout
  {
    // windows/system32/gwx/gwxworkersomethingorother

    public int CalculatedWidth { get; set; } = 0;
    public int CalculatedHeight { get; set; } = 0;

    public int RowMinHeight { get; set; } = 14;
    public int RowMaxHeight { get; set; } = -1;

    public int ColMinWidth { get; set; } = 16;
    public int ColMaxWidth { get; set; } = -1;

    public GraphicsTableCell this[int row, int col] { get { return Rows[row].Columns[col]; } }
    public GraphicsTableRow this[int row] { get { return Rows[row]; } }

		public Point TableBorderSize = new Point(0,0);
		public Color TableBorderColor = Color.Black;
    public Padding BorderSize = new Padding(0,0,0,0);

		public IGraphicsClient Client { get; set; }

		public List<GraphicsTableRow> Rows { get; set; } = new List<GraphicsTableRow>();

    /// <summary>
    /// resize each column to match the first.
    /// </summary>
    public void InitializeLayout()
    {
      int x = 0, y = 0;

      foreach (var row in Rows)
      {
        x = 0;
        foreach (var col in row.Columns)
        {
          if (col.Index == 0) continue;
          col.Width = this[0, col.Index].Width;
          if (col.Content != null)
          {
            col.Content.X = x;
            col.Content.Y = y;
            col.Content.Width = this[0, col.Index].IntWidth;
            col.Content.Height = this[0].IntHeight;
          }
          x += col.IntWidth;
        }
        y += row.IntHeight;
      }
      CalculatedHeight = y;
      CalculatedWidth = x;
    }
    public void Initialize(int nRows, int nCols)
    {
      if (nRows == this.Rows.Count) return;
      Rows.Clear();
      for (int i = 0; i < nRows; i++) Rows.Add(new GraphicsTableRow(this, nCols) { Index = i });
    }
	}

}




