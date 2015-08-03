/*
 * tfooo with #develop
 */

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

using gen.snd.IffForm;

namespace gen.snd.Formats
{
	public partial class SoundFont2
	{
		public struct RIFF
		{
			public char[] header;
			public int Length;
			public char[] Tag;
			public Dictionary<long,RIFFsub> rss;
			
			public RIFF(FileStream fx, BinaryReader breed)
			{
				long origin = fx.Position;
				header = breed.ReadChars(4);
				Length = breed.ReadInt32();
				long pos = fx.Position;
				Tag = breed.ReadChars(4);
				rss = new Dictionary<long,RIFFsub>();
				while (fx.Position < pos+Length)
				{
					RIFFsub rsb = new RIFFsub(breed,fx,this);
					fx.Seek(rsb.Length-4,SeekOrigin.Current);
				}
				fx.Seek(origin+pos,SeekOrigin.Begin);
			}
		}
		
		public struct RIFFsub
		{
			public char[] header;
			public int Length;
			public char[] Tag;
			
			public RIFFsub(BinaryReader br, FileStream sss,RIFF rfx)
			{
				long pos = sss.Position;
				header = br.ReadChars(4);
				Length = br.ReadInt32();
				Tag = br.ReadChars(4);
				rfx.rss.Add(pos,this);
			}
		}

		[StructLayout(LayoutKind.Sequential) ]
		public struct sdta
		{
			public	char[]	infoHead;
			public	int			Length;
		}
		
		[StructLayout(LayoutKind.Sequential) ]
		public struct pdta
		{
			public	char[]	infoHead;
			public	int			Length;
		}

		[StructLayout(LayoutKind.Sequential) ]
		public struct rangesType
		{
			public	byte		Low;
			public	byte		High;
		}

		[StructLayout(LayoutKind.Sequential) ]
		public struct genAmountType
		{
			public	rangesType	Ranges;
			public	short		shAmount;
			public	long		wAmount;
		}

		public enum SFSampleLink : ushort
		{
			monoSample = 1,
			rightSample = 2,
			leftSample = 4,
			linkedSample = 8,
			RomMonoSample = 0x8001,
			RomRightSample = 0x8002,
			RomLeftSample = 0x8004,
			RomLinkedSample = 0x8008
		}
		
		public enum TGenValue
		{
			unsupported,
			absoluteTimeCents,
			smpls,
			smpls32k,
			centsFs,
			cent,
			cb,
			cbFs,
			/// <summary>actual value: -12000 to 5000;
			/// <para>1sec to 20sec</para></summary>
			timeCent,
			/// <summary>sustainModEnv is -0.10 to 1.00 with actual values of 0-1000.
			/// <para>Other percentages are also 0-1000.</para></summary>
			percent,
			tCentKey,
			cbAttn,
			midiKeyRange,
			midiKey,
			midiVel,
			sampleMode,
			semitone,
			sampleID,
			bitFlags,
			centKey,
		}

