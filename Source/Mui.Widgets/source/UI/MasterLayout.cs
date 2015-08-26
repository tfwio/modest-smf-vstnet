/* oio * 8/3/2015 * Time: 6:40 AM
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ren_mbqt_layout
{
  /// <summary>
  /// Description of MasterLayout.
  /// </summary>
  public partial class MasterLayout : UserControl, IGraphicsClientHandle
  {
    [Browsable(false),EditorBrowsable(EditorBrowsableState.Never)]
    public GraphicsUiManager GraphicsManager { get; set; }

    public MasterLayout()
    {
      InitializeComponent();

      this.DoubleBuffered = true;
      GraphicsManager = new MyUI();
      // error here.
      GraphicsManager.Initialize();
    }
    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.Clear(BackColor);
      GraphicsManager.Paint(e.Graphics);

    }
  }
  public class MyUI : GraphicsUiManager
  {
    [Browsable(false), NonSerialized, EditorBrowsable(EditorBrowsableState.Never)]
    LabelControl label1 = new LabelControl()
    {
      Name = "label1",
      Text = "mr label one",
    };
    [Browsable(false), NonSerialized, EditorBrowsable(EditorBrowsableState.Never)]
    LabelControl label2 = new LabelControl()
    {
      Name = "label2",
      Text = "mr label two",
    };
    [Browsable(false), NonSerialized, EditorBrowsable(EditorBrowsableState.Never)]
    LabelControl label3 = new LabelControl()
    {
      Name = "label3",
      Text = "mr label three",
    };


    public override void Initialize()
    {

      LayoutManager.Initialize(3, 3);

      LayoutManager[0].Height = 24;
      LayoutManager[1].Height = 48;
      LayoutManager[2].Height = 64;
      LayoutManager[0, 0].Width = 100;
      LayoutManager[0, 1].Width = 100;
      LayoutManager[0, 2].Width = 100;

      LayoutManager.InitializeLayout();

      // error here.
      this.Children.Add(label1);
      this.Children.Add(label2);
      this.Children.Add(label3);

      LayoutManager[0, 0].Content = label1;
      LayoutManager[1, 1].Content = label2;
      LayoutManager[2, 2].Content = label3;

    }
  }

}
