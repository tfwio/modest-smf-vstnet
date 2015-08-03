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

namespace gen.snd.Vst.Xml
{
	/// <summary>
	/// Channel assignment goes from MidiFile.Track
	/// </summary>
	public class ChannelAssignment
	{
		[XmlAttribute("id")] public int GeneratorIndex { get;set; }
		[XmlAttribute("from")] public string From { get;set; }
		[XmlAttribute("to")] public string To { get;set; }
		
		[XmlIgnore] public MidiChannelSet ChannelSetFrom { get { return From; } set { From = value; } }
		[XmlIgnore] public MidiChannelSet ChannelSetTo { get { return To; } set { To = value; } }
		
		/// <summary>
		/// Reserved for remapping or automating cc to autoparams
		/// </summary>
		[XmlAttribute("cc")] public string CC { get; set; }
		
		/// <summary>Default = 0</summary>
		[DefaultValue(0),XmlAttribute("pat")] public int Pat { get; set; }
		
	//		[XmlAttribute("pgm")] public int Pgm { get;set; }
		public ChannelAssignment()
		{
		}
		
		public ChannelAssignment Create(
			VstPluginManager manager,
			int index)
		{
			return Create(manager,index,null,null,null);
		}
		
		public ChannelAssignment Create(
			VstPluginManager manager,
			int index,
			string c_from,
			string c_to,
			string[] cc)
		{
			ChannelAssignment module = new ChannelAssignment();
			module.GeneratorIndex = index;
			module.From = c_from;
			module.To = c_to;
			module.Pat = manager.GeneratorModules[index].PluginCommandStub.GetProgram();
			if (cc!=null) module.CC = string.Join(",",cc);
			return module;
		}
	}
}