		#region GeneratorConst
		public enum SFGenConst : short
		{
			/// <summary>
			/// The offset, in sample data points, beyond the Start sample header parameter to the first sample data point to be played for this instrument. For example, if Start were 7 and startAddrOffset were 2, the first sample data point played would be sample data point 9.
			/// </summary>
			startAddrsOffset			= 0 ,
			/// <summary>
			/// The offset, in sample sample data points, beyond the End sample header parameter to the last sample data point to be played for this instrument. For example, if End were 17 and endAddrOffset were -2, the last sample data point played would be sample data point 15.
			/// </summary>
			endAddrsOffset				= 1 ,
			/// <summary>
			/// The offset, in sample data points, beyond the Startloop sample header parameter to the first sample data point to be repeated in the loop for this instrument. For example, if Startloop were 10 and startloopAddrsOffset were -1, the first repeated loop sample data point would be sample data point 9.
			/// </summary>
			startloopAddrsOffset	= 2 ,
			/// <summary>
			/// The offset, in sample data points, beyond the Endloop sample header parameter to the sample data point considered equivalent to the Startloop sample data point for the loop for this instrument. For example, if Endloop were 15 and endloopAddrsOffset were 2, sample data point 17 would be considered equivalent to the Startloop sample data point, and hence sample data point 16 would effectively precede Startloop during looping.
			/// </summary>
			endloopAddrsOffset		= 3 ,
			/// <summary>
			/// The offset, in 32768 sample data point increments beyond the Start sample header parameter and the first sample data point to be played in this instrument. This parameter is added to the startAddrsOffset parameter. For example, if Start were 5, startAddrsOffset were 3 and startAddrsCoarseOffset were 2, the first sample data point played would be sample data point 65544.
			/// </summary>
			startAddrsCoarseOffset		= 4 ,
			/// <summary>
			/// This is the degree, in cents, to which a full scale excursion of the Modulation LFO will influence pitch. A positive value indicates a positive LFO excursion increases pitch; a negative value indicates a positive excursion decreases pitch. Pitch is always modified logarithmically, that is the deviation is in cents, semitones, and octaves rather than in Hz. For example, a value of 100 indicates that the pitch will first rise 1 semitone, then fall one semitone.
			/// </summary>
			modLfoToPitch				= 5 ,
			/// <summary>
			/// This is the degree, in cents, to which a full scale excursion of the Vibrato LFO will influence pitch. A positive value indicates a positive LFO excursion increases pitch; a negative value indicates a positive excursion decreases pitch. Pitch is always modified logarithmically, that is the deviation is in cents, semitones, and octaves rather than in Hz. For example, a value of 100 indicates that the pitch will first rise 1 semitone, then fall one semitone.
			/// </summary>
			vibLfoToPitch				= 6 ,
			/// <summary>
			/// This is the degree, in cents, to which a full scale excursion of the Modulation Envelope will influence pitch. A positive value indicates an increase in pitch; a negative value indicates a decrease in pitch. Pitch is always modified logarithmically, that is the deviation is in cents, semitones, and octaves rather than in Hz. For example, a value of 100 indicates that the pitch will rise 1 semitone at the envelope peak.
			/// </summary>
			modEnvToPitch				= 7 ,
			/// <summary>
			/// This is the cutoff and resonant frequency of the lowpass filter in absolute cent units. The lowpass filter is defined as a second order resonant pole pair whose pole frequency in Hz is defined by the Initial Filter Cutoff parameter. When the cutoff frequency exceeds 20kHz and the Q (resonance) of the filter is zero, the filter does not affect the signal.
			/// </summary>
			initialFilterFc				= 8 ,
			/// <summary>
			/// This is the height above DC gain in centibels which the filter resonance exhibits at the cutoff frequency. A value of zero or less indicates the filter is not resonant; the gain at the cutoff frequency (pole angle) may be less than zero when zero is specified. The filter gain at DC is also affected by this parameter such that the gain at DC is reduced by half the specified gain. For example, for a value of 100, the filter gain at DC would be 5 dB below unity gain, and the height of the resonant peak would be 10 dB above the DC gain, or 5 dB above unity gain. Note also that if initialFilterQ is set to zero or less and the cutoff frequency exceeds 20 kHz, then the filter response is flat and unity gain.
			/// </summary>
			initialFilterQ				= 9 ,
			/// <summary>
			/// This is the degree, in cents, to which a full scale excursion of the Modulation LFO will influence filter cutoff frequency. A positive number indicates a positive LFO excursion increases cutoff frequency; a negative number indicates a positive excursion decreases cutoff frequency. Filter cutoff frequency is always modified logarithmically, that is the deviation is in cents, semitones, and octaves rather than in Hz. For example, a value of 1200 indicates that the cutoff frequency will first rise 1 octave, then fall one octave.
			/// </summary>
			modLfoToFilterFc			= 10 ,
			/// <summary>
			/// This is the degree, in cents, to which a full scale excursion of the Modulation Envelope will influence filter cutoff frequency. A positive number indicates an increase in cutoff frequency; a negative number indicates a decrease in filter cutoff frequency. Filter cutoff frequency is always modified logarithmically, that is the deviation is in cents, semitones, and octaves rather than in Hz. For example, a value of 1000 indicates that the cutoff frequency will rise one octave at the envelope attack peak.
			/// </summary>
			modEnvToFilterFc			= 11 ,
			/// <summary>
			/// The offset, in 32768 sample data point increments beyond the End sample header parameter and the last sample data point to be played in this instrument. This parameter is added to the endAddrsOffset parameter. For example, if End were 65536, startAddrsOffset were -3 and startAddrsCoarseOffset were -1, the last sample data point played would be sample data point 32765.
			/// </summary>
			endAddrsCoarseOffset		= 12 ,
			/// <summary>
			/// This is the degree, in centibels, to which a full scale excursion of the Modulation LFO will influence volume. A positive number indicates a positive LFO excursion increases volume; a negative number indicates a positive excursion decreases volume. Volume is always modified logarithmically, that is the deviation is in decibels rather than in linear amplitude. For example, a value of 100 indicates that the volume will first rise ten dB, then fall ten dB.
			/// </summary>
			modLfoToVolume				= 13 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			unused						= 14 ,
			/// <summary>
			/// This is the degree, in 0.1% units, to which the audio output of the note is sent to the chorus effects processor.
			/// A value of 0% or less indicates no signal is sent from this note; a value of 100% or more indicates the note is sent at full level.
			/// Note that this parameter has no effect on the amount of this signal sent to the “dry” or unprocessed portion of the output.
			/// For example, a value of 250 indicates that the signal is sent at 25% of full level (attenuation of 12 dB from full level) to the chorus effects processor.
			/// </summary>
			chorusEffectsSend			= 15 ,
			/// <summary>
			/// This is the degree, in 0.1% units, to which the audio output of the note is sent to the reverb effects processor.
			/// A value of 0% or less indicates no signal is sent from this note; a value of 100% or more indicates the note is sent at full level.
			/// Note that this parameter has no effect on the amount of this signal sent to the “dry” or unprocessed portion of the output.
			/// For example, a value of 250 indicates that the signal is sent at 25% of full level (attenuation of 12 dB from full level) to the reverb effects processor.
			/// </summary>
			reverbEffectsSend			= 16 ,
			/// <summary>
			/// This is the degree, in 0.1% units, to which the “dry” audio output of the note is positioned to the left or right output.
			/// A value of -50% or less indicates the signal is sent entirely to the left output and not sent to the right output; a value of +50% or more indicates the note is sent entirely to the right and not sent to the left.
			/// A value of zero places the signal centered between left and right.
			/// For example, a value of -250 indicates that the signal is sent at 75% of full level to the left output and 25% of full level to the right output.
			/// </summary>
			pan							= 17 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			unused2						= 18 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			unused3						= 19 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			unused4						= 20 ,
			/// <summary>
			/// This is the delay time, in absolute timecents, from key on until the Modulation LFO begins its upward ramp from zero value. A value of 0 indicates a 1 second delay. A negative value indicates a delay less than one second and a positive value a delay longer than one second. The most negative number (-32768) conventionally indicates no delay. For example, a delay of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			delayModLFO					= 21 ,
			/// <summary>
			/// This is the frequency, in absolute cents, of the Modulation LFO’s triangular period. A value of zero indicates a frequency of 8.176 Hz. A negative value indicates a frequency less than 8.176 Hz; a positive value a frequency greater than 8.176 Hz. For example, a frequency of 10 mHz would be 1200log2(.01/8.176) = -11610.
			/// </summary>
			freqModLFO					= 22 ,
			/// <summary>
			/// This is the delay time, in absolute timecents, from key on until the Vibrato LFO begins its upward ramp from zero value. A value of 0 indicates a 1 second delay. A negative value indicates a delay less than one second; a positive value a delay longer than one second. The most negative number (-32768) conventionally indicates no delay. For example, a delay of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			delayVibLFO					= 23 ,
			/// <summary>
			/// This is the frequency, in absolute cents, of the Vibrato LFO’s triangular period. A value of zero indicates a frequency of 8.176 Hz. A negative value indicates a frequency less than 8.176 Hz; a positive value a frequency greater than 8.176 Hz. For example, a frequency of 10 mHz would be 1200log2(.01/8.176) = -11610.
			/// </summary>
			freqVibLFO					= 24 ,
			/// <summary>
			/// This is the delay time, in absolute timecents, between key on and the start of the attack phase of the Modulation envelope. A value of 0 indicates a 1 second delay. A negative value indicates a delay less than one second; a positive value a delay longer than one second. The most negative number (-32768) conventionally indicates no delay. For example, a delay of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			delayModEnv					= 25 ,
			/// <summary>
			/// This is the time, in absolute timecents, from the end of the Modulation Envelope Delay Time until the point at which the Modulation Envelope value reaches its peak. Note that the attack is “convex”; the curve is nominally such that when applied to a decibel or semitone parameter, the result is linear in amplitude or Hz respectively. A value of 0 indicates a 1 second attack time. A negative value indicates a time less than one second; a positive value a time longer than one second. The most negative number (-32768) conventionally indicates instantaneous attack. For example, an attack time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			attackModEnv				= 26 ,
			/// <summary>
			/// This is the time, in absolute timecents, from the end of the attack phase to the entry into decay phase, during which the envelope value is held at its peak. A value of 0 indicates a 1 second hold time. A negative value indicates a time less than one second; a positive value a time longer than one second. The most negative number (-32768) conventionally indicates no hold phase. For example, a hold time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			holdModEnv					= 27 ,
			/// <summary>
			/// This is the time, in absolute timecents, for a 100% change in the Modulation Envelope value during decay phase. For the Modulation Envelope, the decay phase linearly ramps toward the sustain level. If the sustain level were zero, the Modulation Envelope Decay Time would be the time spent in decay phase. A value of 0 indicates a 1 second decay time for a zero-sustain level. A negative value indicates a time less than one second; a positive value a time longer than one second. For example, a decay time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			decayModEnv					= 28 ,
			/// <summary>
			/// This is the decrease in level, expressed in 0.1% units, to which the Modulation Envelope value ramps during the decay phase. For the Modulation Envelope, the sustain level is properly expressed in percent of full scale. Because the volume envelope sustain level is expressed as an attenuation from full scale, the sustain level is analogously expressed as a decrease from full scale. A value of 0 indicates the sustain level is full level; this implies a zero duration of decay phase regardless of decay time. A positive value indicates a decay to the corresponding level. Values less than zero are to be interpreted as zero; values above 1000 are to be interpreted as 1000. For example, a sustain level which corresponds to an absolute value 40% of peak would be 600.
			/// </summary>
			sustainModEnv				= 29 ,
			/// <summary>
			/// This is the time, in absolute timecents, for a 100% change in the Modulation Envelope value during release phase. For the Modulation Envelope, the release phase linearly ramps toward zero from the current level. If the current level were full scale, the Modulation Envelope Release Time would be the time spent in release phase until zero value were reached. A value of 0 indicates a 1 second decay time for a release from full level. A negative value indicates a time less than one second; a positive value a time longer than one second. For example, a release time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			releaseModEnv				= 30 ,
			/// <summary>
			/// This is the degree, in timecents per KeyNumber units, to which the hold time of the Modulation Envelope is decreased by increasing MIDI key number. The hold time at key number 60 is always unchanged. The unit scaling is such that a value of 100 provides a hold time which tracks the keyboard; that is, an upward octave causes the hold time to halve. For example, if the Modulation Envelope Hold Time were -7973 = 10 msec and the Key Number to Mod Env Hold were 50 when key number 36 was played, the hold time would be 20 msec.
			/// </summary>
			keynumToModEnvHold			= 31 ,
			/// <summary>
			/// This is the degree, in timecents per KeyNumber units, to which the hold time of the Modulation Envelope is decreased by increasing MIDI key number. The hold time at key number 60 is always unchanged. The unit scaling is such that a value of 100 provides a hold time that tracks the keyboard; that is, an upward octave causes the hold time to halve. For example, if the Modulation Envelope Hold Time were -7973 = 10 msec and the Key Number to Mod Env Hold were 50 when key number 36 was played, the hold time would be 20 msec.
			/// </summary>
			keynumToModEnvDecay			= 32 ,
			/// <summary>
			/// This is the delay time, in absolute timecents, between key on and the start of the attack phase of the Volume envelope. A value of 0 indicates a 1 second delay. A negative value indicates a delay less than one second; a positive value a delay longer than one second. The most negative number (-32768) conventionally indicates no delay. For example, a delay of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			delayVolEnv					= 33 ,
			/// <summary>
			/// This is the time, in absolute timecents, from the end of the Volume Envelope Delay Time until the point at which the Volume Envelope value reaches its peak. Note that the attack is “convex”; the curve is nominally such that when applied to the decibel volume parameter, the result is linear in amplitude. A value of 0 indicates a 1 second attack time. A negative value indicates a time less than one second; a positive value a time longer than one second. The most negative number (-32768) conventionally indicates instantaneous attack. For example, an attack time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			attackVolEnv				= 34 ,
			/// <summary>
			/// This is the time, in absolute timecents, from the end of the attack phase to the entry into decay phase, during which the Volume envelope value is held at its peak. A value of 0 indicates a 1 second hold time. A negative value indicates a time less than one second; a positive value a time longer than one second. The most negative number (-32768) conventionally indicates no hold phase. For example, a hold time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			holdVolEnv					= 35 ,
			/// <summary>
			/// This is the time, in absolute timecents, for a 100% change in the Volume Envelope value during decay phase. For the Volume Envelope, the decay phase linearly ramps toward the sustain level, causing a constant dB change for each time unit. If the sustain level were -100dB, the Volume Envelope Decay Time would be the time spent in decay phase. A value of 0 indicates a 1-second decay time for a zero-sustain level. A negative value indicates a time less than one second; a positive value a time longer than one second. For example, a decay time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			decayVolEnv					= 36 ,
			/// <summary>
			/// This is the decrease in level, expressed in centibels, to which the Volume Envelope value ramps during the decay phase. For the Volume Envelope, the sustain level is best expressed in centibels of attenuation from full scale. A value of 0 indicates the sustain level is full level; this implies a zero duration of decay phase regardless of decay time. A positive value indicates a decay to the corresponding level. Values less than zero are to be interpreted as zero; conventionally 1000 indicates full attenuation. For example, a sustain level which corresponds to an absolute value 12dB below of peak would be 120.
			/// </summary>
			sustainVolEnv				= 37 ,
			/// <summary>
			/// This is the time, in absolute timecents, for a 100% change in the Volume Envelope value during release phase. For the Volume Envelope, the release phase linearly ramps toward zero from the current level, causing a constant dB change for each time unit. If the current level were full scale, the Volume Envelope Release Time would be the time spent in release phase until 100dB attenuation were reached. A value of 0 indicates a 1-second decay time for a release from full level. A negative value indicates a time less than one second; a positive value a time longer than one second. For example, a release time of 10 msec would be 1200log2(.01) = -7973.
			/// </summary>
			releaseVolEnv				= 38 ,
			/// <summary>
			/// This is the degree, in timecents per KeyNumber units, to which the hold time of the Volume Envelope is decreased by increasing MIDI key number. The hold time at key number 60 is always unchanged. The unit scaling is such that a value of 100 provides a hold time which tracks the keyboard; that is, an upward octave causes the hold time to halve. For example, if the Volume Envelope Hold Time were -7973 = 10 msec and the Key Number to Vol Env Hold were 50 when key number 36 was played, the hold time would be 20 msec.
			/// </summary>
			keynumToVolEnvHold			= 39 ,
			/// <summary>
			/// This is the degree, in timecents per KeyNumber units, to which the hold time of the Volume Envelope is decreased by increasing MIDI key number. The hold time at key number 60 is always unchanged. The unit scaling is such that a value of 100 provides a hold time that tracks the keyboard; that is, an upward octave causes the hold time to halve. For example, if the Volume Envelope Hold Time were -7973 = 10 msec and the Key Number to Vol Env Hold were 50 when key number 36 was played, the hold time would be 20 msec.
			/// </summary>
			keynumToVolEnvDecay			= 40 ,
			/// <summary>
			/// This is the index into the INST sub-chunk providing the instrument to be used for the current preset zone. A value of zero indicates the first instrument in the list. The value should never exceed two less than the size of the instrument list. The instrument enumerator is the terminal generator for PGEN zones. As such, it should only appear in the PGEN sub-chunk, and it must appear as the last generator enumerator in all but the global preset zone.
			/// </summary>
			instrument					= 41 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			reserved1					= 42 ,
			/// <summary>
			/// This is the minimum and maximum MIDI key number values for which this preset zone or instrument zone is active.
			/// <para>The LS byte indicates the highest and the MS byte the lowest valid key.</para>
			/// <para>The keyRange enumerator is optional, but when it does appear, it must be the first generator in the zone generator list.</para>
			/// </summary>
			keyRange					= 43 ,
			/// <summary>
			/// This is the minimum and maximum MIDI velocity values for which this preset zone or instrument zone is active.
			/// <para>The LS byte indicates the highest and the MS byte the lowest valid velocity.</para>
			/// <para>The velRange enumerator is optional, but when it does appear, it must be preceded only by keyRange in the zone generator list.</para>
			/// </summary>
			velRange					= 44 ,
			/// <summary>
			/// The offset, in 32768 sample data point increments beyond the Startloop sample header parameter and the first sample data point to be repeated in this instrument’s loop.
			/// This parameter is added to the startloopAddrsOffset parameter.
			/// For example, if Startloop were 5, startloopAddrsOffset were 3 and startAddrsCoarseOffset were 2, the first sample data point in the loop would be sample data point 65544.
			/// </summary>
			startLoopAddrsCoarseOffs	= 45 ,
			/// <summary>
			/// This enumerator forces the MIDI key number to effectively be interpreted as the value given.
			/// This generator can only appear at the instrument level.
			/// Valid values are from 0 to 127.
			/// </summary>
			keynum						= 46 ,
			/// <summary>
			/// This enumerator forces the MIDI velocity to effectively be interpreted as the value given. This generator can only appear at the instrument level. Valid values are from 0 to 127.
			/// </summary>
			velocity					= 47 ,
			/// <summary>
			/// This is the attenuation, in centibels, by which a note is attenuated below full scale. A value of zero indicates no attenuation; the note will be played at full scale. For example, a value of 60 indicates the note will be played at 6 dB below full scale for the note.
			/// </summary>
			initialAttenuation			= 48 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			reserved2					= 49 ,
			/// <summary>
			/// The offset, in 32768 sample data point increments beyond the Endloop sample header parameter to the sample data point considered equivalent to the Startloop sample data point for the loop for this instrument. This parameter is added to the endloopAddrsOffset parameter. For example, if Endloop were 5, endloopAddrsOffset were 3 and endAddrsCoarseOffset were 2, sample data point 65544 would be considered equivalent to the Startloop sample data point, and hence sample data point 65543 would effectively precede Startloop during looping.
			/// </summary>
			endloopAddrsCoarseOffset 	= 50 ,
			/// <summary>
			/// This is a pitch offset, in semitones, which should be applied to the note. A positive value indicates the sound is reproduced at a higher pitch; a negative value indicates a lower pitch. For example, a Coarse Tune value of -4 would cause the sound to be reproduced four semitones flat.
			/// </summary>
			coarseTune					= 51 ,
			/// <summary>
			/// This is a pitch offset, in cents, which should be applied to the note. It is additive with coarseTune. A positive value indicates the sound is reproduced at a higher pitch; a negative value indicates a lower pitch. For example, a Fine Tuning value of -5 would cause the sound to be reproduced five cents flat.
			/// </summary>
			fineTune					= 52 ,
			/// <summary>
			/// This is the index into the SHDR sub-chunk providing the sample to be used for the current instrument zone. A value of zero indicates the first sample in the list. The value should never exceed two less than the size of the sample list. The sampleID enumerator is the terminal generator for IGEN zones. As such, it should only appear in the IGEN sub-chunk, and it must appear as the last generator enumerator in all but the global zone.
			/// </summary>
			sampleID					= 53 ,
			/// <summary>
			/// This enumerator indicates a value which gives a variety of Boolean flags describing the sample for the current instrument zone. The sampleModes should only appear in the IGEN sub-chunk, and should not appear in the global zone. The two LS bits of the value indicate the type of loop in the sample: 0 indicates a sound reproduced with no loop, 1 indicates a sound which loops continuously, 2 is unused but should be interpreted as indicating no loop, and 3 indicates a sound which loops for the duration of key depression then proceeds to play the remainder of the sample.
			/// </summary>
			sampleModes					= 54 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			reserved3					= 55 ,
			/// <summary>
			/// This parameter represents the degree to which MIDI key number influences pitch. A value of zero indicates that MIDI key number has no effect on pitch; a value of 100 represents the usual tempered semitone scale.
			/// </summary>
			scaleTuning					= 56 ,
			/// <summary>
			/// This parameter provides the capability for a key depression in a given instrument to terminate the playback of other instruments. This is particularly useful for percussive instruments such as a hi-hat cymbal. An exclusive class value of zero indicates no exclusive class; no special action is taken. Any other value indicates that when this note is initiated, any other sounding note with the same exclusive class value should be rapidly terminated. The exclusive class generator can only appear at the instrument level. The scope of the exclusive class is the entire preset. In other words, any other instrument zone within the same preset holding a corresponding exclusive class will be terminated.
			/// </summary>
			exclusiveClass				= 57 ,
			/// <summary>
			/// This parameter represents the MIDI key number at which the sample is to be played back at its original sample rate. If not present, or if present with a value of -1, then the sample header parameter Original Key is used in its place. If it is present in the range 0-127, then the indicated key number will cause the sample to be played back at its sample header Sample Rate. For example, if the sample were a recording of a piano middle C (Original Key = 60) at a sample rate of 22.050 kHz, and Root Key were set to 69, then playing MIDI key number 69 (A above middle C) would cause a piano note of pitch middle C to be heard.
			/// </summary>
			overridingRootKey			= 58 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered.
			/// </summary>
			unused5						= 59 ,
			/// <summary>
			/// Unused, reserved. Should be ignored if encountered. Unique name provides value to end of defined list.
			/// </summary>
			endOper						= 60
		}
		#endregion
		
