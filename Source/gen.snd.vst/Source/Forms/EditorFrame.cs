using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using gen.snd.Vst.Module;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;

namespace gen.snd.Vst.Forms
{
	/// <summary>
	/// The frame in which a custom plugin editor UI is displayed.
	/// </summary>
	public partial class EditorFrame : Form
	{
		#region Fields
		INaudioVstContainer VstContainer { get;set; }
		VstPlugin Context { get; set; }
		IVstHostCommandStub host { get { return Context.HostCommandStub; } }
		IVstPluginCommandStub plugin { get { return Context.PluginCommandStub; } }
		Control pluginUI { get;set; }
		
		object locker = new object();
		
		Keys KeysOn;
		public int Octave { get { return octave; } set { octave = value; } } int octave = 4;
		#endregion
		
		#region Key Input
		
		List<Keys> KeysSent = new List<Keys>();
		
		byte GetKey(Keys keyin) {
			byte value= Convert.ToByte(NAudioVstContainer.KMap[keyin] + (octave * 12));
			double val = 120;
			try {
				val = host.GetTimeInfo(VstTimeInfoFlags.TempoValid).Tempo;
			} catch {
				val = 0;
			}
			Text = string.Format("Time: {1}, Key: {0}",value,val);
			return Convert.ToByte(NAudioVstContainer.KMap[keyin] + (octave * 12));
		}
		void SendOn(Keys keyin)
		{
			try {
				plugin.ProcessEvents( new VstEvent[]{new VstMidiEvent(0,0,0,new byte[]{ 0x90,GetKey(keyin),0xFF },0,0)} );
			} catch {
			} finally {
				KeysSent.Add(keyin);
			}
		}
		void SendOff(Keys keyin)
		{
			try {
				plugin.ProcessEvents( new VstEvent[]{new VstMidiEvent(0,0,0,new byte[]{ 0x80,GetKey(keyin),0xFF },0,0)} );
			} catch {
			} finally {
				KeysSent.Remove(keyin);
			}
		}

		List<Keys> KeysMapped(Keys key)
		{
			List<Keys> list = new List<Keys>();
			foreach (Keys k in NAudioVstContainer.KMap.Keys) if (k==key) list.Add(k);
			if (list.Count==0) return null;
			return list;
		}

		protected void KeyDown(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyData.HasFlag(Keys.W)) { Close(); return; }
			base.OnKeyDown(e);
			List<Keys> list = KeysMapped(e.KeyCode);
			if (list==null) return;
			int i = 0;
			foreach (Keys k in list)
			{
				if (KeysSent.Contains(k)) { i++; continue; }
				SendOn(k);
				i++;
			}
		}
		protected void KeyUp(object sender, KeyEventArgs e)
		{
			base.OnKeyUp(e);
			List<Keys> list = KeysMapped(e.KeyCode);
			if (list==null) return;
			int i = 0;
			foreach (Keys k in list)
			{
				if (!KeysSent.Contains(k)) { i++; continue; }
				SendOff(k);
				i++;
			}
		}
		void AddKeyEvent(Control ctl)
		{
			ctl.KeyDown += KeyDown;
			ctl.KeyUp += KeyUp;
		}
		
		#endregion
		
		#region Constructor
		/// <summary>
		/// Default ctor.
		/// </summary>
		public EditorFrame()
		{
			InitializeComponent();
			pluginUI = panel2;
			octaveUpDown.Value = Octave;
			octaveUpDown.ValueChanged += Event_ChangeOctave;
			AddKeyEvent(pluginUI);
			AddKeyEvent(panel2);
			AddKeyEvent(button1);
			comboBox1.DisplayMember = "Name";
		}
		
		public EditorFrame(INaudioVstContainer owner, VstPlugin ctx) : this()
		{
			Context = ctx;
			VstContainer = owner;
//			plugin.SetBlockSize(512);
			Context.InitializeComboBox(-1,comboBox1);
		}

		#endregion
		
		#region Form Overrides
		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);
			Init();
			VstContainer.VstPlayer.BufferCycle -= HandleProcessed;
			VstContainer.VstPlayer.BufferCycle += HandleProcessed;
		}

		/// <summary>
		/// Shows the custom plugin editor UI.
		/// </summary>
		/// <param name="owner"></param>
		/// <returns></returns>
		public new DialogResult ShowDialog(IWin32Window owner)
		{
			Init();
			return base.ShowDialog(owner);
		}
		protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
		{
			base.OnClosing(e);
			if (e.Cancel == false)
			{
				VstContainer.VstPlayer.BufferCycle -= HandleProcessed;
				plugin.EditorClose();
			}
		}
		#endregion
		
		#region Support Methods
		void Init()
		{
			this.Text = plugin.GetEffectName();
			if (!plugin.PluginContext.PluginInfo.Flags.HasFlag(VstPluginFlags.IsSynth))
			{
				button1.Visible = false;
				lblOct.Visible = false;
				octaveUpDown.Visible = false;
			}
			EditorResize();
		}
		
		void EditorResize()
		{
			if (plugin.PluginContext.PluginInfo.Flags.HasFlag(VstPluginFlags.HasEditor))
			{
				plugin.EditorOpen(pluginUI.Handle);
				plugin.EditorIdle();
				Rectangle wndRect = Rectangle.Empty;
				if (plugin.EditorGetRect(out wndRect)) SetFSize(wndRect);
			}
			else this.Size = this.SizeFromClientSize(pluginUI.Size);
			plugin.EditorIdle();
		}
		
		void SetFSize(Rectangle rect)
		{
			this.Size = this.SizeFromClientSize(new Size(rect.Width, rect.Height+panel2.Height));
			panel2.Size = this.SizeFromClientSize(new Size(rect.Width, rect.Height));
		}
		
		void NoteThrow()
		{
			List<VstMidiEvent> events = new List<VstMidiEvent>();
			bool realtime = false;
			lock (locker)
			{
				events.Add( new VstMidiEvent(    0,0,0,new byte[]{ 0x90,0x30,0xFF,0 },0,0,realtime));
				events.Add( new VstMidiEvent(96000,0,0,new byte[]{ 0x80,0x30,0x00,0 },0,0,realtime));
				plugin.ProcessEvents(events.ToArray());
			}
			events.Clear();
			events = null;
		}

		#endregion
		
		#region Event Handlers
		
		// no longer supported (still wired in)
		void Event_ChangeOctave(object sender, EventArgs e) { button1.Focus(); Octave=(int)octaveUpDown.Value; }
		
		// Important
		void HandleProcessed(object obj, NAudioVSTCycleEventArgs e)
		{
			plugin.EditorIdle();
		}
		
		// not used
		void Event_PluginCalled(object sender, PluginCalledEventArgs e)
		{
			switch (e.Message)
			{
				case "SizeWindow()":
					Rectangle rect;
					/*if (plugin.EditorGetRect(out rect)) */
					SetFSize((Rectangle)e.Data);
					host.ProcessIdle();
					break;
				case "UpdateDisplay()":
					panel2.Invalidate();
					host.ProcessIdle();
					break;
			}
		}
		// not used
		void Button2Click(object sender, EventArgs e)
		{
			Action nt = NoteThrow;
			nt.Invoke();
		}

		#endregion
		
	}
}
