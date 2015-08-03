#region User/License
// oio * 2005-11-12 * 04:19 PM
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
using System.IO;
using System.Windows.Forms;

namespace gen.snd.Modules
{
	abstract public class BasicAudioModule<TModule> : BasicAudioModule, IAudioModule<TModule>
	{
		protected string FilePath;
		protected string FileName { get { return Path.GetFileName(FilePath); } }
		
		public TModule AudioModule {
			get { return audioModule; }
			set { audioModule = value; }
		} internal TModule audioModule;
		
		public IList<IAudioSample> PcmSamples {
			get { return pcm_smpl; }
			internal set { pcm_smpl = value as List<IAudioSample>; }
		} internal List<IAudioSample> pcm_smpl = new List<IAudioSample>();
		
		public bool IsModuleLoaded {
			get { return isModuleLoaded; }
			internal protected set { isModuleLoaded = value; }
		} bool isModuleLoaded = false;
		
		abstract public void LoadModule();
		
		public override void PlaySample()
		{
			throw new NotImplementedException();
		}
	}
}
