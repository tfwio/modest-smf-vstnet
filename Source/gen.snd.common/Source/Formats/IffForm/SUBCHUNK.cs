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
	public struct SUBCHUNK
	{
		static readonly SUBCHUNK empty = new SUBCHUNK(){ ckID="    ", ckLength=-1};
		public static SUBCHUNK Empty { get { return empty; } }
		
		[MarshalAs(UnmanagedType.ByValTStr,SizeConst=4)]
		public	string			ckID;		//	usually RIFF unless it's a sub-tag
		public	int				ckLength;	//	minus eight
		public SUBCHUNK(BinaryReader bx)
		{
				this.ckID = IOHelper.GetString(bx.ReadBytes(4));
			try
			{
				this.ckLength = bx.ReadInt32();
			}
			catch (System.IO.EndOfStreamException e)
			{
				System.Windows.Forms.MessageBox.Show(e.Message,this.ckID);
			//	this.ckID = null;
				this.ckLength = 0;
			}
		}
	}
}
