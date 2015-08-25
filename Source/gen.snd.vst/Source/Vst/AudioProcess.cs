#region User/License
// oio * 7/19/2012 * 11:33 AM

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
using System.Linq;

using gen.snd.Vst.Module;
using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;

namespace gen.snd.Vst
{
	class AudioProcess : IDisposable
	{
		internal int BlockSize = 0, nch = 0;
		VstAudioBuffer[] binput, boutput;
		public VstAudioBuffer[] BufferInput { get { return binput; } }
		public VstAudioBuffer[] BufferOutput { get { return boutput; } }
		
		public AudioProcess(NAudioVST naudiovst, VstPlugin plugin, int nch, int blockSize)
		{
			this.nch = nch;
			this.BlockSize = blockSize;
			UpdateBlockSize(naudiovst,plugin);
		}
		public AudioProcess(NAudioVST naudiovst, VstPlugin plugin, AudioProcess parent)
		{
			this.nch = parent.nch;
			this.BlockSize = parent.BlockSize;
			UpdateBlockSize(naudiovst,plugin,parent);
		}
		void UpdateBlockSize( NAudioVST naudiovst, VstPlugin plugin )
		{
			VstAudioBufferManager ii = null, io = null;
			
			ii  = new VstAudioBufferManager(plugin.PluginInfo.AudioInputCount, BlockSize);
			io = new VstAudioBufferManager(plugin.PluginInfo.AudioOutputCount, BlockSize);
			
			UpdateBlock(naudiovst,plugin);
			
			binput = ii.ToArray();
			boutput = io.ToArray();
		}
		void UpdateBlockSize( NAudioVST naudiovst, VstPlugin plugin, AudioProcess parent)
		{
			VstAudioBufferManager io = new VstAudioBufferManager(plugin.PluginInfo.AudioOutputCount, BlockSize);
			UpdateBlock(naudiovst,plugin);
			binput = parent.boutput;
			boutput = io.ToArray();
		}
		
		public void Process(VstPlugin plugin) { Process(plugin, binput, boutput); }
		public void Process(VstPlugin plugin, VstAudioBuffer[] inputs) { Process(plugin,inputs,boutput); }
		public void Process(VstPlugin plugin, VstAudioBuffer[] insI, VstAudioBuffer[] insO)
		{
			plugin.PluginCommandStub.StartProcess();
			plugin.PluginCommandStub.ProcessReplacing(insI, insO);
			plugin.PluginCommandStub.StopProcess();
		}
		
		void UpdateBlock(NAudioVST naudiovst, VstPlugin plugin)
		{
			plugin.PluginCommandStub.SetBlockSize(BlockSize);
			plugin.PluginCommandStub.SetSampleRate(naudiovst.Settings.Rate);
			plugin.PluginCommandStub.SetProcessPrecision(VstProcessPrecision.Process32);
		}
		
		public void Dispose()
		{
			binput = null;
			boutput = null;
		}
		
	}
}
