/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace gen.snd.IffForm
{
	[StructLayout(LayoutKind.Sequential) ]
	public struct INFOsub
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string	Type;
		public	int		Length;
		public INFOsub(BinaryReader bx, FileStream fx, INFO nfo)
		{
			long orig = fx.Position;
			Type = IOHelper.GetString(bx.ReadChars(4));
			Length = bx.ReadInt32();
			nfo.nfosub.Add(orig, this);
		}
		
	}
}
