/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;

namespace gen.snd
{
	public interface IMidiClock : IMidiClock_MBQTPF
	{
		int Division { get; set; }
		double PulsesPerPPQDivision { get; }
		double MSPQN { get; }
	}
	/// <summary>
	/// Clock, Measure, bar, quarter, tick, pulse, frame
	/// </summary>
	public interface IMidiClock_MBQTPF
	{
		
		double Measure { get; }
		double Bar { get; }
		double Tick { get; }
		double Pulses { get; set; }
		double Frame { get; }
		double Quarter { get; }
		double Quarters { get; }
	}
	public interface IAudioClock
	{
		int Rate { get; set; }
		/// <summary>
		/// This is the provided sample position.
		/// It may be set explicity, or indirectly by setting Pulses.
		/// </summary>
		double Samples { get; set; }
		int Samples32 { get; }
		int Samples32Floor { get; }
		double SamplesPerQuarter { get; }
		double SamplesPerClock { get; }
		/// <summary>
		/// Samplerate in Hz
		/// </summary>
	}
	public interface IClock : IMidiClock, IAudioClock
	{
		/// <summary>
		/// (was SampleClock) abstract class should virtualize this and return an object
		/// so that derived classes may implement.
		/// </summary>
		/// <returns></returns>
		object Clone();
		
		double Tempo { get; set; }
		
		double SamplesFromPulses(ulong p, double tempo, int rate, int division);
		double SamplesFromPulses(double p, double tempo, int rate, int division);
		
		SampleClock SolvePPQ(double samples);
		SampleClock SolvePPQ(double samples, int rate, double tempo, int division);
		SampleClock SolvePPQ(double samples, int rate, double tempo, int division, bool inTicks);
		SampleClock SolvePPQ(double samples, ITimeConfiguration config);
		
		SampleClock SolveSamples(double pulses);
		SampleClock SolveSamples(double pulses, int rate, double tempo, int division, bool inTicks);
		SampleClock SolveSamples(double pulses, ITimeConfiguration config);
		
		double ClocksAtPosition { get; }
		double ClocksAtPositionFloor { get; }
		double SPQN { get; }
		
		TimeSpan Time { get; }
		string TimeString { get; }
		
		double QuarterHelper { get; }
		double QuarterHelperInhibited { get; }
		double QuartersOffsetInQuarters1 { get; }
		double QuartersOffset { get; }
		
		bool IsQuarterOffsetInTicks { get; set; }
		string MeasureString { get; }
	}
}
