/*
 * Date: 11/12/2005
 * Time: 4:19 PM
 */

// Horse barn for rent

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

using gen.snd.Midi.Common;
using gen.snd.Midi.Structures;
using CliEvent = System.EventArgs;

namespace gen.snd.Midi
{
	public interface IMidiParser_Parser
	{
		#region TIME ( … ) GetMBT, GetMbtString
		/// <summary>
		/// Gets a string value.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns>UTF8 Decoded</returns>
		string GetMetaString(int offset);
		/// 
		/// <summary>Measure:Bar:Ticks</summary>
		/// <param name="value">Pulses</param>
		/// <returns>Measure:Bar:Quarters:Ticks +/- Quarters</returns>
		string GetMbtString(ulong value);
		#endregion
		#region META
		string GetMetaSTR(int offset);
		/// <summary>
		/// We place the imaginary caret one byte before the next read.
		/// </summary>
		/// <param name="offset"></param>
		/// <returns></returns>
		int GetMetaNextPos(int offset);
		/// <summary>
		/// There is no plus (its not used).
		/// Reason being, I've not ever encountered a RSE Meta event.
		/// </summary>
		/// <param name="offset"></param>
		/// <param name="plus"></param>
		/// <returns></returns>
		int GetMetaLen(int offset, int plus);
		byte[] GetMetaValue(int offset);
		byte[] GetMetaData(int offset);
		byte[] GetMetaBString(int offset);
//		byte[] GetMetaStringValue(int offset);
//		byte[] GetMetaValue(int offset);
		#endregion
		
		#region CH (string)
		/// <summary>
		/// Parse runningstatus channel bit (for messages that support this); Non-RSE
		/// </summary>
		/// <param name="v">message value bit</param>
		string chV(int v);
		/// <summary>
		/// process running status bit with event valuestring?; RSE specific
		/// </summary>
		/// <param name="v">message value bit</param>
		/// <returns>
		/// string.Format("{0} {1}", string.Format("{0:X2}", RunningStatus32), GetEventValueString(v))
		/// </returns>
		string chRseV(int v);
		/// <summary>
		/// Valid on one track after or during parse operation;
		/// Parse runningstatus channel bit.
		/// </summary>
		/// <remarks>
		/// For messages that support this.
		/// </remarks>
		string ch { get; }
		#endregion
		#region NEXT.POS
		int GetNextRsePosition(int offset);
		int GetNextPosition(int offset);
		#endregion
		#region VALUE.LEN
		int GetRseEventLength(int offset);
		int GetEventLength(int offset);
		#endregion
		
		#region VALUE.EVENT
		byte[] GetRseEventValue(int offset);
		byte[] GetEventValue(int offset);
		#endregion
		#region VALUE.EVENT-STRING
		string GetRseEventString(int offset);
		string GetEventString(int offset);
		#endregion
		#region VALUE-STRING
		string GetRseEventValueString(int offset);
		string GetEventValueString(int offset);
		#endregion
	}
}
