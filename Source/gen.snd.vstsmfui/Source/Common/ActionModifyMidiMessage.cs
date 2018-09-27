/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Linq;
using modest100.Midi;
using on.smfio;

namespace modest100.Forms
{

	public class ActionModifyMidiMessage : MidiEditorAction
	{
		int FindOld(IMidiParser parser)
		{
			return parser.MidiDataList[track].FindIndex(
				m =>
				m.Pulse == oldDelta &&
				m.Message == oldMessage &&
				m.Data==oldData
			);
		}
		int FindNew(IMidiParser parser)
		{
			return parser.MidiDataList[track].FindIndex(
				m =>
				m.Pulse == newDelta &&
				m.Message == newMessage &&
				m.Data==newData
			);
		}
		
		public void Modify(MIDIMessage input, IMidiParser parser, long delta, int msg, params byte[] data)
		{
			//this.track = track;
			
			this.oldDelta = input.Pulse;
			this.oldMessage = input.Message;
			this.oldData = input.Data;
			//
			this.newDelta = delta;
			this.newMessage = msg;
			this.newData = data;
		}
		public void Modify(IMidiParser parser)
		{
			int msgindex = FindOld(parser);
			if (msgindex!=-1) SetNewData(parser,msgindex);
		}
		public void Revert(IMidiParser parser)
		{
			int msgindex = FindNew(parser);
			if (msgindex!=-1) SetOldData(parser,msgindex);
		}
		
		void SetNewData(IMidiParser parser, int index)
		{
			parser.MidiDataList[track][index].Pulse = newDelta;
			parser.MidiDataList[track][index].Message = newMessage;
			parser.MidiDataList[track][index].Data = newData;
		}
		void SetOldData(IMidiParser parser, int index)
		{
			parser.MidiDataList[track][index].Pulse = oldDelta;
			parser.MidiDataList[track][index].Message = oldMessage;
			parser.MidiDataList[track][index].Data = oldData;
		}
		
	}
}
