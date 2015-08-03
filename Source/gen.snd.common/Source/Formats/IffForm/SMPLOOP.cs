/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace gen.snd.IffForm
{
	
	public enum smpLoopType : int
	{
		foreward = 0,
		alternating = 1,
		backward = 2,
		ordained
	}
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct _smp
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			ckID;
		public	int				ckLength;
		
		public	int				smpManufacturer;
		public	int				smpProduct;
		public	int				smpPeriod;
		public	int				smpMidiUnityNote;
		public	int				smpPitchFraction;
		public	int				smpSMPTEFmt;
		public	int				smpSMPTEOffset;
		public	int				smpSampleLoops;
		public	int				smpSamplerData;
	}
	
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct _smpl
	{
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			ckID;
		public	int				ckLength;
		
		public	int				smpManufacturer;
		public	int				smpProduct;
		public	int				smpPeriod;
		public	int				smpMidiUnityNote;
		public	int				smpPitchFraction;
		public	int				smpSMPTEFmt;
		public	int				smpSMPTEOffset;
		public	int				smpSampleLoops;
		public	int				smpSamplerData;
		public	_smpLoop[]		smpLoops;
	}
	
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct _smpLoop
	{
		public	int				CuePointID;
		
		public	smpLoopType		smplType;
		public	int				smplStart;
		public	int				smplEnd;
		public	int				smplFraction;
		public	int				smplCount;
		public	_smpLoop(BinaryReader br)
		{
			//	cks.ckSmpl.smpSampleLoops
//				byte[] red = null;
//				this = new _smpLoop();
//				red = br.ReadBytes(Marshal.SizeOf(this));
//				this = (_smpLoop)MMX.RIFF_WAVE.mread((object)this,red);
//				red=null;
			
			this.CuePointID = br.ReadInt32();
			this.smplType = (smpLoopType)br.ReadInt32();
			this.smplStart = br.ReadInt32();
			this.smplEnd = br.ReadInt32();
			this.smplFraction = br.ReadInt32();
			this.smplCount = br.ReadInt32();
		}
	}
}
