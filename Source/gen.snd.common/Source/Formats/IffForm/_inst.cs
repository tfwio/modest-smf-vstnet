/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Runtime.InteropServices;

namespace gen.snd.IffForm
{
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct _inst
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			ckID; // 'inst'
		public	int				ckLength;
		
		public	byte			uNote;
		public	byte			fineTune;
		public	byte			Gain;
		public	byte			noteLow;
		public	byte			noteHigh;
		public	byte			velLow;
		public	byte			velHigh;
	}
}
