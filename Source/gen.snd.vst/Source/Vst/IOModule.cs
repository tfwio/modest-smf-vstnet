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

namespace gen.snd.Vst
{
  using IVstPluginContext=Jacobi.Vst.Core.Host.IVstPluginContext;
  using VstAudioBuffer=Jacobi.Vst.Core.VstAudioBuffer;
  
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
    
    public IOModule Reset(int blockSize, VstPlugin vstI, VstPlugin vstO)
    {
      BlockSize = blockSize;
      PluginResetBuffers(vstI,vstO,blockSize);
      return this;
    }
    
    // Can this in fact be a bit redundant?
    /// AudioProcess Step 2 (1.1) / N.
    void PluginResetBuffers(VstPlugin instrument, VstPlugin effect, int blockSize)
    {
      if (Inputs!=null)
      {
        if (Inputs.BlockSize!=blockSize && instrument!=null)
          Inputs = PluginResetBuffer(instrument,blockSize);
      }
      else if (instrument!=null)
        Inputs = PluginResetBuffer(instrument,blockSize);
      
      if (Outputs!=null)
      {
        if (Outputs.BlockSize!=blockSize && effect!=null)
          Outputs = PluginResetBuffer(effect,blockSize);
      }
      else if (effect!=null)
        Outputs = PluginResetBuffer(effect,blockSize);
    }
    
    /// AudioProcess Step 2.1 (1.1.1) / N.
    AudioModule PluginResetBuffer(VstPlugin plugin, int blockSize)
    {
      return plugin != null ? new AudioModule(
        plugin,
        blockSize,
        plugin.Host.VstPlayer.Settings.Rate
       ) : null;
    }
    
    // 
    // Plugin
    // ===================================
    
    /// <summary>
    /// <para>
    /// AudioProcess STARTING POINT as called within <see cref="VSTStream32"/>.ProcessReplace(int).
    /// </para>
    /// </summary>
    /// <remarks>Internally, this is our starting point from.</remarks>
    /// <param name="instrument"></param>
    /// <param name="effect"></param>
    /// <returns></returns>
    public VstAudioBuffer[] GeneralProcess(VstPlugin instrument, VstPlugin effect)
    {
      if ( Inputs==null || Inputs.BlockSize!=BlockSize )
        PluginResetBuffers(instrument, effect, BlockSize);
      
      PluginPreProcess(instrument,BlockSize,Inputs[0].ToArray(),Inputs[1].ToArray());
      
      return PluginProcess(effect,Inputs[1].ToArray(),Outputs[1].ToArray());
    }
    
    
    /// <summary>
    /// AudioProcess Step 3 / N.
    /// With midi info
    /// </summary>
    /// <param name="plugin"></param>
    /// <param name="blockSize"></param>
    /// <param name="instrument"></param>
    /// <param name="effect"></param>
    VstAudioBuffer[] PluginPreProcess(VstPlugin plugin, int blockSize, VstAudioBuffer[] instrument, VstAudioBuffer[] effect)
    {
      
      // This is the new
      // ---------------
      
      // 1. Look up current sample-position
      // 2. 
      
      
      // This is the old ...
      // ---------------
      if (HasMidi(plugin.Host.Parent))
        VstMidiEnumerator.SendMidi2Plugin( plugin, plugin.Host.Parent, blockSize );
      
      return ProcessReplacing(plugin,instrument,effect);
    }
    
    /// <summary>
    /// AudioProcess Step 3.1 / N.
    /// </summary>
    /// <param name="plugin">Plugin Context</param>
    /// <param name="inputs">VstAudioBuffer</param>
    /// <param name="outputs">VstAudioBuffer</param>
    /// <seealso cref="Jacobi.Vst.Core.IVstPluginCommandsBase.ProcessReplacing(VstAudioBuffer[],VstAudioBuffer[])"/>
    VstAudioBuffer[] ProcessReplacing(IVstPluginContext plugin, VstAudioBuffer[] inputs, VstAudioBuffer[] outputs)
    {
      plugin.PluginCommandStub.StartProcess();
      plugin.PluginCommandStub.ProcessReplacing(inputs, outputs);
      plugin.PluginCommandStub.StopProcess();
      return outputs;
    }
    
    /// <summary>
    /// Step 4 of N (N = 4)
    /// <para>
    /// If plugin is null, ignore audio-process and return empty inputs/outputs,
    /// otherwise calling on the plugin's ProcessReplacing (<see cref="PluginProcessReplacing"/>).
    /// </para>
    /// <para>Convert mono input to 2-channel.</para>
    /// </summary>
    /// <returns>Audio data after processing.</returns>
    /// <param name="plugin"></param>
    /// <param name="inputs"></param>
    /// <param name="outputs">VstAudioBuffer[2]</param>
    /// <seealso cref="PluginProcessReplacing"/>
    VstAudioBuffer[] PluginProcess(
      IVstPluginContext plugin,
      VstAudioBuffer[] inputs,
      VstAudioBuffer[] outputs)
    {
      VstAudioBuffer[] newinputs = inputs.Length == 1 ?
        new VstAudioBuffer[] { inputs[0], inputs[0] } :
        inputs;
      
      return plugin == null ? newinputs : ProcessReplacing(plugin, newinputs, outputs);
    }
    
  }
}
