/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

namespace System
{
 /*
  * around half of this is quite un-usable.
  */
	/// <summary>
	/// Structure Marshaling, String-casting, &#133;
	/// </summary>
	public class IOHelper
	{
		static public T ReadChunk<T>(long position, Stream fs)
			where T:struct
		{
			T Structure = new T();
			fs.Seek(position,SeekOrigin.Begin);
			
			int size = Marshal.SizeOf(Structure/*cks.ckFmt*/);
			byte[] red = new byte[size];
			
			fs.Read(red,Convert.ToInt32(position),size);
			
			Structure = IOHelper.xread<T>(Structure,red);
			red = null;
			return Structure;
		}
		static public T ReadChunk<T>(long position, Stream fs, BinaryReader bread)
			where T:struct
		{
			T Structure = new T();
			fs.Seek(position,SeekOrigin.Begin);
			byte[] red = bread.ReadBytes(Marshal.SizeOf(Structure/*cks.ckFmt*/));
			Structure = IOHelper.xread<T>(Structure,red);
			red = null;
			return Structure;
		}
		static public T[] ReadChunk<T>(long position, int count, Stream fs, BinaryReader bread)
			where T:struct
		{
			T[] Structure = new T[count];
			fs.Seek(position,SeekOrigin.Begin);
			byte[] red = bread.ReadBytes(Marshal.SizeOf(Structure/*cks.ckFmt*/));
			Structure = IOHelper.xread<T[]>(Structure,red);
			red = null;
			return Structure;
		}
		static public T[] ReadChunk<T>(long position, int numberOfChunks, Stream s)
			where T:struct
		{
			T[] Structure = new T[numberOfChunks];
			
			T test = new T();
			int StructureSize = Marshal.SizeOf(test);
			// the number of bytes we're going to read is "numberOfChunks * StructureSize"
			
			s.Seek(Convert.ToInt32(position),SeekOrigin.Begin);
			
			int size = Marshal.SizeOf(Structure);
			
			byte[] bytes = new byte[size];
			s.Read(bytes,Convert.ToInt32(position),size);
			
			Structure = IOHelper.xread<T[]>(Structure,bytes);
			bytes = null;
			return Structure;
		}
		
		static public T xread<T>(object o, byte[] data)
		{
			return (T) mread(o,data);
		}
		static public object mread(object reffer, byte[] data)
		{
			IntPtr hrez = Marshal.AllocHGlobal( Marshal.SizeOf(reffer) );
			Marshal.Copy( data, 0, hrez, Marshal.SizeOf(reffer) );
			reffer = Marshal.PtrToStructure( hrez, reffer.GetType());
			Marshal.FreeHGlobal( hrez );
			return reffer;
		}

		#region STRING CONVERSION UTILITY
		// this is a helper function that finds the first (char)0 in the string and
		// returns a string up to that point.
		public static string GetZerodStr(char[] stx)
		{
			int boo = GetString(stx).IndexOf((char)0,0);
			string tst = GetString(stx);
			if (boo >=0) tst = tst.Substring(0,boo).Trim();
			return tst;
		}
		/**
		 * There has got to be zero padding or something going on here.
		 **/
		/// <summary>
		/// Converts char[] to Default String object
		/// </summary>
		/// <param name="inchr">[in] char[]</param>
		/// <returns>Default (encoding) string</returns>
		static public string GetString (char[] inchr) { string m = ""; for (int i=0; i< inchr.Length; i++) m += inchr[i]; return m; }
		/// <summary>
		/// Converts byte[] to Default String object
		/// </summary>
		/// <param name="inchr">[in] byte[]</param>
		/// <returns>Default (encoding) string</returns>
		static public string GetString (byte[] inchr)
		{
			string m = "";
			for (int i=0; i< inchr.Length; i++)
				m += (char)inchr[i]; return m;
		}
		#endregion
	}
}
