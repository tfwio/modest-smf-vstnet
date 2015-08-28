using System;
using System.Drawing;
using System.Drawing.Text;
namespace System.Drawing.Text
{
	public class FontIndex
	{
		PrivateFontCollection LocalFontCollection {
			get;
			set;
		}

		System.Collections.Generic.List<string> KeyIndex {
			get;
			set;
		}

		Font FromKey(string Key, float fontSize, FontStyle style, GraphicsUnit graphicsUnit)
		{
			int lastindex = KeyIndex.IndexOf(Key);
			return lastindex == -1 ? null : new Font(LocalFontCollection.Families[lastindex], fontSize, style, graphicsUnit);
		}

		Font FromKey(string Key, float fontSize, FontStyle style)
		{
			int lastindex = KeyIndex.IndexOf(Key);
			return lastindex == -1 ? null : new Font(LocalFontCollection.Families[lastindex], fontSize, style);
		}

		Font FromKey(string Key, float fontSize)
		{
			int lastindex = KeyIndex.IndexOf(Key);
			return lastindex == -1 ? null : new Font(LocalFontCollection.Families[lastindex], fontSize);
		}

		public Font this[string Key, float size, FontStyle style, GraphicsUnit unit] {
			get {
				return FromKey(Key, size, style, unit);
			}
		}

		public Font this[string Key, float size, FontStyle style] {
			get {
				return FromKey(Key, size, style);
			}
		}

		public Font this[string Key, float size] {
			get {
				return FromKey(Key, size);
			}
		}
    
		public Font GetFont(string Key, float fontSize, FontStyle style=FontStyle.Regular, GraphicsUnit unit=GraphicsUnit.Point, byte gdiCharSet=1)
		{
			int lastindex = KeyIndex.IndexOf(Key);
			return new Font(LocalFontCollection.Families[lastindex],fontSize,style,unit,gdiCharSet);
		}
		
		public void AddFamily(System.IO.FileInfo file, string Key)
		{
			try {
//		    LocalFontCollection.AddFontFile(file.FullName);
				LocalFontCollection.AddFontResource(file);
				KeyIndex.Add(Key);
			}
			catch (Exception e) {
				throw e;
			}
		}

		public FontIndex()
		{
			LocalFontCollection = new PrivateFontCollection();
			KeyIndex = new System.Collections.Generic.List<string>();
//			KeyIndex.Add("Nothing");
		}

		~FontIndex()
		{
			LocalFontCollection.Dispose();
			LocalFontCollection = null;
			KeyIndex.Clear();
			KeyIndex = null;
		}
	}
}




