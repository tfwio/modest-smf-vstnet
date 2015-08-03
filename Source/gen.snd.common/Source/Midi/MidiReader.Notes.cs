/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace gen.snd.Midi
{
	partial class MidiReader : IMidiParser_Notes
	{
		byte this[int track, int index] {
			get { return SmfFileHandle[track, index]; }
		}
		
		public List<MidiData> Notes {
			get { return notes; } set { notes = value; }
		} List<MidiData> notes = new List<MidiData>();
		
		/// <inheritdoc/>
		public void CheckNote(MidiMsgType type, ulong ppq, byte ch, int offset, byte b, bool rs)
		{
			byte n = 0, v = 0;
			switch (type) {
				case MidiMsgType.CC:
					n = this[SelectedTrackNumber, offset + (rs ? 0 : 1)];
					v = this[SelectedTrackNumber, offset + (rs ? 0 : 1) + 1];
					Debug.Print("Control Change : {0}{1} {2}", SmfStringFormatter.GetKeySharp(n), SmfStringFormatter.GetOctave(n), v);
					break;
				case MidiMsgType.NoteOn:
					n = this[SelectedTrackNumber, offset + (rs ? 0 : 1)];
					v = this[SelectedTrackNumber, offset + (rs ? 0 : 1) + 1];
//					Debug.Print("Note On : {0}{1} {2}",SmfStringFormatter.GetKeySharp(n),SmfStringFormatter.GetOctave(n),v);
						if (v == 0)
							CloseNote(ppq, n, v);
						else
							Notes.Add(new MidiNote(ch, n, ppq, v));

					break;
				case MidiMsgType.NoteOff:
					n = this[SelectedTrackNumber, offset + (rs ? 0 : 1)];
					v = this[SelectedTrackNumber, offset + (rs ? 0 : 1) + 1];
//					Debug.Print("Note Off: {0}{1} {2}",SmfStringFormatter.GetKeySharp(n),SmfStringFormatter.GetOctave(n),v);
					CloseNote(ppq, n, v);
					break;
			}
		}
		
		/// <inheritdoc/>
		public void CloseNote(ulong ppq, byte k, short v)
		{
			MidiNote note = GetNote(k, -1) as MidiNote;
			if (note == null) {
				Console.WriteLine("note wasn't found: {0} {1}:{2}", MBT.GetString(ppq, MidiReader.FileDivision), k, v);
				return;
			}
			note.Len = Convert.ToInt32(ppq - note.Start);
			note.V2 = v;
		}
		
		/// <inheritdoc/>
		public MidiData GetNote(byte k, short v)
		{
			return Notes.Where((N => CheckNote(N, k, -1))).FirstOrDefault();
		}
		
		bool CheckNote(MidiData d, byte k, short v)
		{
			if (d is MidiNote)
				// check for key and velocity?
				return (d as MidiNote).K == k && (d as MidiNote).V2 == v;
			return false;
		}
	}
}
