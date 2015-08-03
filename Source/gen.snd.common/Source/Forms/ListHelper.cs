using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using gen.snd.Formats;

namespace gen.snd.Forms
{
	static class ListHelper
	{
		static public void AddColumns(this ListView lv, params string[] columns)
		{
			lv.Columns.Clear();
			foreach (string str in columns) { lv.Columns.Add(str);  }
		}
		
		static public void lvcols(ref ListView lv, string[] columns)
		{
			lv.Columns.Clear();
			foreach (string str in columns) { lv.Columns.Add(str);  }
		}
		
		static public void lvsize(ref ListView lv, ColumnHeaderAutoResizeStyle style)
		{
			foreach (ColumnHeader ch in lv.Columns) ch.AutoResize(style);
		}
	}
}
