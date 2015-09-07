/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
	static class poop
	{
		static public bool IsIn(this int value, int min, int max)
		{
			if (value < min)
				return false;
			if (value > max)
				return false;
			return true;
		}
	}
}


