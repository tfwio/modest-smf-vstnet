/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
  
	public class LeftMenuWidgetGroup : WidgetGroup
	{
	  
		public override void Initialize()
		{
			base.Initialize();
			Gap=0;
			Dock = DockStyle.Left;
			Bounds = new FloatRect(4,48,48,48);
			var DefaultBounds = new FloatRect(4, 48, 48, 48);
			var DPadding = new Padding(4);
			var awesome=Parent.FontIndex["awesome",18f];
			Widgets = new Widget[]
			{
			  new WidgetButton(Parent) { Text = FaStr.Bars, Bounds = DefaultBounds, Padding=DPadding, Font=awesome }, // bars
			  new WidgetButton(Parent) { Text = FaStr.Music, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // h-bars
			  new WidgetButton(Parent) { Text = FaStr.EllipsisV, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // v-bars
			  new WidgetButton(Parent) { Text = FaStr.Play, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // fore
			  new WidgetButton(Parent) { Text = FaStr.Backward, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // back
			  new WidgetButton(Parent) { Text = FaStr.FolderOpen, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // folder-open
			  new WidgetButton(Parent) { Text = FaStr.FolderOpenO, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // folder-open
			  new WidgetButton(Parent) { Text = FaStr.Save, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // font
			};
			TopToBottom();
		}
    public override void Paint(PaintEventArgs arg)
    {
//      using (var rgn = new Region(this.Bounds))
//      {
//        arg.Graphics.Clip = rgn;
//        arg.Graphics.Clear(Color.Green);
//        arg.Graphics.ResetClip();
//      }
      base.Paint(arg);
    }

		public override void DoLayout()
		{
			base.DoLayout();
			Height = Parent.Size.Height;
			TopToBottom();
		}

	}
}