		#region DefaultModulators
		public enum DefaultModulators : uint
		{
			MIDI_NoteOn_Velocity_to_Innitial_Attenuation= 1282 ,
			MIDI_NoteOn_Velocity_to_Filter_Cutoff= 258 ,
			MIDI_Channel_Pressure_to_Vibrato_LFO_Pitch_Depth= 13  ,
			MIDI_Continuous_Controller_1_to_Vibrato_LFO_Pitch = 129  ,
			MIDI_Continuous_Controller_7_to_Initial_Attenuation= 1415 ,
			MIDI_Continuous_Controller_10_to_Pan_Position= 650 ,
			MIDI_Continuous_Controller_11_to_Initial_Attenuation= 1419 ,
			MIDI_Continuous_Controller_91_to_Reverb_Effects_Send= 219  ,
			MIDI_Continuous_Controller_93_to_Chorus_Effects_Send= 221  ,
			MIDI_Pitch_Wheel_to_Initial_Pitch_Controlled_by_MIDI_Pitch_Wheel_Sensitivity = 526
		}
		#endregion
		
		#region HYDRA
		[StructLayout(LayoutKind.Sequential) ]
		public struct listHYDRA
		{
			public int				phdrLen;
			public List<PHDR>		phdr ;
			public int				pbagLen;
			public List<PBAG>		pbag;
			public int				pmodLen;
			public List<PMOD>		pmod;
			public int				pgenLen;
			public List<PGEN>		pgen;
			public int				instLen;
			public List<INST>		inst;
			public int				ibagLen;
			public List<IBAG>		ibag;
			public int				imodLen;
			public List<IMOD>		imod;
			public int				igenLen;
			public List<IGEN>		igen;
			public int				shdrLen;
			public List<SHDR>		shdr;
			
