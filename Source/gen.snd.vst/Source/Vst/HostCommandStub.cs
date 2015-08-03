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
using System.Drawing;
using System.IO;

using gen.snd.Vst.Module;
using Jacobi.Vst.Core;
using Jacobi.Vst.Core.Host;

namespace gen.snd.Vst
{
	/// <summary>
	/// The HostCommandStub class represents the part of the host that a plugin can call.
	/// </summary>
	public class HostCommandStub : IVstHostCommandStub
	{
		const double billion = 1000000000;
		const double billionth = 1 / billion; // 0.000000001
		
		/// <summary>
		/// Main timing calculator.
		/// </summary>
		SampleClock st = new SampleClock();
		
		// track this to the timeinfo function
		static readonly Jacobi.Vst.Core.VstSmpteFrameRate smpte_rate = VstSmpteFrameRate.Smpte30fps;
		
		public INaudioVST Parent { get; private set; }
		
		public HostCommandStub(INaudioVST parent)
		{
			Parent = parent;
		}
		
		#region (Static CTOR and) File Dialogs
		/// <summary>
		/// Initialize common dialogs.  I'm not quite sure how to implement these if if they should even be.
		/// </summary>
		static HostCommandStub()
		{
			ofd = new System.Windows.Forms.OpenFileDialog();
			sfd = new System.Windows.Forms.SaveFileDialog();
			fbd = new System.Windows.Forms.FolderBrowserDialog();
		}
		
		static System.Windows.Forms.OpenFileDialog ofd;
		static System.Windows.Forms.SaveFileDialog sfd;
		static System.Windows.Forms.FolderBrowserDialog fbd;
		#endregion
		
		#region PluginCalledEventArgs/Handler
		
		/// <summary>
		/// Raised when one of the methods is called.
		/// </summary>
		public event EventHandler<PluginCalledEventArgs> PluginCalled;
		
		private void RaisePluginCalled(string message)
		{
			RaisePluginCalled(message,null);
		}
		private void RaisePluginCalled(string message, object data)
		{
			EventHandler<PluginCalledEventArgs> handler = PluginCalled;
			if(handler != null)
			{
				handler(this, new PluginCalledEventArgs(message,data));
			}
		}
		#endregion
		
		#region IVstHostCommandsStub Members
		
		/// <inheritdoc />
		
		public IVstPluginContext PluginContext {
			get { return pluginContext; }
			set { pluginContext = value; }
		} IVstPluginContext pluginContext;
		
		#endregion
		
		#region IVstHostCommands20 Members
		
		/// <inheritdoc />
		public bool BeginEdit(int index)
		{
			RaisePluginCalled("BeginEdit(" + index + ")");
			
			return false;
		}
		
		/// <inheritdoc />
		public Jacobi.Vst.Core.VstCanDoResult CanDo(string cando)
		{
			RaisePluginCalled("CanDo(" + cando + ")");
			return Jacobi.Vst.Core.VstCanDoResult.Unknown;
		}
		
		/// <inheritdoc />
		public bool CloseFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
		{
			RaisePluginCalled("CloseFileSelector(" + fileSelect.Command + ")");
			return false;
		}
		
		/// <inheritdoc />
		public bool EndEdit(int index)
		{
			
			RaisePluginCalled("EndEdit(" + index + ")");
			return false;
		}
		
		/// <inheritdoc />
		public Jacobi.Vst.Core.VstAutomationStates GetAutomationState()
		{
			RaisePluginCalled("GetAutomationState()");
			Console.WriteLine("GetAutomationState: {0}",Jacobi.Vst.Core.VstAutomationStates.ReadWrite);
			return Jacobi.Vst.Core.VstAutomationStates.ReadWrite;
//			return Jacobi.Vst.Core.VstAutomationStates.ReadWrite;
//			return Jacobi.Vst.Core.VstAutomationStates.Off;
		}
		
		/// <inheritdoc />
		public int GetBlockSize()
		{
			RaisePluginCalled("GetBlockSize()");
			return (int)Parent.CurrentSampleLength;
		}
		
		/// <inheritdoc />
		public string GetDirectory()
		{
			string path = string.Empty;
			FileInfo di = new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location);
			path=di.Directory.FullName;
			RaisePluginCalled("GetDirectory()");
			return path;
		}
		
		/// <inheritdoc />
		public int GetInputLatency()
		{
			RaisePluginCalled("GetInputLatency()");
			return 0;
		}
		
		/// <inheritdoc />
		public Jacobi.Vst.Core.VstHostLanguage GetLanguage()
		{
			RaisePluginCalled("GetLanguage()");
			return Jacobi.Vst.Core.VstHostLanguage.NotSupported;
		}
		
