/* tfwxo * 1/19/2016 * 2:10 AM */
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using NAudio.Wave;
using NAudio.Wave.Asio;
// using nplayer.audio;

#if !LOG_TO_DEBUG
using NLog=System.Console;
#else
using NLog=System.Diagnostics.Debug;
#endif

//otherwise NMediaFoundationPlayer
using IXAudioProvider= NAudio.Wave.IWaveProvider; //nplayer.audio.MediaFoundationReader_v1_8_3;
//using M_IMF=NAudio.Wave.MediaFoundationReader

namespace on.atb
{
  public interface IPlayerConfig
  {
    float Amplitude { get; set; }
    float AmpInit { get; set; }
    string PlayerPath { get; set; }
    // on.FFmeta.TimeClass LastTimePosition { get; set; }
    EngineType AudioEngineType { get; set; }
    int AudioDriverIntID { get; set; }
    string AudioDriverStrID { get; set; }
    string AudioEngine_String { get; set; }
    FileInfo LocalFile { get; }
    string[] approved_extensions_audio { get; }
    string[] approved_extensions_video { get; }
    
    void SaveConfig();
  }
  interface IPlayerModel
  {
    void Click_PlayPause(object sender, EventArgs e);
    void Click_ResetReload(object sender, EventArgs e);
    void Click_Stop(object sender, EventArgs e);
    void Combo_FormatIndexChanged(object sender, EventArgs e);
    // on.FFmeta.TimeClass TimePosition { get; set; }
    
    IPlayerConfig ParentWin { get; set; }
    
    //    void LoadFile(FileInfo filePath);
  }
//  interface IPlayerModel<T>: IPlayerModel
//    where T:System.Windows.Forms.Form
//  {
//    T ParentWin { get; set; }
//  }
  [Flags]
  public enum EngineType
  {
    ASIO        = 1,
    DirectSound = 2,
    WaveOut     = 3,
    WASAPI      = 4,
    Default     = 2, // DirectSound is default
  }
  /// <summary>
  /// This was written for a synth, we'll see if i can get it working in this context...
  /// </summary>
  public class AudioDevice : IDisposable
  {
    // from NAudio.
    int MsToBytes(int ms)
    {
      int num = ms * (AudioProvider.WaveFormat.AverageBytesPerSecond / 1000);
      return num - num % AudioProvider.WaveFormat.BlockAlign;
    }
    
    public void Play()
    {
      if ((AudioEngine == null)) return;
      if (AudioEngine.PlaybackState==PlaybackState.Playing) {
        //        if (PlayerModel!=null) PlayerModel.Click_PlayPause();
        return;
      }
      NLog.WriteLine("<{0}> {1}; HasPlayer: {2}", this, "Play", AudioProvider==null);
      AudioEngine.Play();
    }
    /// <summary>
    /// <see cref="ExecuteStoppedActions"/>
    /// </summary>
    public void Stop()
    {
      NLog.WriteLine("Stopping...");
      
      // there may be no stopped actions
      if (AudioEngine == null) {
        ExecuteStoppedActions();
        return;
      }
      
      if (AudioEngine.PlaybackState==PlaybackState.Stopped) {
        NLog.WriteLine("Already stopped...");
        ExecuteStoppedActions();
        return;
      }
      AudioEngine.Stop();
    }
    
    // this was introduced with the idea of a media foundation interface implementation (player)
    // nothing has been implemented.
    public void InitializeMedia(FileInfo file)
    {
      this.Stop();
    }
    
    // // MainWindow.SelectProgram ()
    // public void InitializePgm(DsFormModel dsynth, bool forceInit=false)
    // {
    //   Model = dsynth;
    //   Synth.Pattern.Preset [Synth.Pattern.SelectedIndex] = Model.SelectedPreset;
    //   if (forceInit) Synth.Initialize (bpm, BufferSize);
    // }
    
    public AudioDevice()
    {
      //Synth = new
    }
    
    /// <summary>
    /// We need you to preconfigure a DeviceID before calling switch.
    /// Imagine an apply button where you have to select a latency before
    /// applying (type of thing).
    /// </summary>
    /// <param name="engineType"></param>
    /// <param name="latency"></param>
    /// <param name="deviceID"></param>
    /// <param name="deviceName"></param>
    public void SwitchAudioEngine(EngineType engineType, int latency=80, int deviceID=0, string deviceName=null)
    {
      NLog.WriteLine("<{0}> {1}", GetType().Name, "Audio engine reload/change/update.");
      
      if (AudioEngine!=null)
      {
        // is playing or paused
        if (AudioEngine.PlaybackState != PlaybackState.Stopped)
        {
          NLog.WriteLine("<{0}> {1}", GetType().Name, "Audio engine reload/change/update.");
          
          ActionsForStopped.Insert(0, ()=>Setup(engineType,latency,deviceID,deviceName)); // ensure this is the first thing that occurs.
          AudioEngine.Stop();
        }
        // is allready stopped
        else Setup(engineType, latency, deviceID, deviceName);
      }
      else Setup(engineType, latency, deviceID, deviceName);
    }
    
