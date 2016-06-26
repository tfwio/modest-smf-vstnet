/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Drawing.Text;
using System.IO;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;

namespace mui_smf
{
  /// <summary>Description of MainForm.</summary>
  public partial class MuiForm : MuiBase
  {
    ContextMenu TrackSelectionContextMenu = new ContextMenu();

    public gen.snd.Midi.MidiReader MidiReader { get; set; }

    protected internal WidgetGroupTopMenu Facto { get; set; }
    protected internal WidgetGroupLeftMenu WidgetMenu { get; set; }
    protected internal WidgetGroupMidiList MidiList { get; set; }
    
    public MuiForm()
    {
      DoubleBuffered = true;
      InitializeComponent();
      InitializeWidgets(new AppService_MidiFile());
      
      OnResize(null); // forces initial rendering
      OnSizeChanged(null); // forces initial rendering on the midi control
    }
    
    protected override void Design()
    {
      Widgets = new Widget[]
      {
        Facto = new WidgetGroupTopMenu(),
        WidgetMenu = new WidgetGroupLeftMenu(),
        MidiList = new WidgetGroupMidiList(this)
        {
          Bounds = new FloatRect { X = 64, Y=64, Width=600, Height=400 },
          Font = new Font(Font.FontFamily, 10.0f, FontStyle.Regular)
        }
      };
    }

    public event EventHandler GotMidiFile;
    protected internal virtual void OnGotMidiFile(EventArgs e)
    {
      var handler = GotMidiFile;
      if (handler != null)
        handler(this, e);
    }
    
    protected override void OnPaint(PaintEventArgs e)
    {
      // TODO: find controls within clip-region for rendering.
      // Needs z-index-like implementation in MuiBase.
      if (MouseM == null) return;
      var bgColor = Focused ? Painter.DictColour[ColourClass.Dark40] : SystemColors.WindowFrame;
        e.Graphics.Clear(bgColor);
      foreach (var widget in Widgets) widget.Paint(e);
    }
  }
  
}
