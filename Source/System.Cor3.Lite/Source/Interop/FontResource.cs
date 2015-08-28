using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
  /// <summary>
  /// Do not bother using the following interop method.
  /// Use the PrivateFontCollection's `AddFont(string file)` method.
  /// I understand this method being useful for resources being loaded from a byte array context.
  /// See: http://stackoverflow.com/questions/556147/how-to-quickly-and-easily-embed-fonts-in-winforms-app-in-c-sharp
  /// </summary>
  static public class FontResourceExtension
  {
    [System.Runtime.InteropServices.DllImport("gdi32")]
    static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
    
    static public Font GetFontResource(this PrivateFontCollection outFontCollection, System.IO.FileInfo ttfFontFile, float fontSize, uint count=0)
    {
      AddFontResource(outFontCollection, ttfFontFile, count);
      
      return new Font(outFontCollection.Families[outFontCollection.Families.Length-1], fontSize);
    }
    static public void AddFontResource(this PrivateFontCollection outFontCollection, System.IO.FileInfo ttfFontFile, uint count=0)
    {
      if (outFontCollection==null) throw new ArgumentException();
      
      byte[] fontData = System.IO.File.ReadAllBytes(ttfFontFile.FullName);
      IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
      Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
      
      outFontCollection.AddMemoryFont(fontPtr, fontData.Length);
      IntPtr ptr = AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref count);
      if (ptr==IntPtr.Zero) throw new NullReferenceException();
      
      Marshal.FreeCoTaskMem(fontPtr);
      fontData=null;
    }
  }
  
  
}


