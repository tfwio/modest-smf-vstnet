/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace ren_mbqt_layout
{
  /// <summary>
  /// Description of MainForm.
  /// </summary>
  public partial class MainForm : Form
  {
    static readonly Dictionary<string, Color> AppColor = new Dictionary<string, Color>{
      {"dark",Color.FromArgb(255,50,50,50) },
      {"black",Color.Black },
      {"white",Color.White },
      {"main",Color.Red },
    };
    static readonly Dictionary<string, Brush> AppBrushes = new Dictionary<string, Brush>{
      {"black",Brushes.Black },
      {"white",Brushes.White },
      {"main",Brushes.Red },
    };
    static readonly Dictionary<string, Pen> Pens = new Dictionary<string, Pen>{
      {"main",new Pen(AppBrushes["main"],24){StartCap=LineCap.Round,EndCap=LineCap.Round}},
      {"black",new Pen(AppBrushes["black"],24){StartCap=LineCap.Round,EndCap=LineCap.Round}},
      {"white",new Pen(AppBrushes["white"],24){StartCap=LineCap.Round,EndCap=LineCap.Round}},
    };

    protected bool HasControlKey = false;
    
    public FloatPoint MouseD { get; set; }
    public FloatPoint MouseU { get; set; }
    public FloatPoint MouseN { get; set; }
    public FloatPoint MouseM { get; set; }
    
    public int OffsetX { get;set; }
    public int OffsetY { get;set; }
    
    
    FloatPoint ClientMouse { get { return new FloatPoint(PointToClient(MousePosition)) - new FloatPoint(Padding.Left,Padding.Top); } }
    
    protected void OnMouseWheel(object sender, MouseEventArgs e)
    {
      if (HasControlKey) {
        OffsetX = (e.Delta > 0) ? OffsetX + 1 : OffsetX - 1;
        if (OffsetX <= 0) OffsetX = 0;
      } else {
        this.OffsetY = (e.Delta > 0) ? OffsetY + 1 : OffsetY - 1;
        if (OffsetY > 127) this.OffsetY = 127;
        else if (this.OffsetY <= 0) this.OffsetY = 0;
      }
    }
    protected override void OnMouseDown(MouseEventArgs e)
    {
      base.OnMouseDown(e);
      MouseD = MousePosition;
    }
    protected override void OnMouseUp(MouseEventArgs e)
    {
      base.OnMouseUp(e);
      MouseN = MousePosition;
      MouseU = MousePosition;
      MouseN = null;
      MouseD = null;
      
    }

    // we should have an undo-redo state-machine
    // even though were not using it yet.
    public Stack<object> StateMachine;
    
    protected override void OnMouseMove(MouseEventArgs e)
    {
      base.OnMouseMove(e);
      MouseM = MousePosition;
      Invalidate();
    }

    Timer appTimer = new Timer()
    {
      Interval = 100
    };
    public MainForm()
    {
      InitializeComponent();
      this.DoubleBuffered = true;
      this.MouseWheel += OnMouseWheel;
      this.appTimer.Tick += AppTimer_Tick;
      appTimer.Start();
    }

    readonly FloatPoint POrigin = new FloatPoint(30,30);
    FloatPoint PCoords = new FloatPoint(0,0);
    readonly float PFactor = 30F;
    readonly float PIncrement = 0.05F;
    float PValue = 0;
    readonly float PMax = 1F;
    readonly float PMin = 0F;

    void IncrementY()
    {
      PValue += PIncrement;
      PCoords.Y = PValue * PFactor;
      if (PValue > PMax) PValue = PMin;
      Invalidate();
    }

    private void AppTimer_Tick(object sender, EventArgs e)
    {
      IncrementY();
    }

    FloatRect myRect = new FloatRect(200,100,100,100);
    FloatPoint PClock = new FloatPoint(40,150);


    protected override void OnPaint(PaintEventArgs e)
    {
      e.Graphics.Clear(AppColor["dark"]);

      e.Graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
      e.Graphics.SmoothingMode = SmoothingMode.HighQuality;

      PCoords.X = 30;
      e.Graphics.DrawString("ASOME", this.Font, AppBrushes["black"], PCoords);

      PCoords.X += 100;
      e.Graphics.DrawString("BSOME", this.Font, AppBrushes["black"], PCoords);

      PCoords.X += 100;
      e.Graphics.DrawString("3SOME", this.Font, AppBrushes["black"], PCoords);

      PCoords.X += 100;
      e.Graphics.DrawString("XSOME", this.Font, AppBrushes["black"], PCoords);

      PCoords.X += 100;
      e.Graphics.DrawString("Some Text", this.Font, AppBrushes["black"], PCoords);

      PCoords.X += 100;
      e.Graphics.DrawString("Some Text", this.Font, AppBrushes["black"], PCoords);

      PCoords.X += 100;
      e.Graphics.DrawString("Some Text", this.Font, AppBrushes["black"], PCoords);

      e.Graphics.DrawString(
        DateTime.Now.ToString("hh:mm:ss.fff tt"),
        this.Font, AppBrushes["black"],
        PClock
      );

      e.Graphics.DrawPie(Pens["main"], myRect, 45, 180);

    }
  }
}
