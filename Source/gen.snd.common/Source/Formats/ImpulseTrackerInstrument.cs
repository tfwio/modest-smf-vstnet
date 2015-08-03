#region User/License
// oio * 7/19/2012 * 7:04 AM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
#endregion
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace gen.snd.Formats.ImpluseTracker
{
	public class ITI
	{
		public string[] NoteName
		{
			get { return noteName; }
			set { noteName = value; }
		} string[] noteName = new string[127];
		
		static public class Resources
		{
			static public string GetSampleHeader(impx smp)
			{
				return string.Format(
					iti_smp_header_fmt,
					smp.impsC5Speed,		smp.impsCvt,		smp.impsDfP,
					smp.impsFlag,			smp.impsGlobalVolume,
					smp.impsLength,			smp.impsLoopBegin,	smp.impsSusLoopEnd,
					smp.impsVibDepth,		smp.impsVibRate,	smp.impsVibSpeed,
					smp.impsVibType
				);
			}
			
			public const string iti_smp_header_fmt =
				"c5speed: {0:##,###,###,##0}, " +
				"cvt: {1}, " +
				"defaultpan: {2}, " +
				"flags, {3}" +
				"GbVol: {4:##,###,###,##0}, " +
				"len: {5:##,###,###,##0}, " +
				"ls: {6:##,###,###,##0}, " +
				"le: {7:##,###,###,##0}, " +
				"vdep: {8:##,###,###,##0}, " +
				"vrat: {9:##,###,###,##0}, " +
				"vspd: {10:##,###,###,##0}, " +
				"vtyp: {11:##,###,###,##0}";
		}
		
		#region Properties
		
		public impi ITI_INST {
			get { return iti_instrument; }
			set { iti_instrument = value; }
		} impi iti_instrument = new impi();
		
		public impx[] ITI_SMPH {
			get { return iti_sampleheaders; }
			set { iti_sampleheaders = value; }
		} impx[] iti_sampleheaders;

		#endregion
		
		#region ITI Structures
		/// <summary>
		/// Envelope Point<br/>
		/// byte : epVal<br/>
		/// short : epPos
		///</summary>
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct enveloPoint
		{
			public byte		epVal;
			public short	epPos;
		}
		/// <summary>
		/// keyMap structure to contain key->sample map
		///</summary>
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct keyMap
		{
			public byte		epVal;
			public byte		epPos;
		}
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		/// <summary>
		/// Envelop Point Information
		/// </summary>
		public struct envL
		{
			public evl		envFlag;
			public byte		envNodeCount;
			public byte		envLoopStart;
			public byte		envLoopEnd;
			public byte		envSusLoopStart;
			public byte		envSusLoopEnd;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=25)]
			public enveloPoint[] envPoints;
		}
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct impi
		{
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=4)]
			public string						IMPI;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=12)]
			public string						impDosFileName;
			public byte							impNull0;
			public NewNoteAction				impNewNoteAction;
			public DuplicateCheckType			impDuplicateCheck;
			public DuplicateCheckAction			impDuplicateCheckAct;
			public short						impFadeOut;
			public byte							impPitchPanSeperation;
			public byte							impPitchPanCenter;
			public byte							impGlobalVol;
			public byte							impDefaultPan;
			public byte							impRandomVolumeVar;
			public byte							impRandomPanVariation;
			public short						impTrackVers;
			public byte							impNumberOfSamples;
			public byte							impNull1;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=26)]
			public string						impInstrumentName;
			public byte							impIFC;
			public byte							impIFR;
			public byte							impMCh;
			public byte							impMPr;
			public short						impMidiBank;
			[MarshalAs(UnmanagedType.ByValArray, SizeConst=120)]
			public keyMap[]						impNoteMap;
			public envL							impVolEnvelop;
			public envL							impPanEnvelop;
			public envL							impEFXEnvelop;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst=7)]
			public string						impPadding;
		}
		
		/// <summary>
		/// sammple
		/// </summary>
		[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
		public struct impx
		{
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
			public string						IMPS;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=12)]
			public string						impsDosFileName;
			public byte							impsNull0;
			public byte							impsGlobalVolume;
			public flg							impsFlag;
			public byte							impsVolume;
			[MarshalAs(UnmanagedType.ByValTStr,SizeConst=26)]
			public string						impsSampleName;
			public cvt							impsCvt;
			public byte							impsDfP;
			public int							impsLength;
			public int							impsLoopBegin;
			public int							impsLoopEnd;
			public int							impsC5Speed;
			public int							impsSusLoopBein;
			public int							impsSusLoopEnd;
			public int							impsSamplePointer;
			public byte							impsVibSpeed;
			public byte							impsVibDepth;
			public byte							impsVibRate;
			public byte							impsVibType;
		}
		[ StructLayout( LayoutKind.Sequential, CharSet=CharSet.Ansi, Pack=1 )]
		public struct sampleData
		{
			public int		impsFileSize;
			public short	impsDate;
			public short	impsTime;
			public byte		impsFormat; // 89 in length
			public byte[]	impsByteSampleData;
			public short[]	impsShrtSampleData;
			// who fucking knows what kind of data is in here allready?
		}
		#endregion

		#region Enumerations
		[Flags] public enum flg : byte
		{
			IMPS = 1, //sample associated with header.
			PCM16 = 2,  //16 bit, Off = 8 bit.
			S = 4, //stereo, Off = mono. Stereo samples not supported yet
			C = 8, //compressed samples.
			L = 16, //Use loop
			SL = 32, //Use sustain loop
			p = 64, //Ping Pong loop, Off = Forwards loop
			Sp = 128 //Ping Pong Sustain loop, Off = Forwards Sustain loop
		}
		[Flags]	public enum cvt : byte
		{
			u = 1,
			moterola_intel = 2,
			delta_pcm = 4,
			delta = 8,
			txWave12bit = 16,
			lrasP = 32,
			res0 = 64,
			res1 = 128
		}
		[Flags]	public enum evl : byte
		{
			nutn = 0,
			on = 1,
			loop = 2,
			sustain = 4
		}
		public enum ramp : byte
		{
			Sine = 0,
			Ramp = 1,
			Squa = 2,
			Rand = 3
		}
		public enum NewNoteAction : byte
		{
			Cut = 0,
			Continue = 1,
			Note_Off = 2,
			Note_Fade = 3
		}
		public enum DuplicateCheckType : byte
		{
			Off = 0,
			Note = 1,
			Sample = 2,
			Instrument = 3
		}
		public enum DuplicateCheckAction : byte
		{
			Cut = 0,
			NoteOff = 1,
			NoteFade = 2
		}
		#endregion
		
		#region itinst default method
		public ITI(string filename)
		{
			impi fil = new impi();
			// FIXME: Possible re-use
			this.noteName = MidiHelper.OctaveMacro();
			
			using (FileStream rop = new FileStream(
				filename,
				FileMode.Open,
				FileAccess.Read,
				FileShare.ReadWrite))
				using (BinaryReader bob = new BinaryReader(rop))
			{
				byte[] mal = bob.ReadBytes(Marshal.SizeOf(fil));
				this.iti_instrument = IOHelper.xread<impi>(fil,mal);
				// Get Samples
				{
					impx[] samp = new impx[iti_instrument.impNumberOfSamples];
					for (int i = 0; i < iti_instrument.impNumberOfSamples;i++)
						samp[i] = IOHelper.xread<impx>(
							samp[i],
							bob.ReadBytes(Marshal.SizeOf(samp[i]))
						);
					this.ITI_SMPH = samp;
				}
				
				bob.Close();
				rop.Close();
				mal = null;
			}
			
		}
		
		#endregion
		
	}
}