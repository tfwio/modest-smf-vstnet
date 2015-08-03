using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

using gen.snd.Vst.Module;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Interop.Host;

namespace gen.snd.Vst.Forms
{
	class PluginProgram: INotifyPropertyChanged
	{
		byte[] pgmBackup;
		IVstPluginContext Plugin;
		
		private VstCCPgm ActiveProgram {
			get { return activeProgram; }
		} VstCCPgm activeProgram;
		
		public int GetProgramId()
		{
			return Plugin.PluginCommandStub.GetProgram();
		}
		
		static public void Initialize(INaudioVstContainer owner, VstPlugin ctx)
		{
			PluginProgram pgm = new PluginProgram();
			pgm.Plugin = ctx;
			ctx.PluginCommandStub.SetBlockSize(512);
			owner.VstHost.ProcessIdle();
			pgm.PgmInit(owner,ctx,-1);
		}
		
		void PgmInit(INaudioVstContainer owner, VstPlugin Context, int selectedPgm)
		{
			if (selectedPgm!=-1) {
				VstCCPgm backupPgm = new VstCCPgm( Context, selectedPgm );
				pgmBackup = backupPgm.GetChunk(true);
			}
			Notify("Initialized");
//			foreach (VstCCPgm program in VstCCPgm.EnumPrograms(Context))
//				comboBox1.Items.Add(program);
//			if (selectedPgm != -1) comboBox1.SelectedIndex = selectedPgm;
	//			comboBox1.SelectedIndexChanged += new EventHandler(PgmChanged);
		}
		
		void Notify(string value) { if (PropertyChanged!=null) PropertyChanged(this,new PropertyChangedEventArgs(value)); }
		
		public void PgmChanged(object sender, EventArgs e)
		{
			activeProgram.Apply(Plugin); Notify("ProgramChanged");
		}
		
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
