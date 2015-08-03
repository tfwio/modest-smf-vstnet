/*
 * tfooo in #DEVLEOP
 * DateTime: 12/27/2006 : 9:04 AM
 */

using System;

namespace gen.snd
{
	static class MidiHelper
	{
		static public string[] OctaveMacro()
		{
			string[] ax = new string [ 128 ];
			for (int i = 0; i<127; i++) ax[i] = string.Format("{0}{1}",keys[i % 12],Convert.ToInt32(i/12));
			return ax;
		}
		static public string[] OctaveMacro(int lo, int hi)
		{
			string[] ax = new string [ hi-lo ];
			int octave = 0;
			for (int i = octave; i < hi; i+=12)  {
				ax[i] = string.Format("{0}{1}",keys[i % 12],Convert.ToInt32(i/12));
			}
			return ax;
		}
		static readonly string[] keys = new string[12]{ "C ","C#","D ","D#","E ","F ","F#","G ","G#","A ","A#","B " };
	}
}
