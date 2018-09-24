/*
 * User: xo
 * Date: 8/5/2017
 * Time: 4:22 PM
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using on.atb;

namespace on.FFmeta.nplayer
{
  /// Description of Configurator.
  public partial class Configurator : Form
  {
    
    public IPlayerConfig ParentConfig { get; set; }
    
    public Configurator()
    {
      InitializeComponent();
      DeviceDriver_Types.Remove("Default");
    }
    
    protected override void OnLoad(EventArgs e)
    {
      base.OnLoad(e);
      
      InitializeBind();
      UpdateSelectedDevice();
      
    }
    
    void UpdateSelectedDevice()
    {
      this.blCurrent.Text = string.Format(
        "<b>device</b>: {0}<br /><b>selected</b>: {1}",
        AudioDevice.GetNameForDevice(ParentConfig.AudioEngineType),
        AudioDevice.GetNameForDevice_Selection(ParentConfig.AudioEngineType,ParentConfig.AudioDriverIntID, ParentConfig.AudioDriverStrID)
       );
    }
    
    void InitializeBind()
    {
      cbDriverType.DataSource = DeviceDriver_Types;
      cbDriverType.Text = ParentConfig.AudioEngine_String;
      
      UpdateDriverNamesList();
      
      cbDriverType.SelectedIndex = DeviceDriver_Types.IndexOf(ParentConfig.AudioEngine_String);
      cbDriver_SubType.SelectedIndex = ParentConfig.AudioDriverIntID;
      cbDriverType.SelectedIndexChanged += cbDriverDevice_SelectedIndexChanged;
    }

    void cbDriverDevice_SelectedIndexChanged(object sender, EventArgs e)
    {
      UpdateDriverNamesList();
    }
    
    void Click_Button_Save(object sender, EventArgs e)
    {
      ParentConfig.AudioEngine_String = cbDriverType.Text;
      ParentConfig.AudioDriverIntID = cbDriver_SubType.SelectedIndex;
      ParentConfig.AudioDriverStrID = AudioDevice.GetStringIDForDevice_Selection(
        ParentConfig.AudioEngineType,
        ParentConfig.AudioDriverIntID,
        ParentConfig.AudioDriverStrID
       );
      ParentConfig.SaveConfig();
      UpdateSelectedDevice();
    }
    
    void UpdateDriverNamesList()
    {
      var eng = (EngineType)Enum.Parse(typeof(EngineType),cbDriverType.Text);
      
      cbDriver_SubType.DataSource = null;
      cbDriver_SubType.DisplayMember = null;
      DeviceDriver_SubTypes.Clear();
      cbDriver_SubType.Enabled = false;
      
      switch (eng)
      {
        case EngineType.ASIO:
          DeviceDriver_SubTypes.Clear();
          DeviceDriver_SubTypes.AddRange(NAudio.Wave.AsioOut.GetDriverNames());
          cbDriver_SubType.DataSource = DeviceDriver_SubTypes;
          cbDriver_SubType.Enabled = true;
          break;
        case EngineType.DirectSound:
          DeviceDriver_SubTypes.AddRange(NAudio.Wave.DirectSoundOut.Devices);
          cbDriver_SubType.DataSource = DeviceDriver_SubTypes;
          //var dev = DeviceDriver_SubTypes[0] as NAudio.Wave.DirectSoundDeviceInfo;
          //dev.Description
          cbDriver_SubType.DisplayMember = "Description";
          cbDriver_SubType.Enabled = true;
          break;
        case EngineType.WASAPI:
          DeviceDriver_SubTypes.Clear();
          using (var enu = new NAudio.CoreAudioApi.MMDeviceEnumerator())
            for (int i = 0, maxCount = enu.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.Active).Count; i < maxCount; i++)
          {
            var dev = enu.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.Active)[i];
            var ndev= new {DevFriendlyName=dev.DeviceFriendlyName,FriendlyName=dev.FriendlyName,id=dev.ID,idx=dev};
            DeviceDriver_SubTypes.Add(ndev);
          }
          cbDriver_SubType.DataSource = DeviceDriver_SubTypes;
          cbDriver_SubType.DisplayMember = "FriendlyName";
          cbDriver_SubType.Enabled = true;
          break;
        case EngineType.WaveOut:
          DeviceDriver_SubTypes.Clear();
          for (int i = 0, DeviceDriverNamesCount = NAudio.Wave.WaveOut.DeviceCount; i < DeviceDriverNamesCount; i++) {
            DeviceDriver_SubTypes.Add(NAudio.Wave.WaveOut.GetCapabilities(i));
          }
          cbDriver_SubType.DataSource = DeviceDriver_SubTypes;
          cbDriver_SubType.DisplayMember = "ProductName";
          cbDriver_SubType.Enabled = true;
          break;
      }
      // 
      if (ParentConfig.AudioEngineType==eng)
      {
        cbDriver_SubType.SelectedIndex = ParentConfig.AudioDriverIntID;
      }
    }
    
    List<string> DeviceDriver_Types { get; set; } = new List<string>(Enum.GetNames(typeof(EngineType)));
    List<object> DeviceDriver_SubTypes { get; set; } = new List<object>();
    
  }
}
