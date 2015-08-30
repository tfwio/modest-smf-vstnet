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
  public partial class MuiForm : MuiBase, IMui
  {
    public FontIndex FontIndex { get; protected set; }
    
    #region Timer
    
    Timer appTimer = new Timer() { Interval = 100 };
    public event EventHandler Tick {
      add { appTimer.Tick += value; }
      remove { appTimer.Tick -= value; }
    }
    void AppTimer_Tick(object sender, EventArgs e)
    {
      this.Incrementor.IncrementY();
      // none of the widgets implement this guy.
      foreach (var widget in Widgets)
        widget.Increment();
      // draw
      Invalidate();
    }
    
    #endregion
    
    FloatRect myRect = new FloatRect(200, 100, 100, 100);
    IncrementUtil Incrementor = new IncrementUtil();
    
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
      
      Facto = new DefaultWidgetGroup();
      WidgetMenu = new MenuWidgetGroup();
      
      MouseWheel += OnMouseWheel;
      
      appTimer.Tick += AppTimer_Tick;
      appTimer.Start();
      
      var TopGridLoc = new FloatRect(10, 10, 100, 28);
      var DPadding = new Padding(4);
      
      float i = TopGridLoc.X + TopGridLoc.Width;
      var sliderrect = new FloatRect(460, 10, 200, 24);
      
      Widgets = new Widget[] {
        Facto,
        WidgetMenu,
        new ClockWidget(this) {
          Bounds = new FloatRect(48, 150, 200, 24),
          Padding = DPadding,
        },
      };
      Facto.Initialize(this);
      WidgetMenu.Initialize(this);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      if (MouseM == null) return;
      e.Graphics.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
      e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
      
      e.Graphics.Clear(Painter.DictColour[ColourClass.Dark50]);
      
//      WidgetMenu.Paint(e);
//      Facto.Paint(e);
      foreach (var widget in Widgets) widget.Paint(e);
    }
  }
  
}
