/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Drawing;
namespace ren_mbqt_layout
{
	public class AppControl
	{
		public FloatRect Bounds { get; set; }

		public string Text { get; set; }

		public double Value { get; set; }

		// this should attach to its parent
		bool IsActive { get; set; }

		bool NeedsPaint { get; set; }

		virtual public void Paint(Graphics g)
		{
		}
		
	}
}


