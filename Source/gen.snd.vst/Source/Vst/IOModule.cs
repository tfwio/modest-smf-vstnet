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

namespace gen.snd.Vst
{
  public class IOModule : IDisposable
  {
    int BlockSize = 0;
    
    public AudioModule Inputs, Outputs;
    
    VstAudioBuffer[] mod0Input, mod0Output, mod1Input, mod1Output;
    
    public void Dispose()
    {
      this.mod0Input = null;
      this.mod1Input = null;
      this.mod0Output = null;
      this.mod1Output = null;
      
      if (Inputs != null) this.Inputs.Dispose();
      if (Outputs != null) this.Outputs.Dispose();
    }
    
    bool HasMidi(IMidiParserUI ui)
    {
      return ui.MidiParser != null &&
        ui.MidiParser.SmfFileHandle != null &&
        ui.MidiParser.MidiDataList.Count > 0;
    }
    
    static public IOModule Create(int blockSize, VstPlugin vstI, VstPlugin vstO)
    {
      var module = new IOModule();
      return module.Reset(blockSize,vstI,vstO);
    }
    
    public bool CheckSize(int size)
    {
      return size==BlockSize;
    }
    
    
    
    public IOModule Reset(int blockSize, VstPlugin vstI, VstPlugin vstO)
    {
      BlockSize = blockSize;
      PluginResetBuffers(vstI,vstO,blockSize);
      return this;
    }
    
    void PluginResetBuffers(VstPlugin input, VstPlugin output, int blockSize)
    {
      if (Inputs!=null) { if (Inputs.BlockSize!=blockSize && input!=null) Inputs = PluginResetBuffer(input,blockSize); }
      else if (input!=null) Inputs = PluginResetBuffer(input,blockSize);
      
      if (Outputs!=null) { if (Outputs.BlockSize!=blockSize && output!=null) Outputs = PluginResetBuffer(output,blockSize); }
      else if (output!=null) Outputs = PluginResetBuffer(output,blockSize);
    }
    
    AudioModule PluginResetBuffer(VstPlugin plugin, int blockSize)
    {
      if (plugin!=null) return new AudioModule(plugin,blockSize,plugin.Host.VstPlayer.Settings.Rate);
      return null;
    }
    
    
    
    #region PLUGIN
    
    public VstAudioBuffer[] GeneralProcess(VstPlugin vstInput, VstPlugin vstOutput)
    {
      if (Inputs==null || Inputs.BlockSize!=BlockSize) PluginResetBuffers(vstInput,vstOutput,BlockSize);
      PluginPreProcess1(vstInput,BlockSize,Inputs[0].ToArray(),Inputs[1].ToArray());
      return PluginProcess(vstOutput,Inputs[1].ToArray(),Outputs[1].ToArray());
    }
    
    
    /// <summary>
    /// With midi info
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="blockSize"></param>
    /// <param name="inputs"></param>
    /// <param name="outputs"></param>
    VstAudioBuffer[] PluginPreProcess1(VstPlugin plugin, int blockSize, VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
    {
      if (HasMidi(plugin.Host.Parent))
        VstMidiEnumerator.SendMidi2Plugin( plugin, plugin.Host.Parent, blockSize );
      return PluginPreProcess2(plugin,inputs,outputs);
    }
    /// <summary>
    /// plugin.PluginCommandStub.ProcessReplacing(inputs, outputs);
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="inputs"></param>
    /// <param name="outputs"></param>
    VstAudioBuffer[] PluginPreProcess2(VstPlugin plugin, VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
    {
      plugin.PluginCommandStub.StartProcess();
      plugin.PluginCommandStub.ProcessReplacing(inputs, outputs);
      plugin.PluginCommandStub.StopProcess();
      return outputs;
    }
    /// <summary>
    /// Ignore nulll entries.
    /// Convert output to 2 channel if necessary.
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="inputs"></param>
    /// <param name="outputs">VstAudioBuffer[2]</param>
    VstAudioBuffer[] PluginProcess(VstPlugin plugin, VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
    {
      VstAudioBuffer[] newinputs = inputs.Length == 1 ? new VstAudioBuffer[] {
        inputs[0],
        inputs[0]
      } : inputs;
      return plugin == null ? newinputs : PluginPreProcess2(plugin, newinputs, outputs);
    }
    
    #endregion
  }
}
