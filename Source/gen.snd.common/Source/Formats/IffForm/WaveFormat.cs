/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Runtime.InteropServices;

namespace gen.snd.IffForm
{
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct WaveFormat
	{
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string	ckID;
		public	int		ckLength;
		
		public	short	fmtTag;
		public	ushort	fmtChannels;
		public	uint	fmtRate;
		public	uint	fmtBPSec;
		public	ushort	fmtBlock;
		public	ushort	fmtBPSmp;
	}
}