			public short BagIndex(int index) { return inst[index].bagIndex; }
			public string GetStringRange(int index) { return string.Format( "Key Range: {0} to {1}", BagIndex(index), BagIndex(index+1)); }
			
			public listHYDRA(FileStream fst, BinaryReader brd, long seekStart)
			{
				fst.Seek(seekStart+12, SeekOrigin.Begin);
				char[] temp;
				phdr = new List<PHDR>(); pbag = new List<PBAG>();
				pmod = new List<PMOD>(); pgen = new List<PGEN>();
				inst = new List<INST>(); ibag = new List<IBAG>();
				imod = new List<IMOD>(); igen = new List<IGEN>();
				shdr = new List<SHDR>();
				//PHDR
				temp = brd.ReadChars(4);
				phdrLen = brd.ReadInt32(); int i;
				for (i = 0; i < (phdrLen / 38); i++) phdr.Add(new PHDR(brd));
				//PBAG
				temp = brd.ReadChars(4);
				pbagLen = brd.ReadInt32();
				for (i = 0; i < pbagLen / 4; i++) pbag.Add(new PBAG(brd));
				//PMOD
				temp = brd.ReadChars(4);
				pmodLen = brd.ReadInt32();
				for (i = 0; i < pmodLen / 10; i++) this.pmod.Add(new PMOD(brd));
				//PGEN
				temp = brd.ReadChars(4);
				pgenLen = brd.ReadInt32();
				for (i = 0; i < pgenLen / 4; i++) this.pgen.Add(new PGEN(brd));
				//INST
				temp = brd.ReadChars(4);
				instLen = brd.ReadInt32();
				for (i = 0; i < instLen / 22; i++) this.inst.Add(new INST(brd));
				//IBAG
				temp = brd.ReadChars(4);
				ibagLen = brd.ReadInt32();
				for (i = 0; i < ibagLen / 4; i++) this.ibag.Add(new IBAG(brd));
				//IMOD
				temp = brd.ReadChars(4);
				imodLen = brd.ReadInt32();
				for (i = 0; i < imodLen / 10 ; i++) this.imod.Add(new IMOD(brd));
				//IGEN
				temp = brd.ReadChars(4);
				igenLen = brd.ReadInt32();
				for (i = 0; i < igenLen / 4; i++) this.igen.Add(new IGEN(brd));
				//SHDR
				temp = brd.ReadChars(4);
				shdrLen = brd.ReadInt32();
				for (i = 0; i < shdrLen / 46; i++) this.shdr.Add(new SHDR(brd));
			}
		}
		
