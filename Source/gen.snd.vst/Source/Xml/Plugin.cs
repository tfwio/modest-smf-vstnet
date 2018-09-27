/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Xml.Serialization;

using gen.snd.Vst.Module;

namespace gen.snd.Vst.Xml
{
	
	public class Plugin : PluginBase
	{
		[XmlAttribute]
		public string Title { get;set; }
		
		[XmlAttribute]
		public int PgmID { get;set; }
		
		[XmlAttribute]
		public string Path { get;set; }
		
		public Plugin() : base() { }
		public Plugin(VstPlugin source) : this()
		{
			if (!string.IsNullOrEmpty(source.Title)) Title = source.Title;
			else Title = "(Unknown Plugin)";
			PgmID = (source.ActiveProgram==null) ? 0 : source.ActiveProgram.ID;
			byte[] ck = source.PgmData;
			if (ck!=null) ProgramDump = ck;
		}
	}
	
}
