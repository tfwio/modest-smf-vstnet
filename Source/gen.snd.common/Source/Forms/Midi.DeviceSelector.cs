/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/15/2005
 * Time: 12:09 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using gen.snd.Interop;

namespace gen.snd.Forms
{

	/// <summary>
	/// Description of DeviceSelector.
	/// </summary>
	public class DeviceSelector : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ColumnHeader cTitle;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ColumnHeader cType;
		private System.Windows.Forms.ColumnHeader cUnk1;
		private System.Windows.Forms.ListView lvD;
		
		public DeviceSelector()
		{
			InitializeComponent();
			GetDevices();
		}
		void GetDevices()
		{
			List<MIDIOUTCAPS> list = new List<MIDIOUTCAPS>(new MIDI_DevCaps());
			foreach (MIDIOUTCAPS MC in list)
			{
				addlv(lvD, new string[] { MC.szPname, MC.wChannelMask.ToString("X"), MC.wTechnology.ToString() });
			}
		}
		
		#region Windows Forms Designer generated code
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent() {
			this.lvD = new System.Windows.Forms.ListView();
			this.cTitle = new System.Windows.Forms.ColumnHeader();
			this.cType = new System.Windows.Forms.ColumnHeader();
			this.cUnk1 = new System.Windows.Forms.ColumnHeader();
			this.button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lvD
			// 
			this.lvD.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
									this.cTitle,
									this.cType,
									this.cUnk1});
			this.lvD.Dock = System.Windows.Forms.DockStyle.Fill;
			this.lvD.Location = new System.Drawing.Point(0, 0);
			this.lvD.Name = "lvD";
			this.lvD.Size = new System.Drawing.Size(379, 117);
			this.lvD.TabIndex = 0;
			this.lvD.UseCompatibleStateImageBehavior = false;
			this.lvD.View = System.Windows.Forms.View.Details;
			// 
			// cTitle
			// 
			this.cTitle.Text = "Device Name";
			this.cTitle.Width = 179;
			// 
			// cType
			// 
			this.cType.Text = "Channel Mask";
			this.cType.Width = 59;
			// 
			// cUnk1
			// 
			this.cUnk1.Text = "Technology";
			this.cUnk1.Width = 39;
			// 
			// button1
			// 
			this.button1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.button1.Location = new System.Drawing.Point(0, 117);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(379, 24);
			this.button1.TabIndex = 1;
			this.button1.Text = "DONE";
			this.button1.Click += new System.EventHandler(this.Button1Click);
			// 
			// DeviceSelector
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(379, 141);
			this.Controls.Add(this.lvD);
			this.Controls.Add(this.button1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "DeviceSelector";
			this.Text = "DeviceSelector";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.DeviceSelectorClosing);
			this.ResumeLayout(false);
		}
		#endregion

		void addlv(ListView lvx, string[] ast)
		{
			ListViewItem lvi = new ListViewItem(ast[0]);
			for (int i=1;i<ast.Length;i++) lvi.SubItems.Add(ast[i]);
			lvx.Items.Add(lvi);
		}
		
		void Button1Click(object sender, System.EventArgs e)
		{
			this.Dispose();
		}
		
		void DeviceSelectorClosing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			
		}
		
		public int sid
		{
			get{
				return lvD.FocusedItem.Index-1;
			}
			set{
				//this.
			}
		}
	}
}
