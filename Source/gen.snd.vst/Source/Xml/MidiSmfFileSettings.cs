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
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using gen.snd;
using gen.snd.Vst;
using gen.snd.Vst.Module;
using gen.snd.Vst.Xml;
using Jacobi.Vst.Core;
using on.smfio.util;

namespace gen.snd.Vst.Xml
{
	public class MidiSmfFileSettings
	{
		private float mindb = -48;
		
		#region ATTR db
		/// <summary>
		/// float number from 0 to 1.
		/// </summary>
		[XmlAttribute("db"),DefaultValue(1.0)] public float MasterVolume { get;set; }
		[XmlIgnore] public float db { get { return (float)Math.Log10(MasterVolume); } }
		[XmlIgnore] public float percent { get { return (float) 1-(db/mindb); } }
		//			float db = 20 * (float)Math.Log10(Volume);
		//			float percent = 1 - (db / MinDb);

		#endregion
		#region ATTR smf, cfg, tk
		
		[XmlAttribute("smf")] public string MidiFileName { get;set; }
		[XmlAttribute("cfg")] public string ConfigurationFile { get;set; }
		[XmlAttribute("tk")] public int SelectedMidiTrack { get;set; }
		
		#endregion
		
		#region ELM 'Bar'
    [XmlElement] public Loop Bar { get;set; }
		#endregion

		// not used yet
		#region MIDI FILE[]
		
		[XmlArrayItem("file",typeof(MidiFile)),XmlArray("midi")]
		public List<MidiFile> Midi {
			get { return midi; }
			set { midi = value; }
		} List<MidiFile> midi = new List<MidiFile>();
		
		#endregion
		
		#region TEMP
		
		public string ActiveInstrument { get;set; }
		public string ActiveEffect { get;set; }

		#endregion

		#region GEN
		[XmlArrayItem("module",typeof(VstModule))]
		[XmlArray("generators")]
		public List<VstModule> Generators {
			get { return generators; }
			set { generators = value; }
		} List<VstModule> generators;

		#endregion
		#region MOD
		[XmlArrayItem("module",typeof(VstModule))]
		[XmlArray("modules")]
		public List<VstModule> Modules {
			get { return modules; }
			set { modules = value; }
		} List<VstModule> modules;
		#endregion
		
		[XmlArrayItem("auto")]
		
		[XmlArray("param-test")]
		public List<AutomationParam> AutoParams {
			get { return @autoParams; }
			set { @autoParams = value; }
		} List<AutomationParam> autoParams;
		
		[XmlElement("plugin")]
		public Plugin Plugin { get;set; }
		
		public MidiSmfFileSettings()
		{
		}
	}

}
