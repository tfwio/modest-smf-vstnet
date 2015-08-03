/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 1/3/2007
 * Time: 12:22 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.IO;
using System.Runtime.InteropServices;

namespace gen.snd.Formats
{
	public class ds2cs
	{
		[DllImport( "ds2wav.dll", CharSet = CharSet.Ansi )]
		public static extern void ds2wav (
			[MarshalAs(UnmanagedType.LPStr)]
			string			dspath,
			[MarshalAs(UnmanagedType.LPStr)]
			string			wavpath
		);
		
		public ds2cs(string	filepath)
		{
			string dsfile = Path.GetFullPath(filepath);
			if (File.Exists(dsfile) && ((Path.GetExtension(dsfile))==".ds"))
			{
				string path2 = filepath.Replace(".ds",".wav");
				ds2wav(filepath,path2);
				path2 = null;
			}
		}
	}
}
