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

using gen.snd.Vst.Module;
using gen.snd.Vst.Xml;
using Jacobi.Vst.Core.Host;

namespace gen.snd.Vst
{
	public interface INaudioVstContainer
	{
		MidiSmfFile RuntimeSettings { get;set; }
		IMidiParserUI Parent { get; }
		NAudioVST VstPlayer { get; }
		HostCommandStub VstHost { get; }
		
		VstPluginManager PluginManager { get; }
		
		bool IsPlayerStopped { get; }
		
		void PlayerDestroy();
		void PlayerPlay(/*IVstPluginContext ctx*/);
		void PlayerPause(/*IVstPluginContext ctx*/);
	}
	/// <summary>
	/// <para>• NAudioVstContainer contains VstPlayer (a NAudio wrapper).</para>
	/// <para>• IVstHostCommandStub implementation (pointing to NAudioVST VstPlayer)</para>
	/// <para>Essentially, the class is THE control point for VST Host.</para>
	/// <para>Secondly, the NAudioVstContainer is where audio processing is
	/// contained in addition for various sound and time configurations that are
	/// used in HostCommandStub.</para>
	/// <para>Keep in mind that (NAudioVstPlayer) VstPlayer is where much of the host
	/// related action is triggered.</para>
	/// </summary>
	public class NAudioVstContainer : INaudioVstContainer
	{
		long SampleStartPosition = 0;
		
		public MidiSmfFile RuntimeSettings {
			get { return runtimeSettings; }
			set { runtimeSettings = value; }
		} MidiSmfFile runtimeSettings;

		public VstPluginManager PluginManager { get { return pluginManager; } } VstPluginManager pluginManager;
		
    public IMidiParserUI Parent { get { return parent; } } IMidiParserUI parent;

		public NAudioVST VstPlayer { get { return vstPlayer; } } NAudioVST vstPlayer;

		public HostCommandStub VstHost { get { return vstPlayer.VstHostCommandStub; } }

		public bool IsPlayerStopped { get { return VstPlayer.XAudio==null; } }

		#region Const
		static public readonly Dictionary<Keys,byte> KMap = new Dictionary<Keys,byte>(){
			{ Keys.Q, 0 },{ Keys.D2, 1 },
			{ Keys.W, 2 },{ Keys.D3, 3 },
			{ Keys.E, 4 },
			{ Keys.R, 5 },{ Keys.D5, 6 },
			{ Keys.T, 7 },{ Keys.D6, 8 },
			{ Keys.Y, 9 },{ Keys.D7, 10 },
			{ Keys.U, 11 },
		};
		#endregion
		
		public void PlayerDestroy()
		{
			if (IsPlayerStopped) return;
			VstPlayer.Stop();
		}
		
		public void PlayerPlay(/*IVstPluginContext ctx*/)
		{
			PlayerDestroy();
			VstPlayer.Prepare(/*ctx*/);
			VstPlayer.Volume = 1.0f;
			VstPlayer.Play();
		}
		public void PlayerPause(/*IVstPluginContext ctx*/)
		{
			VstPlayer.Pause();
		}
		
		public NAudioVstContainer(IMidiParserUI parent)
		{
			this.parent = parent;
			this.vstPlayer = new NAudioVST(this);
			this.pluginManager = new VstPluginManager(this);
		}
		
	}
}
