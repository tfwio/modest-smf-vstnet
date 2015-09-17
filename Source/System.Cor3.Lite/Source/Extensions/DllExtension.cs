/* oOo * 11/19/2007 : 8:00 AM */
using System;
using System.Xml.Serialization;
namespace System
{
	static public class DllExtension
	{
		// http://stackoverflow.com/questions/1001404/check-if-unmanaged-dll-is-32-bit-or-64-bit
		// Returns true if the dll is 64-bit, false if 32-bit, and null if unknown
		public static bool? UnmanagedDllIs64Bit(this System.IO.FileInfo dllPath)
		{
			return dllPath.FullName.UnmanagedDllIs64Bit();
		}

		public static bool? UnmanagedDllIs64Bit(this string dllPath)
		{
			switch (GetDllMachineType(dllPath)) {
				case MachineType.IMAGE_FILE_MACHINE_AMD64:
				case MachineType.IMAGE_FILE_MACHINE_IA64:
					return true;
				case MachineType.IMAGE_FILE_MACHINE_I386:
					return false;
				default:
					return null;
			}
		}

		public static bool? UnmanagedDllIs32Bit(this System.IO.FileInfo dllPath)
		{
			return dllPath.FullName.UnmanagedDllIs32Bit();
		}

		public static bool? UnmanagedDllIs32Bit(this string dllPath)
		{
			switch (GetDllMachineType(dllPath)) {
				case MachineType.IMAGE_FILE_MACHINE_AMD64:
				case MachineType.IMAGE_FILE_MACHINE_IA64:
					return false;
				case MachineType.IMAGE_FILE_MACHINE_I386:
					return true;
				default:
					return null;
			}
		}

		static public MachineType GetDllMachineType(this System.IO.FileInfo input)
		{
			return input.FullName.GetDllMachineType();
		}

		static public MachineType GetDllMachineType(this string dllPath)
		{
			// See http://www.microsoft.com/whdc/system/platform/firmware/PECOFF.mspx
			// Offset to PE header is always at 0x3C.
			// The PE header starts with "PE\0\0" =  0x50 0x45 0x00 0x00,
			// followed by a 2-byte machine type field (see the document above for the enum).
			//
			using (var fs = new System.IO.FileStream(dllPath, System.IO.FileMode.Open, System.IO.FileAccess.Read))
				using (var br = new System.IO.BinaryReader(fs)) {
					MachineType machineType = MachineType.IMAGE_FILE_MACHINE_UNKNOWN;
//					bool isgood = false;
					try {
						fs.Seek(0x3c, System.IO.SeekOrigin.Begin);
						Int32 peOffset = br.ReadInt32();
						fs.Seek(peOffset, System.IO.SeekOrigin.Begin);
						UInt32 peHead = br.ReadUInt32();
						if (peHead != 0x00004550)
							// "PE\0\0", little-endian
							throw new Exception("Can't find PE header");
						machineType = (MachineType)br.ReadUInt16();
//						isgood = true;
					}
					catch {
//						isgood = false;
					}
					finally {
						br.Close();
						fs.Close();
					}
					return machineType;
				}
		}
	}
}


