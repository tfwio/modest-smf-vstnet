/* tfooo with #develop */

using System;

namespace gen.snd.Formats
{
	public partial class SoundFont2
	{
		public TGenValue GetGenValueType(int indexIGEN)
		{
			return GetGenValueType(this.hyde.igen[indexIGEN].Generator);
		}
		/// <summary>
		/// This method has not been used.
		/// Get the type of generator value.
		/// This value is provided by the IGEN hydra-table.
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		static public TGenValue GetGenValueType(SFGenConst value)
		{
			switch (value)
			{
				case SFGenConst.keyRange:
					return TGenValue.midiKeyRange;
				case SFGenConst.attackVolEnv:case SFGenConst.holdVolEnv:case SFGenConst.decayVolEnv:case SFGenConst.sustainVolEnv:case SFGenConst.releaseVolEnv:
				case SFGenConst.attackModEnv:case SFGenConst.holdModEnv:case SFGenConst.decayModEnv:case SFGenConst.sustainModEnv:case SFGenConst.releaseModEnv:
					return TGenValue.absoluteTimeCents;
				case SFGenConst.sampleModes:
					return TGenValue.sampleMode;
				case SFGenConst.endAddrsOffset:
				case SFGenConst.startloopAddrsOffset:
				case SFGenConst.endloopAddrsOffset:
				case SFGenConst.startLoopAddrsCoarseOffs:
				case SFGenConst.endloopAddrsCoarseOffset:
					return TGenValue.smpls;
				case SFGenConst.startAddrsCoarseOffset:
				case SFGenConst.endAddrsCoarseOffset:
					return TGenValue.smpls32k;
				case SFGenConst.modLfoToPitch:
				case SFGenConst.vibLfoToPitch:
				case SFGenConst.modEnvToPitch:
					return TGenValue.centsFs;
				case SFGenConst.initialFilterFc:
				case SFGenConst.freqModLFO:
				case SFGenConst.freqVibLFO:
				case SFGenConst.fineTune:
					return TGenValue.cent;
				case SFGenConst.initialFilterQ:
				case SFGenConst.initialAttenuation:
					return TGenValue.cb;
				case SFGenConst.modLfoToVolume:
					return TGenValue.cbFs;
				case SFGenConst.chorusEffectsSend:
				case SFGenConst.reverbEffectsSend:
				case SFGenConst.pan:
					return TGenValue.percent;
				case SFGenConst.sampleID:
					return TGenValue.sampleID;
				default:
					return TGenValue.unsupported;
			}
		}
	}
}
