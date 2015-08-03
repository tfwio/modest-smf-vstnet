using System;
using System.IO;
using System.Runtime.InteropServices;

namespace gen.snd.Formats
{
	public class AKP
	{
		#region sequence of structures
		
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpCK
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;		//	usually RIFF unless it's a sub-tag
				public	int				ckLength;	//	minus eight
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckTag;		//	the tagname
				public	akpProgram		ckPrg;
				public	akpOut			ckOut;
				public	akpTune			ckTune;
				public	akpLFO1			ckLFO1;
				public	akpLFO2			ckLFO2;
				public	akpMods			ckMods;
			//	public	akpKeyGroup[]		ckKeyGroup;
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpProgram
			{
				
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;	//	'prg '
				public	int			ckLength;	//	rifflen (06)
				
				public	byte			Unk0;	//	01
				public	byte			MidiProgNum;	//	MIDI program number (0) 0 = OFF
				public	byte			NumberOfKeyGroups;	//	number of keygroups (1) 1 -> 99
				public	byte			Unk1;	//	00
				public	byte			Unk2;	//	02
				public	byte			Unk3;	//	00
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpOut
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;	//	'out '
				public	int			ckLength;	//	(8)
				
				public	byte			out0;
				public	byte			Loudness;	//	(85)	0 to 100
				public	byte			AmpMod1;	//	(0)	0 to 100 ...
				public	byte			AmpMod2;
				public	byte			PanMod1;
				public	byte			PanMod2;
				public	byte			PanMod3;
				public	byte			VelSens; // (+025)	-100 to 100
			}
				
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpTune
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;	//	'tune'
				public	int			ckLength;	//	22
				
				public	byte			tun0;	//	00
				public	byte			SemiTone;	//	-36 to 36
				public	byte			Fine;	//	-50 to 50
				[MarshalAs(UnmanagedType.ByValArray,SizeConst=12)]
				public	byte[]			Octave;	//	(zero)	-50 to 50
				public	byte			PitchBendUp;	//	(2)	0 to 24
				public	byte			PitchBendDown;	//	(2)	0 to 24
				public	akpBend			BendMode;	//	0 = NORMAL, 1 = HOLD
				public	byte			AfterTouch;	//	-12 to 12
				public	byte			tun1;	//	(zero)	unkn
				public	byte			tun2;	//	(zero)	unkn
				public	byte			tun3;	//	(zero)	unkn
			}
				
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpLFO1
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;	//	'lfo ' LFO1
				public	int			ckLength;	//	12
				
