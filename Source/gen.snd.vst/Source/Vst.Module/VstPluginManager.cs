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
using System.ComponentModel;
using System.Linq;

using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;

namespace gen.snd.Vst.Module
{

	public class VstPluginManager : INotifyPropertyChanged
	{
		const string config_file = "settings.cfg";

		void OnPropertyChanged(string key)
		{
			if (PropertyChanged!=null) PropertyChanged(this,new PropertyChangedEventArgs(key));
			if (key=="AddPlugin")    OnPluginListRefreshed();
			if (key=="RemovePlugin") OnPluginListRefreshed();
		}

		#region PLUGINS
		public List<VstPlugin> Plugins {
			get { return _plugins; }
			set { _plugins = value; OnPropertyChanged("Plugins"); }
		} List<VstPlugin> _plugins = new List<VstPlugin>();
		
		public IEnumerable<VstPlugin> VstInstruments {
			get { return Plugins.Where(p=> p.IsInstrument).OrderBy(p=>p.Title); }
		}
		public IEnumerable<VstPlugin> VstEffects {
			get { return Plugins.Where( p=> !p.IsInstrument).OrderBy(p=> p.Title) ; }
		}
		public List<string> PluginsList {
			get { return pluginsList; }
			set { pluginsList = value; OnPropertyChanged("PluginsList"); }
		} List<string> pluginsList = new List<string>();
		#endregion

		#region MODULES
		
		public List<VstPlugin> GeneratorModules {
			get { return moduleGenerator; }
			set { moduleGenerator = value; }
		} List<VstPlugin> moduleGenerator = new List<VstPlugin>();
		
		public List<VstPlugin> EffectModules {
			get { return moduleEffect; }
			set { moduleEffect = value; }
		} List<VstPlugin> moduleEffect = new List<VstPlugin>();
		
		#endregion

		#region MODULE SELECTED
		
		/// <summary>
		/// The currently selected list item
		/// </summary>
		public VstPlugin ActivePlugin {
			get { return activePlugin; }
			set {
				if (value==null)
				{
					activePlugin = null;
					return;
				}
				if (value.PluginInfo.Flags.HasFlag(VstPluginFlags.IsSynth)) MasterPluginInstrument = activePlugin = value;
				else MasterPluginEffect = activePlugin = value;
				OnPropertyChanged("ActivePlugin"); }
		} VstPlugin activePlugin;
		#endregion
		#region MODULE INSTRUMENT
		/// <summary>
		/// The currently selected list item
		/// </summary>
		public VstPlugin MasterPluginInstrument {
			get { return masterPluginInstrument; }
			set { masterPluginInstrument = value; OnPropertyChanged("MasterPluginInstrument"); }
		} VstPlugin masterPluginInstrument;
		
		/// <summary>
		/// The currently selected list item
		/// </summary>
		public VstPlugin MasterPluginEffect {
			get { return masterPluginEffect; }
			set { masterPluginEffect = value; OnPropertyChanged("MasterPluginEffect"); }
		} VstPlugin masterPluginEffect;
		#endregion
		#region MODULE INIT
		void InitializeModules()
		{
			GeneratorModules.Clear();
			if (masterPluginInstrument!=null) GeneratorModules.Add(masterPluginInstrument);
			
			EffectModules.Clear();
			if (masterPluginEffect!=null) EffectModules.Add(masterPluginEffect);
		}
		#endregion
		
		#region CONFIG
		/// <summary>This should be the ‘PluginCollector’ config file.</summary>
		public string CurrentConfigurationFile {
			get { return currentConfigurationFile; }
			set { currentConfigurationFile = value; LoadConfigurationFile(value); OnPropertyChanged("CurrentConfigurationFile"); }
		} string currentConfigurationFile;

		#endregion
		
		#region (private Host access)
		public INaudioVstContainer Parent { get; set; }
		IVstHostCommandStub Host { get { return Parent.VstHost; } }
		IVstPluginContext Plugin { get { return Parent.VstHost.PluginContext; } }
		bool HasPlugin { get { return Plugin != null; } }
		#endregion
		
		#region List Methods
		
		/// <summary>
		/// Bare in mind that each time you add a plugin, all
		/// loaded contexts are dropped and reloaded.
		/// </summary>
		/// <param name="pluginPath"></param>
		public void AddPlugin(params string[] pluginPath)
		{
			foreach (string path in pluginPath)
			{
				VstPlugin ctx;
				try{
					ctx = OpenPlugin(path);
				} catch {
					continue;
				}
				if ( ctx != null && !HasPluginPath(path) )
				{
					_plugins.Add(ctx);
					OnPropertyChanged("AddPlugin");
				}
			}
		}

		bool HasPluginPath(string path)
		{
			foreach (VstPlugin p in Plugins)
				if (p.PluginPath==path) return true;
			return false;
		}