		#region PHDR 38
		/// <summary>
		/// Presets
		/// </summary>
		/// <remarks>
		/// The PHDR sub-chunk is a required sub-chunk listing all presets within the SoundFont compatible file.
		/// It is always a multiple of thirty-eight bytes in length, and contains a minimum of two records,
		/// one record for each preset and one for a terminal record according to the structure.
		/// </remarks>
		[StructLayout(LayoutKind.Sequential)]
		public struct PHDR // (n) * 38 bytes long
		{
			public char[]	presetName;
			public short	preset;
			public short 	bank;
			public short	presetBagIndex;
			public int		library;
			public int		genera;
			public int		morphology;
			public PHDR(BinaryReader br)
			{
				presetName = br.ReadChars(20);
				preset = br.ReadInt16();
				bank = br.ReadInt16();
				presetBagIndex = br.ReadInt16();
				library = br.ReadInt32();
				genera = br.ReadInt32();
				morphology = br.ReadInt32();
			}
		}
		#endregion
		
		#region PBAG 4
		/// <summary>
		/// Preset Zone Index.
		/// </summary>
		/// <remarks>
		/// The PBAG sub-chunk is a required sub-chunk listing all preset zones within the SoundFont compatible file. It is always a multiple of four bytes in length, and contains one record for each preset zone plus one record for a terminal zone according to the structure
		/// </remarks>
		[StructLayout(LayoutKind.Sequential) ]
		public struct PBAG
		{
			public short	gen;
			public short	mod;
			public PBAG(BinaryReader br)
			{
				gen  = br.ReadInt16();
				mod  = br.ReadInt16();
			}
		}
		#endregion

