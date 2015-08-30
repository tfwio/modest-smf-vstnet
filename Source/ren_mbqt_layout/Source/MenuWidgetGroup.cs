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
  
  public struct FaChar
  {
    public char CharValue { get; set; }
    public string Key { get; set; }
    public FaChar(char CharValue, string Key)
    {
      this.CharValue = CharValue;
      this.Key = Key;
    }
    public FaChar(FontAwesome CharValue, string Key)
    {
      this.CharValue = (char)CharValue;
      this.Key = Key;
    }
    public FaChar(uint CharValue, string Key)
    {
      this.CharValue = (char)CharValue;
      this.Key = Key;
    }
    public FaChar(KeyValuePair<string,string> KeyValue)
    {
      this.Key = KeyValue.Key;
      this.CharValue = KeyValue.Value[0];
    }
    static public implicit operator KeyValuePair<string,string>(FaChar input) { return input.CharValue.ToString(); }
    static public implicit operator string(FaChar input) { return input.CharValue.ToString(); }
    static public implicit operator char(FaChar input) { return input.CharValue; }
    
  }
	public class MenuWidgetGroup : WidgetGroup
	{
	  
		public override void Initialize()
		{
			base.Initialize();
			Gap=0;
			Dock = DockStyle.Left;
			Bounds = new FloatRect(0,0,64,Parent.Size.Height);
			var DefaultBounds = new FloatRect(0, 0, 48, 48);
			var DPadding = new Padding(4);
			var awesome=Parent.FontIndex["awesome",18f];
			Widgets = new Widget[]
			{
			  new ButtonWidget(Parent) { Text = FontAwesomeChar.Bars, Bounds = DefaultBounds, Padding=DPadding, Font=awesome }, // bars
			  new ButtonWidget(Parent) { Text = Awesome["h-bars"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // h-bars
			  new ButtonWidget(Parent) { Text = Awesome["v-bars"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // v-bars
			  new ButtonWidget(Parent) { Text = Awesome["fore"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // fore
				new ButtonWidget(Parent) { Text = Awesome["back"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // back
				new ButtonWidget(Parent) { Text = Awesome["folder-open"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // folder-open
				new ButtonWidget(Parent) { Text = Awesome["folder-open-o"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // folder-open
				new ButtonWidget(Parent) { Text = Awesome["font"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // font
				new ButtonWidget(Parent) { Text = Awesome["forward"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // forward
				new ButtonWidget(Parent) { Text = Awesome["keyboard"], Bounds = DefaultBounds.Clone(), Padding=DPadding, Font=awesome }, // keyboard
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




