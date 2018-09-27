using System;
namespace modest100.Forms
{
	class MidiTimeState
	{
		public MidiTimeState()
		{
			pxPpn = 4;
			pxTpq = 1;
			pxTpn = pxPpn * 4;
			pxTpb = pxTpn * 4;
		}

    /// <summary>
    /// </summary>
    /// <param name="pxPerTick">Number of quarters</param>
    /// <param name="pxPerNote"></param>
    /// <param name="pxPerBar"></param>
    public void InitializeSnappingBoundaries(int pxPerTick, int pxPerNote, int pxPerBar)
		{
			pxTpb = (pxTpn = (pxPpn = pxPerTick) * pxPerNote) * pxPerBar;
		}

		// Pixels Per Quarter
		public int PxPpn {
			get {
				return pxPpn;
			}
			set {
				pxPpn = value;
			}
		}

		int pxPpn = 4;

		/// <summary>
		/// This is f-ing nonsense.
		/// pxTpq is set here as 1.
		/// Of course this can be used as leverage in the future,
		/// however here its f-ing nonsense!
		/// </summary>
		public int PxTpq {
			get {
				return pxTpq * pxPpn;
			}
		}

		int pxTpq = 1;

		public int PxTpn {
			get {
				return pxTpn;
			}
		}

		public int PxTpb {
			get {
				return pxTpb;
			}
		}

		int pxTpn;

		int pxTpb;
	}
}

