/* oio * 8/3/2015 * Time: 6:40 AM
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
namespace ren_mbqt_layout
{
  public interface IGraphicsClientHandle
  {
    int Left { get; set; }
    int Top { get; set; }

    int Width { get; set; }
    int Height { get; set; }

    Size ClientSize { get; set; }
    Rectangle ClientRectangle { get; }
    Point Location { get; set; }
    Padding Padding { get; set; }

    Color BackColor { get; set; }
    Color ForeColor { get; set; }

    Font Font { get; set; }

    GraphicsUiManager GraphicsManager { get; }
  }
  public interface IGraphicsClient
  {
    string Name { get; set; }

    int X { get; set; }
    int Y { get; set; }

    int Width { get; set; }
    int Height { get; set; }

    FloatPoint ClientSize { get; set; }
    FloatRect ClientRect { get; set; }

    Padding Padding { get; set; }
    Padding Borders { get; set; }

    Font Font { get; set; }

    void Paint(Graphics graphics);
    IGraphicsClientHandle ParentClient { get; set; }
  }

  abstract public class GraphicsClient : IGraphicsClient
  {
    /// <summary>Manditory.</summary>
    public string Name { get; set; }

    public string Text { get; set; } = null;
    public Font Font { get; set; }

    /// <summary>
    ///  All calculations derive here.
    /// </summary>
    public FloatRect ClientRect { get; set; }

    public FloatRect ClientRectPadded {
      get {
        var r = ClientRect;
        r.Width = r.Width - Padding.Horizontal;
        r.Height = r.Height - Padding.Vertical;
        r.X = r.X + Padding.Left;
        r.Y = r.Y + Padding.Top;
        return r;
      }
    }

    public int X { get { return Convert.ToInt32(ClientRect.X); } set { ClientRect.X = value; } }
    public int Y { get { return Convert.ToInt32(ClientRect.Y); } set { ClientRect.Y = value; } }
    public FloatPoint Location { get { return ClientRect.Location; } set { ClientRect.Location = value; } }

    public int Width { get { return Convert.ToInt32(ClientSize.X); } set { ClientSize.X = value; } }
    public int Height { get { return Convert.ToInt32(ClientSize.Y); } set { ClientSize.Y = value; } }
    public FloatPoint ClientSize { get { return ClientRect.Size; } set { ClientRect.Size = value; } }

    public Padding Padding { get; set; }
    public Padding Borders { get; set; }

    public IGraphicsClientHandle ParentClient { get; set; }

    public ContentAlignment Alignment { get; set; } = ContentAlignment.TopLeft;

    virtual public void Paint(Graphics graphics)
    {
    }

  }
  public class LabelControl : GraphicsClient
  {
    public LabelControl()
    {
    }
    public override void Paint(Graphics graphics)
    {
      if (!string.IsNullOrEmpty(Text))
      {
        var font = this.Font == null ? ParentClient.Font : Font;
        graphics.DrawString(Text, font, Brushes.Black, ClientRect, StringFormat.GenericTypographic);
      }
    }
  }
	abstract public class GraphicsUiManager
	{
    public IGraphicsClientHandle Parent { get; set; }

    protected GraphicsTableLayout LayoutManager { get; set; } = new GraphicsTableLayout();

    public List<IGraphicsClient> Children { get; set; } = new List<IGraphicsClient>();

    virtual public void Paint(Graphics graphics)
    {
      foreach (var x in Children)
      {
        using (var r = new Region(x.ClientRect))
        {
          graphics.Clip = r;
          x.Paint(graphics);
          graphics.ResetClip();
        }
      }
    }
    abstract public void Initialize();
	}

}


