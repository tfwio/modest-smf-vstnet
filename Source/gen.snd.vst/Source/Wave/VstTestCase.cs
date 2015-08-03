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
using Jacobi.Vst.Core;
using Jacobi.Vst.Interop.Host;
using NAudio.Wave;

namespace DspAudio.Wave
{
	/// http://stackoverflow.com/questions/3088593/vst-net-vs-naudio-vstaudiobuffer-vs-pcmstream-buffer
	static class VstTestCase
	{
		static void Method(VstPluginContext PluginContext, int bytesWritten, byte[] destBuffer)
		{
			int inputCount = PluginContext.PluginInfo.AudioInputCount;
			int outputCount = PluginContext.PluginInfo.AudioOutputCount;
			int blockSize = bytesWritten;
	
			VstAudioBufferManager inputMgr = new VstAudioBufferManager(inputCount, blockSize);
			VstAudioBufferManager outputMgr = new VstAudioBufferManager(outputCount, blockSize);
	
			foreach (VstAudioBuffer buffer in inputMgr.ToArray())
			{
				for (int i = 0; i < blockSize; i++)
				{
					buffer[i] = (float)destBuffer[i] / 128.0f - 1.0f;
				}
			}
	
			PluginContext.PluginCommandStub.SetBlockSize(blockSize);
			PluginContext.PluginCommandStub.SetSampleRate(44.8f);
	
			PluginContext.PluginCommandStub.StartProcess();
			PluginContext.PluginCommandStub.ProcessReplacing(inputMgr.ToArray(), outputMgr.ToArray());
			PluginContext.PluginCommandStub.StopProcess();
	
			foreach (VstAudioBuffer buffer in outputMgr.ToArray())
			{
				for (int i = 0; i < blockSize; i++)
				{
					destBuffer[i] = Convert.ToByte(((float)buffer[i] + 1.0f) * 128.0f);
				}
			}
			inputMgr.ClearBuffer(inputMgr.ToArray()[0]);
			inputMgr.ClearBuffer(inputMgr.ToArray()[1]);
			inputMgr.Dispose();
			outputMgr.ClearBuffer(outputMgr.ToArray()[0]);
			outputMgr.ClearBuffer(outputMgr.ToArray()[1]);
			outputMgr.Dispose();
	
		}
	}
}
