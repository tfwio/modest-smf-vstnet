/*
 * (tfooo) in #DEVELOP
 */

using System;
using System.Runtime.InteropServices;

namespace gen.snd.Formats
{
	/// <summary>
	/// sndPlaySound API
	/// </summary>
	public class MM_Sys
	{
		[Flags]
		/// <summary>
		/// sndPlaySound API parameter;
		/// </summary>
		public enum sndFlags : int
		{
			SYNC		= 0,
			ASYNC		= 1,
			NODEFAULT	= 2,
			MEMORY		= 4,
			LOOP		= 8,
			NOSTOP		= 16,
			NOWAIT		= 32,
			PURGE		= 64, 	//	WINVER >= 0x0400
			APPLICATION	= 128,	//	WINVER >= 0x0400
			ALIAS		= 65536,
			FILENAME	= 131072,
			RESOURCE	= 262148,
			ALIAS_ID	= 1114112
		}
		
		[DllImport( "winmm"/*.dll*/, CharSet = CharSet.Ansi )]
		/// <summary>
		/// winmm api
		/// </summary>
		/// <param name="snd">File path pointing to the wav file (ACM compatible)</param>
		/// <param name="sflg">sndFlags</param>
		/// <returns></returns>
		public static extern bool sndPlaySound
		(
			[MarshalAs(UnmanagedType.LPStr)]
			string			snd,
			sndFlags		sflg
		);

	}
}
