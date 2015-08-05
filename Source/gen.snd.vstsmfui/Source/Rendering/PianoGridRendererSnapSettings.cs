// oio * 10/31/2012 * 12:27 PM
using System;
namespace modest100.Rendering
{
	class PianoGridRendererSnapSettings
	{
		/// <param name="ppn">parts per note</param>
		/// <param name="tpq">ticks per quarter</param>
		public PianoGridRendererSnapSettings(int ppn, int tpq)
		{
			pxPpn = ppn;
			pxTpq = tpq;
			pxTpn = pxPpn * 4;
			pxTpb = pxTpn * 4;
		}
		
		#region Snapping
		
		/// <param name="ppt">parts per tick?</param>
		/// <param name="ppn">parts per note</param>
		/// <param name="ppb">parts per bar</param>
		public void InitializeSnappingBoundaries(int ppt, int ppn, int ppb) {
      pxTpb = (pxTpn = (pxPpn = ppt) * ppn) * ppb;
    }
		
		#endregion
		
		public int PxPpn {
      get { return pxPpn; }
      set { pxPpn = value; }
    } int pxPpn;

		public int PxTpq {
      get { return pxTpq * pxPpn; }
    } int pxTpq;

    public int PxTpn {
      get { return pxTpn; }
    } int pxTpn;

		public int PxTpb {
      get { return pxTpb; }
    } int pxTpb;

  }
	
}
