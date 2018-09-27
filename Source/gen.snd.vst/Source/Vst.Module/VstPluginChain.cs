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
using System.Linq;


namespace gen.snd.Vst.Module
{
	public class VstPluginChain
	{
		public string Title { get;set; }
		/// <summary>
		/// returns true only if the chain at zero is instrument.
		/// If this is generator, then there should concievably be only one element in the chain.
		/// </summary>
		public bool IsGenerator { get { return RuntimeChain==null ? false : ( ( RuntimeChain.Count==0 ) ? false : RuntimeChain[0].IsInstrument);  } }
		public List<VstPlugin> RuntimeChain { get ; set ; }
		
		public VstPluginChain()
		{
		}
		static public VstPluginChain Create(string title, params VstPlugin[] plugins)
		{
			VstPluginChain chain = new VstPluginChain();
			chain.RuntimeChain.AddRange(plugins);
			chain.Title = title;
			return chain;
		}
	}
}
