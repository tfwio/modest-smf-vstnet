/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using gen.snd;
using gen.snd.Vst;
using gen.snd.Vst.Module;

namespace gen.snd.Vst.Xml
{
	public class PluginBase
	{
		/// <summary>
		/// bank
		/// </summary>
		[XmlIgnore] public byte[] ProgramDump { get; set; }
		/// <summary>
		/// preset
		/// </summary>
		[XmlIgnore] public byte[] PatchDump { get; set; }
		/// <summary>
		/// bank
		/// </summary>
		[XmlElement("prgm")] public XmlNode[] CDataProgram
		{
			get
			{
				var dummy = new XmlDocument();
				if (ProgramDump==null) return null;
				return new XmlNode[] {
					dummy.CreateCDataSection(
						Convert.ToBase64String(ProgramDump,Base64FormattingOptions.InsertLineBreaks)
					)
				};
			}
			set
			{
				if (value == null)
				{
					ProgramDump = null;
					return;
				}
				
				if (value.Length != 1)
				{
					throw new InvalidOperationException(
						String.Format(
							"Invalid array length {0}", value.Length));
				}
				if (string.IsNullOrEmpty(value[0].Value))
					ProgramDump = new byte[0];
				else ProgramDump = Convert.FromBase64String(value[0].Value);
				//				MessageBox.Show(gen.snd.Midi.MidiReader.SmfString.byteToString(ProgramDump));
			}
		}
		/// <summary>
		/// preset
		/// </summary>
		[XmlElement("ptch")] public XmlNode[] CDataPatch
		{
			get
			{
				var dummy = new XmlDocument();
				if (PatchDump==null) return null;
				return new XmlNode[] {
					dummy.CreateCDataSection(
						Convert.ToBase64String(PatchDump,Base64FormattingOptions.InsertLineBreaks)
					)
				};
			}
			set
			{
				if (value == null)
				{
					PatchDump = null;
					return;
				}
				
				if (value.Length != 1)
				{
					throw new InvalidOperationException(
						String.Format(
							"Invalid array length {0}", value.Length));
				}
				if (string.IsNullOrEmpty(value[0].Value)) PatchDump = new byte[0];
				else PatchDump = Convert.FromBase64String(value[0].Value);
			}
		}
	}
}
