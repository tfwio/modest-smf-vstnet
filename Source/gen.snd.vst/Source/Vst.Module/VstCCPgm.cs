/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Diagnostics;

using Jacobi.Vst.Core.Host;
using Jacobi.Vst.Core.Plugin;
using Bitset = on.smfio.SmfString;
using IPluginCommander = Jacobi.Vst.Core.Host.IVstPluginCommandStub;

namespace gen.snd.Vst.Module
{


	public class VstCCPgm
	{
		Stack<KeyValuePair<int, float>[]> history = new Stack<KeyValuePair<int, float>[]>();
		public Stack<KeyValuePair<int, float>[]> History {
			get { return history; }
		}
		
		public byte[] DataBnk, DataPgm;
		// pgm id, plugin
		VstPlugin plugin;
		
		#region Params
		
		/// i haven't tested throwing this call when a plugin is closed,
		/// or isnt running.
		public void SetParamAutomated(VstCCParam value)
		{
			try { plugin.HostCommandStub.SetParameterAutomated(value.ID,value.Value); }
			catch { throw; }
		}
		
		/// i haven't tested throwing this call when a plugin is closed,
		/// or isnt running.
		public void SetParam(VstCCParam value)
		{
			try { plugin.PluginCommandStub.SetParameter(value.ID,value.Value); }
			catch { throw; }
		}
		public void SetParam(int id, float value)
		{
			try { plugin.PluginCommandStub.SetParameter(id,value); }
			catch { throw; }
		}
		
		public VstCCParam this[int i] { get { return VstCCParam.Load(this,i); } }
		
		public IEnumerable<KeyValuePair<int,VstCCParam>> GetKeyedParameters()
		{
			for (int i = 0; i < plugin.PluginInfo.ParameterCount; i++)
				yield return new KeyValuePair<int,VstCCParam>(i,VstCCParam.Load(this,i));
		}
		public IEnumerable<KeyValuePair<int,float>> GetKeyValueParameters()
		{
			for (int i = 0; i < plugin.PluginInfo.ParameterCount; i++)
				yield return new KeyValuePair<int,float>(i, GetValue(i));
		}
		public IEnumerable<VstCCParam> GetParameters()
		{
			for (int i = 0; i < plugin.PluginInfo.ParameterCount; i++)
				yield return VstCCParam.Load(this,i);
		}
		
		float GetValue(int i) { return plugin.PluginCommandStub.GetParameter(i); }
		
		#endregion
		
		#region Internals
		VstPluginInfo info { get { return plugin.PluginInfo; } }
		int CountPrograms { get { return info.ProgramCount; } }
		public IPluginCommander Stub { get { return plugin.PluginCommandStub; } }
		#endregion
		
		#region Props
		public int ID  { get { return id; } private set { id = value; Stub.SetProgram(id); } } int id;
		public string Name { get { return name; } set { name = value; Stub.SetProgramName(name); } } string name;
		#endregion
		
		#region CHUNK
		
		public byte[] GetChunk(bool isPreset) { return Stub.GetChunk(isPreset); }
		/// <summary>
		/// 
		/// </summary>
		/// <param name="context">pugin</param>
		/// <param name="data">byte dump</param>
		/// <param name="isPresent">true if pgm</param>
		/// <returns></returns>
		public int SetChunk(IVstPluginContext context, byte[] data, bool isPreset) { return context.PluginCommandStub.SetChunk(data,isPreset); }
		
		#endregion
		
		#region APPLY
		/// set program or pattern data.
		/// <param name="isPat">true if data is pattern.  false if data is patch</param>
		/// <param name="data">binary dump</param>
		/// <param name="pluginContext">plugin</param>
		public void Apply(IVstPluginContext pluginContext, byte[] data, bool isPat)
		{
			SetChunk(pluginContext,data,true);
		}
		/// 
		/// <param name="pluginContext">plugin</param>
		public void Apply(IVstPluginContext pluginContext)
		{
			Apply(pluginContext,this.id);
		}
		/// .begin<br/>
		///   set(pgmid)<br/>
		///   ck=getchunk<br/>
		///   if (ck.NotNull) setchunk<br/>
		/// .end
		/// <param name="pluginContext">plugin</param>
		/// <param name="pgmId">the program we're modifying</param>
		public void Apply(IVstPluginContext pluginContext, int pgmId/*, byte[] data*/)
		{
			{
				plugin.begin();
				pluginContext.PluginCommandStub.BeginSetProgram();
				plugin.PluginCommandStub.SetProgram(pgmId);
				byte[] ck = GetChunk(true);
				// if we have program data, send it to the plugin
				if (ck!=null) Apply(pluginContext,ck,true);
				ck = null;
				plugin.end();
			}
			plugin.idle();
		}
		/// 
		public void Apply() { Apply(plugin); }
		#endregion
		
		public VstCCPgm(VstPlugin plugin, int id)
		{
			this.plugin = plugin;
//			begin();
			ID = id;
			Name = Stub.GetProgramName();
//			end();
		}
		
		static public IEnumerable<VstCCPgm> EnumPrograms(VstPlugin ctx)
		{
			for (int i = 0; i < ctx.PluginInfo.ProgramCount; i++)
				yield return new VstCCPgm( ctx, i );
		}
		
	}
}
