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
using System.Diagnostics;
using System.IO;

using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Core.Plugin;
//using Bitset = MidiReader.SmfString;
using IPluginCommander = Jacobi.Vst.Core.Host.IVstPluginCommandStub;

namespace gen.snd.Vst.Module
{
	struct VstPluginBank {
		const int ckMagic = 0x43636E4B;
		//						Std. Chunk Header
		public int    ChunkMagic;         // 'CcnK
		public int    ChunkSize;           // of this chunk, excl. magic + byteSize
		//						Plugin ID
		public int    PluginMagic;            // 'FxBk'
		public int    Version;
		public int    PluginId;               // fx unique id
		public int    PluginVersion;
		public int    NumPrograms;
		//						Reservation
		public byte[/* 128 */] future;
		//						Data
		public byte[/* NumPrograms */][/* Var-Len */] PgmData;
		static VstPluginBank Prepare(IVstPluginContext plugin)
		{
			VstPluginBank bank = new VstPluginBank();
			bank.ChunkMagic = ckMagic;
			bank.PluginId = plugin.PluginInfo.PluginID;
			return bank;
		}
		static public VstPluginBank Read(VstPlugin plugin, string fileName)
		{
			VstPatchChunkInfo x;
			VstPluginBank bank = VstPluginBank.Prepare(plugin);
			using (FileStream stream = new FileStream(fileName,FileMode.Open,FileAccess.Read,FileShare.ReadWrite))
				using (BinaryReader reader = new BinaryReader(stream,System.Text.Encoding.Unicode))
			{
				bank.ChunkMagic = reader.ReadInt32();
				bank.ChunkSize = reader.ReadInt32();
				bank.PluginMagic = reader.ReadInt32();
				bank.Version = reader.ReadInt32();
				bank.PluginId = reader.ReadInt32();
				bank.PluginVersion = reader.ReadInt32();
				bank.NumPrograms = reader.ReadInt32();
				
			}
			return bank;
		}
//		static public bool Write(VstPluginBank bank, string fileName)
//		{
//			
//		}
		
	}
}
