#region User/License
// oio * 7/31/2012 * 11:12 PM

// Permission is hereby granted, free of charge, to any person obtaining a
// copy of this software and associated documentation files (the "Software"),
// to deal in the Software without restriction, including without limitation
// the rights to use, copy, modify, merge, publish, distribute, sublicense,
// and/or sell copies of the Software, and to permit persons to whom the
// Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included
// in all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING
// FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER
// DEALINGS IN THE SOFTWARE.
#endregion

using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

using gen.snd.Vst.Forms;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Core.Plugin;
using Jacobi.Vst.Interop.Host;

namespace gen.snd.Vst.Module
{
	/// <summary>
	/// implements: IVstPluginContext
	/// </summary>
	public class VstPlugin : IVstPluginContext, IDisposable
	{
		public bool IgnoreMidiProgramChange {
			get { return ignoreMidiProgramChange; }
			set { ignoreMidiProgramChange = value; }
		} bool ignoreMidiProgramChange = false;
		
		#region iplugin
		
		public void idle(){ if (HasEditorDialog) PluginCommandStub.EditorIdle(); }
		public bool begin(){ return PluginCommandStub.BeginSetProgram(); }
		public bool end() { return PluginCommandStub.EndSetProgram(); }
		
		#endregion
		
		public bool IProvideEditor { get { return PluginInfo.Flags.HasFlag(VstPluginFlags.HasEditor); } }
		public bool IProvideProgramChunks { get { return PluginInfo.Flags.HasFlag(VstPluginFlags.ProgramChunks); } }
		
		static readonly byte[] Bmpty = new byte[0];
		
		public int MidiInputCount {
			get { return PluginCommandStub.GetNumberOfMidiInputChannels(); }
		}
		
		#region PROGRAM LIST

		List<VstCCPgm> programs = new List<VstCCPgm>();
		public void InitializePrograms()
		{
			programs.Clear(); programs.AddRange(VstCCPgm.EnumPrograms(this));
		}
		
		#endregion
		#region PROGRAM INDEX, SELECTION
		
		public int ProgramIndex { get { return context.PluginCommandStub.GetProgram(); } set { context.PluginCommandStub.SetProgram(value); } }
		
		// look for usage.
		public VstCCPgm ActiveProgram { get;set; }
		
		#endregion
		#region PROGRAM DATA
		byte[] pgmBackup;
		public byte[] PgmData
		{
			get
			{
				byte[] data = null;
				if (ActiveProgram!=null) {
					data = ActiveProgram.GetChunk(true);
				} else {
					ActiveProgram = new VstCCPgm(this,ProgramIndex);
					data = PgmData;
				}
				return data;
			}
			set { if (value!=Bmpty) TrySetData(value,false); }
		}
		#endregion
		#region PRESET DATA
		public byte[] PrsData
		{
			get
			{
				byte[] data = null;
				if (ActiveProgram!=null) {
					data = ActiveProgram.GetChunk(true);
				} else {
					var pgm = new VstCCPgm(this,ProgramIndex);
					data = pgm.GetChunk(true);
				}
				return data;
			}
			set { if (value!=Bmpty) TrySetData(value,true); }
		}
		#endregion
		
		// some methods require capability
		// not yet implemented.
		// • Plugin Supports ( pat & prs dumps )
		// • xml resource id=1 being used?
		
		#region PROGRAM ACTION
		
		public void ProgramAction(int pgm, params Action<int>[] methods)
		{
			context.PluginCommandStub.BeginSetProgram();
			foreach (var method in methods) method.Invoke(pgm);
			context.PluginCommandStub.EndSetProgram();
		}
		public void ProgramAction(int pgm, float value, Action<int,float> method)
		{
			context.PluginCommandStub.BeginSetProgram();
			method.Invoke(pgm,value);
			context.PluginCommandStub.EndSetProgram();
		}
		public void ProgramAction(int pgm, byte[] value, bool isPat, Action<int,byte[],bool> method)
		{
			begin();
			method.Invoke(pgm,value,isPat);
			end();
		}

		#endregion
		#region PROGRAM ACTION backup pat, prs
		
		void ProgramAction_BackupPrs(int pat)
		{
			var pgm = new VstCCPgm( this, (pat!=-1) ? pat : 0 );
			pgmBackup = pgm.GetChunk(true);
			pgm = null;
		}
		void ProgramAction_RestorePrs(int pat)
		{
			if (pgmBackup!=null)
				PluginCommandStub.SetChunk(pgmBackup,true);
		}
		
		void ProgramAction_BackupPgm(int selectedPgm)
		{
			var pgm = new VstCCPgm( this, (selectedPgm!=-1) ? selectedPgm : 0 );
			pgmBackup = pgm.GetChunk(false);
			pgm = null;
		}
		void ProgramAction_RestorePgm(int selectedPgm)
		{
			if (pgmBackup!=null)
				PluginCommandStub.SetChunk(pgmBackup,true);
		}
		
		#endregion
		#region PROGRAM SETTING

		public void ApplyPgm(int id, float value)
		{
			var pgm = new VstCCPgm(this,id);
			pgm.Stub.SetParameter(id,value);
			pgm = null;
			
		}
		void ProgramAction_SetValue(int id, float value)
		{
			var pgm = new VstCCPgm(this,id);
			pgm.SetParam(id,value);
		}
		void ProgramAction_SetChunk(int id, byte[] value, bool isPat)
		{
			var pgm = new VstCCPgm(this,id);
			if (value!=null && value != Bmpty) pgm.Apply(this);
		}
		
		public void TrySetData(byte[] value, bool isPreset)
		{
			ProgramAction(ProgramIndex,value,isPreset,ProgramAction_SetChunk);
		}
		