    internal void Setup(Func<IWaveProvider> provider)
    {
      Setup(provider, audioEngineType, DesiredLatency, EngineSubID, EngineStrID);
    }
    internal void Setup(Func<IWaveProvider> provider, EngineType engineType, int latency, int deviceID, string deviceName)
    {
      SetupWaveProvider = provider;
      Setup(engineType,latency,deviceID, deviceName);
    }
    
    
    
    void Setup(EngineType engineType, int latency=80, int deviceID=0, string deviceName=null)
    {
      NLog.WriteLine("AudioDevice.Setup: attempting to create audio device");
      if (AudioEngine!=null)
      {
        AudioEngine.Dispose();
        AudioEngine = null;
        // Why sleep?
        Thread.Sleep(500);
      }
      
      switch (engineType)
      {
        case EngineType.ASIO:
          AudioEngine = deviceName == null ? new AsioOut() : new AsioOut(deviceName);
          break;
        case EngineType.DirectSound:
          int j = 0;
          for (var i = NAudio.Wave.DirectSoundOut.Devices.GetEnumerator(); i.MoveNext(); )
          {
            var dc = i.Current;
            if (j==deviceID) { AudioEngine = new DirectSoundOut(dc.Guid,latency); break; }
            j++;
          }
          break;
        case EngineType.WaveOut:
          AudioEngine = new WaveOut(){DeviceNumber=deviceID,DesiredLatency=latency*2};
          break;
        case EngineType.WASAPI:
          NAudio.CoreAudioApi.MMDevice dev=null;
          using (var x = new NAudio.CoreAudioApi.MMDeviceEnumerator())
            for (int i = 0, maxCount = x.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.All).Count; i < maxCount; i++)
          {
            var y = x.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.All)[i];
            if (y.ID == deviceName) dev = y;
          }
          AudioEngine = dev == null ?
            new WasapiOut(NAudio.CoreAudioApi.AudioClientShareMode.Shared, DesiredLatency) :
            new WasapiOut(dev, NAudio.CoreAudioApi.AudioClientShareMode.Shared, false, DesiredLatency);
          break;
      }

      AudioEngineType = engineType;
      EngineSubID = deviceID;
      EngineStrID = deviceName;
      AudioEngine.PlaybackStopped += OnAudioDeviceStopped;
      
