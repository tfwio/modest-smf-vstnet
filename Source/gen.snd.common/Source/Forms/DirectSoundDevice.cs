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
	public class DirectSoundDevice : UserControl
	{
		public DirectSoundDevice()
		{
			InitializeComponent();
		}
		
		#region Designer
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		void InitializeComponent()
		{
			this.SuspendLayout();
			// 
			// DirectSoundDevice
			// 
			this.Name = "DirectSoundDevice";
			this.Size = new System.Drawing.Size(366, 189);
			this.ResumeLayout(false);
		}
		#endregion
	}
}
