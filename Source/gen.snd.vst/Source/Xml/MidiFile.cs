/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

using gen.snd.Vst.Xml;

namespace gen.snd.Vst.Xml
{
	public class MidiFile
	{
		[XmlAttribute] public string Title { get;set; }
		[XmlAttribute] public string Path { get;set; }
		
		[XmlElement("gen")]
		public List<ChannelAssignment> Gen {
			get { return gen; }
			set { gen = value; }
		} List<ChannelAssignment> gen;
		
		public MidiFile()
		{
		}
	}
}
