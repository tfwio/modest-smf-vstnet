/*
 * Created by SharpDevelop.
 * User: tfooo
 * Date: 11/12/2005
 * Time: 4:19 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using Jacobi.Vst.Core;
using IPluginCommander = Jacobi.Vst.Core.Host.IVstPluginCommandStub;

namespace gen.snd.Vst.Module
{
	public class VstCCParam
	{
		IPluginCommander Stub;
		
		static public VstCCParam Load(VstCCPgm p, int id)
		{
			VstCCParam pc = new VstCCParam(){ ID = id, ProgramID = p.ID };
			pc.Stub = p.Stub;
			return pc;
		}
		
		public override string ToString()
		{
			return (string.Format("{0}: {1} {2}, {3}",Name,Display,Label,Value));
		}

		
		#region Properties
		public int ProgramID { get; set; }
		public int ID { get; set; }
		public string Name { get { return Stub.GetParameterName(ID); } }
		public string Label { get { return Stub.GetParameterLabel(ID); } }
		public string Display { get { return Stub.GetParameterDisplay(ID); } }
		public float Value { get { return Stub.GetParameter(ID); } set { Stub.SetParameter(ID, value); Stub.EditorIdle(); } }
		VstParameterProperties Properties { get { return Stub.GetParameterProperties(ID); } }
		#endregion
	}
}
