/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;

namespace ren_mbqt_layout
{
  
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MuiForm : MuiBase
  {
    FloatRect myRect = new FloatRect(200, 100, 100, 100);
    
    WidgetGroup Facto = null;
    WidgetGroup WidgetMenu = null;
    
    public MuiForm()
    {
      FontIndex = new FontIndex();
      
      System.IO.FileInfo file;
      
      file = System.Reflection.Assembly.GetExecutingAssembly().GetAppFile("asset/adfx3.ttf");
      FontIndex.AddFamily(file, "adfx");
      
      file = System.Reflection.Assembly.GetExecutingAssembly().GetAppFile("asset/fontawesome-webfont.ttf");
      FontIndex.AddFamily(file, "awesome");
      DoubleBuffered = true;
      
      MouseD = FloatPoint.Empty;
      MouseU = FloatPoint.Empty;
      MouseM = FloatPoint.Empty;
      
      InitializeComponent();
      
      Facto = new TopMenuWidgetGroup(){Parent=this};
      WidgetMenu = new LeftMenuWidgetGroup(){Parent=this};
      
      MouseWheel += OnMouseWheel;
      
      AppTimer.Tick += AppTimer_Tick;
      AppTimer.Start();
      
      var TopGridLoc = new FloatRect(10, 10, 100, 28);
      var DPadding = new Padding(4);
      
      float i = TopGridLoc.X + TopGridLoc.Width;
      var sliderrect = new FloatRect(460, 10, 200, 24);
      
      Widgets = new Widget[] {
        Facto,
        WidgetMenu,
      };
      Facto.Initialize(this);
      WidgetMenu.Initialize(this);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      if (MouseM == null) return;
      e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      e.Graphics.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
      e.Graphics.PixelOffsetMode = System.Drawing.Drawing2D.PixelOffsetMode.HighQuality;
      
      e.Graphics.Clear(Painter.DictColour[ColourClass.Dark40]);
//      using (var p = new Pen(Color.FromArgb(255, 0x99,0xff,0x00), 6))
//        e.Graphics.DrawRectangle(p, new FloatRect(0,0,Width,Height));
      
//      WidgetMenu.Paint(e);
//      Facto.Paint(e);
        foreach (var widget in Widgets) widget.Paint(e);
    }
  }
  
}