      if (AudioProvider == null) // TODO: IMPLEMENT PROVIDER HERE
      {
        this.AudioProvider = SetupWaveProvider();
        // Synth.EndOfProgram += OnStoppedHandler;
      }
      else
      {
        var ap = AudioProvider as IXAudioProvider;
        (AudioProvider as IDisposable).Dispose();
        ap = null;
        this.AudioProvider = SetupWaveProvider.Invoke();
      }
      var p = AudioProvider as IXAudioProvider;
      // if (p!=null) p.SetAmplitude(PlayerModel.ParentWin.Amplitude);
      AudioEngine.Init(AudioProvider);
      NLog.WriteLine ("ENGINE STARTED");
      Play();
    }
    
    void OnAudioDeviceStopped(object sender, StoppedEventArgs e)
    {
      NLog.WriteLine("ENGINE STOPPED");
      ExecuteStoppedActions();
    }
    void ExecuteStoppedActions()
    {
      if (ActionsForStopped.Count > 0)
      {
        for (int i = 0, ActionsForStoppedCount = ActionsForStopped.Count; i < ActionsForStoppedCount; i++)
        {
          var a = ActionsForStopped[i]; //AudioEngineType();
          NLog.WriteLine("::Executing {0}'th action", i);
          a.Invoke();
        }
        ActionsForStopped.Clear();
      }
    }
    /// this was used (prior) by a ds2 implementation to trigger audio-engine-stop
    /// when a program (and/or pattern) ended.
    void OnStoppedHandler(object sender, EventArgs e) {
      NLog.WriteLine("ENGINE STOP REQUEST");
    }

    #region IDisposable implementation
    public void Dispose()
    {
      NLog.WriteLine("<{0}> {1}", GetType(), "Dispose()");
      if (ActionsForStopped.Count > 0) throw new Exception("There are actions scheduled for when playback stops. (not good).");
      if (AudioEngine!=null)
      {
        NLog.WriteLine("BEGIN: WAITING TO DISPOSE ASIO-PLAYER...");
        while (AudioEngine.PlaybackState!=PlaybackState.Stopped)
        {
          try {
            NLog.WriteLine("Waiting for audio idle...");
            AudioEngine.Stop();
          } catch (Exception) {
            NLog.WriteLine("ERROR: Waiting for audio idle...");
            //throw;
          }
          Thread.Sleep(400);
        }
      }
      if (AudioEngine != null) AudioEngine.Dispose();
    }
    #endregion
    
    internal const double DefaultSampleRate = 44100D;
    
    /// IWavePlayer
    internal IWavePlayer AudioEngine { get; set; } // asio, directsound, etc...
    public IWaveProvider  AudioProvider { get; set; } // ms-media-foundation
    internal IPlayerModel PlayerModel { get; set; } // the ui
    
    //public DsFormModel Model { get; set; }
    
    
    // // This should not be here and/or never should be used in this fashion.
    // internal double BPM {
    //   get { return bpm; }
    //   set {
    //     bpm = value;
    //     if (Synth != null) Synth.Pattern.Clock.SetBpm(bpm);
    //     else Console.WriteLine ("ABORTED: BUFFER NOT SET.");
    //   }
    // } internal double bpm = 126D;
    
    /// <summary>
    /// - samplerate always 44100
    /// </summary>
    internal int DesiredSamplesPerBuffer { set { DesiredLatency = (int)Math.Round(value / DefaultSampleRate * 1000D); } }

    internal int DesiredLatency = 300;
    int BufferSize { get { return (AudioEngine is AsioOut) ? AudioEngine.Field32("nbSamples") : MsToBytes(DesiredLatency); } }

    public EngineType AudioEngineType {
      get { return audioEngineType; }
      set { audioEngineType = value; }
    } EngineType audioEngineType = EngineType.Default;
    // disable once ConvertToConstant.Local
    int EngineSubID = 0; // This is set to default --- we need to update this per user setting.
    string EngineStrID=null;
    
    internal System.Collections.Generic.List<Action> ActionsForStopped { get; set; } = new System.Collections.Generic.List<Action>();
    
    public Func<IWaveProvider> SetupWaveProvider { get; set; }

    internal static string GetNameForDevice(EngineType device) { return device == EngineType.Default ? "DirectSound": device.ToString(); }
    /// <summary>
    /// Deices should generally be verified by their name or GUID-like identity.
    /// 
    /// Currently we're using an int index, and migrating to a more adequate solution.
    /// </summary>
    internal static string GetNameForDevice_Selection(EngineType eng, int subType, string subTypeID)
    {
      switch (eng)
      {
        case EngineType.ASIO:
          return NAudio.Wave.AsioOut.GetDriverNames().ElementAt(subType);
        case EngineType.DirectSound:
          return NAudio.Wave.DirectSoundOut.Devices.ElementAt(subType).Description;
        case EngineType.WASAPI:
          NAudio.CoreAudioApi.MMDevice dev=null;
          using (var x = new NAudio.CoreAudioApi.MMDeviceEnumerator())
            for (int i = 0, maxCount = x.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.All).Count; i < maxCount; i++)
          {
            var y = x.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.All)[i];
            if (y.ID == subTypeID) dev = y;
          }
          return dev ==null ? null : dev.FriendlyName;
        case EngineType.WaveOut:
          return NAudio.Wave.WaveOut.GetCapabilities(subType).ProductName;
        default:
          return "[unknown]";
      }
    }
    
    internal static string GetStringIDForDevice_Selection(EngineType eng, int subType, string subTypeID)
    {
      switch (eng)
      {
          case EngineType.ASIO: return NAudio.Wave.AsioOut.GetDriverNames().ElementAt(subType);
          case EngineType.DirectSound: return NAudio.Wave.DirectSoundOut.Devices.ElementAt(subType).Guid.ToString();
        case EngineType.WASAPI:
          NAudio.CoreAudioApi.MMDevice dev=null;
          using (var x = new NAudio.CoreAudioApi.MMDeviceEnumerator())
            for (int i = 0, maxCount = x.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.All).Count; i < maxCount; i++)
          {
            var y = x.EnumerateAudioEndPoints(NAudio.CoreAudioApi.DataFlow.Render, NAudio.CoreAudioApi.DeviceState.All)[i];
            if (y.ID == subTypeID) dev = y;
          }
          return dev ==null ? null : dev.ID;
        case EngineType.WaveOut:
          return NAudio.Wave.WaveOut.GetCapabilities(subType).ProductGuid.ToString() ?? NAudio.Wave.WaveOut.GetCapabilities(subType).ProductName;
        default:
          return "[unknown]";
      }
    }
  }
  
}
namespace System
{
  using System.Reflection;
  
  // for access to ASIO buffer size --- pertains to NAudio 1.7.3
  static class ___x___
  {
    /// <summary>
    /// Uses reflection to get the field value from an object.
    /// </summary>
    /// <param name="instance">The instance object.</param>
    /// <param name="fieldName">The field's name which is to be fetched.</param>
    /// <returns>The field value from the object.</returns>
    //-  hacked from
    //-  zpt: http://stackoverflow.com/questions/3303126/how-to-get-the-value-of-private-field-in-c
    internal static int Field32(this object instance, string fieldName)
    {
      Type         type = instance.GetType();
      BindingFlags bindFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Static;
      FieldInfo    field = type.GetField(fieldName, bindFlags);
      return (int) field.GetValue(instance);
    }
  }
}