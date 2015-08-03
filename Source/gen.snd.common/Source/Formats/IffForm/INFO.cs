/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace gen.snd.IffForm
{
	[StructLayout(LayoutKind.Sequential, Pack=1, CharSet=CharSet.Ansi) ]
	public struct INFO
	{	[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string		infoHead;
		public	int			Length;
		public	iver		ifil;
		public	ZSTR		inam;
		public	ZSTR		isng;
		public Dictionary<long,INFOsub> nfosub;
		
		public INFO(int ckSize, BinaryReader bir, FileStream fis)
		{
			ifil = new iver(); inam = new ZSTR(); isng = new ZSTR();
			
			long origin = fis.Seek(8,SeekOrigin.Current);
			infoHead = IOHelper.GetString(bir.ReadChars(4));
			Length = bir.ReadInt32();
			long pos = fis.Position;
			nfosub = new Dictionary<long,INFOsub>();
			while (fis.Position < pos+ckSize-4)
			{
				long hand = fis.Position;
				INFOsub inx = new INFOsub(bir,fis,this);
				fis.Seek(hand+inx.Length+8,SeekOrigin.Begin);
			}
		}
	}
}
