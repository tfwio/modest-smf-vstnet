/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 12/26/2005
 * Time: 5:44 AM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace gen.snd.Forms
{

	public interface IUi
	{
		/// <summary></summary>
		Rectangle ClientRectangle { get ; }
	}
	public interface IUiOffset
	{
		/// <summary></summary>
		event EventHandler XOffsetChanged;
		/// <summary></summary>
		event EventHandler YOffsetChanged;
	}
	internal interface IKnobSetting : IUi
	{
		/// <summary></summary>
		int Maximum { get; set; }
		/// <summary></summary>
		int Minimum { get; set; }
		/// <summary></summary>
		int Length { get; set; }
		/// <summary></summary>
		KnobType Mode { get; set; }
		/// <summary></summary>
		int Offset { get; set; }
		/// <summary></summary>
		int KnobFrameIndex { get; set; }
		/// <summary></summary>
		Rectangle RowsCols { get; set; }
		/// <summary></summary>
		Image KnobImage { get; set; }
	}
}
