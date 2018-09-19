modest-smf-vstnet
=================

MIDI-parser + VstNet + NAudio  
A 'modest' Windows.Forms app testing sending MIDI data to a single VST v2.4 instrument and effect.

Idea: test the MIDI parser (now 'smfio') using Jacobi's VstNet binaries (particularly `Jacobi.Vst.Core` and `Jacobi.Vst.Interop` libraries) using NAudio for audio-output.  

https://github.com/tfwio/modest-smf-vstnet

OBVIOUS QUIRKS
--------------

- Win32/x86 VST are supported.
- No editing, no piano-view.. we just load up a midi file and play it via the
  VST chain selected (or active).
- Audio Configuration Panel needs to be configured each time the app is launched.  
  *vst-plugins may be loaded with erroneous configuration settings by default.*
- MIDI->SetTempo is not yet processed
- Most VST Plugins seem to have no problem loading, however a 'plugin-loader'
  does not seem possible.  Each Plugin should be loaded manually via the UI
  or by editing the XML configuration file next to the program.  
  Attempts at writing a Plugin-Loader have thus far failed.
- NAudio implementation here (customized: v1.7.3) is pretty old.

COMPILING
---------------

requirements:

- python3 for PREBUILD step(s)
- DotNet Framework v4.0 (e.g. Visual Studio Express)
- GIT

clone this repo and enter into that directory
```
git clone https://github.com/tfwio/modest-smf-vstnet
pushd modest-smf-vstnet
```
clone smfio from the `./Source` sub-directory.
```
pushd Source
git clone https://github.com/tfwio/modest-smf-vstnet
popd
```

CLASS HIERARCHY
---------------

This is a quick overview of the implementation's class-hierarchy.

SIGNIFICANT CLASSES: NAudioVST, VstStream32, IOModule

- ModestForm
  - VstContainer typeof(NAudioVstContainer)
    - VstPlayer (NAudioVST)
      - VstHostCommandStub (HostCommandStub); *part of the host that plugin calls on*
    - PluginManager (VstPluginManager)
      - VstPlugin Collection via configuration settings file.
      - GeneratorModules (List<VstPlugin>)
      - EffectModules (List<VstPlugin>)
      - ActivePlugin (VstPlugin) — as selected in the application's main UI.
      - MasterPluginInstrument — only one instrument is supported mapped to the following effect.
      - MasterPluginEffects

MIDI PROCESSING

- source/gen.snd.vst/Midi/VstMidiEnumerator.cs

When the parser is parsing our data, it appends data to a main VstEvent dictionary via a specific delegate From within the MIDI Parser.  An Event is triggered upon the occurrence of each Midi Message, entailing a little bit of information about each message.  We have defined methods which take care of parsing, however it would.

- (**TimeConfiguration**) ModestForm.VstContainer.VstPlayer.Settings
- **SampleClock**
- **VstMidiEnumerator**
- **MidiMessageType** (Enum) Used in the MidiParser's (event found) delegate.
    - Undefined, MetaInf, MetaStr, System, SysCommon, Channel, Cc, NoteOn, NoteOff
- **MidiReader** 
    - (public) MidiReader.**Read** is the reader's starting-point.
    - (public) MidiReader.**ParseAll** and (private) **Parse** is going to assign a default parser which will throw all our track data into our `DictList<int,MidiMessage>`.
    - (private) MidiReader.**PARSER_MIDIDataList** is the name of our default parser as defined within the confines of our reader class.

PLAY IS CLICKED

- **ModestForm**.**Play**-Click
    - **NAudioVstContainer**.PlayerPlay
        - destroy, prepare, maximize volume
        - (NAudioVst) --- **VstPlayer.Play**()
            - Plugin Instrument and Effect(s) are turned On().
            - Volume is set from the NAudioVst.**Volume** setting.
            - (IWavePlayer) NAudioVst.XAudio.Play is called.
            - (bool) NAudioVst.**isRunning** is set to true.
