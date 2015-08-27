using System;
using System.Drawing;
using System.Drawing.Text;
using System.Runtime.InteropServices;

namespace System.Drawing.Text
{
  /// <summary>
  /// See: http://stackoverflow.com/questions/556147/how-to-quickly-and-easily-embed-fonts-in-winforms-app-in-c-sharp
  /// </summary>
  static public class FontResourceExtension
  {
    [System.Runtime.InteropServices.DllImport("gdi32")]
    static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);
    
    static public Font GetFontResource(this PrivateFontCollection outFontCollection, System.IO.FileInfo ttfFontFile, float fontSize)
    {
      if (outFontCollection==null) throw new ArgumentException();
      
      byte[] fontData = System.IO.File.ReadAllBytes(ttfFontFile.FullName);
      IntPtr fontPtr = Marshal.AllocCoTaskMem(fontData.Length);
      Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
      
      outFontCollection.AddMemoryFont(fontPtr, fontData.Length);
      uint dummy = 0;
      AddFontMemResourceEx(fontPtr, (uint)fontData.Length, IntPtr.Zero, ref dummy);
      
      Marshal.FreeCoTaskMem(fontPtr);
      fontData=null;
      
      return new Font(outFontCollection.Families[outFontCollection.Families.Length-1], fontSize);
    }
  }
}


