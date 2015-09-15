/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;

namespace gen.snd
{
  /// <summary>
  /// The SampleClock is ignorant to number of channels.
  /// Aside form that, it can be used to determine MBQT
  /// from Sample-Position and (maybe) vice-vs. (I hadn't
  /// gone over this in a while...)
  /// </summary>
	public class SampleClock : IClock
	{
    // We are missing tempo change locations here.
    // Sample-accurate timing  should be stored perhaps in a stack, yet how are we to index the data?
    // -----------------------------------------------
    // 
    // dictionary<long,ushort> or dictionary<ticks,division>
    // 
    // 

		#region IAudioClock
		public double Samples {
			get { return samples; }
			set { pulses = (samples = value) / (60 / Tempo * Rate) * Division; }
		} double samples;
		#endregion
		#region Properties

		/// <summary>
		/// We don't Floor the samples here for accuracy to the nearest sample;
		/// </summary>
		public int Samples32 {
			get { return Convert.ToInt32(samples); }
		}
		/// <summary>
		/// We use Math.Floor.
		/// </summary>
		public int Samples32Floor {
			get { return Convert.ToInt32(Math.Floor(samples)); }
		}

		public int Rate { get; set; }
		
		public double Tempo { get; set; }
		
		/// <summary>typically 48,72,96,120,144,168,192,216,240,360,384,480</summary>
		public int Division { get; set; }

		public double PulsesPerPPQDivision {
			get { return (Division / 24); }
		}

		public double SamplesPerClock {
			get { return this.SamplesFromPulses(PulsesPerPPQDivision, Tempo, Rate, Division); }
		}

		public double ClocksAtPosition {
			get { return Pulses / PulsesPerPPQDivision; }
		}

		public double ClocksAtPositionFloor {
			get { return Math.Floor(ClocksAtPosition); }
		}

		#endregion
		
		#region Properties: Read Only; Samples|Seconds Per Quarter
		
		public double MSPQN {
			get { return 60000000.0 / Tempo; }
		}
		public double SPQN {
			get { return 60.0 / Tempo; }
		}
		
		public TimeSpan Time {
			get { return TimeSpan.FromSeconds(Samples / Rate); }
		}
		public string TimeString {
			get {
				TimeSpan t = Time;
				return string.Format(
					"{0:00}:{1:00}:{2:00}.{3:000}",
					t.Hours,
					t.Minutes,
					t.Seconds,
					t.Milliseconds);
			}
		}
		
		/// <summary>SecondsPerQuarter * Rate</summary>
		public double SamplesPerQuarter {
			get { return SPQN * Rate; }
		}

		/// <summary>
		/// QuartersOffsetInQuarters (Zero inclusive)
		/// </summary>
		public double QuarterHelper {
			get { return (QuartersOffset / Division); }
		}

		/// <summary>
		/// QuartersOffsetInQuarters (Zero inclusive)
		/// </summary>
		public double QuarterHelperInhibited {
			get { return (QuartersOffsetInQuarters1 / Division); }
		}

		/// <summary>
		/// This provides a number from 0 to a maxumum value < 4.
		/// Use QuarterOffset for a value that is not limited to a boundary < 4.
		/// </summary>
		public double QuartersOffsetInQuarters1 {
			get { return (QuartersOffset) % 4; }
		}

		/// <summary>
		/// The number of elapsed quarters.  Calculated from Samples / SamplesPerQuarter
		/// where SamplesPerQuarter = SecondsPerQuarter * Rate and SecondsPerQuarter is
		/// Tempo / SampleRate.
		/// </summary>
		/// <remarks>
		/// A value of 1 is in quarter notes would translate to 480 if we're using 480ppq.
		/// To be clear, 1ppq in this variable would be stored as 1.
		/// </remarks>
		public double QuartersOffset {
			get { return Samples / SamplesPerQuarter; }
		}

		string MeasureStringFormat {
			get { return IsQuarterOffsetInTicks ? Strings.ms_mbqt : Strings.ms_mbt; }
		}

		public string MeasureString {
			get { return string.Format(MeasureStringFormat, Measure, Bar, Math.Floor(Tick), Math.Floor(Quarter)); }
		}

		#endregion

		#region MBQT

