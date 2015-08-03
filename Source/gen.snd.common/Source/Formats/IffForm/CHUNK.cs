/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace gen.snd.IffForm
{
	[ StructLayout( LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi )]
	public struct CHUNK
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			ckID;		//	usually RIFF unless it's a sub-tag
		public	int				ckLength;	//	minus eight
		public	string			ckTag;		//	the tagname
		public CHUNK(BinaryReader bx)
		{
			this.ckID = IOHelper.GetString(bx.ReadBytes(4));
			this.ckLength = bx.ReadInt32();
			this.ckTag = IOHelper.GetString(bx.ReadBytes(4));
		}
	}
}
