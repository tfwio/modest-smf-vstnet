/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;

namespace gen.snd
{
	/// <summary>
	/// This class should be used to generate frequency tables.
	/// </summary>
	static public class Frequency
	{
		const double note_count = 128;
		const double NOTES_PER_OCTAVE = 12.0d;
		const double FIRST_TUNED_NOTE = 9.0d;
		
		const double TUNING = 13.75;//440/2^5;
		
		static double note(double freq, int noteNumber) { return note(freq,noteNumber,5,9,12); }
		static double note(int noteNumber) { return note(440,noteNumber,5,9,12); }
		
		static public double note(
			double freq,
			double intervalTarget,
			double intervalPower,
			double intervalOffset,
			double intervalDivisor)
		{
			double pow		= Math.Pow(2,intervalPower);
			double interval	= intervalTarget - intervalOffset;
			interval	   /= intervalDivisor;
			// interval Power sets the target octave number to 5.
			// interval Offset sets the target Key to be the nineth;
			// therefore, octave octave[5]+semitone[9]
			return
				( freq / pow ) *
//				( pow ) *
				( Math.Pow(2,interval) );
		}
		
		/// <summary>
		/// Provides a key-refernce table for 128 keys.
		/// Note that this is just for reference and isn't really used anywhere.
		/// </summary>
		/// <returns></returns>
		public static double[] GetFrequencyTable()
		{
			double[] freqTable = new double[128];
			for (int i=0; i<note_count; i+=1) freqTable[i]=note(i);
			return freqTable;
		}
		//static double cents(double frq, double offset) { return 0.0; }
	
	}
}
