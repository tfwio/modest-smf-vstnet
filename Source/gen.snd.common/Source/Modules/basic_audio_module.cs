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
	public class AudioDeviceConfiguration
	{
		public long SampleRate			= 0;
		public long BitsPerSample		= 0;
		public long NumberOfSamples		= 0;
		public long NumberOfChannels	= 0;
	}
	
	public interface IAudioSample
	{
		int StreamPosition { get; }
		int SampleRate { get; }
		int BitsPerSample { get; }
		int NumberOfChannels { get; }
	}
	
	public interface IAudioModule
	{
		List<IAudioSample> PcmSamples { get; }
		void PlaySample();
	}
	public interface IAudioModule<TModule> : IAudioModule
	{
		bool CanPlay { get; }
		void Play();
		bool HasSampleNames { get; }
		IList<IAudioSample> PcmSamples { get; }
		void ViewInitialize();
		void ViewReset();
		void ViewDetach();
		TModule AudioModule {get;set;}
		bool IsModuleLoaded { get; }
		void LoadModule();
	}
	
	public abstract class BasicAudioModule : IAudioModule
	{
		internal bool canPlay = false;
		public bool CanPlay { get { return canPlay; } }
		
		public void Play() { if (CanPlay) PlayMethod(); }
		internal virtual void PlayMethod() { }
		
		public bool HasSampleNames {
			get { return hasSampleNames; }
			set { hasSampleNames = value; }
		} bool hasSampleNames = false;
		
		public List<IAudioSample> PcmSamples {
			get { return _pcmSamples; }
		} readonly List<IAudioSample> _pcmSamples = new List<IAudioSample>();
		
		virtual public void PlaySample() {}
		virtual public void ViewInitialize() {}
		virtual public void ViewReset() {}
		virtual public void ViewDetach() {}
	}
	


}
