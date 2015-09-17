/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace gen.snd
{
	public class PulseValue
	{
		#region STATIC
		
		const string typenames = "dbl,double|Decebels,dB|tick,t|pulses,p|quarters,q|ms|smp|s";
		
		static string alltypes { get { return string.Join("|",typenames.Split(',')); } }
		
		static readonly Regex RegexParser = new Regex(
			@"(?<unit>([0-9.]+))\s*(?<type>({typenames}))".Replace("{typenames}",alltypes),
			RegexOptions.CultureInvariant|
			RegexOptions.IgnoreCase);
		
		readonly static DeltaType DefaultAutomationType = DeltaType.Ticks;
		
		static public implicit operator string(PulseValue unit) { return unit.ValueString; }
		
		static public implicit operator PulseValue(string unit) { return new PulseValue(unit); }
		
		/// <summary>
		/// in ticks
		/// </summary>
		/// <param name="unit"></param>
		/// <returns></returns>
		static public implicit operator PulseValue(double unit) { return new PulseValue(unit,DefaultAutomationType); }
		
		#endregion
		
		public double Value { get;set; }
		public DeltaType DeltaMode { get;set; }

		#region TO SAMPLES
		/// <summary>
		/// intended from pulses or samples
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		public double ToSamples(ITimeConfiguration config)
		{
			return ToSamples(config.Rate,config.Tempo,config.Division);
		}
		/// <exception cref="ArgumentException">Only applies to DeltaType.Samples and DeltaType.Pulses.</exception>
		public double ToSamples(int rate, double tempo, int division)
		{
			if (this.DeltaMode== DeltaType.Ticks) return ( 60 / tempo * rate) * ( Value / division );
			else if (this.DeltaMode== DeltaType.Samples) return Value;
			throw new ArgumentException (Strings.DeltaArgumentException);
		}

		#endregion
		#region TO PULSES
		/// <summary>
		/// intended from samples or pulses
		/// </summary>
		/// <param name="config"></param>
		/// <returns></returns>
		/// <exception cref="ArgumentException">Only applies to DeltaType.Samples and DeltaType.Pulses.</exception>
		public double ToPulses(ITimeConfiguration config) { return ToPulses(config.Rate,config.Tempo,config.Division); }
		public double ToPulses(int rate, double tempo, int division)
		{
			if (DeltaMode== DeltaType.Samples) return Value / (60 / tempo * rate) * division;
			if (DeltaMode== DeltaType.Pulses) return Value;
			throw new ArgumentException(Strings.DeltaArgumentException);
		}
		#endregion

		public string ValueString { get { return string.Format("{0}{1}",Value,ConvertType(DeltaMode)); } }

		public override string ToString()
		{
			return string.Format(Strings.AutomationUnitToString,ValueString,DeltaMode);
		}

		public PulseValue()
		{
		}
		public PulseValue(string unit)
		{
			SetValue(unit);
		}
		public PulseValue(double value, DeltaType mode)
		{
			Value = value;
			DeltaMode = mode;
		}

		#region REGEX

		public void SetValue(string value)
		{
			Regex r = RegexParser;
			MatchCollection m = r.Matches(value);
			this.Value = double.Parse(m[0].Groups["unit"].Value);
			this.DeltaMode = ConvertType(m[0].Groups["type"].Value);
			r = null;
			m = null;
		}

		#endregion
		#region REGEX TYPE
		internal static DeltaType ConvertType(string type)
		{
			string t = type.ToLower();
			switch (t)
			{
					case "dec": return DeltaType.None;
					case "dbl": return DeltaType.None;
					case "Double": return DeltaType.None;
					
					case "dB": return DeltaType.Decibels;
					
					case "ms": return DeltaType.Milliseconds;
					
					case "pulses": return DeltaType.Pulses;
					case "p": return DeltaType.Pulses;
					
					case "quarters": return DeltaType.Quarters;
					case "q": return DeltaType.Quarters;
					
					case "smp": return DeltaType.Samples;
					case "s": return DeltaType.Samples;
					
					case "t": return DeltaType.Ticks;
					case "ticks": return DeltaType.Ticks;
					
					default: return DeltaType.None;
			}
		}
		internal static string ConvertType(DeltaType type)
		{
			switch (type)
			{
					case DeltaType.Milliseconds: return "ms";
					case DeltaType.Quarters: return "q";
					case DeltaType.Samples: return "smp";
					case DeltaType.Ticks: return "t";
					case DeltaType.Pulses: return "p";
					case DeltaType.Decibels: return "dB";
					default: return string.Empty;
			}
		}
		#endregion

	}
}
