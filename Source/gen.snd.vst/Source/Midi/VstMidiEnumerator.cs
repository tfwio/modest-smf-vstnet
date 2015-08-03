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
using System.Diagnostics;
using System.Linq;

using gen.snd.Midi;
using gen.snd.Vst;
using gen.snd.Vst.Module;
using gen.snd.Vst.Xml;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using NAudio.CoreAudioApi;
using NAudio.Wave;

namespace gen.snd.Vst
{
	static public class VstMidiEnumerator
	{
		readonly static MidiChannelComparer ChannelComparer = new MidiChannelComparer();

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

        static public IEnumerable<KeyValuePair<int,VstPlugin>> GetInstrumentMapping(IMidiParserUI ui)
		{
			foreach (VstPlugin plugin in GetInstruments(ui))
				for (int i=0; i<plugin.PluginCommandStub.GetNumberOfMidiInputChannels(); i++)
					yield return new KeyValuePair<int,VstPlugin>(i,plugin);
		}
		
		class MidiChannelComparer : IEqualityComparer<MidiMessage>
		{
			public bool Equals(MidiMessage x, MidiMessage y) { return x.ChannelBit == y.ChannelBit; }
			public int GetHashCode(MidiMessage obj) { return base.GetHashCode(); }
		}
		
		#region MIDI compliant
		
		static public IEnumerable<int> GetTrackIndex(IMidiParser parser)
		{
			for (int i = 0; i < parser.SmfFileHandle.NumberOfTracks; i++)
			{
				yield return i;
			}
		}
		static public IEnumerable<KeyValuePair<int,string>> GetMidiTrackNamesByIndex(IMidiParser parser)
		{
			foreach (int i in GetTrackIndex(parser))
			{
				string trackname = string.Format(Strings.Filter_MidiTrack, i + 1 );
				yield return new KeyValuePair<int,string>(i,trackname);
			}
		}
		static public IEnumerable<MidiMessage> MidiTrackDistinctChannels(int trackid, IMidiParser parser)
		{
			return parser.MidiDataList[trackid].Distinct(ChannelComparer);
		}
		
		#endregion
		
		#region MIDI compliant (hasmidiparser)
		
		// not used, just uncommented and left here.
		/// <summary>
		/// returns true if a parser is present
		/// </summary>
		/// <param name="ui"></param>
		/// <returns></returns>
		static bool HasMidiParser(IMidiParserUI ui)
		{
			if (
				ui.MidiParser != null &&
				ui.MidiParser.MidiDataList.Count > 0 &&
				ui.MidiParser.SmfFileHandle != null
			) return false;
			return true;
		}
		
		#endregion
		
		#region VST complient
		
		static public void SendMidi2Plugin(VstPlugin vstMidiPlugin, IMidiParserUI ui, int blockSize)
		{
			if (vstMidiPlugin==null) return ;
			List<VstEvent> EventR = new List<VstEvent>(GetSampleOffsetBlock(ui, vstMidiPlugin.IgnoreMidiProgramChange,blockSize));
			VstEvent[] range = EventR.ToArray();
			EventR.Clear();
			if (range!=null && range.Length > 0)
				vstMidiPlugin.PluginCommandStub.ProcessEvents(range);
		}
		
		static VstEvent[] GetSampleOffsetBlock(IMidiParserUI ui, bool ignoreMidiPgm, int blockSize)
		{
			return VstEvent_Range(ui, ignoreMidiPgm, ui.VstContainer.VstPlayer.SampleOffset, blockSize).ToArray();
		}
		
		/// <summary>
		/// Process messages looking for Channel and Sysex messages.
		/// look at channel-message parsing for channel message types (or look into this).
		/// Sort the elements by timing.
		/// </summary>
		/// <param name="Parser">core</param>
		/// <param name="start">Begin in samples</param>
		/// <param name="len">Length from begin in samples</param>
		/// <returns>Filtered Events</returns>
		static public IEnumerable<VstEvent> VstEvent_Range(IMidiParserUI ui, bool ignoreMidiPgm, double start, int len)
		{
			List<VstEvent> list = new List<VstEvent>();
			{
				SampleClock c = new SampleClock(ui.VstContainer.VstPlayer.Settings);
				foreach (MidiMessage item in MidiMessage_Range(ui, new Loop(){Begin=start,Length=len}))
				{
					if (item.MessageBit==0xC0 && ignoreMidiPgm) continue;
					if (item is MidiChannelMessage) list.Add(item.ToVstMidiEvent(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
					else if (item is MidiSysexMessage) list.Add(item.ToVstMidiSysex(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
				}
				c = null;
			} list.Sort(SortAlgo);
			foreach (VstEvent vstevent in list) yield return vstevent;
		}
		static IEnumerable<MidiMessage> MidiMessage_Range(IMidiParserUI Parser, Loop loop)
		{
			SampleClock c = new SampleClock(Parser.VstContainer.VstPlayer.Settings);
			for (int trackId = 0; trackId < Parser.MidiParser.SmfFileHandle.NumberOfTracks; trackId++)
			{
				var elements = Parser.MidiParser.MidiDataList[trackId].Where( msg0 => msg0.IsContained( c, loop ) );
				foreach ( MidiMessage item in elements ) yield return item;
			}
			c = null;
		}
		static int SortAlgo( VstEvent a, VstEvent b )
		{
			return a.DeltaFrames.CompareTo(b.DeltaFrames);
		}

		#endregion
		
		#region Midi Enumerations
		
		/// <summary>
		/// Seems not to be used.
		/// </summary>
		/// <param name="ui"></param>
		/// <param name="loop"></param>
		/// <returns></returns>
		static VstEvent[] EnumerateMidiData(IMidiParserUI ui,Loop loop)
		{
			return FilterSampleRange(ui,loop.Begin,loop.Length.FloorMinimum(0).ToInt32());
		}
		
		/// <summary>
		/// Process messages looking for Channel and Sysex messages.
		/// look at channel-message parsing for channel message types (or look into this).
		/// </summary>
		/// <param name="Parser">core</param>
		/// <param name="start">Begin in samples</param>
		/// <param name="len">Length from begin in samples</param>
		/// <returns>Filtered Events</returns>
		static VstEvent[] FilterSampleRange(IMidiParserUI ui, double start, int len)
		{
			if (HasParserErrors(ui)) return null;
			List<VstEvent> list = new List<VstEvent>();
			{
				SampleClock c = new SampleClock(ui.VstContainer.VstPlayer.Settings);
				
				foreach (MidiMessage item in MidiMessage_Range(ui, new Loop(){Begin=start,Length=len}))
				{
					if (item is MidiChannelMessage) list.Add(item.ToVstMidiEvent(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
					else if (item is MidiSysexMessage) list.Add(item.ToVstMidiSysex(Convert.ToInt32(ui.VstContainer.VstPlayer.SampleOffset),ui.VstContainer.VstPlayer.Settings,c));
				}
				c = null;
			}
			list.Sort(SortAlgo);
			return list.ToArray();
		}
		
		static bool HasParserErrors(IMidiParserUI ui)
		{
			if (ui==null) return true;
			if (ui.MidiParser.MidiDataList.Count==0) return true;
			return false;
		}
		
		#endregion
	}
}