		#region PMOD 10
		/// <summary>
		/// Modulator Zones
		/// </summary>
		[StructLayout(LayoutKind.Sequential) ]
		public struct PMOD
		{
			public short	src;
			public short	dst;
			public short	amt;
			public short	amtsrc;
			public short	trans;
			public PMOD(BinaryReader br)
			{
				src = br.ReadInt16();
				dst = br.ReadInt16();
				amt = br.ReadInt16();
				amtsrc = br.ReadInt16();
				trans = br.ReadInt16();
			}
		}
		#endregion

		#region PGEN 4
		/// <summary>
		/// Generator Zones
		/// </summary>
		[StructLayout(LayoutKind.Sequential) ]
		public struct PGEN
		{
			public SFGenConst GeneratorType { get { return (SFGenConst) this.SFGen; } }
			
			public short	SFGen;
			public byte		TypeLo;
			public byte		TypeHi;
			
			/// <summary>
			/// This could be Endien specific.
			/// We're going to have to test endien-ness.
			/// See: 'agrad' usage.
			/// </summary>
			public int		Shift8 { get { return (TypeHi << 8) + TypeLo; } }
			
			public PGEN(BinaryReader br)
			{
				SFGen = br.ReadInt16();
				TypeLo = br.ReadByte();
				TypeHi = br.ReadByte();
			}
		}
		#endregion

