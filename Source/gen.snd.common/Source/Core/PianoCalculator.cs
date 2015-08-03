/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq.Expressions;

using gen.snd.Midi.Common;
using gen.snd.Midi.Structures;
using CliEvent = System.EventArgs;
using CliHandler = System.EventHandler;

namespace gen.snd.Midi
{
	/// <summary>
	/// Pixel translation to midi timing.
	/// <para>‘Tick’: Mouse-Axis on X while.</para>
	/// <para>‘Value’: Midi Key Number (from mouse Y axis).</para>
	/// </summary>
	/// <remarks>
	/// Division and Tick are the two properties you can set.
	/// </remarks>
	public class PianoCalculator
	{
		/// ( p / 4 / 4 / 4 ); add 1 for non-inclusive zero; [n]/64;
		public double Measure { get { return ( x / 4 / 4 / 4 ).FloorMinimum(0); } }
		/// ( p / 4 / 4 ); add 1 for non-inclusive zero; [n]/16;
		public double Bar     { get { return ( x / 4 / 4 ).FloorMinimum(0); } }
		/// Modulus % limited to 4; add 1 for non-inclusive zero
		public double BarMod     { get { return Bar % 4; } }
		/// ( p / 4 ); add 1 for non-inclusive zero; [n]/4;
		public double Note    { get { return ( x / 4 ).FloorMinimum(0); } }
		public double NoteMod { get { return Note % 4; } }
		/// ( p / 4 ); add 1 for non-inclusive zero
		/// (this is the same as notes; alias)
		public double Quarter { get { return ( x / Division ).FloorMinimum(0); } }
		/// Modulus % limited to 4; add 1 for non-inclusive zero
		public double QuarterMod { get { return Quarter % Division; } }
		/// (required) the tick or x-pixel position.
		/// add 1 for non-inclusive zero; [n]/1;
		public double Tick    { get { return   x; } set { x = value.FloorMinimum( 0 ); } } double x;
		public double Value    { get { return   y; } set { y = value.FloorMinimum( 0 ); } } double y;
		public double Division { get; set; }
		
		public string MBQT { get { return string.Format(Strings.PianoMBQTFormat, Measure+1, BarMod+1, NoteMod+1, Tick); } }
		public string MBQ { get { return string.Format(Strings.PianoMBQFormat, Measure+1, BarMod+1, NoteMod+1, Tick); } }
		public override string ToString() { return MBQT; }
		/// <summary>
		/// Tick (point.X) and Value (point.Y) are calculated.
		/// </summary>
		/// <param name="pointiput">discarded or ignored</param>
		/// <param name="division"></param>
		/// <returns></returns>
		static public PianoCalculator Create(FloatPoint pointiput, double division)
		{
			return new PianoCalculator().SetValue(pointiput, division);
		}
		public PianoCalculator SetValue(FloatPoint input, double division)
		{
			Division = division;
			Tick = input.X / division;
			Value = input.Y;
			return this;
		}
	}
}