		/// <inheritdoc />
		public int GetOutputLatency()
		{
			RaisePluginCalled("GetOutputLatency()");
			return 0;
		}
		
		/// <inheritdoc />
		public Jacobi.Vst.Core.VstProcessLevels GetProcessLevel()
		{
			RaisePluginCalled("GetProcessLevel()");
//			return Jacobi.Vst.Core.VstProcessLevels.User;
//			return Jacobi.Vst.Core.VstProcessLevels.Realtime;
			return Jacobi.Vst.Core.VstProcessLevels.Realtime;
//			return Jacobi.Vst.Core.VstProcessLevels.Unknown;
		}
		
		/// <inheritdoc />
		public string GetProductString()
		{
			RaisePluginCalled("GetProductString()");
			return "VST.NET\0";
		}
		
		/// <inheritdoc />
		public float GetSampleRate()
		{
			RaisePluginCalled("GetSampleRate()");
			return Convert.ToSingle(Parent.Settings.Rate);
		}
		
		/// <inheritdoc />
		public Jacobi.Vst.Core.VstTimeInfo GetTimeInfo(Jacobi.Vst.Core.VstTimeInfoFlags filterFlags)
		{
			RaisePluginCalled("GetTimeInfo(" + filterFlags + ")");
			// our sample calculator performs:
			// Create TimeInfo class
			VstTimeInfo vstTimeInfo = new VstTimeInfo();
			// most common settings
			vstTimeInfo.SamplePosition				= Parent.SampleOffset + Parent.BufferIncrement;
			vstTimeInfo.SampleRate						= Parent.Settings.Rate;
			// 
			filterFlags |= VstTimeInfoFlags.ClockValid;
			if (filterFlags.HasFlag(VstTimeInfoFlags.ClockValid))
			{
				int cp = Convert.ToInt32(
					st.SolvePPQ( Parent.SampleOffset, Parent.Settings ).ClocksAtPosition
				);
				vstTimeInfo.SamplesToNearestClock = st
					.SolveSamples( cp * 24 , Parent.Settings ).Samples32Floor;
			}
			// NanoSecondsValid
			filterFlags |= VstTimeInfoFlags.NanoSecondsValid;
			if (filterFlags.HasFlag(VstTimeInfoFlags.NanoSecondsValid))
			{
				vstTimeInfo.NanoSeconds = (Parent.SampleOffset / Parent.Settings.Rate) * billionth;
			}
			// TempoValid
			filterFlags |= VstTimeInfoFlags.TempoValid;
			if (filterFlags.HasFlag(VstTimeInfoFlags.TempoValid))
			{
				vstTimeInfo.Tempo = Parent.Settings.Tempo;
			}
			// PpqPositionValid
			filterFlags |= VstTimeInfoFlags.PpqPositionValid;
			if (filterFlags.HasFlag(VstTimeInfoFlags.PpqPositionValid))
			{
				vstTimeInfo.PpqPosition	= st.SolvePPQ( vstTimeInfo.SamplePosition, Parent.Settings).Frame;
			}
			// BarStartPositionValid
			filterFlags |= VstTimeInfoFlags.BarStartPositionValid;
			if (filterFlags.HasFlag(VstTimeInfoFlags.BarStartPositionValid))
			{
				vstTimeInfo.BarStartPosition = st.SolvePPQ(vstTimeInfo.SamplePosition, Parent.Settings).Pulses; // * st.SamplesPerQuarter
			}
			// CyclePositionValid
			filterFlags |= VstTimeInfoFlags.CyclePositionValid;
			if (filterFlags.HasFlag(VstTimeInfoFlags.CyclePositionValid))
			{
				vstTimeInfo.CycleStartPosition = Parent.SampleOffset;//st.SolvePPQ(Parent.SampleOffset,Parent.Settings).Frame;
				vstTimeInfo.CycleEndPosition = Parent.SampleOffset+Parent.CurrentSampleLength;
			}
			// TimeSignatureValid
			if (filterFlags.HasFlag(VstTimeInfoFlags.TimeSignatureValid))
			{
				vstTimeInfo.TimeSignatureNumerator = Parent.Settings.TimeSignature.Numerator;
				vstTimeInfo.TimeSignatureDenominator = Parent.Settings.TimeSignature.Denominator;
			}
			// SmpteValid
			if (filterFlags.HasFlag(VstTimeInfoFlags.SmpteValid))
			{
				vstTimeInfo.SmpteFrameRate = smpte_rate; /* 30 fps no-drop */
				vstTimeInfo.SmpteOffset	= 0;
				/* not quite valid */
			}
			vstTimeInfo.Flags = filterFlags;
			return vstTimeInfo;
		}
		
