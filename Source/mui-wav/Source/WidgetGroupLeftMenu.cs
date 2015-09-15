/* oio * 8/3/2015 * Time: 6:39 AM
 */
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Mui;
using Mui.Widgets;
namespace mui_wav
{
  
	public class WidgetGroupLeftMenu : WidgetGroup
	{
	  public WidgetButton BtnLoadMidi { get; set; }
    
    public override void Design()
    {
			Gap=0;
			Dock = DockStyle.Left;
			Bounds = new FloatRect(4,48,48,48);
			var DefaultBounds = new FloatRect(4, 48, 32, 32);
			var DPadding = new Padding(4);
      var awesome = Parent.FontIndex.GetFont("awesome", 19f, FontStyle.Regular, GraphicsUnit.Pixel);
      Widgets = new Widget[]
			{
			  new WidgetButton(Parent) { Text = FontAwesome.Bars, Bounds = DefaultBounds, Padding=DPadding, Font=awesome, Smoother=true }, // bars
        BtnLoadMidi = new WidgetButton(Parent) { Text = FontAwesome.Music, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome, Smoother=true }, // h-bars
			  new WidgetButton(Parent) { Text = FontAwesome.EllipsisV, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome, Smoother=true }, // v-bars
			  new WidgetButton(Parent) { Text = FontAwesome.Play, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome, Smoother=true }, // fore
			  new WidgetButton(Parent) { Text = FontAwesome.Backward, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome, Smoother=true }, // back
			  new WidgetButton(Parent) { Text = FontAwesome.FolderOpen, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome, Smoother=true }, // folder-open
			  new WidgetButton(Parent) { Text = FontAwesome.FolderOpenO, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome, Smoother=true }, // folder-open
			  new WidgetButton(Parent) { Text = FontAwesome.Save, Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome, Smoother=true }, // font
			};
    }
	  
		public override void Initialize(IMui app, Widget client)
		{
			base.Initialize(app,client);

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




