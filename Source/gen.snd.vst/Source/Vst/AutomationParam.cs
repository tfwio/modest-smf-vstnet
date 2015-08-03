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
using System.Xml.Serialization;

namespace gen.snd.Vst
{
	public class AutomationParam
	{
		[XmlAttribute] public int id { get;set; }
		[XmlAttribute] public float value { get;set; }
		
		[XmlAttribute("delta")] public string DeltaString { get; set; }
		[XmlIgnore] public PulseValue Delta { get { return DeltaString; } set { DeltaString = value; } }
		
		public AutomationParam()
		{
		}
		static public AutomationParam Create(PulseValue delta, int paramId, float paramValue)
		{
			return new AutomationParam(){ Delta=delta, id=paramId, value=paramValue };
		}
		static public AutomationParam Create(double delta, DeltaType t, int paramId, float paramValue)
		{
			return new AutomationParam(){ Delta=new PulseValue(delta,t), id=paramId, value=paramValue };
		}
	}
	

}
