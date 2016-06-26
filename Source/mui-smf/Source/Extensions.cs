/* oio * 8/3/2015 * Time: 6:39 AM */

using System;
using Mui;
using Mui.Widgets;
namespace mui_smf
{
	static class Extensions
	{
	  static public gen.snd.Midi.MBT ToMBT(this ulong deltaTime, gen.snd.Midi.MidiReader reader)
	  {
	    return new gen.snd.Midi.MBT(deltaTime,reader.MidiTimeInfo.Division);
	  }
		static public int MinMax(this int input, int min, int max)
		{
			return input.Min(min).Max(max);
		}

		static public int Max(this int input, int max)
		{
			return input > max ? input : max;
		}

		static public int Min(this int input, int min)
		{
			return input < min ? input : min;
		}
		static public bool IsIn(this int value, int min, int max)
		{
			if (value < min)
				return false;
			if (value > max)
				return false;
			return true;
		}
	}
}