		#region INST 22
		/// <summary>
		/// Instrument Zones
		/// </summary>
		[StructLayout(LayoutKind.Sequential) ]
		public struct INST
		{
			public char[]	iName;
			public short	bagIndex;
			
			public INST(BinaryReader br)
			{
				iName = br.ReadChars(20);
				bagIndex = br.ReadInt16();
			}
		}
		#endregion

		#region IBAG 4
		[StructLayout(LayoutKind.Sequential) ]
		public struct IBAG
		{
			public short	gen;
			public short	mod;
			public IBAG(BinaryReader br)
			{
				gen = br.ReadInt16();
				mod = br.ReadInt16();
			}
		}
		#endregion

		#region IMOD 10
		[StructLayout(LayoutKind.Sequential) ]
		public struct IMOD
		{
			public short	src;
			public short	dst;
			public short	amt;
			public short	amtsrc;
			public short	trans;
			public IMOD(BinaryReader br)
			{
				src = br.ReadInt16();
				dst = br.ReadInt16();
				amt = br.ReadInt16();
				amtsrc = br.ReadInt16();
				trans = br.ReadInt16();
			}
		}
		#endregion

		#region IGEN 4
		[StructLayout(LayoutKind.Sequential) ]
		public struct IGEN
		{
			public byte		gen;
			public SFGenConst Generator { get { return (SFGenConst) gen; } }
			
