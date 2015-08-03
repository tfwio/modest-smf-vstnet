/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;

namespace gen.snd
{
	// m is the key number
	// Note: we're tuning to 440
	// Note: Key 69 ('m') is tuned to 440
	//
	// m  =  12*log2(fm/440 Hz)
	// (and)
	// fm  =  2(m−69)/12(440 Hz).

	static public class BpmCalculator
	{
		static public double GetHz(double bpm, double denominator, double numerator, double multiplier)
		{
			double result = (bpm / 60) / 4;
			double x1 = numerator / denominator;
			return  result * x1 * multiplier;
		}
	}
}