		void CreateContextIfNoneExists()
		{
			if (PluginCommandStub==null) CreateContext(PluginPath);
		}
		
		#endregion
		
		#region PROGRAM COMBO

		void Event_ComboBoxSelection(object sender, EventArgs e)
		{
			ActiveProgram = (sender as ComboBox).SelectedItem as VstCCPgm;
			ActiveProgram.Apply(this);
		}
		public void InitializeComboBox(int selectedPgm, ComboBox comboBox1 )
		{
			InitializePrograms();
			
			comboBox1.DataSource = programs;
			comboBox1.SelectedIndexChanged += new EventHandler(Event_ComboBoxSelection);
			if (selectedPgm != -1) comboBox1.SelectedIndex = selectedPgm;
		}

		#endregion
		
		//
		#region Main Properties
		
		VstPluginContext context;
		public INaudioVstContainer Host { get;set; }
		
		public List<int> OutputPin {
			get { return outputPin; }
		} List<int> outputPin = new List<int>();
		
		public string PluginPath { get; private set; }
		
		#endregion
		#region Title Info
		public string Title { get { return (!string.IsNullOrEmpty(effectname)) ? effectname : filetitle; } }
		public string Vendor { get { return (!string.IsNullOrEmpty(vendor)) ? vendor : filetitle; } }
		
		string effectname { get { return PluginCommandStub.GetEffectName(); } }
		string filetitle { get { return System.IO.Path.GetFileNameWithoutExtension(PluginPath); } }
		string vendor { get { return PluginCommandStub.GetVendorString(); } }
		#endregion
		#region IsOpen IsOn Open Close On Off IsInstrument
		
		public void On() { try { this.PluginCommandStub.MainsChanged(true); } catch { isOn=false; return; } isOn = true; }
		public void Off() { try { this.PluginCommandStub.MainsChanged(false); } catch {  } isOn = false; }
		public bool IsOn { get { return isOn; } set { isOn = value; } } bool isOn = false;
		
		public void Open() { try {context.PluginCommandStub.Open();} catch {} finally {isOpen = true;} }
		public void Close() { try {context.PluginCommandStub.Close();} catch {} finally {isOpen = false;}  }
		public bool IsOpen { get { return isOpen; } set { isOpen = value; } } bool isOpen = false;
		
		public bool IsInstrument
		{
			get { return PluginInfo.Flags.HasFlag(VstPluginFlags.IsSynth); }
		}
		
		#endregion
		
		public VstPlugin(string path, INaudioVstContainer host)
		{
			this.Host = host;
			CreateContext(path);
		}
		
		#region Context
		
		void DestroyContext()
		{
			if (this.context!=null)
			{
				if (HasEditorDialog) EditorDestroy();
				if (isOn) Off();
				if (isOpen) context.PluginCommandStub.Close();
				isOpen = false;
				try {
					context = null;
				} catch { MessageBox.Show("Error Disposing Plugin"); }
			}
		}
		void CreateContext(string path)
		{
			if (context!=null)
			{
				try{ context.PluginCommandStub.Close(); } catch {}
				try{ context.Dispose(); } catch {}
				try{ context = null; } catch {}
			}
			context = VstPluginContext.Create(path,Host.VstHost);
			PluginCommandStub.SetSampleRate( Convert.ToSingle(Host.VstPlayer.Settings.Rate) );
			Set("PluginPath", PluginPath = path);
			Set("HostCmdStub", Host.VstHost );
			if (context!=null) Open();
		}
		public void Renew()
		{
			DestroyContext();
			CreateContext(PluginPath);
		}

		#endregion
		
		#region EditorFrame
		
		public bool HasEditorDialog { get { return EditorDialog!=null; } } EditorFrame EditorDialog;
		
		public void EditorCreate()
		{
			EditorDestroy();
			EditorDialog = new EditorFrame(Host,this);
			EditorDialog.Show();
		}
		
		public void EditorDestroy()
		{
			if (HasEditorDialog) {
				EditorDialog.Close();
				EditorDialog.Dispose();
				EditorDialog = null;
			}
		}
		
		#endregion
		
		#region IVstPluginContext
		
		/// <inheritdoc />
		public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged
		{
			add { context.PropertyChanged += value; }
			remove { context.PropertyChanged -= value; }
		}
		/// <inheritdoc />
		public Jacobi.Vst.Core.Host.IVstHostCommandStub HostCommandStub { get { return context.HostCommandStub; } }
		/// <inheritdoc />
		public Jacobi.Vst.Core.Host.IVstPluginCommandStub PluginCommandStub { get { return context.PluginCommandStub; } }
		
    /// <inheritdoc />
		public VstPluginInfo PluginInfo { get { return context.PluginInfo; } set { context.PluginInfo = value; } }
		
    /// <inheritdoc />
		public void Set<T>(string keyName, T value) { context.Set<T>(keyName,value); }
		
    /// <inheritdoc />
		public T Find<T>(string keyName) { return context.Find<T>(keyName); }
		
    /// <inheritdoc />
		public void Remove(string keyName) { context.Remove(keyName); }
		
    /// <inheritdoc />
		public void Delete(string keyName) { context.Delete(keyName); }

		/// <inheritdoc />
		public void AcceptPluginInfoData(bool raiseEvents) { context.AcceptPluginInfoData(raiseEvents); }
		
		#endregion
		
		public void Dispose()
		{
			// try {
			// 	if (
      //     context!=null &&
      //     context.PluginCommandStub != null
      //     ) {
			// 		context.PluginCommandStub.Close();
			// 		context.Dispose();
			// 	}
			// }
			// catch{}
			context = null;
		}
		
	}
}
