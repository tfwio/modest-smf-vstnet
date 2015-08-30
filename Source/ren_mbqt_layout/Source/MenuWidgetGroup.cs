/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace ren_mbqt_layout
{
  
	public class MenuWidgetGroup : WidgetGroup
	{
	  
		public override void Initialize()
		{
			base.Initialize();
			Gap=0;
			Dock = DockStyle.Left;
			Bounds = new FloatRect(0,48,48,48);
			var DefaultBounds = new FloatRect(0, 48, 48, 48);
			var DPadding = new Padding(4);
			var awesome=Parent.FontIndex["awesome",18f];
			Widgets = new Widget[]
			{
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.Bars.ToString(), Bounds = DefaultBounds, Padding=DPadding, Font=awesome }, // bars
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.Music.ToString(), Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // h-bars
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.EllipsisV.ToString(), Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // v-bars
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.Play.ToString(), Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // fore
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.Backward.ToString(), Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // back
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.FolderOpen.ToString(), Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // folder-open
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.FolderOpenO.ToString(), Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // folder-open
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.Save.ToString(), Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // font
			};
			TopToBottom();
		}
    public override void Paint(PaintEventArgs arg)
    {
      using (var rgn = new Region(this.Bounds))
      {
        arg.Graphics.Clip = rgn;
        arg.Graphics.Clear(Color.Green);
        arg.Graphics.ResetClip();
      }
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




