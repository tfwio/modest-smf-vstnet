/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
	
	public class AppService_MidiFile : MuiAppService<MuiForm>
	{
    readonly OpenFileDialog MidiFileDialog = new OpenFileDialog() { Filter="Midi Fliles|*.mid;*.midi" };
    
    public override void Register()
    {
      Client.GetWidget<WidgetGroupLeftMenu>("WidgetMenu").BtnLoadMidi.MouseUp += BtnLoadMidi_ParentClick;
      Client.GotMidiFile                    += MuiForm_GotMidiFile;
    }
    public override void Unregister()
    {
      base.Unregister();
      Client.GetWidget<WidgetGroupLeftMenu>("WidgetMenu").BtnLoadMidi.MouseUp -= BtnLoadMidi_ParentClick;
      Client.GotMidiFile                    -= MuiForm_GotMidiFile;
    }

    void MuiForm_GotMidiFile(object sender, EventArgs e)
    {
      var file=new System.IO.FileInfo(MidiFileDialog.FileName);
      Client.Text = string.Format("SMF-UI - [{0}]",file.Name);
      //MidiEnumerator mi;
    }
    void BtnLoadMidi_ParentClick(object sender, MouseEventArgs e)
    {
      if (e.Button==MouseButtons.Left)
      {
        if (MidiFileDialog.ShowDialog() == DialogResult.OK)
        {
          Client.MidiReader = new gen.snd.Midi.MidiReader(MidiFileDialog.FileName);
          Client.OnGotMidiFile(EventArgs.Empty);
        }
      }
    }
	}
}