			public byte		genAmt;
			public byte		genHi;
			public byte		genLo;
			
			public int Int16Bits { get { return ((genHi << 8) + genLo); } }
			
			public IGEN(BinaryReader br)
			{
				gen = br.ReadByte();
				genAmt = br.ReadByte();
				genHi = br.ReadByte();
				genLo = br.ReadByte();
			}
		}
		#endregion

		#region SHDR 46
		/// <summary>
		/// Sample Header
		/// </summary>
		[StructLayout(LayoutKind.Sequential) ]
		public struct SHDR
		{
			public char[]		iName; //20 bytes long
			public int			LenA;
			public int			LenB;
			public int			LoopA;
			public int			LoopB;
			public int			SampleRate; // 40
			public byte			Pitch;
			public byte			PitchC;
			public short			Link;
			public short			Type;
			public SHDR(BinaryReader br)
			{
				iName		= br.ReadChars(20);
				LenA		= br.ReadInt32();
				LenB		= br.ReadInt32();
				LoopA		= br.ReadInt32();
				LoopB		= br.ReadInt32();
				SampleRate	= br.ReadInt32(); // 40
				Pitch		= br.ReadByte();
				PitchC		= br.ReadByte();
				Link		= br.ReadInt16();
				Type		= br.ReadInt16();
			}
		}
		#endregion

		// sfVersionTag : IFF 'iver'
		
		#endregion
	}

	#region SF2 Helpers
	public enum pram_type
	{
		sampledatapoints_abs = 0xA01,
		sampledatapoints_rel = 0x01,
		semitones_ams = 0xA02,
		semitones_rel = 0x2,
		cents_abs = 0xA3,
		cents_rel = 0x3,
		centibels_abs = 0xA4,
		centibels_rel = 0x4,
		timecents_abs = 0xA5,
		timecents_rel = 0x5,
		precent_abs = 0xA6,
		precent_rel = 0x6
	}
	#endregion
	

}
