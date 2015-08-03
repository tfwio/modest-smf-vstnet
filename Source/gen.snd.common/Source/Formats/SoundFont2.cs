/*
 * tfooo with #develop
 */

using System;
using System.Collections.Generic;
using System.IO;

using gen.snd.IffForm;

namespace gen.snd.Formats
{
	/// <summary>
	/// SoundFont v2.4
	/// </summary>
	public partial class SoundFont2
	{
		#region Fields/Properties
		public string[] nn;
		public RIFF  riff;
		public INFO nfo;
		public listHYDRA hyde;
		#endregion
		
		#region Static Fields: Reader/Stream
		public static BinaryReader bread;
		public static FileStream fstream;
		#endregion
	
		public SoundFont2(string filena)
		{
			init(filena);
		}
		
		/// <summary>
		/// Used for logging information on a given format structure.
		/// </summary>
		public string outp;
		
		/// <summary>
		/// Read all but sample data from sf2 into memory.
		/// </summary>
		/// <param name="fname">File name.</param>
		public void init(string fname)
		{
			outp = string.Empty;
			
			nn = MidiHelper.OctaveMacro();
			fstream = File.Open(fname, FileMode.Open, FileAccess.Read);
			bread = new BinaryReader(fstream,System.Text.Encoding.ASCII);
			
			riff = new RIFF(fstream,bread); int len;
			
			string format = string.Format(
				"RIFF: '{0}', " +
				"Length: {1:###,###,###,##0}, " +
				"Tag: '{2}'\n" +
				"----------------------\n",
				IOHelper.GetString(riff.header),
				riff.Length,
				IOHelper.GetString(riff.Tag)
			);
			
			System.Diagnostics.Debug.Print(format);
			outp += format;
			foreach (KeyValuePair<long,SoundFont2.RIFFsub> rsx in riff.rss)
			{
				outp += ("" + rsx.Key.ToString("##,###,###,##0")).PadLeft(12,' ') + " | " + IOHelper.GetString(rsx.Value.header)+(" <"+rsx.Value.Length.ToString() +">").PadLeft(16)+" \'" + IOHelper.GetString(rsx.Value.Tag) + "\'\r\n";
				string foo = (IOHelper.GetString(rsx.Value.Tag));
				switch (foo)
				{
					case "INFO":
						len = rsx.Value.Length;
						nfo = new INFO(len,bread,fstream);
						foreach (KeyValuePair<long, INFOsub> mox in nfo.nfosub)
						{
							string vers = ""; long po; ZSTR nx;
							switch (mox.Value.Type)
							{
								case "ifil":
									po = fstream.Position;
									fstream.Seek(mox.Key+8, SeekOrigin.Begin);
									nfo.ifil.Major = bread.ReadInt16();
									nfo.ifil.Minor = bread.ReadInt16();
									fstream.Seek(po, SeekOrigin.Begin);
									vers = nfo.ifil.Major.ToString() + "." + nfo.ifil.Minor.ToString();
									break;
								case "isng":
									po = fstream.Position;
									fstream.Seek(mox.Key+8, SeekOrigin.Begin);
									nfo.isng.StrValue = bread.ReadChars(mox.Value.Length);
									fstream.Seek(po, SeekOrigin.Begin);
									vers = IOHelper.GetString(nfo.isng.StrValue).ToString() + "";
									break;
								case "INAM":
									po = fstream.Position;
									fstream.Seek(mox.Key+8, SeekOrigin.Begin);
									nfo.inam.StrValue = bread.ReadChars(mox.Value.Length);
									fstream.Seek(po, SeekOrigin.Begin);
									vers = IOHelper.GetString(nfo.inam.StrValue).ToString() + "";
									break;
								default:
									po = fstream.Position;
									nx = new ZSTR();
									fstream.Seek(mox.Key+8, SeekOrigin.Begin);
									nx.StrValue = bread.ReadChars(mox.Value.Length);
									fstream.Seek(po, SeekOrigin.Begin);
									vers = IOHelper.GetString(nx.StrValue).ToString().Trim((char)0).Trim((char)0x22) + "";
									break;
							}
							outp +=  "" + mox.Key.ToString("##,###,###,##0").PadLeft(12) + ": " + (mox.Value.Type) + ": " +vers.TrimEnd((char)0)+ "\r\n";
						}
						break;
					case "pdta":
						len = rsx.Value.Length;
						hyde = new listHYDRA(fstream,bread,rsx.Key);
	
						outp += "PHDR"+hyde.phdr.Count.ToString()+"\r\n";
						foreach (PHDR ph in hyde.phdr)
						{
							outp += ph.preset.ToString().PadLeft(3,'0') + ":" + ph.bank.ToString().PadLeft(3,'0')
								+ " " +  IOHelper.GetZerodStr(ph.presetName)
								+ " (lib=" +  ph.genera.ToString()
								+ " genera=" +  ph.genera.ToString()
								+ " morphology=" +  ph.morphology.ToString()
								+ " pbagndx=" +  ph.presetBagIndex.ToString()
								+ ")\r\n";
						}
						outp += "PBAG"+hyde.pbag.Count.ToString()+"\r\n";
						foreach (PBAG pb in hyde.pbag)
							outp += "\t" + pb.gen.ToString()
								+ ":" + pb.mod.ToString() + "\r\n";
						outp += "PGEN"+hyde.pgen.Count.ToString()+"\r\n";
						foreach (PGEN pm in hyde.pgen)
						{
							outp += "\tsfGen="+pm.SFGen.ToString()
								+ " hi=" + pm.TypeHi.ToString()
								+ " lo=" + pm.TypeLo.ToString() + "\r\n";
						}
						outp += "PMOD"+hyde.pmod.Count.ToString()+"\r\n";
						foreach (PMOD pm in hyde.pmod)
						{
							outp += "\tsrc="+pm.src.ToString()
								+ " dst=" + pm.dst.ToString()
								+ " amt=" + pm.amt.ToString()
								+ " amtsrc=" + pm.amtsrc.ToString()
								+ " trans=" + pm.trans.ToString()+ "\r\n";
						}
						outp += "INST"+hyde.inst.Count.ToString()+"\r\n";
						foreach (INST pms in hyde.inst)
						{
							outp += "name="+IOHelper.GetZerodStr(pms.iName)
								+ " iBag=" + pms.bagIndex.ToString() + "\r\n";
						}
						outp += "IBAG: "+hyde.ibag.Count.ToString()+"\r\n";
						foreach (IBAG pms in hyde.ibag)
						{
							outp += "\tBagIndex=" + pms.gen.ToString()
								+ " dst=" + pms.mod.ToString() + "\r\n";
						}
						outp += "IMOD: "+hyde.imod.Count.ToString()+"\r\n";
						foreach (IMOD pm in hyde.imod)
						{
							outp += string.Format(
								"\t	amt: {0}, src: {1}, dst: {2}, amtsrc: {3}, trans: {4}\n",
								pm.amt,pm.src,pm.dst,pm.amtsrc,pm.trans
							);
						}
						outp += "SHDR: "+hyde.shdr.Count.ToString()+"\r\n";
						foreach (SHDR pm in hyde.shdr)
						{
							outp +=  IOHelper.GetZerodStr(pm.iName).ToString()
								+ " src=" + pm.LenA.ToString()
								+ " trans=" + pm.LenB.ToString() + "\r\n";
						}
						break;
				}
			}
			fstream.Close();
			bread.Close();
			//	}
			//	ofd.Dispose();
		}
		
	
	}
}