		public void RemovePlugin(VstPlugin ctx)
		{
			if ( ctx != null )
			{
                string path = ctx.PluginPath; // checkme
                ctx.PrsData = null; // checkme
                ctx.Dispose();
                pluginsList.Remove(path); // checkme
				_plugins.Remove(ctx);
				OnPropertyChanged("RemovePlugin");
			}
		}
		
		#endregion
		
		#region event: PluginListRefreshed
		
		/// <summary>
		/// Triggered when you add, remove or otherwise clear the plugin-list.
		/// </summary>
		public event EventHandler PluginListRefreshed;
		protected virtual void OnPluginListRefreshed() {
			if (PluginListRefreshed != null) PluginListRefreshed(this, EventArgs.Empty);
		}

		#endregion
		
		public VstPluginManager(INaudioVstContainer container)
		{
			Parent = container;
		}
		
		#region Public Methods
		
		public VstPlugin OpenPlugin(string pluginPath)
		{
			return OpenPlugin(pluginPath, false);
		}
		
		public VstPlugin OpenPlugin(string pluginPath, bool ignoreAdd)
		{
			bool haserror = false;
			VstPlugin ctx = null;
			try {
				ctx = new VstPlugin(pluginPath,Parent);
			} catch (Exception xx) {
			  haserror = true;
//			  System.Diagnostics.Debug.Print("{0}",xx);
//				System.Windows.Forms.MessageBox.Show(
//					xx.ToString(), "VstPluginManager", System.Windows.Forms.MessageBoxButtons.OK,
//					System.Windows.Forms.MessageBoxIcon.Error
//				);
			  Console.Out.WriteLine("Had an error loading");
			}
			if (!ignoreAdd && !haserror) PluginsList.Add(pluginPath);
			if (ctx!=null) ctx.PluginCommandStub.Open();
			return ctx;
		}
		
		/// <summary>
		/// Triggered when the active plugin is reset (or changes?)
		/// </summary>
		public event EventHandler ActivePluginReset;
		
		public void ReloadActivePlugin()
		{
			if (ActivePlugin!=null)
			{
				ActivePlugin.Renew();
				if (ActivePluginReset!=null) ActivePluginReset(null,null);
			}
		}
		
		public void ReleaseAllPlugins()
		{
			ReleaseAllPlugins(false);
		}
		
		/// <summary>
		/// Unload (and Dispose) each loaded plugin.
		/// </summary>
		public void ReleaseAllPlugins(bool andRefreshList)
		{
			foreach (VstPlugin ctx in _plugins) {
				ctx.Close();
				ctx.Dispose(); //ctx = null;
			}
			_plugins.Clear();
			if (andRefreshList) OnPluginListRefreshed();
		}
		
		/// <summary>
		/// Unload each plugin, then reload all plugins.
		/// </summary>
		public void ResetPluginList()
		{
			ReleaseAllPlugins();
			pluginsList.Reverse();
			Stack<string> newlist = new Stack<string>(pluginsList);
			pluginsList.Reverse();
			while (newlist.Count > 0)
			{
				string name = newlist.Pop();
				VstPlugin ctx = OpenPlugin(name,true);
				if (ctx != null) _plugins.Add(ctx);
			}
			newlist = null;
			// trigger update event
			OnPluginListRefreshed();
		}
		
		/// <summary>
		/// Read a configuration file and load it into our
		/// active set of PluginContexts.
		/// </summary>
		/// <remarks>Note that all we are doing here
		/// is setting the name of the configuration file
		/// as doing so initializes (reads) the file using the
		/// <see cref="LoadConfigurationFile" /> method.
		/// </remarks>
		/// <param name="file">if no file, defaults to 'settings.cfg'</param>
		public void Read(string file = config_file)
		{ CurrentConfigurationFile = file; }
		
		public void LoadConfigurationFile(string file)
		{
			pluginsList.Clear();
			if (!System.IO.File.Exists(config_file)) return;
			pluginsList = PluginCollector.Read(file);
			ResetPluginList();
		}
		
		/// <summary>
		/// This method should generally be ignored.
		/// </summary>
		/// <param name="file"></param>
		public void Append(string file = config_file)
		{
			foreach (string pigin in PluginCollector.Read(file).ToArray())
				if (!pluginsList.Contains(pigin)) pluginsList.Add(pigin);
			ResetPluginList();
		}
		/// <summary>
		/// Writes all loaded plugin paths to the configuration file.
		/// </summary>
		/// <param name="file"></param>
		public void Write(string file = config_file) {
			PluginCollector.Write(file,this.pluginsList.ToArray());
		}

		#endregion
		
		/// <summary>
		/// Currently, AddPlugin and RemovePlugin trigger the PluginListRefreshed event.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;
	}
}
