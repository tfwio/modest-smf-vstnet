/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Mui;
using Mui.Widgets;

namespace mui_smf
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MuiForm : MuiBase
  {
    public gen.snd.Midi.MidiReader MidiReader { get; set; }

    FloatRect myRect = new FloatRect(200, 100, 100, 100);

    TopMenuWidgetGroup Facto { get; set; }
    LeftMenuWidgetGroup WidgetMenu { get; set; }
    WidgetGroupMidiList MidiList { get; set; }
    void PreInitialize()
    {
      //
      // base settings
      // ---------------------------------------
      AppTimer.Interval = 90;

      MouseD = FloatPoint.Empty;
      MouseU = FloatPoint.Empty;
      MouseM = FloatPoint.Empty;

      FontIndex = new FontIndex();

      System.IO.FileInfo file;

      file = System.Reflection.Assembly.GetExecutingAssembly().GetAppFile("asset/adfx3.ttf");
      FontIndex.AddFamily(file, "adfx");

      file = System.Reflection.Assembly.GetExecutingAssembly().GetAppFile("asset/fontawesome-webfont.ttf");
      FontIndex.AddFamily(file, "awesome");
    }
    public MuiForm()
    {
      DoubleBuffered = true;
      InitializeComponent();

      PreInitialize();

      //
      // Widgets
      // ---------------------------------------

      Widgets = new Widget[] {
        Facto = new TopMenuWidgetGroup(),
        WidgetMenu = new LeftMenuWidgetGroup(),
        MidiList = new WidgetGroupMidiList(this)
        {
          Bounds = new FloatRect { X = 64, Y=64, Width=600, Height=400 },
          Font = new Font(Font.FontFamily, 10.0f, FontStyle.Regular)
        }
      };
      InitializeWidgets();

      WidgetMenu.BtnLoadMidi.MouseUp += BtnLoadMidi_ParentClick;
      MouseWheel += OnMouseWheel;
    }
    void InitializeWidgets()
    {
      foreach (var w in Widgets) w.Initialize(this);
      OnResize(null); // forces initial rendering
      OnSizeChanged(null); // forces initial rendering on the midi control

      AppTimer.Tick += AppTimer_Tick;
      AppTimer.Start();

    }

    private void BtnLoadMidi_ParentClick(object sender, MouseEventArgs e)
    {
      if (e.Button==MouseButtons.Left) MessageBox.Show("Hi!");
    }

    protected override void OnClientSizeChanged(EventArgs e)
    {
      base.OnClientSizeChanged(e);
      Invalidate();
    }

    protected override void OnPaint(PaintEventArgs e)
    {
      // TODO: find controls within clip-region for rendering.
      // Needs z-index-like implementation in MuiBase.

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