		/// <inheritdoc />
		public string GetVendorString()
		{
			RaisePluginCalled("GetVendorString()");
			return "Jacobi Software"+(char)0;
		}
		
		/// <inheritdoc />
		public int GetVendorVersion()
		{
			RaisePluginCalled("GetVendorVersion()");
			return 1000;
		}
		
		/// <inheritdoc />
		public bool IoChanged()
		{
			RaisePluginCalled("IoChanged()");
			return false;
		}
		
		/// <inheritdoc />
		public bool OpenFileSelector(Jacobi.Vst.Core.VstFileSelect fileSelect)
		{
			switch(fileSelect.Command)
			{
				case VstFileSelectCommand.DirectorySelect:
					{
						if (!string.IsNullOrEmpty(fileSelect.InitialPath) && System.IO.Directory.Exists(fileSelect.InitialPath)) fbd.SelectedPath = fileSelect.InitialPath;
						if (!string.IsNullOrEmpty(fileSelect.Title)) fbd.Description = fileSelect.Title;
						if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK) fileSelect.ReturnPaths = new string[]{ fbd.SelectedPath };
					} break;
				case VstFileSelectCommand.FileLoad:
					{
						if (!string.IsNullOrEmpty(fileSelect.InitialPath) && System.IO.Directory.Exists(fileSelect.InitialPath)) ofd.InitialDirectory = fileSelect.InitialPath;
						if (!string.IsNullOrEmpty(fileSelect.Title)) ofd.Title = fileSelect.Title;
						if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) fileSelect.ReturnPaths = new string[]{ ofd.FileName };
					} break;
				case VstFileSelectCommand.FileSave:
					{
						if (!string.IsNullOrEmpty(fileSelect.InitialPath) && System.IO.Directory.Exists(fileSelect.InitialPath)) sfd.InitialDirectory = fileSelect.InitialPath;
						if (!string.IsNullOrEmpty(fileSelect.Title)) sfd.Title = fileSelect.Title;
						if (fbd.ShowDialog() == System.Windows.Forms.DialogResult.OK) fileSelect.ReturnPaths = new string[]{ sfd.FileName };
					} break;
				case VstFileSelectCommand.MultipleFilesLoad:
					{
						if (!string.IsNullOrEmpty(fileSelect.InitialPath) && System.IO.Directory.Exists(fileSelect.InitialPath)) sfd.InitialDirectory = fileSelect.InitialPath;
						if (!string.IsNullOrEmpty(fileSelect.Title)) sfd.Title = fileSelect.Title;
						if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) fileSelect.ReturnPaths = ofd.SafeFileNames;
					} break;
			}
			RaisePluginCalled("OpenFileSelector(" + fileSelect.Command + ")");
			return false;
		}
		
		/// <summary>
		/// Requests the host to process the <paramref name="events"/>.
		/// </summary>
		/// <param name="events">Must not be null.</param>
		/// <returns>Returns true if supported by the host.</returns>
		public bool ProcessEvents(Jacobi.Vst.Core.VstEvent[] events)
		{
			foreach (VstEvent vste in events) Console.WriteLine("hvste: {0}",vste.Data.StringifyHex());
			RaisePluginCalled("ProcessEvents(" + events.Length + ")");
			return true;
		}
		
		/// <inheritdoc />
		// not implemented
		public bool SizeWindow(int width, int height)
		{
			// RaisePluginCalled("SizeWindow(" + width + ", " + height + ")");
			RaisePluginCalled("SizeWindow()", new Rectangle(0,0,width,height));
			return false;
		}
		
		/// <inheritdoc />
		public bool UpdateDisplay()
		{
			RaisePluginCalled("UpdateDisplay()");
			return false;
		}
		
		#endregion
		
		#region IVstHostCommands10 Members
		
		/// <inheritdoc />
		public int GetCurrentPluginID()
		{
			RaisePluginCalled("GetCurrentPluginID()");
			return PluginContext.PluginInfo.PluginID;
		}
		
		/// <inheritdoc />
		public int GetVersion()
		{
			RaisePluginCalled("GetVersion()");
			return 0x01000000;
		}
		
		/// <inheritdoc />
		public void ProcessIdle()
		{
			RaisePluginCalled("ProcessIdle()");
		}
		
		/// <inheritdoc />
		public void SetParameterAutomated(int index, float value)
		{
			Console.WriteLine("SetParamAutomated[{0},{1}]",index,value);
			RaisePluginCalled("SetParameterAutomated(" + index + ", " + value + ")");
			
//			try {
//				this.PluginContext.PluginCommandStub.EditorIdle();
//			} catch { }
		}
		
		#endregion
	}
}
