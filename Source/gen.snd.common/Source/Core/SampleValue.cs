/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
namespace gen.snd
{
	public class SampleValue : PulseValue
	{
		readonly static new DeltaType DefaultAutomationType = DeltaType.Samples;

		static public implicit operator string(SampleValue unit) {
			return unit.ValueString;
		}

		static public implicit operator SampleValue(double unit) {
			return new SampleValue(unit, DefaultAutomationType);
		}

		public SampleValue()/* : this(double.NaN,DeltaType.Samples)*/
		{
			this.DeltaMode = DeltaType.Samples;
		}

		public SampleValue(double value) : this(value, DeltaType.Samples)
		{
		}

		public SampleValue(string value)
		{
			SetValue(value);
		}

		SampleValue(double value, DeltaType t)
		{
			Value = value;
			DeltaMode = t;
		}
	}
}


