/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace gen.snd
{
	public class MidiChannelSet
	{
		static public List<int> Parse(string channels)
		{
			List<int> list = new List<int>();
			foreach (string value in channels.Split('|')) list.Add(int.Parse(value));
			return list;
		}
		
		static public implicit operator MidiChannelSet(string channels) { return new MidiChannelSet(){ Channels=Parse(channels) }; }
		static public implicit operator MidiChannelSet(int[] channels) { return new MidiChannelSet(){ Channels=new List<int>(channels) }; }
		static public implicit operator string(MidiChannelSet channels) { return channels.ChannelString; }
		
		// disable once ConvertToAutoProperty
		[XmlIgnore] public List<int> Channels {
			get { return channels; }
			set { channels = value; }
		} List<int> channels;
		
		IEnumerable<string> StringEnumerator { get { foreach (int i in channels) yield return i.ToString(); } }
		IEnumerable<int> ChannelEnumerator { get { foreach (int i in channels) yield return i; } }
		
		public string ChannelString {
			get { return string.Join("|",StringEnumerator.ToArray()); }
			set { channels = Parse(value); }
		}
		
	}
}
