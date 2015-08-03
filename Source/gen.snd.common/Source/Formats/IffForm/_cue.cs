/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Runtime.InteropServices;

namespace gen.snd.IffForm
{

	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct _cue
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			ckID;
		public	int				ckLength;
		
		public	int				cueCount;
		public	_cuePoint[]		cuePoints;
	}
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct _cuePoint
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			ckID;
		public	int				ckLength;
		
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			cptFcc;
		public	int				cptStart;
		public	int				cptBlockStart;
		public	int				cptOffset;
	}
}
