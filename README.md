# modest-smf-vstnet

MIDI-parser+VstNet+NAudio; A 'modest' Windows.Forms app testing sending MIDI (our midi parser) data to a single VST v2.4 instrument and effect (migrated out of the old gen.snd project-space)


# Implementation Notes

I've written this document in order to sync one's mentality to the source-code in this project which was written rather quickly.  The project was written to test my little MIDI parser (MidiReader) and also to see first-hand what limitations might exist within utilization of Jacobi's VstNet binaries (particularly `Jacobi.Vst.Core` and `Jacobi.Vst.Interop` libraries).

The single-most obvious (to me) issues that I have with the software as it stands:

- audio configuration panel needs to be configured after launching the application.
- Aside from the above bullet, all vst-plugins are loaded with erronious configuration settings by default.
    - we might be able to correct this issue by re-loading all the plugins when the audio-configuration changes.
    - For now, it appearsh that re-loading the 'active' instrument and effect corrects the problem, but this obviously not suitable as a base-library for elaborating upon.
    - The solution to the particular issue noted here would most-likely be to re-write the `PluginManager` where needed.

¡**YET EVEN MORESO OBVIOUS**!

It had become quite evident to me that there are going to be issues loading vst-plugins some time back.  For this we need a plugin-loader.  Writing a plugin-tester has not been fun, but I'm working on it.

The mechanics in place pertaining to plugin-management are not easily accessable to the user-interface.  If you are trying out a compiled version of this app, you will need to  manually edit the configuration file.

# Class Hierarchy

This is a quick overview of the implementation's class-hierarchy.

- ModestForm
    - VstContainer typeof(NAudioVstContainer)
        - VstPlayer (NAudioVST)
            - VstHostCommandStub (HostCommandStub)---The HostCommandStub class represents the part of the host that a plugin can call
        - PluginManager (VstPluginManager)
            - VstPlugin Collection via configuration settings file.
            - GeneratorModules (List<VstPlugin>)
            - EffectModules (List<VstPlugin>)
            - ActivePlugin (VstPlugin) --- as selected in the application's main UI.
            - MasterPluginInstrument --- only one instrument is supported mapped to the following effect.
            - MasterPluginEffects

## During Playback

What's worth pointing out: we're missing the ability to loop a particular bar running the clock.  Conversely, there are a few issues causing this, but thats why we're writing documentation and source-code.  There are actually two major issues that we are intent upon resolving, and a few more things that can use some attention such as providing a much more adequate audio-processing library implementation (NAudio is fine, just---not my first implementation as written here).  The biggest issue I had with the audio-implementation has been fixed, however I would like to see more audio-driver support---the big issue of (prior) consern caused a unavoidable crash every time the application was closed and has since been fixed.

### Timing Issues

1.   No (pre-process) buffering happens during our `VstHost.AudioProcess()`
2.   Tempo changes are not calculated into the MIDI injection calculated during the AudioProcess.
    -   Playback (timing) is dependant upon calculations of the Sample-Position.

**THINK DOUBLE-BUFFERING** where we have a particular amount in a pre-buffer in MIDI resolute time.


### MIDI Processing

As pointed out, we are missing calculations for **Tempo-Changes**.

Our MIDI parser is dependant upon a few key classes.

When the parser is parsing our data, it appends data to a main MidiMessage dictionary via a specific delegate From within the MIDI Parser.  An Event is triggered upon the occurance of each Midi Message, entailing a little bit of information about each message.  We have defined methods which take care of parsing, however it would.

- (**TimeConfiguration**) ModestForm.VstContainer.VstPlayer.Settings
- **SampleClock**
- **VstMidiEnumerator**
- **MidiMessageType** (Enum) Used in the MidiParser's (event found) delegate.
    - Undefined, MetaInf, MetaStr, System, SysCommon, Channel, Cc, NoteOn, NoteOff
- **MidiReader** 
    - (public) MidiReader.**Read** is the reader's starting-point.
    - (public) MidiReader.**ParseAll** and (private) **Parse** is going to assign a default parser which will throw all our track data into our `DictList<int,MidiMessage>`.
    - (private) MidiReader.**PARSER_MIDIDataList** is the name of our default parser as defined within the confines of our reader class.

### Play Is Clicked

- **ModestForm**.**Play**-Click
    - **NAudioVstContainer**.PlayerPlay
        - destroy, prepare, maximize volume
        - (NAudioVst) --- **VstPlayer.Play**()
            - Plugin Instrument and Effect(s) are turned On().
            - Volume is set from the NAudioVst.**Volume** setting.
            - (IWavePlayer) NAudioVst.XAudio.Play is called.
            - (bool) NAudioVst.**isRunning** is set to true.


Now from here, we're going to have to take a look at the audio implementation in NAudio.

More-so realistically, we need to be looking at **VstStream32** and **VstMidiEnumerator**.







