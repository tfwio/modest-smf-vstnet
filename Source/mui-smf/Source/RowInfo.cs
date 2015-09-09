/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
using System.Linq;
namespace mui_smf
{
	class RowInfo
	{
		// Default Integer (Key-row)
		public int I {
			get;
			set;
		}

		// Offset Integer
		public int IO {
			get;
			set;
		}

		// Reversed Integer
		public int R {
			get;
			set;
		}

		// Reversed Integer
		public int RO {
			get;
			set;
		}

		public bool IsIvory {
			get;
			set;
		}

		// For drawing
		public bool CanDo {
			get;
			set;
		}

		public float Top {
			get;
			set;
		}

		public Point[] Line {
			get;
			set;
		}
	}
}