		/// <summary>
		/// For use in displays (ui) — MBT string
		/// </summary>
		public bool IsQuarterOffsetInTicks { get; set; }
		/// V/4/4 … +1
		public double Measure {
			get { return (Math.Floor((QuartersOffset) / 16) + 1); }
		}
		/// V/4%4 … +1
		public double Bar {
			get { return (Math.Floor((QuartersOffset) / 4) % 4) + 1; }
		}
		/// <summary>
		/// ( D * ( ( ( ( S / ( ( 60.0 / T ) * R ) ) % 4 )  / D) * D ) ) % D<br/>
		/// Value is S; where S=Samples, D=Division, T=Tempo, R=Rate
		/// </summary>
		public double Tick {
			get { return (Division * (QuarterHelperInhibited * Division)) % Division; }
		}
		/// (60 / T * R) * (p / D)
		public double Pulses {
			get { return pulses; }
			set {
				pulses = value;
				samples = SamplesFromPulses(Convert.ToUInt64(pulses), Tempo, (int)Rate, (int)Division);
			}
		} double pulses;

		public double Frame {
			get { return Pulses / Division; }
		}

		public double Quarter {
			get { return QuartersOffsetInQuarters1 + 1; }
		}

		/// <summary>This one depends on IsQuarterOffsetInTicks</summary>
		public double Quarters {
			get { return (IsQuarterOffsetInTicks ? Tick : Quarter); }
		}

		#endregion
		public double SamplesFromPulses(ulong p, double tempo, int rate, int division)
		{
			return SamplesFromPulses(Convert.ToDouble(p), tempo, rate, division);
		}
		public double SamplesFromPulses(double p, double tempo, int rate, int division)
		{
			return (60 / tempo * rate) * (p / division);
		}
		#region PPQ
		public SampleClock SolvePPQ(double samples)
		{
			Samples = samples;
			return this;
		}
		public SampleClock SolvePPQ(double samples, int rate, double tempo, int division)
		{
			return SolvePPQ(samples, rate, tempo, division, true);
		}
		public SampleClock SolvePPQ(double samples, int rate, double tempo, int division, bool inTicks)
		{
			Rate = rate;
			Tempo = tempo;
			Division = division;
			IsQuarterOffsetInTicks = inTicks;
			Samples = samples;
			return this;
		}
		public SampleClock SolvePPQ(double samples, ITimeConfiguration config)
		{
			Rate = config.Rate;
			Tempo = config.Tempo;
			Division = config.Division;
			IsQuarterOffsetInTicks = true;
			Samples = samples;
			return this;
		}
		#endregion
		#region SMP
		public SampleClock SolveSamples(double pulses)
		{
			Pulses = pulses;
			return this;
		}
		public SampleClock SolveSamples(double pulses, int rate, double tempo, int division, bool inTicks)
		{
			Rate = rate;
			Tempo = tempo;
			Division = division;
			IsQuarterOffsetInTicks = inTicks;
			Pulses = pulses;
			return this;
		}

		public SampleClock SolveSamples(double pulses, ITimeConfiguration config)
		{
			Rate = config.Rate;
			Tempo = config.Tempo;
			Division = config.Division;
			IsQuarterOffsetInTicks = true;
			Pulses = pulses;
			return this;
		}
		#endregion

		public SampleClock()
		{
		}
		public SampleClock(ITimeConfiguration config)
		{
			Rate = config.Rate;
			Division = config.Division;
			Tempo = config.Tempo;
		}

		public SampleClock(double samples, int rate, double tempo, int division) : this(samples, rate, tempo, division, true)
		{
		}
		public SampleClock(double samples, int rate, double tempo, int division, bool inTicks)
		{
			SolvePPQ(samples, rate, tempo, division, inTicks);
		}

		public static SampleClock Create(double pulses, int rate, double tempo, int division)
		{
			SampleClock clock = new SampleClock(0, rate, tempo, division, true);
			return clock.SolveSamples(pulses);
		}
		
		public object Clone()
		{
			return new SampleClock(this.samples, this.Rate, this.Tempo, this.Division, this.IsQuarterOffsetInTicks);
		}
		public T Clone<T>() where T:class,IClock
		{
			return (T)(object)(new SampleClock(this.samples, this.Rate, this.Tempo, this.Division, this.IsQuarterOffsetInTicks));
		}
	}
}
