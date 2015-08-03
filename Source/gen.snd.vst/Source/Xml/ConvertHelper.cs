/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using gen.snd;
using gen.snd.Vst;
using gen.snd.Vst.Module;
using gen.snd.Vst.Xml;
using Jacobi.Vst.Core;

namespace gen.snd.Vst.Xml
{
	static class ConvertHelper
	{
		static public int[] ToInt32Array(this string data)
		{
			List<int> list = new List<int>();
			foreach (string item in data.Split(','))
			{
				list.Add(int.Parse(item.Trim()));
			}
			return list.ToArray();
		}
		static public string ConvertIntData(int[] data)
		{
			if (data==null) return null;
			string[] strings = new string[data.Length];
			for (int d =0; d < data.Length; d++) strings[d] = data.ToString();
			string returned = string.Join(", ",strings);
			Array.Clear(strings,0,strings.Length);
			strings = null;
			return returned;
		}
		static public string ToCdfString(this int[] data)
		{
			return ConvertIntData(data);
		}
	}
}
