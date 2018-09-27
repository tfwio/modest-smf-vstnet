#region User/License
// oio * 7/31/2012 * 11:12 PM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Linq;

using gen.snd.Midi;
using gen.snd.Vst;
using gen.snd.Vst.Module;
using on.smfio;
using on.smfio.util;


namespace gen.snd.Vst
{
  // global::VstEvent struct (defined in cpp)
  // was conflicting with the abstract Jacobi.Vst.Core.VstEvent
  using NVstEvent=Jacobi.Vst.Core.VstEvent;
  
  static public class VstMidiEnumerator
  {
    class MidiChannelComparer : IEqualityComparer<MIDIMessage>
    {
      public bool Equals(MIDIMessage x, MIDIMessage y) { return x.ChannelBit == y.ChannelBit; }
      public int GetHashCode(MIDIMessage obj) { return base.GetHashCode(); }
    } readonly static MidiChannelComparer ChannelComparer = new MidiChannelComparer();

    static public IEnumerable<VstPlugin> GetInstruments(IMidiParserUI ui)
    {
      foreach (VstPlugin plugin in ui.VstContainer.PluginManager.VstInstruments)
        yield return plugin;
    }

    static public IEnumerable<VstPlugin> GetEffects(IMidiParserUI ui)
    {
      foreach (VstPlugin plugin in ui.VstContainer.PluginManager.VstEffects)
        yield return plugin;
    }

    // static public IEnumerable<KeyValuePair<int,VstPlugin>> GetInstrumentMapping(IMidiParserUI ui)
    // {
    //   foreach (VstPlugin plugin in GetInstruments(ui))
    //     for (int i=0; i<plugin.PluginCommandStub.GetNumberOfMidiInputChannels(); i++)
    //       yield return new KeyValuePair<int,VstPlugin>(i,plugin);
    // }
    
    static IEnumerable<int> EnumerateTrackIndex(this IMidiParser parser)
    {
      for (int i = 0; i < parser.SmfFileHandle.NumberOfTracks; i++)
        yield return i;
    }
    
    static public IEnumerable<KeyValuePair<int,string>> GetMidiTrackNameDictionary(this IMidiParser parser)
    {
      foreach (int i in parser.EnumerateTrackIndex())
      {
        string trackname = string.Format(Strings.Filter_MidiTrack, i + 1 );
        yield return new KeyValuePair<int,string>(i,trackname);
      }
    }
    static public IEnumerable<MIDIMessage> MidiTrackDistinctChannels(this IMidiParser parser, int trackid)
    {
      return parser.MidiDataList[trackid].Distinct(ChannelComparer);
    }
    
    #region VST
    
    static public void SendMidi2Plugin(VstPlugin vstMidiPlugin, IMidiParserUI ui, int blockSize)
    {
      if (vstMidiPlugin==null) return;
      
      var EventR = new List<NVstEvent>(
        GetSampleOffsetBlock(ui, vstMidiPlugin.IgnoreMidiProgramChange, blockSize)
       );
      
      NVstEvent[] range = EventR.ToArray();
      EventR.Clear();
      EventR = null;
      
      if (range!=null && range.Length > 0)
        vstMidiPlugin.PluginCommandStub.ProcessEvents(range);
      
    }
    
    static NVstEvent[] GetSampleOffsetBlock(IMidiParserUI ui, bool ignoreMidiPgm, int blockSize)
    {
      return VstEvent_Range(ui, ignoreMidiPgm, ui.VstContainer.VstPlayer.SampleOffset, blockSize).ToArray();
    }
    
    /// <summary>
    /// Process messages looking for Channel and Sysex messages.
    /// look at channel-message parsing for channel message types (or look into this).
    /// Sort the elements by timing.
    /// </summary>
    /// <param name="ui"></param>
    /// <param name="ignoreMidiPgm"></param>
    /// <param name="start"></param>
    /// <param name="len"></param>
    /// <returns>Filtered Events</returns>
    static public IEnumerable<NVstEvent> VstEvent_Range(IMidiParserUI ui, bool ignoreMidiPgm, double start, int len)
    {
      var list = new List<NVstEvent>();
      SampleClock c = new SampleClock(ui.VstContainer.VstPlayer.Settings);
      
      foreach (MIDIMessage item in MidiMessage_Range(ui, new Loop(){Begin=start,Length=len}))
      {
        if (item.MessageBit==0xC0 && ignoreMidiPgm) continue;
        
        if (item is ChannelMessage)
          list.Add(item.ToVstMidiEvent(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
        
        else if (item is SysExMessage)
          list.Add(item.ToVstMidiSysex(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
        
      }
      
      c = null;
      
      list.Sort(SortAlgo);
      
      foreach (NVstEvent vstevent in list) yield return vstevent;
    }

    /// <summary>
    /// Retrieve all MIDI events from all tracks as found in the MIDI parser.
    /// </summary>
    /// <param name="Parser"></param>
    /// <param name="loop"></param>
    /// <returns></returns>
    static IEnumerable<MIDIMessage> MidiMessage_Range(IMidiParserUI Parser, Loop loop)
    {
      SampleClock c = new SampleClock(Parser.VstContainer.VstPlayer.Settings);

      for (int trackId = 0; trackId < Parser.MidiParser.SmfFileHandle.NumberOfTracks; trackId++)
      {
        var elements = Parser.MidiParser.MidiDataList[trackId].Where( msg0 => msg0.IsContained( c, loop ) );
        foreach ( MIDIMessage item in elements ) yield return item;
      }

      c = null;
    }
    static int SortAlgo( NVstEvent a, NVstEvent b )
    {
      return a.DeltaFrames.CompareTo(b.DeltaFrames);
    }

    #endregion
    
    // /// <summary>
    // /// Process messages looking for Channel and Sysex messages.
    // /// look at channel-message parsing for channel message types (or look into this).
    // /// </summary>
    // /// <param name="ui">core</param>
    // /// <param name="start">Begin in samples</param>
    // /// <param name="len">Length from begin in samples</param>
    // /// <returns>Filtered Events</returns>
    // static NVstEvent[] FilterSampleRange(IMidiParserUI ui, double start, int len)
    // {
    //   if (HasParserErrors(ui)) return null;
    //   
    //   var list = new List<NVstEvent>();
    //   SampleClock c = new SampleClock(ui.VstContainer.VstPlayer.Settings);
    //   
    //   foreach (MIDIMessage item in MidiMessage_Range(ui, new Loop(){Begin=start,Length=len}))
    //   {
    //     if (item is ChannelMessage) list.Add(item.ToVstMidiEvent(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
    //     else if (item is SysExMessage) list.Add(item.ToVstMidiSysex(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
    //   }
    //   c = null;
    //   
    //   list.Sort(SortAlgo);
    //   return list.ToArray();
    // }
    // 
    // static bool HasParserErrors(IMidiParserUI ui)
    // {
    //   if (ui==null) return true;
    //   if (ui.MidiParser.MidiDataList.Count==0) return true;
    //   return false;
    // }
  }
}
