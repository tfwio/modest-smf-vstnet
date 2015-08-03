/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;
using System.Xml.Serialization;
namespace gen.snd
{
	/// <summary>
	/// Notice we don't feel like providing MBT parser given
	/// that one may be provided by samples or ticks.
	/// Samples and ticks are the expected input types.
	/// </summary>
	[Flags]
	public enum DeltaType
	{
		[XmlEnum("dbl")]
		None = 0,
		/// <summary>
		/// A logrithmic value
		/// </summary>
		[XmlEnum("dB")]
		Decibels,
		[XmlEnum("ms")]
		Milliseconds,
		[XmlEnum("q")]
		/// Same as Pulses
		Quarters,
		[XmlEnum("s")]
		Samples,
		/// <summary>
		/// 1 * 4 = Note * 4 = Bar * 4 = Measure.
		/// </summary>
		[XmlEnum("p")]
		Pulses,
		/// <summary>
		/// ticks per pulse (int TPQ, PPQ) given a divisor such as MTRK.DIVISION, tempo and time-signature.
		/// </summary>
		[XmlEnum("t")]
		Ticks,
	}
}