				public	byte			lfo0;	//	(1)
				public	akpWav			Type;	//	(1)
				public	byte			Rate;	//	(43) 0 to 100
				public	byte			Delay;	//	(0) to 100
				public	byte			Depth;	//	(0)
				public	akpFlip			Sync;	//	(0) = OFF, 1 = ON
				public	byte			lfo1;	//	(1)
				public	byte			ModWheel;	//	(15)	0 to 100
				public	byte			Aftertouch;	//	(0) to 100
				public	byte			ModRate;	//	-100 to (0) to 100
				public	byte			ModDelay;	//	-100 to (0) to 100
				public	byte			ModDepth;	//	-100 to (0) to 100
			}
				
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpLFO2
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;		//	'lfo ' LFO2
				public	int				ckLength;	//	12
				
				public	byte			lfoo0;			//	(1)
				public	akpWav			lfooType;		//	(0)
				public	byte			lfooRate;		//	(0) to 100
				public	byte			lfooDelay;		//	(0) to 100
				public	byte			lfooDepth;		//	(0)
				public	byte			lfoo1;			//	(1)
				public	akpFlip			lfooReTrigger;	//	(0) = OFF, 1 = ON
				public	byte			lfoo2;			//	(0)
				public	byte			lfoo3;			//	(0)
				public	byte			lfooModRate;	//	-100 to (0) to 100
				public	byte			lfooModDelay;	//	-100 to (0) to 100
				public	byte			lfooModDepth;	//	-100 to (0) to 100
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpMods
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;	//	'mods'
				public	int			ckLength;	//	(38)
				
				public	byte			mod0;				//	(01)
				public	byte			mod1;				//	(00)
				public	byte			mod2;				//	(11)
				public	byte			mod3;				//	(00)
				public akpModB			AmpMod1Src;			//	(02,06)
				public akpModB			AmpMod2Src;			//	(02,03)
				public akpModB			PanMod1Src;			//	(01,08)
				public akpModB			PanMod2Src;			//	(01,06)
				public akpModB			PanMod3Src;			//	(01,01)
				public akpModB			LFO1RateModSrc;		//	(04,06)
				public akpModB			LFO1DelayModSrc;	//	(05,06)
				public akpModB			LFO1DepthModSrc;	//	(03,06)
				public akpModB			LFO2RateModSrc;		//	(07,00)
				public akpModB			LFO2DelayModSrc;	//	(08,00)
				public akpModB			LFO2DepthModSrc;	//	(06,00)
				//	KeyGroup Mod Sources
				public akpModB			PitchMod1Src;		//	(00,07)
				public akpModB			PitchMod2Src;		//	(00,0b)
				public akpModB			AmpModSrc;			//	(02,05)
				public akpModB			FilterModInput1;	//	(09,05)
				public akpModB			FilterModInput2;	//	(09,08)
				public akpModB			FilterModInput3;	//	(09,09)
				
			}
			
			#region Structure Helper (modulator)
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpModB
			{
				public	byte	modBit;
				public akpMod	modValue;
			}
			#endregion
			//	First keygroup starts

			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpKeyGroup
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;	//	'kgrp'
				public	int			ckLength;	//	(336)
				
				public	akpKeyLoc		ckKeyLoc;
				public	akpAmpEnv		ckAmpEnv;
				public	akpFilterEnv	ckFilterEnv;
				public	akpAuxEnv		ckAuxEnv;
				public	akpFilter		ckFilter;
				public	akpZone			ckZone;
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpKeyLoc
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;		//	'kloc'
				public	int			ckLength;		//	(16)
			//	01,03,01,04
				public byte			kloc0;			//	01
				public byte			kloc1;			//	03
				public byte			kloc2;			//	01
				public byte			kloc3;			//	04
				
				public byte			noteLow;		//	(21) to 127
				public byte			noteHigh;		//	(21) to 127
				public byte			tuneSemi;		//	(0)	-36 to 36
				public byte			tuneCourse;		//	(0)	-50 to 50
				public overFX		fxOverride;
				public byte			fxSendLevel;	//	(0) to 100
				public byte			pitchMod1;		//	(0) -100 to 100
				public byte			pitchMod2;		//	(0) -100 to 100
				public byte			ampMod;			//	(0) -100 to 100
				public akpFlip		zoneXFade;		//	(0)
				public byte			muteGroup;		//	hehe
				public byte			kloc4;			//	0
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpAmpEnv
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;		//	'env '
				public	int			ckLength;		//	(18)
				
				public	byte		env0;			//	01
				public	byte		Attack;			//	(0) to 100
				public	byte		env1;			//	00
				public	byte		Decay;			//	(50)	0 to 100
				public	byte		Release;		//	(0) to 100
				public	byte		env2;			//	00
				public	byte		env3;			//	00
				public	byte		Sustain;		//	(64)	0 to 100
				public	byte		env4;			//	00
				public	byte		env5;			//	00
				public	byte		Velo_to_Attack;	//	(0)	-100 to 100
				public	byte		env6;			//	00
				public	byte		KeyScale;		//	(0)	-100 to 100
				public	byte		env7;			//	00
				public	byte		On_Vel_to_Rel;	//	(0)	-100 to 100
				public	byte		OFF_Vel_to_Rel;	//	(0)	-100 to 100
				public	byte		env8;			//	00
				public	byte		env9;			//	00
				
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpFilterEnv
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string		ckID;			//	'env '
				public	int			ckLength;		//	(18)
				
				public	byte		env0;			//	01
				public	byte		Attack;			//	(0) to 100
				public	byte		env1;			//	00
				public	byte		Decay;			//	(50)	0 to 100
				public	byte		Release;		//	(0)	15 to 100
				public	byte		env2;			//	00
				public	byte		env3;			//	00
				public	byte		Sustain;		//	(100)	0 to 100
				public	byte		env4;			//	00
				public	byte		FilterEnvDepth;	//	00 -100 to 100
				public	byte		Velo_to_Attack;	//	(0)	-100 to 100
				public	byte		env6;			//	00
				public	byte		KeyScale;		//	(0)	-100 to 100
				public	byte		env7;			//	00
				public	byte		On_Vel_to_Rel;	//	(0)	-100 to 100
				public	byte		OFF_Vel_to_Rel;	//	(0)	-100 to 100
				public	byte		env8;			//	00
				public	byte		env9;			//	00
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpAuxEnv
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;				//	'env '
				public	int			ckLength;				//	(18)
				
				public	byte		env0;					//	01
				public	byte		Rate1;					//	(0) to 100
				public	byte		Rate2;					//	00
				public	byte		Rate3;					//	(50)	0 to 100
				public	byte		Rate4;					//	(0)	15 to 100
				public	byte		Level1;					//	00
				public	byte		Level2;					//	00
				public	byte		Level3;					//	(100)	0 to 100
				public	byte		Level4;					//	00
				public	byte		env1;					//	00
				public	byte		Vel_to_Rate_1;			//	(0)	-100 to 100
				public	byte		env2;					//	00
				public	byte		Keyboard_to_R2andR4;	//	(0)	-100 to 100
				public	byte		env3;					//	00
				public	byte		Vel_to_Rate_4;			//	(0)	-100 to 100
				public	byte		OFF_Vel_to_Rel;			//	(0)	-100 to 100
				public	byte		Vel_to_Out_Level;		//	00
				public	byte		env4;					//	85
				
			}

			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpFilter
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;				//	'filt'
				public	int				ckLength;			//	(10)
				
				public	byte			Filt0;				//	01
				public	akpFltr			fltMode;
				public	byte			Cutoff;				//	0 to (100)
				public	byte			Reso;				//	(0) to 12
				public	byte			keyTrack;			//	(0) -36 to 36
				public	byte			ModInput1;			//	(0)	-100 to 100
				public	byte			ModInput2;			//	(0)	-100 to 100
				public	byte			ModInput3;			//	(0)	-100 to 100
				public	akpHeadRoom		HeadRoom;			//	(0)
				public	byte			Filt1;				//	(0)
			}
			
			[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
			public struct akpZone
			{
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
				public	string			ckID;			//	'zone'
				public	int				ckLength;		//	(46)
				
				public	byte			zone0;			//	(01)
				public	byte			sampleNameLength;	//	nn
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=13)]
			//	if it starts with zero then there is no sample
				public	string			sampleName;		//	paddedwithzero(12=maxlen)
				[MarshalAs(UnmanagedType.ByValTStr,SizeConst=12)]
				public	string			sampleNameMaybe;	//	paddedwithzero(12=maxlen)
				public	byte			velocityLow;	//	(0) to 127
				public	byte			velocityHigh;	//	(0) to 127
				public	byte			tuneFine;		//	(0)	-50 to 50
				public	byte			tuneSemi;		//	(0)	-36 to 36
				public	byte			filter;			//	(0)	-100 to 100
				public	byte			pan;			//	(0)	-100 to 100
				public	akpLoop			playback;		//	04
				public	outP			chOut;			//	(0)
				public	byte			zoneLevel;		//	(0)	-100 to 100
				public	akpFlip			kbTrack;		//	01
				public	short			veloStart;		//	(0)	-9999 to 9999
			}
			
			
		#endregion
		#region enum
			
		public enum	akpMod : byte
		{
			NO_SOURCE = 0,
			MODWHEEL = 1,
			BEND = 2,
			AFTERTOUCH = 3,
			EXTERNAL = 4,
			VELOCITY = 5,
			KEYBOARD = 6,
			LFO1 = 7,
			LFO2 = 8,
			AMPENV = 9,
			FLTENV = 10,
			AUXENV = 11,
			dMODWHEEL = 12,
			dBEND = 13,
			dEXTERNAL = 14
		}
		
		public enum	akpWav : byte
		{
			SINE = 0,
			TRIANGLE = 1,
			SQUARE = 2,
			SQUAREp = 3,
			SQUAREm = 4,
			SAWbi = 5,
			SAWp = 6,
			SAWn = 7,
			RAND = 8
		}
		
		public enum akpFltr : byte
		{
			LP_2POLE		= 00,
			LP_4POLE		= 01,
			LP_2POLEp		= 02,
			BP_2POLE		= 03,
			BP_4POLE		= 04,
			BP_2POLEp		= 05,
			HP_1POLE		= 06,
			HP_2POLE		= 07,
			HP_1POLEp		= 08,
			LO_to_HI		= 09,
			LO_to_BAND	= 10,
			BAND_to_HI	= 11,
			NOTCH_1			= 12,
			NOTCH_2			= 13,
			NOTCH_3			= 14,
			NOTCH_WIDE	= 15,
			NOTCH_BI		= 16,
			PEAK_1			= 17,
			PEAK_2			= 18,
			PEAK_3			= 19,
			PEAK_WIDE		= 20,
			PEAK_BI			= 21,
			PHASER_1		= 22,
			PHASER_2		= 23,
			PHASE_BI		= 24,
			VOWELISER  	= 25
		}
		
		public enum overFX : byte
		{
			OFF = 0,
			FX1 = 1,
			FX2 = 2,
			RV3 = 3,
			RV4 = 4
		}
		
		public enum outP : byte
		{
			MULTI				= 0,
			ch_1_and_2			= 1,
			ch_3_and_4			= 2,
			ch_5_and_6			= 3,
			ch_7_and_8			= 4,
			ch_9_and_10			= 5,
			ch_11_and_12		= 6,
			ch_13_and_14		= 7,
			ch_15_and_16 		= 8,
			ch_01				= 9,
			ch_02				= 10,
			ch_03				= 11,
			ch_04				= 12,
			ch_05				= 13,
			ch_06				= 14,
			ch_07				= 15,
			ch_08				= 16,
			ch_09				= 17,
			ch_10				= 18,
			ch_11				= 19,
			ch_12				= 20,
			ch_13				= 21,
			ch_14				= 22,
			ch_15				= 23,
			ch_16				= 24
		}
		
		public enum akpHeadRoom : byte
		{
			db00 = 0,
			db06 = 1,
			db12 = 2,
			db18 = 3,
			db24 = 4,
			db30 = 5
		}
		
		public enum akpLoop : byte
		{
			NONE			= 0,
			ONE_SHOT		= 1,
			LOOP_IN_REL		= 2,
			LOOP_TILL_REL	= 3,
			AS_SAMPLE		= 4,
		}
		
		public enum	akpFlip : byte { OFF = 0, ON = 1 }
		
		public enum	akpBend : byte { NORMAL = 0, HOLD = 1 }
		#endregion
		
		#region	Properties
		private	FileStream		fs;
		private	BinaryReader	bread;
		#endregion
		
		#region Marshal.PtrToStructure
		static private object mread(object reffer, byte[] data)
		{
			IntPtr hrez = Marshal.AllocHGlobal( Marshal.SizeOf(reffer) );
			Marshal.Copy( data, 0, hrez, Marshal.SizeOf(reffer) );
			reffer = Marshal.PtrToStructure( hrez, reffer.GetType());
			Marshal.FreeHGlobal( hrez );
			return reffer;
		}
		#endregion

		#region IO HELPER
		
		private	akpCK			akpIO;
		public	akpCK AkpIO { get { return akpIO; } set { akpIO = value; } }
		
	//	private	akpKeyGroup[]	akpKeyGroupx;
		
		
		public AKP(string path)
		{
			akpIO = new akpCK();
			
			fs = File.OpenRead(path);
			bread = new BinaryReader(fs);
			
		//	long tpos = fs.Position - Marshal.SizeOf(CX);
			byte[] red = bread.ReadBytes(Marshal.SizeOf(akpIO));
		                             
			akpIO = (akpCK)mread((object)akpIO,red);
			
			bread.Close();
			fs.Close();
		}
		
		#endregion
		
	}
}
