/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.IO;
using System.Xml;
using System.Xml.Serialization;

namespace gen.snd.Vst.Xml
{
	public class MidiSmfFile : SerializableClass<MidiSmfFile>
	{
		const string DefaultFileFilter = "modest100 Runtime|*.vstmid";
		protected override string FileFilter {
			get { return DefaultFileFilter; }
		}
		[XmlElement("settings")]
		public MidiSmfFileSettings Settings { get;set; }
		
		public MidiSmfFile()
		{
			base.fileFilter = DefaultFileFilter;
		}
	}
}
